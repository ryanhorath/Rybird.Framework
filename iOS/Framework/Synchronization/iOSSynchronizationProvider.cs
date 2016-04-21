using System;
using UIKit;
using System.Threading.Tasks;

namespace Rybird.Framework
{
    public class iOSSynchronizationProvider : ISynchronizationProvider
    {
        private readonly UIApplicationDelegate _application;

        public iOSSynchronizationProvider(UIApplicationDelegate application)
        {
            _application = application;
        }

        public Task RunAsync(Action action)
        {
            var tcs = new TaskCompletionSource<object>();
            _application.InvokeOnMainThread(() =>
            {
                try
                {
                    action();
                    tcs.SetResult(null);
                }
                catch (Exception ex)
                {
                    tcs.TrySetException(ex);
                }

            });
            return tcs.Task;
        }
    }
}