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

namespace Rybird.Framework.Android.Demo.ViewModels
{
    public class MainActivityViewModel : FrameworkPageViewModel
    {
        public MainActivityViewModel(IPlatformProviders providers) : base(providers)
        {

        }

        protected override void OnCreated(string parameter)
        {
            base.OnCreated(parameter);

        }
    }
}