using System;
using System.Collections.Generic;
using Windows.System;
using Windows.UI.Core;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Rybird.Framework;
using System.Threading.Tasks;

namespace Rybird.Framework
{
    public class FrameworkPage : Page, IFrameworkPage
    {
        private bool _isNew = true;

        public FrameworkPage()
        {
            if (!Windows.ApplicationModel.DesignMode.DesignModeEnabled)
            {

                // When this page is part of the visual tree make two changes:
                // 1) Map application view state to visual state for the page
                // 2) Handle keyboard and mouse navigation requests
                this.Loaded += (sender, e) =>
                {
                    Window.Current.SizeChanged += this.WindowSizeChanged;

                    InvalidateVisualState();

                    // Keyboard and mouse navigation only apply when occupying the entire window
                    if (this.ActualHeight == Window.Current.Bounds.Height &&
                        this.ActualWidth == Window.Current.Bounds.Width)
                    {
                        // Listen to the window directly so focus isn't required
                        Window.Current.CoreWindow.Dispatcher.AcceleratorKeyActivated +=
                            CoreDispatcher_AcceleratorKeyActivated;
                        Window.Current.CoreWindow.PointerPressed +=
                            this.CoreWindow_PointerPressed;
                    }
                };

                // Undo the same changes when the page is no longer visible
                this.Unloaded += (sender, e) =>
                {
                    Window.Current.SizeChanged -= this.WindowSizeChanged;

                    Window.Current.CoreWindow.Dispatcher.AcceleratorKeyActivated -=
                        CoreDispatcher_AcceleratorKeyActivated;
                    Window.Current.CoreWindow.PointerPressed -=
                        this.CoreWindow_PointerPressed;
                };
            }
        }

        public void InitializePage(FrameworkPageViewModel viewModel)
        {
            DataContext = viewModel;
        }

        #region Navigation support

        /// <summary>
        /// Invoked on every keystroke, including system keys such as Alt key combinations, when
        /// this page is active and occupies the entire window. Used to detect keyboard navigation
        /// between pages even when the page itself doesn't have focus.
        /// </summary>
        private void CoreDispatcher_AcceleratorKeyActivated(CoreDispatcher sender, AcceleratorKeyEventArgs args)
        {
            var virtualKey = args.VirtualKey;

            // Only investigate further when Left, Right, or the dedicated Previous or Next keys
            // are pressed
            if ((args.EventType == CoreAcceleratorKeyEventType.SystemKeyDown ||
                args.EventType == CoreAcceleratorKeyEventType.KeyDown) &&
                (virtualKey == VirtualKey.Left || virtualKey == VirtualKey.Right ||
                (int)virtualKey == 166 || (int)virtualKey == 167))
            {
                var coreWindow = Window.Current.CoreWindow;
                var downState = CoreVirtualKeyStates.Down;
                bool menuKey = (coreWindow.GetKeyState(VirtualKey.Menu) & downState) == downState;
                bool controlKey = (coreWindow.GetKeyState(VirtualKey.Control) & downState) == downState;
                bool shiftKey = (coreWindow.GetKeyState(VirtualKey.Shift) & downState) == downState;
                bool noModifiers = !menuKey && !controlKey && !shiftKey;
                bool onlyAlt = menuKey && !controlKey && !shiftKey;

                if (((int)virtualKey == 166 && noModifiers) ||
                    (virtualKey == VirtualKey.Left && onlyAlt))
                {
                    // When the previous key or Alt+Left are pressed navigate back
                    args.Handled = true;
                    var vm = DataContext as FrameworkPageViewModel;
                    if (vm != null)
                    {
                        vm.NavigateBack();
                    }
                }
            }
        }

        /// <summary>
        /// Invoked on every mouse click, touch screen tap, or equivalent interaction when this
        /// page is active and occupies the entire window. Used to detect browser-style next and
        /// previous mouse button clicks to navigate between pages.
        /// </summary>
        /// <param name="sender">Instance that triggered the event.</param>
        /// <param name="args">Event data describing the conditions that led to the event.</param>
        private void CoreWindow_PointerPressed(CoreWindow sender, PointerEventArgs args)
        {
            var properties = args.CurrentPoint.Properties;

            // Ignore button chords with the left, right, and middle buttons
            if (!(properties.IsLeftButtonPressed || properties.IsRightButtonPressed ||
                properties.IsMiddleButtonPressed))
            {
                // If back or foward are pressed (but not both) navigate appropriately
                bool backPressed = properties.IsXButton1Pressed;
                bool forwardPressed = properties.IsXButton2Pressed;
                if (backPressed ^ forwardPressed)
                {
                    args.Handled = true;
                    if (backPressed)
                    {
                        var vm = DataContext as FrameworkPageViewModel;
                        if (vm != null)
                        {
                            vm.NavigateBack();
                        }
                    }
                }
            }
        }

        #endregion

        #region Visual state switching

        private FrameworkOrientation _currentOrientation = DetermineVisualState();
        private void WindowSizeChanged(object sender, WindowSizeChangedEventArgs e)
        {
            InvalidateVisualState();
        }

        private static FrameworkOrientation DetermineVisualState()
        {
            if (Window.Current.Bounds.Width >= Window.Current.Bounds.Height)
            {
                return FrameworkOrientation.Landscape;
            }
            else
            {
                return FrameworkOrientation.Portrait;
            }
        }

        public void InvalidateVisualState()
        {
            var orientation = DetermineVisualState();
            if (orientation != _currentOrientation)
            {
                VisualStateManager.GoToState(this, orientation.ToString(), false);
                _currentOrientation = orientation;
            }
        }

        #endregion

        #region Process lifetime management

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (ViewModel != null)
            {
                bool hasState = e.NavigationMode == NavigationMode.Back && _isNew;
                ViewModel.NavigatedTo((string)e.Parameter, hasState);
                if (hasState)
                {
                    IReadOnlyDictionary<string, object> state = null;
                    ViewModel.LoadState(state);
                }
            }
            _isNew = false;
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            if (ViewModel != null)
            {
                ViewModel.NavigatedFrom();
                if (e.NavigationMode != NavigationMode.Back)
                {
                    IDictionary<string, object> state = null;
                    ViewModel.SaveState(state);
                }
            }
        }

        private FrameworkPageViewModel ViewModel
        {
            get { return DataContext as FrameworkPageViewModel; }
        }

        #endregion
    }
}
