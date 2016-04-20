using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using System.Reflection;
using Nito.AsyncEx;
using System.Windows.Controls;
using System.Windows.Navigation;
using System.Windows;

namespace Rybird.Framework
{
    public class WpfNavigationProvider : INavigationProvider
    {
        private readonly NavigationService _navigationService;
        private readonly IFrameworkTypeResolver _typeResolver;
        private readonly IPlatformProviders _platformProviders;
        private IWpfFrameworkPage _currentPage;

        public WpfNavigationProvider(NavigationService navigationService, IMvvmTypeResolver typeResolver, 
            ISynchronizationProvider synchronizationProvider, IResourcesProvider resourcesProvider)
        {
            _navigationService.ThrowIfNull("navigationService");
            _navigationService = navigationService;
            _typeResolver = typeResolver;
            _platformProviders = new PlatformProviders(this, synchronizationProvider, resourcesProvider);
            _navigationService.Navigated += MainFrame_Navigated;
        }

        private void MainFrame_Navigated(object sender, NavigationEventArgs e)
        {
            var page = e.Content as IWpfFrameworkPage;
            if (page != null)
            {
                _currentPage = page;
                var viewModel = page.ViewModel;
                if (viewModel == null)
                {
                    var viewModelType = _typeResolver.ResolveViewModelTypeFromViewType(page.GetType());
                    viewModel = _typeResolver.InstantiatePageViewModel(viewModelType, _platformProviders);
                    page.InitializePage(viewModel, (string)e.ExtraData);
                }
                page.NavigatedTo();
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
                var error = string.Format(CultureInfo.CurrentCulture, _platformProviders.Resources.GetString("FrameNavigationServiceUnableResolveMessage"), viewModelType.FullName);
                throw new ArgumentException(error, "viewModel");
            }
            if (_currentPage != null)
            {
                _currentPage.NavigatedFrom();
            } 
            var result = _navigationService.Navigate(pageType, parameter);
            return Task.FromResult<bool>(result);
        }

        public Task<bool> GoBackAsync()
        {
            if (_currentPage != null)
            {
                _currentPage.NavigatedFrom();
            }
            _navigationService.GoBack();
            return TaskConstants.BooleanTrue;
        }

        public bool CanGoBack
        {
            get { return _navigationService.CanGoBack; }
        }
    }
}
