using System;
using UIKit;

namespace Rybird.Framework
{
    public class iOSFrameworkTypeResolver : FrameworkTypeResolverBase
    {
        private readonly Guid _mainWindowGuid = Guid.NewGuid();
        private readonly UIApplicationDelegate _application;
        private readonly IFrameworkTypeResolver _typeResolver;

        public iOSFrameworkTypeResolver(UIApplicationDelegate application, IFrameworkTypeResolver typeResolver)
        {
            Guard.AgainstNull(application, "application");
            _application = application;
            Guard.AgainstNull(typeResolver, "typeResolver");
            _typeResolver = typeResolver;
        }

        protected override IPerWindowPlatformProviders GeneratePerWindowProviders(object window)
        {
            var navigationController = (UINavigationController)window;
            var resourcesProvider = new iOSResourcesProvider();
            var synchronizationProvider = new iOSSynchronizationProvider(_application);
            var navigationProvider = new iOSNavigationProvider(navigationController, _typeResolver, resourcesProvider);
            var providers = new PerWindowPlatformProviders(navigationProvider, synchronizationProvider, resourcesProvider);
            return providers;
        }

        protected override Guid GetUniqueIdForWindow(object window)
        {
            return _mainWindowGuid;
        }
    }
}
