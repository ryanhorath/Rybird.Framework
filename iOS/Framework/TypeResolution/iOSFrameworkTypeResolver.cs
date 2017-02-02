using System;
using UIKit;

namespace Rybird.Framework
{
    public class iOSFrameworkTypeResolver : FrameworkTypeResolverBase
    {
        private readonly Guid _mainWindowGuid = Guid.NewGuid();
        private readonly UIApplicationDelegate _application;

        public iOSFrameworkTypeResolver(UIApplicationDelegate application)
        {
            Guard.AgainstNull(application, "application");
            _application = application;
        }

        protected override IPlatformProviders GeneratePerWindowProviders(object window)
        {
            var navigationController = (UINavigationController)window;
            var resourcesProvider = new iOSResourcesProvider();
            var synchronizationProvider = new iOSSynchronizationProvider(_application);
            var navigationProvider = new iOSNavigationProvider(navigationController, this, synchronizationProvider, resourcesProvider);
            var providers = new PlatformProviders(navigationProvider, synchronizationProvider, resourcesProvider);
            return providers;
        }

        protected override Guid GetUniqueIdForWindow(object window)
        {
            return _mainWindowGuid;
        }
    }
}
