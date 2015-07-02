using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rybird.Framework
{
    public interface IWpfFrameworkPage : IFrameworkPage
    {
        void InitializePage(FrameworkPageViewModel viewModel, string parameter);
        void NavigatedTo();
        void NavigatedFrom();
    }
}
