using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rybird.Framework
{
    public class NavigationCommand<TViewModel> : DelegateCommand
            where TViewModel : FrameworkPageViewModel
    {
        public NavigationCommand(INavigationProvider navigation)
            : base(() => navigation.NavigateAsync<TViewModel>()) { }
    }
}
