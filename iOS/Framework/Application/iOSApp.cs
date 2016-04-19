using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;

namespace Rybird.Framework
{
    public abstract class iOSApp : UIApplicationDelegate
    {
        private INavigationProvider _navigationProvider;
        private ISynchronizationProvider _synchronizationProvider;
        private IResourcesProvider _resourcesProvider;
        private ILoggingProvider _loggingProvider;
        private IMvvmTypeResolver _typeResolver;

        protected abstract Type MainPageViewModelType
        {
            get;
        }

        private UINavigationController _navigationController;
        protected virtual UINavigationController NavigationController
        {
            get
            {
                return _navigationController ?? (_navigationController = new UINavigationController());
            }
        }

        public override void FinishedLaunching(UIApplication application)
        {
            OnInitialize();
            OnSetupWindow();
            base.FinishedLaunching(application);
        }

        protected virtual void OnSetupWindow()
        {
            UIWindow window = new UIWindow(UIScreen.MainScreen.Bounds);
            window.RootViewController = NavigationController;
            window.MakeKeyAndVisible();
            var result = _navigationProvider.NavigateAsync(MainPageViewModelType);
        }

        protected virtual void OnInitialize()
        {
            _typeResolver = new DefaultMvvmTypeResolver();
            _loggingProvider = new DefaultLoggingProvider();
            _resourcesProvider = new iOSResourcesProvider();
            _synchronizationProvider = new iOSSynchronizationProvider(this);
            _navigationProvider = new iOSNavigationProvider(NavigationController, _typeResolver, new PlatformProviders(GetNavigationProvider, _synchronizationProvider, _resourcesProvider));
        }

        private INavigationProvider GetNavigationProvider
        {
            get { return _navigationProvider; }
        }
    }
}