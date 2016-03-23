using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Xamarin.Forms;
using Nito.AsyncEx;

namespace Rybird.Framework
{
    public class XamarinFormsNavigationProvider : IXamarinFormsNavigationProvider
    {
        private readonly IMvvmTypeResolver _typeResolver;
        private readonly IPlatformProviders _platformProviders;
        private IXamarinFormsFrameworkPage _currentPage;
        private NavigationPage _rootNavigationPage;
        private INavigation _xamarinNavigation;
        private Stack<IXamarinFormsFrameworkPage> _pageStack = new Stack<IXamarinFormsFrameworkPage>();

        public XamarinFormsNavigationProvider(
            NavigationPage rootNavigationPage,
            IMvvmTypeResolver typeResolver,
            IPlatformProviders platformProviders)
        {
            _rootNavigationPage = rootNavigationPage;
            _xamarinNavigation = rootNavigationPage.Navigation;
            _typeResolver = typeResolver;
            _platformProviders = platformProviders;
        }

        public IXamarinFormsFrameworkPage CurrentFrameworkPage
        {
            get { return _currentPage; }
        }

        public NavigationPage CurrentPage
        {
            get { return (NavigationPage)_currentPage; }
        }

        private bool _isStartPageInitialized = false;
        public void RegisterAndInitializeStartPage(IXamarinFormsFrameworkPage page)
        {
            if (_isStartPageInitialized)
            {
                throw new Exception("Start page has already been initialized.");
            }
            InitializePage(page, null);
            _isStartPageInitialized = true;
        }

        private void InitializePage(IXamarinFormsFrameworkPage page, string parameter)
        {
            _currentPage = page;
            if (page != null)
            {
                var viewModelType = _typeResolver.ResolveViewModelTypeFromViewType(page.GetType());
                var viewModel = _typeResolver.InstantiatePageViewModel(viewModelType, _platformProviders);
                _pageStack.Push(page);
                page.Initialize(viewModel, parameter);
            }
        }

        public async Task<bool> NavigateAsync<TViewModel>(string parameter = null) where TViewModel : FrameworkPageViewModel
        {
            return await NavigateAsync(typeof(TViewModel), parameter);
        }

        public async Task<bool> NavigateAsync(Type viewModelType, string parameter = null)
        {
            Type pageType = _typeResolver.ResolveViewTypeFromViewModelType(viewModelType);
            if (pageType == null)
            {
                throw new Exception(string.Format("Unable to resolve ViewModel {0} to a view type.", viewModelType.FullName));
            }
            var page = (NavigationPage)_typeResolver.InstantiatePageView(pageType);
            var frameworkPage = page as IXamarinFormsFrameworkPage;
            if (frameworkPage != null && frameworkPage.ViewModel == null)
            {
                InitializePage(frameworkPage, parameter);
            }
            await _rootNavigationPage.PushAsync(page);
            return true;
        }

        public bool CanGoBack
        { 
            get { return _pageStack.Count > 1; }
        }

        public async Task<bool> GoBackAsync()
        {
            _pageStack.Pop();
            if (_pageStack.Any())
            {
                _currentPage = _pageStack.Peek();
            }
            await _xamarinNavigation.PopAsync();
            return true;
        }

        public bool CanOpenWindow
        {
            get { return false; }
        }

        public Task OpenWindowAsync<TViewModel>(string parameter = null) where TViewModel : FrameworkPageViewModel
        {
            throw new NotSupportedException();
        }

        public bool CanGoUp
        {
            get { return false; }
        }

        public Task LoadState()
        {
            return TaskConstants.Completed;
        }

        public Task SaveState()
        {
            return TaskConstants.Completed;
        }
    }
}
