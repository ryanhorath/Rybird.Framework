using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Navigation;

namespace Rybird.Framework
{
    public abstract class WpfApp : Application, IFrameworkApp
    {
        private INavigationProvider _navigationProvider;
        private ILoggingProvider _loggingProvider;
        private IMvvmTypeResolver _typeResolver;
        private ISynchronizationProvider _synchronization;
        private IResourcesProvider _resources;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            OnInitialize();
        }

        protected abstract NavigationService RootNavigationService
        {
            get;
        }

        protected virtual void OnInitialize()
        {
            _loggingProvider = new DefaultLoggingProvider();
            _synchronization = new SynchronizationProvider();
            _resources = new WpfResourcesProvider();
            _typeResolver = new DefaultMvvmTypeResolver();
            // TODO: Implement WPF device info
            _navigationProvider = new WpfNavigationProvider(RootNavigationService, _typeResolver, new PlatformProviders(GetNavigationProvider, _synchronization, _resources, null));
        }

        private INavigationProvider GetNavigationProvider()
        {
            return _navigationProvider;
        }

        public bool IsSuspending
        {
            get { return false; }
        }
    }
}
