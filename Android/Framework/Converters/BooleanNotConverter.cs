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
    public class BooleanNotConverter : IBindingConverter<bool, bool>
    {
        public bool Convert(bool source)
        {
            return !source;
        }

        public bool ConvertBack(bool converted)
        {
            return !converted;
        }
    }
}