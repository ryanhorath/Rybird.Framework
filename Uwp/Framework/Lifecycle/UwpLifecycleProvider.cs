using System;
using System.Collections.Generic;
using System.Globalization;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using System.Threading.Tasks;
using System.Reflection;
using Nito.AsyncEx;
using Windows.ApplicationModel.Core;
using Windows.UI.ViewManagement;
using Windows.UI.Core;

namespace Rybird.Framework
{
    public class UwpLifecycleProvider : ILifecycleProvider
    {
        private readonly IMvvmTypeResolver _typeResolver;
        private readonly ISynchronizationProvider _synchronizationProvider;
        private readonly IResourcesProvider _resourcesProvider;
        private readonly IDeviceInfoProvider _deviceInfoProvider; 
        private readonly ISessionStateService _sessionStateService;

        public UwpLifecycleProvider(IMvvmTypeResolver typeResolver, ISynchronizationProvider synchronizationProvider,
            IResourcesProvider resourcesProvider, IDeviceInfoProvider deviceInfoProvider)
        {
            _typeResolver = typeResolver;
            _synchronizationProvider = synchronizationProvider;
            _resourcesProvider = resourcesProvider;
            _deviceInfoProvider = deviceInfoProvider;
            //_sessionStateService = new SessionStateService(_frame);
        }

        public async Task OpenWindowAsync<TViewModel>(string parameter = null) where TViewModel : FrameworkPageViewModel
        {
            var newCoreView = CoreApplication.CreateNewView();

            ApplicationView newAppView = null;
            var currentViewId = ApplicationView.GetApplicationViewIdForWindow(Window.Current.CoreWindow);

            await newCoreView.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, async () =>
                {
                    newAppView = ApplicationView.GetForCurrentView();
                    Window.Current.Content = new Frame();
                    var navigation = new UwpNavigationProvider(Window.Current, _typeResolver, _synchronizationProvider, _resourcesProvider, _deviceInfoProvider);
                    await navigation.NavigateAsync<TViewModel>(parameter);
                    Window.Current.Activate();
                });

            await ApplicationViewSwitcher.TryShowAsStandaloneAsync(newAppView.Id, ViewSizePreference.UseHalf, currentViewId, ViewSizePreference.UseHalf);
        }

        public bool CanOpenWindow
        {
            get { return true; }
        }

        public async Task LoadState()
        {
            await _sessionStateService.RestoreSessionStateAsync();
            _sessionStateService.RestoreFrameState();
        }

        public async Task SaveState()
        {
            await _sessionStateService.SaveAsync();
        }
    }
}
