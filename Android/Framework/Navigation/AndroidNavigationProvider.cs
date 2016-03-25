using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Android.Content;
using Android.Support.V4.App;
using Android.App;
using Nito.AsyncEx;

namespace Rybird.Framework
{
    public class AndroidNavigationProvider : IAndroidNavigationProvider
    {
        private readonly IMvvmTypeResolver _typeResolver;
        private readonly IPlatformProviders _platformProviders;
        private IFrameworkActivity _currentActivity;

        public AndroidNavigationProvider(
            IMvvmTypeResolver typeResolver,
            ISynchronizationProvider synchronizationProvider,
            IResourcesProvider resourcesProvider,
            IDeviceInfoProvider deviceInfoProvider)
        {
            _typeResolver = typeResolver;
            _platformProviders = new PlatformProviders(this, synchronizationProvider, resourcesProvider, deviceInfoProvider);
        }

        public virtual bool InitializeActivity(IFrameworkActivity activity)
        {
            activity.ThrowIfNull("activity");
            _currentActivity = activity;
            if (IsViewModelCached(activity))
            {
                activity.DataContext = RetrieveViewModel<FrameworkPageViewModel>(activity);
                return false;
            }
            else
            {
                var viewModelType = _typeResolver.ResolveViewModelTypeFromViewType(activity.GetType());
                var viewModel = _typeResolver.InstantiatePageViewModel(viewModelType, _platformProviders);
                CacheViewModel(activity, viewModel);
                activity.DataContext = viewModel;
                return true;
            }
        }

        public void ActivityResumed(IFrameworkActivity activity)
        {
            _currentActivity = activity;
        }

        public async Task<bool> NavigateAsync<TViewModel>(string parameter = null) where TViewModel : FrameworkPageViewModel
        {
            return await NavigateAsync(typeof (TViewModel), parameter);
        }

        public Task<bool> NavigateAsync(Type viewModelType, string parameter = null)
        {
            Type pageType = _typeResolver.ResolveViewTypeFromViewModelType(viewModelType);
            if (pageType == null)
            {
                throw new Exception(string.Format("Unable to resolve ViewModel {0} to a view type.", viewModelType.FullName));
            }
            var intent = new Intent(_currentActivity.Activity, pageType);
            if (parameter != null)
            {
                intent.PutExtra(FrameworkPageViewModel.NavigationParameterKey, parameter);
            }
            _currentActivity.StartActivity(intent);
            return TaskConstants.BooleanTrue;
        }

        public bool CanGoBack
        {
            get { return true; }
        }

        public Task<bool> GoBackAsync()
        {
            var activity = _currentActivity as Activity;
            activity.ThrowIfNull("_currentActivity");
            activity.Finish();
            return TaskConstants.BooleanTrue;
        }

        public Task OpenWindowAsync<TViewModel>(string parameter = null) where TViewModel : FrameworkPageViewModel
        {
            throw new NotSupportedException();
        }

        public bool CanOpenWindow
        { 
            get { return false; }
        }

        // Not needed on Android
        public Task LoadState()
        {
            return TaskConstants.Completed;
        }

        // Not needed on Android
        public Task SaveState()
        {
            return TaskConstants.Completed;
        }

        private IDictionary<string, FrameworkPageViewModel> _viewModelCache = new Dictionary<string, FrameworkPageViewModel>();
        private void CacheViewModel(IFrameworkActivity activity, FrameworkPageViewModel viewModel)
        {
            _viewModelCache[activity.ActivityInstanceId] = viewModel;
        }

        private T RetrieveViewModel<T>(IFrameworkActivity activity) where T : FrameworkPageViewModel
        {
            return (T)_viewModelCache[activity.ActivityInstanceId];
        }

        private bool IsViewModelCached(IFrameworkActivity activity)
        {
            return _viewModelCache.ContainsKey(activity.ActivityInstanceId);
        }
    }
}
