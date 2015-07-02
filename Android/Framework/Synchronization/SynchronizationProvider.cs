using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System.Threading.Tasks;

namespace Rybird.Framework
{
    public class SynchronizationProvider : ISynchronizationProvider
    {
        public Task RunAsync(Action action)
        {
            var tcs = new TaskCompletionSource<object>();
            new Handler(Looper.MainLooper).Post(() =>
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