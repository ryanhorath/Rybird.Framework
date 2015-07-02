using System;
using System.Threading;
using System.Threading.Tasks;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Rybird.Framework
{
    public class SynchronizationProvider : ISynchronizationProvider
    {
        public Task RunAsync(Action action)
        {
            return ((Frame)Window.Current.Content).Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => action()).AsTask();
        }
    }
}