using System;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace Rybird.Framework
{
    public class WpfSynchronizationProvider : ISynchronizationProvider
    {
        private readonly IFrameworkWindow _window;

        public WpfSynchronizationProvider(IFrameworkWindow window)
        {
            _window = window;
        }

        public Task RunAsync(Action action)
        {
            return _window.Dispatcher.InvokeAsync(() => action(), DispatcherPriority.Normal).Task;
        }
    }
}