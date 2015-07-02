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
    public static class JavaObjectExtensions
    {
        public static T ToNetObject<T>(this Java.Lang.Object value)
        {
            if (value != null)
            {
                var wrapper = value as JavaWrapper<T>;
                if (wrapper == null)
                {
                    throw new InvalidOperationException("Unable to convert to .NET object. Only Java.Lang.Object created with .ToJavaObject() can be converted.");
                }
                else
                {
                    using (wrapper)
                    {
                        return wrapper.Instance;
                    }
                }
            }
            return default(T);
        }
    }
}