using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Concurrent;

namespace Rybird.Framework
{
    public static class AsyncHelper
    {
        public static void LoadData<T>(Action<T> callback, Func<Task<T>> loader)
        {
            var prevCtx = SynchronizationContext.Current;
            try
            {
                var syncCtx = new SingleThreadSynchronizationContext();
                SynchronizationContext.SetSynchronizationContext(syncCtx);

                var t = loader();
                t.ContinueWith(
                    delegate { syncCtx.Complete(); }, TaskScheduler.Default);

                syncCtx.RunOnCurrentThread();

                callback(t.GetAwaiter().GetResult());
            }
            finally { SynchronizationContext.SetSynchronizationContext(prevCtx); }
        }

        private sealed class SingleThreadSynchronizationContext : SynchronizationContext
        {
            private readonly BlockingCollection<KeyValuePair<SendOrPostCallback, object>> queue =
                new BlockingCollection<KeyValuePair<SendOrPostCallback, object>>();

            public override void Post(SendOrPostCallback d, object state)
            {
                if (d == null)
                {
                    throw new ArgumentNullException("d");
                }
                this.queue.Add(new KeyValuePair<SendOrPostCallback, object>(d, state));
            }

            public override void Send(SendOrPostCallback d, object state)
            {
                throw new NotSupportedException("Synchronously sending is not supported.");
            }

            public void RunOnCurrentThread()
            {
                foreach (var workItem in this.queue.GetConsumingEnumerable())
                {
                    workItem.Key(workItem.Value);
                }
            }

            public void Complete()
            {
                this.queue.CompleteAdding();
            }
        }
    }
}
