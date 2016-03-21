using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using DemoResource = Rybird.Framework.Demo.Resource;

namespace Rybird.Framework.Android.Demo.Views
{
    [Activity(Label = "Rybird.Framework.Android.Demo", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivityView : Activity
    {
        int count = 1;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(DemoResource.Layout.Main);

            // Get our button from the layout resource,
            // and attach an event to it
            Button button = FindViewById<Button>(DemoResource.Id.MyButton);

            button.Click += delegate { button.Text = string.Format("{0} clicks!", count++); };

            var edit = FindViewById<EditText>(DemoResource.Id.MyEditText);
            
        }
    }
}

