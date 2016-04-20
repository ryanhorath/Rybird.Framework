using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.Content;

namespace Rybird.Framework
{
    public class AndroidResourcesProvider : IResourcesProvider
    {
        private Context _applicationContext;

        public AndroidResourcesProvider(Context applicationContext)
        {
            _applicationContext = applicationContext;
        }

        public string GetString(string key)
        {
            var packageName = _applicationContext.PackageName;
            int resourceId = _applicationContext.Resources.GetIdentifier(key, "string", packageName);
            return _applicationContext.GetString(resourceId);
        }
    }
}