using System;
using System.Threading.Tasks;
using Nito.AsyncEx;

namespace Rybird.Framework
{
    public class iOSLifecycleProvider : ILifecycleProvider
    {
        public bool CanOpenWindow { get { return false; } }

        public Task OpenWindowAsync<TViewModel>(string parameter = null) where TViewModel : FrameworkPageViewModel
        {
            throw new NotSupportedException();
        }

        // Not needed on iOS
        public Task LoadState()
        {
            return TaskConstants.Completed;
        }

        // Not needed on iOS
        public Task SaveState()
        {
            return TaskConstants.Completed;
        }
    }
}