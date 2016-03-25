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
        private readonly IMvvmTypeResolver _typeResolver;
        private IFrameworkApp _application;

        public UwpNavigationProvider(Window window, IMvvmTypeResolver typeResolver,
            ISynchronizationProvider synchronizationProvider, IResourcesProvider resourcesProvider, IDeviceInfoProvider deviceInfoProvider)
        {
            _window = window;
            _frame = (Frame)window.Content;
            _typeResolver = typeResolver;
            _platformProviders = new PlatformProviders(this, synchronizationProvider, resourcesProvider, deviceInfoProvider);
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

        public bool CanGoBack
        {
            get { return _frame.CanGoBack; }
        }
    }
}
