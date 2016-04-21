using System;
using Android.App;
using Android.Runtime;
using Android.OS;
using System.Collections.Generic;
using Android.Content.Res;

namespace Rybird.Framework
{
    public abstract class AndroidApp : Application
    {
        private IAndroidNavigationProvider _navigationProvider;
        private ISynchronizationProvider _synchronizationProvider;
        private IResourcesProvider _resourcesProvider;
        private ILoggingProvider _loggingProvider;
        private IFrameworkTypeResolver _typeResolver;

        // This constructor is used by Xamarin and is required
        public AndroidApp(IntPtr handle, JniHandleOwnership transfer)
                : base(handle, transfer)
        {
        }

        public override void OnCreate()
        {
            base.OnCreate();
            OnInitialize();
            RegisterActivityLifecycleCallbacks(new AndroidLifecycleManager(this));
        }

        protected virtual void OnInitialize()
        {
            _typeResolver = new AndroidFrameworkTypeResolver(this);
            _loggingProvider = new DefaultLoggingProvider();
            _resourcesProvider = new AndroidResourcesProvider(this);
            _synchronizationProvider = new AndroidSynchronizationProvider();
            _navigationProvider = new AndroidNavigationProvider(_typeResolver, _synchronizationProvider, _resourcesProvider);
        }

        #region Activity Lifecycle Events
        internal void OnActivityCreated(Activity activity, Bundle savedInstanceState)
        {
            var frameworkActivity = activity as IFrameworkActivity;
            if (frameworkActivity != null)
            {
                frameworkActivity.InitializeActivity((IAndroidNavigationProvider)_navigationProvider, savedInstanceState);
            }
        }

        internal void OnActivityResumed(Activity activity)
        {
            if (activity is IFrameworkActivity)
            {
                _navigationProvider.ActivityResumed((IFrameworkActivity)activity);
            }
        }

        #endregion Activity Lifecycle Events
    }
}
