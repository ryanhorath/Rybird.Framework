using System;
using System.Globalization;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using System.Threading.Tasks;
using Nito.AsyncEx;

namespace Rybird.Framework
{
    public class UwpNavigationProvider : INavigationProvider
    {
        private readonly Window _window;
        private readonly Frame _frame;
        private readonly ISessionStateService _sessionStateService;
        private readonly IFrameworkTypeResolver _typeResolver;
        private readonly IPerAppPlatformProviders _perAppProviders;

        public UwpNavigationProvider(Window window, IFrameworkTypeResolver typeResolver, ILifecycleProvider lifecycle)
        {
            _window = window;
            _frame = (Frame)window.Content;
            _typeResolver = typeResolver;
            _perAppProviders = new PerAppPlatformProviders(lifecycle);
            _sessionStateService = new SessionStateService(_frame);
            _frame.Navigated += MainFrame_Navigated;
        }

        private void MainFrame_Navigated(object sender, NavigationEventArgs e)
        {
            var page = e.Content as IWindowsRuntimeFrameworkPage;
            if (page != null)
            {
                var viewModelType = _typeResolver.ResolveViewModelTypeFromViewType(page.GetType());
                var perWindowProviders = _typeResolver.GetProvidersForWindow(_window);
                var platformProviders = new PlatformProviders(_perAppProviders, perWindowProviders);
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
    }
}
