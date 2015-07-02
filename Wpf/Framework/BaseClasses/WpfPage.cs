using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;

namespace Rybird.Framework
{
    public class WpfPage : Page, IWpfFrameworkPage
    {
        public WpfPage()
        {
            _currentOrientation = DetermineVisualState();
#if DEBUG
            if (!DesignerProperties.GetIsInDesignMode(this))
#endif
            {
                SizeChanged += WpfPage_SizeChanged;
                MouseDown += WpfPage_MouseDown;
                KeyUp += WpfPage_KeyUp;
                Loaded += WpfPage_Loaded;
            }
        }

        private void WpfPage_Loaded(object sender, RoutedEventArgs e)
        {
            InvalidateVisualState();
        }

        private void WpfPage_KeyUp(object sender, KeyEventArgs e)
        {
            if (IsBackArrowCombinationPressed(e) || e.Key == Key.BrowserBack)
            {
                // When the previous key or Alt+Left are pressed navigate back
                e.Handled = true;
                var vm = DataContext as FrameworkPageViewModel;
                if (vm != null)
                {
                    vm.NavigateBack();
                }
            }
        }

        private static bool IsBackArrowCombinationPressed(KeyEventArgs e)
        {
            return (e.Key == Key.Left && Keyboard.IsKeyDown(Key.LeftAlt) || Keyboard.IsKeyDown(Key.RightAlt));
        }

        private void WpfPage_MouseDown(object sender, MouseButtonEventArgs e)
        {
            // Ignore button chords with the left, right, and middle buttons
            if (!(e.LeftButton == MouseButtonState.Pressed || e.RightButton == MouseButtonState.Pressed ||
                e.MiddleButton == MouseButtonState.Pressed))
            {
                // If back is pressed navigate appropriately
                bool backPressed = e.XButton1 == MouseButtonState.Pressed;
                if (backPressed)
                {
                    e.Handled = true;
                    var vm = DataContext as FrameworkPageViewModel;
                    if (vm != null)
                    {
                        vm.NavigateBack();
                    }
                }
            }
        }

        public void InitializePage(FrameworkPageViewModel viewModel, string parameter)
        {
            viewModel.ThrowIfNull("viewModel");
            DataContext = viewModel;
            ViewModel.Create(parameter);
        }

        public void NavigatedTo()
        {
            // Never call LoadState() because WPF does not need it
            if (ViewModel != null)
            {
                ViewModel.Activate();
            }
        }

        public void NavigatedFrom()
        {
            if (ViewModel != null)
            {
                ViewModel.Deactivate();
                // Never call SaveState() because WPF does not need it
            }
        }

        public FrameworkPageViewModel ViewModel
        {
            get { return (FrameworkPageViewModel)DataContext; }
        }

        #region Visual state switching

        private FrameworkOrientation _currentOrientation;
        private void WpfPage_SizeChanged(object sender, System.Windows.SizeChangedEventArgs e)
        {
            InvalidateVisualState();
        }

        private FrameworkOrientation DetermineVisualState()
        {
            if (WindowWidth >= WindowHeight)
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
    }
}
