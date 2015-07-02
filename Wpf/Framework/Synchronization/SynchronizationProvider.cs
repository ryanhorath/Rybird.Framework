using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using System.Windows.Threading;

namespace Rybird.Framework
{
    public class SynchronizationProvider : ISynchronizationProvider
    {
        public Task RunAsync(Action action)
        {
            return Application.Current.Dispatcher.InvokeAsync(() => action(), DispatcherPriority.Normal).Task;
        }
    }
}