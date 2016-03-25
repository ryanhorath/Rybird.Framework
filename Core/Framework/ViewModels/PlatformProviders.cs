using System;

namespace Rybird.Framework
{
    public class PlatformProviders : IPlatformProviders
    {
        public PlatformProviders(INavigationProvider navigation, ISynchronizationProvider synchronization, IResourcesProvider resources, IDeviceInfoProvider deviceInfo)
        {
            navigation.ThrowIfNull("navigation");
            _navigation = navigation;
            synchronization.ThrowIfNull("synchronization");
            _synchronization = synchronization;
            resources.ThrowIfNull("resources");
            _resources = resources;
            deviceInfo.ThrowIfNull("deviceInfo");
            _deviceInfo = deviceInfo;
        }

        private readonly Func<INavigationProvider> _navigationCallback;
        private INavigationProvider _navigation;
        public INavigationProvider Navigation
        { 
            get
            {
                if (_navigation == null)
                {
                    _navigation = _navigationCallback.Invoke();
                }
                _navigation.ThrowIfNull("navigationCallback cannot return a null value.");
                return _navigation;
            } 
        }

        private readonly ISynchronizationProvider _synchronization;
        public ISynchronizationProvider Synchronization { get { return _synchronization; } }

        private readonly IResourcesProvider _resources;
        public IResourcesProvider Resources { get { return _resources; } }

        private readonly IDeviceInfoProvider _deviceInfo;
        public IDeviceInfoProvider DeviceInfo { get { return _deviceInfo; } }
    }
}
