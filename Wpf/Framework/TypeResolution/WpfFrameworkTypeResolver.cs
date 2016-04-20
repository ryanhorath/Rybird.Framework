using System;

namespace Rybird.Framework
{
    public class WpfFrameworkTypeResolver : FrameworkTypeResolverBase
    {
        protected override IPerWindowPlatformProviders GeneratePerWindowProviders(object window)
        {
            var resourcesProvider = new WpfResourcesProvider();
            var synchronizationProvider = new WpfSynchronizationProvider();
            var navigationProvider = new AndroidNavigationProvider(this, synchronizationProvider, resourcesProvider);
            var providers = new PerWindowPlatformProviders(navigationProvider, synchronizationProvider, resourcesProvider);
            return providers;
        }

        protected override Guid GetUniqueIdForWindow(object window)
        {
            return _mainWindowGuid;
        }
    }
}
