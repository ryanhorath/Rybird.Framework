using System;
using System.Threading.Tasks;
using Nito.AsyncEx;

namespace Rybird.Framework
{
    public class AndroidLifecycleProvider : ILifecycleProvider
    {
        public Task OpenWindowAsync<TViewModel>(string parameter = null) where TViewModel : FrameworkPageViewModel
        {
            throw new NotSupportedException();
        }

        public bool CanOpenWindow
        { 
            get { return false; }
        }

        // Not needed on Android
        public Task LoadState()
        {
            return TaskConstants.Completed;
        }

        // Not needed on Android
        public Task SaveState()
        {
            return TaskConstants.Completed;
        }
    }
}
