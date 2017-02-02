using System;

namespace Rybird.Framework
{
    public class WpfFrameworkTypeResolver : FrameworkTypeResolverBase
    {
        protected override IPlatformProviders GeneratePerWindowProviders(object window)
        {
            var wpfWindow = (IFrameworkWindow)window;
            var resourcesProvider = new WpfResourcesProvider();
            var synchronizationProvider = new WpfSynchronizationProvider(wpfWindow);
            var navigationProvider = new WpfNavigationProvider(wpfWindow.NavigationService, this, synchronizationProvider, resourcesProvider);
            var providers = new PlatformProviders(navigationProvider, synchronizationProvider, resourcesProvider);
            return providers;
        }

        protected override Guid GetUniqueIdForWindow(object window)
        {
            var wpfWindow = (IFrameworkWindow)window;
            return wpfWindow.UniqueId;
        }
    }
}
