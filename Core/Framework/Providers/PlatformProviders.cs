using System;

namespace Rybird.Framework
{
    public class PlatformProviders : IPlatformProviders
    {
        public PlatformProviders(INavigationProvider navigation, ISynchronizationProvider synchronization, IResourcesProvider resources)
        {
            Guard.AgainstNull(navigation, "navigation");
            _navigation = navigation;
            Guard.AgainstNull(synchronization, "synchronization");
            _synchronization = synchronization;
            Guard.AgainstNull(resources, "resources");
            _resources = resources;
        }

        private readonly INavigationProvider _navigation;
        public INavigationProvider Navigation { get { return _navigation; } }

        private readonly ISynchronizationProvider _synchronization;
        public ISynchronizationProvider Synchronization { get { return _synchronization; } }

        private readonly IResourcesProvider _resources;
        public IResourcesProvider Resources { get { return _resources; } }
    }
}
