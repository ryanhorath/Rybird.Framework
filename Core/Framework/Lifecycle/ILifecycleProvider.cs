using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rybird.Framework
{
    public interface ILifecycleProvider
    {
        Task OpenWindowAsync<TViewModel>(string parameter = null) where TViewModel : FrameworkPageViewModel;
        bool CanOpenWindow { get; }
        Task LoadState();
        Task SaveState();
    }
}
