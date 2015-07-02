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
    internal class AndroidLifecycleManager : Java.Lang.Object, Application.IActivityLifecycleCallbacks
    {
        private readonly AndroidApp _androidApp;

        public AndroidLifecycleManager(IntPtr handle, JniHandleOwnership transfer)
            : base(handle, transfer)
        {

        }

        public AndroidLifecycleManager(AndroidApp androidApp)
        {
            _androidApp = androidApp;
        }

        public void OnActivityCreated(Activity activity, Bundle savedInstanceState)
        {
            _androidApp.OnActivityCreated(activity, savedInstanceState);
        }

        public void OnActivityDestroyed(Activity activity)
        {
        }

        public void OnActivityPaused(Activity activity)
        {
        }

        public void OnActivityResumed(Activity activity)
        {
            _androidApp.OnActivityResumed(activity);
        }

        public void OnActivitySaveInstanceState(Activity activity, Bundle outState)
        {
        }

        public void OnActivityStarted(Activity activity)
        {
        }

        public void OnActivityStopped(Activity activity)
        {
        }
    }
}