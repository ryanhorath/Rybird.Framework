using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using System.Reflection;
using Nito.AsyncEx;
using System.Windows.Controls;
using System.Windows.Navigation;
using System.Windows;
using System.Threading;
using System.Windows.Threading;

namespace Rybird.Framework
{
    public class WpfLifecycleProvider : ILifecycleProvider
    {
        private readonly IMvvmTypeResolver _typeResolver;
        private readonly ISynchronizationProvider _synchronizationProvider;
        private readonly IResourcesProvider _resourcesProvider;

        public WpfLifecycleProvider(IMvvmTypeResolver typeResolver, ISynchronizationProvider synchronizationProvider,
            IResourcesProvider resourcesProvider)
        {
            _typeResolver = typeResolver;
            _synchronizationProvider = synchronizationProvider;
            _resourcesProvider = resourcesProvider;
        }

        public Task OpenWindowAsync<TViewModel>(string parameter = null) where TViewModel : FrameworkPageViewModel
        {
            Thread thread = new Thread(() =>
            {
                var window = new Window();
                window.Show();

                window.Closed += (s, e) => window.Dispatcher.InvokeShutdown();

                Dispatcher.Run();
            });
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
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
