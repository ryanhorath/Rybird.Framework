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
    public interface IAndroidNavigationProvider : INavigationProvider
    {
        // Returns true if the activity's viewmodel was not cached and therefore was initialized. Returns false if the viewmodel was cached (already initialized).
        bool InitializeActivity(IFrameworkActivity activity);
        void ActivityResumed(IFrameworkActivity activity);
    }
}