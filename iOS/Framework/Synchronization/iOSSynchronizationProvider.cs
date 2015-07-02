using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;
using System.Threading.Tasks;
using System.Threading;

namespace Rybird.Framework
{
    public class iOSSynchronizationProvider : ISynchronizationProvider
    {
        private UIApplicationDelegate _application;

        public iOSSynchronizationProvider(iOSApp application)
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