using System;
using Windows.ApplicationModel.Resources;
using Windows.UI.Xaml;

namespace Rybird.Framework
{
    public class UwpFrameworkTypeResolver : FrameworkTypeResolverBase
    {
        private readonly Guid _mainWindowGuid = Guid.NewGuid();

        protected override IPlatformProviders GeneratePerWindowProviders(object window)
        {
            var win = (Window)window;
            var resourcesProvider = new UwpResourcesProvider(ResourceLoader.GetForCurrentView());
            var synchronizationProvider = new UwpSynchronizationProvider();
            var navigationProvider = new UwpNavigationProvider(win, this);
            var providers = new PlatformProviders(navigationProvider, synchronizationProvider, resourcesProvider);
            return providers;
        }

        protected override Guid GetUniqueIdForWindow(object window)
        {
            return _mainWindowGuid;
        }
    }
}
