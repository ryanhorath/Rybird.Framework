using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;
using System.Threading.Tasks;
using Nito.AsyncEx;

namespace Rybird.Framework
{
    public class iOSNavigationProvider : INavigationProvider
    {
        private readonly IFrameworkTypeResolver _typeResolver;
        private IiOSViewController _currentController;
        private readonly IPlatformProviders _platformProviders;
        private readonly UINavigationController _navigationController;

        public iOSNavigationProvider(
            UINavigationController navigationController,
            IFrameworkTypeResolver typeResolver,
            ISynchronizationProvider synchronization,
            IResourcesProvider resources)
        {
            navigationController.ThrowIfNull("navigationController");
            _navigationController = navigationController;
            _typeResolver = typeResolver;
            _platformProviders = new PlatformProviders(this, synchronization, resources);
        }

        private void InitializeController(IiOSViewController controller, string parameter)
        {
            _currentController = controller;
            if (controller != null)
            {
                var viewModelType = _typeResolver.ResolveViewModelTypeFromViewType(controller.GetType());
                var viewModel = _typeResolver.InstantiatePageViewModel(viewModelType, _platformProviders);
                controller.Initialize(viewModel, parameter);
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
                throw new Exception(string.Format("Unable to resolve ViewModel {0} to a view type.", viewModelType.FullName));
            }
            IiOSViewController controller = (IiOSViewController)_typeResolver.InstantiatePageView(pageType);
            InitializeController(controller, parameter);
            _navigationController.PushViewController((UIViewController)controller, true);
            return Task.FromResult<bool>(true);
        }

        public bool CanGoBack { get { return true; } }

        public Task<bool> GoBackAsync()
        {
            _navigationController.PopViewController(true);
            return Task.FromResult<bool>(true);
        }

        public bool CanOpenWindow { get { return false; } }

        public Task OpenWindowAsync<TViewModel>(string parameter = null) where TViewModel : FrameworkPageViewModel
        {
            throw new NotSupportedException();
        }

        // Not needed on iOS
        public Task LoadState()
        {
            return TaskConstants.Completed;
        }

        // Not needed on iOS
        public Task SaveState()
        {
            return TaskConstants.Completed;
        }
    }
}