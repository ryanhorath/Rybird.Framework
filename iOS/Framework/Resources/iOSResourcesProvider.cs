using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;

namespace Rybird.Framework
{
    public class iOSResourcesProvider : IResourcesProvider
    {
        public string GetString(string resourceId)
        {
            return NSBundle.MainBundle.LocalizedString(resourceId, "").Replace("&amp;", "&");
        }
    }
}