using Nito.AsyncEx;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Rybird.Framework
{
    public class XamarinFormsSynchronizationProvider : ISynchronizationProvider
    {
        public Task RunAsync(Action action)
        {
            Device.BeginInvokeOnMainThread(action);
            return TaskConstants.Completed;
        }
    }
}

