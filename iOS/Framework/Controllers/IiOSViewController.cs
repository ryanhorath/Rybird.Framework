using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;

namespace Rybird.Framework
{
    public interface IiOSViewController : IFrameworkPage
    {
        void Initialize(FrameworkPageViewModel dataContext, string parameter);
    }
}