using System.Windows;
using System.Windows.Navigation;

namespace Rybird.Framework
{
    public abstract class WpfApp : Application, IFrameworkApp
    {
        private INavigationProvider _navigationProvider;
        private ILoggingProvider _loggingProvider;
        private IFrameworkTypeResolver _typeResolver;
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
            _typeResolver = new WpfFrameworkTypeResolver();
            _navigationProvider = new WpfNavigationProvider(RootNavigationService, _typeResolver, _synchronization, _resources);
        }

        public bool IsSuspending
        {
            get { return false; }
        }
    }
}
