using System;
using System.Collections.Generic;
using System.Globalization;
using Rybird.Framework;
using Windows.ApplicationModel.Resources;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using System.Threading.Tasks;
using System.Reflection;
using Nito.AsyncEx;
using Windows.ApplicationModel.Core;
using Windows.UI.ViewManagement;
using Windows.UI.Core;

namespace Rybird.Framework
{
    public class WindowsRuntimeNavigationProvider : BindableBase, INavigationProvider
    {
        private readonly Window _window;
        private readonly Frame _frame;
        private readonly ISessionStateService _sessionStateService;
        private readonly IMvvmTypeResolver _typeResolver;
        private IFrameworkApp _application;

        public WindowsRuntimeNavigationProvider(IFrameworkApp application, Window window, IMvvmTypeResolver typeResolver, IPlatformProviders platformProviders)
        {
            _application = application;
            _window = window;
            _frame = (Frame)window.Content;
            _typeResolver = typeResolver;
            _platformProviders = platformProviders;
            _sessionStateService = new SessionStateService(_frame);
            _frame.Navigated += MainFrame_Navigated;
        }

        private readonly IPlatformProviders _platformProviders;
        protected virtual IPlatformProviders PlatformProviders
        {
            get { return _platformProviders; }
        }

        private void MainFrame_Navigated(object sender, NavigationEventArgs e)
        {
            var page = e.Content as IWindowsRuntimeFrameworkPage;
            if (page != null)
            {
                var viewModelType = _typeResolver.ResolveViewModelTypeFromViewType(page.GetType());
                page.InitializePage(_typeResolver.InstantiatePageViewModel(viewModelType, PlatformProviders), (string)e.Parameter);
            }
        }

        public async Task<bool> NavigateAsync<TViewModel>(string parameter = null) where TViewModel : FrameworkPageViewModel
        {
            return await NavigateAsync(typeof(TViewModel), parameter);
        }

        public Task<bool> NavigateAsync(Type viewModelType, string parameter = null)
        {
            Type pageType = _typeResolver.ResolveViewTypeFromViewModelType(viewModelType);
            if (pageType == null)
            {
                var error = string.Format(CultureInfo.CurrentCulture, PlatformProviders.Resources.GetString("FrameNavigationServiceUnableResolveMessage"), viewModelType.FullName);
                throw new ArgumentException(error, "viewModel");
            }
            var result = _frame.Navigate(pageType, parameter);
            return Task.FromResult<bool>(result);
        }

        public Task<bool> GoBackAsync()
        {
            
            _frame.GoBack();
            return TaskConstants.BooleanTrue;
        }

        public async Task OpenWindowAsync<TViewModel>(string parameter = null) where TViewModel : FrameworkPageViewModel
        {
            var newCoreView = CoreApplication.CreateNewView();

            ApplicationView newAppView = null;
            var currentViewId = ApplicationView.GetApplicationViewIdForWindow(_window.CoreWindow);

            await newCoreView.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                {
                    newAppView = ApplicationView.GetForCurrentView();
                    Window.Current.Content = new Frame();
                    Window.Current.Activate();
                });

            await ApplicationViewSwitcher.TryShowAsStandaloneAsync(newAppView.Id, ViewSizePreference.UseHalf, currentViewId, ViewSizePreference.UseHalf);
        }

        public bool CanGoBack
        {
            get { return _frame.CanGoBack; }
        }

        public bool CanOpenWindow
        {
            get { return true; }
        }

        public async Task LoadState()
        {
            await _sessionStateService.RestoreSessionStateAsync();
            _sessionStateService.RestoreFrameState();
        }

        public async Task SaveState()
        {
            IsSuspending = true;
            await _sessionStateService.SaveAsync();
        }

        private bool _isSuspending = false;
        public bool IsSuspending
        {
            get { return _isSuspending; }
            set { SetProperty<bool>(ref _isSuspending, value); }
        } 
    }
}
