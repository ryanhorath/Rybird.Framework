using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Rybird.Framework
{
    public class ResourcesProvider : IResourcesProvider
    {
        private Context _applicationContext;

        public ResourcesProvider(Context applicationContext)
        {
            _applicationContext = applicationContext;
        }

        public string GetString(string key)
        {
            String packageName = _applicationContext.PackageName;
            int resourceId = _applicationContext.Resources.GetIdentifier(key, "string", packageName);
            return _applicationContext.GetString(resourceId);
        }
    }
}