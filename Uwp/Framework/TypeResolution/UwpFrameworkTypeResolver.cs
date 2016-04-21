using System;

namespace Rybird.Framework
{
    public class UwpFrameworkTypeResolver : FrameworkTypeResolverBase
    {
        private readonly Guid _mainWindowGuid = Guid.NewGuid();

        public UwpFrameworkTypeResolver()
        {
            Guard.AgainstNull(applicationContext, "applicationContext");
            _applicationContext = applicationContext;
        }

        protected override IPerWindowPlatformProviders GeneratePerWindowProviders(object window)
        {
            var resourcesProvider = new AndroidResourcesProvider(_applicationContext);
            var synchronizationProvider = new AndroidSynchronizationProvider();
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
