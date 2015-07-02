using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Rybird.Framework
{
    public interface IXamarinFormsFrameworkPage : IFrameworkPage
    {
        void Initialize(FrameworkPageViewModel viewModel, string parameter);
    }
}
