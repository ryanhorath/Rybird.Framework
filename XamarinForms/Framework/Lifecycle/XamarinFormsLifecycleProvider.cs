using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Xamarin.Forms;
using Nito.AsyncEx;

namespace Rybird.Framework
{
    public class XamarinFormsLifecycleProvider : ILifecycleProvider
    {

        public bool CanOpenWindow
        {
            get { return false; }
        }

        public Task OpenWindowAsync<TViewModel>(string parameter = null) where TViewModel : FrameworkPageViewModel
        {
            throw new NotSupportedException();
        }

        public Task LoadState()
        {
            return TaskConstants.Completed;
        }

        public Task SaveState()
        {
            return TaskConstants.Completed;
        }
    }
}
