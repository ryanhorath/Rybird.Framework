using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using System.Reflection;
using Nito.AsyncEx;
using System.Windows.Controls;
using System.Windows.Navigation;
using System.Windows;

namespace Rybird.Framework
{
    public class WpfLifecycleProvider : ILifecycleProvider
    {
        private readonly IMvvmTypeResolver _typeResolver;
        private readonly ISynchronizationProvider _synchronizationProvider;
        private readonly IResourcesProvider _resourcesProvider;
        private readonly IDeviceInfoProvider _deviceInfoProvider;

        public WpfLifecycleProvider(IMvvmTypeResolver typeResolver, ISynchronizationProvider synchronizationProvider,
            IResourcesProvider resourcesProvider, IDeviceInfoProvider deviceInfoProvider)
        {
            _typeResolver = typeResolver;
            _synchronizationProvider = synchronizationProvider;
            _resourcesProvider = resourcesProvider;
            _deviceInfoProvider = deviceInfoProvider;
        }

        public Task OpenWindowAsync<TViewModel>(string parameter = null) where TViewModel : FrameworkPageViewModel
        {
            var window = new Window();
            return Task.CompletedTask;
        }

        public bool CanOpenWindow
        {
            get { return true; }
        }

        public Task LoadState()
        {
            return TaskConstants.Completed;
        }

        public Task SaveState()
        {
            return TaskConstants.Completed;
        }
    }
}
