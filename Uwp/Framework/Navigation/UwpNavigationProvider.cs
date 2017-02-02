using System;
using System.Globalization;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using System.Threading.Tasks;
using Nito.AsyncEx;
using Windows.UI.ViewManagement;
using Windows.UI.Core;
using Windows.ApplicationModel.Core;

namespace Rybird.Framework
{
    public class UwpNavigationProvider : INavigationProvider
    {
        private readonly Window _window;
        private readonly Frame _frame;
        private readonly ISessionStateService _sessionStateService;
        private readonly IFrameworkTypeResolver _typeResolver;

        public UwpNavigationProvider(Window window, IFrameworkTypeResolver typeResolver)
        {
            _window = window;
            _frame = (Frame)window.Content;
            _typeResolver = typeResolver;
            _sessionStateService = new SessionStateService(_frame);
            _frame.Navigated += MainFrame_Navigated;
        }

        private void MainFrame_Navigated(object sender, NavigationEventArgs e)
        {
            var page = e.Content as IWindowsRuntimeFrameworkPage;
            if (page != null)
            {
                var viewModelType = _typeResolver.ResolveViewModelTypeFromViewType(page.GetType());
                var platformProviders = _typeResolver.GetProvidersForWindow(_window);
                page.InitializePage(_typeResolver.InstantiatePageViewModel(viewModelType, platformProviders), (string)e.Parameter);
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
                var error = string.Format(CultureInfo.CurrentCulture, "Unable to resolve view model", viewModelType.FullName);
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

        public bool CanGoBack
        {
            get { return _frame.CanGoBack; }
        }

        public async Task OpenWindowAsync<TViewModel>(string parameter = null) where TViewModel : FrameworkPageViewModel
        {
            var newCoreView = CoreApplication.CreateNewView();

            ApplicationView newAppView = null;
            var currentViewId = ApplicationView.GetApplicationViewIdForWindow(Window.Current.CoreWindow);

            await newCoreView.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, async () =>
            {
                newAppView = ApplicationView.GetForCurrentView();
                Window.Current.Content = new Frame();
                var navigation = new UwpNavigationProvider(Window.Current, _typeResolver);
                await navigation.NavigateAsync<TViewModel>(parameter);
                Window.Current.Activate();
            });

            await ApplicationViewSwitcher.TryShowAsStandaloneAsync(newAppView.Id, ViewSizePreference.UseHalf, currentViewId, ViewSizePreference.UseHalf);
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
            await _sessionStateService.SaveAsync();
        }
    }
}
