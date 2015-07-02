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
    public class JavaWrapper<T> : Java.Lang.Object
    {
        public T Instance { get; private set; }

        public JavaWrapper(T instance)
        {
            Instance = instance;
        }
    }
}