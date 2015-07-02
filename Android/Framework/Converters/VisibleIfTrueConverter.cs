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
    public class VisibleIfTrueConverter : IBindingConverter<bool?, ViewStates>
    {
        private ViewStates _nullValue;
        private ViewStates _invisibleState;

        public VisibleIfTrueConverter(ViewStates invisibleState = ViewStates.Invisible, ViewStates nullValue = ViewStates.Invisible)
        {
            _invisibleState = invisibleState;
            _nullValue = nullValue;
        }

        public ViewStates Convert(bool? source)
        {
            return !source.HasValue
                ? _nullValue
                : source.Value
                    ? ViewStates.Visible
                    : _invisibleState;
        }

        public bool? ConvertBack(ViewStates converted)
        {
            return converted == ViewStates.Visible ? true : false;
        }
    }
}