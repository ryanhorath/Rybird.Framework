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
using System.ComponentModel;

namespace Rybird.Framework
{
    internal class ViewVisibilityBinding : ViewBinding
    {
        private View _target;
        private PropertyBinding<ViewStates, bool?> _propertyBinding;

        public ViewVisibilityBinding(View target, INotifyPropertyChanged sourceObject, string sourceProperty)
            : this(target, sourceObject, sourceProperty, null)
        {
        }

        public ViewVisibilityBinding(View target, INotifyPropertyChanged sourceObject, string sourceProperty, IBindingConverter<bool?, ViewStates> converter)
            : base(target)
        {
            _target = target;
            var valueConverter = converter != null ? converter : new VisibleIfTrueConverter();
            _propertyBinding = new PropertyBinding<ViewStates, bool?>(this, "Visibility", sourceObject, sourceProperty, valueConverter);
        }

        public ViewStates Visibility
        {
            get { return _target.Visibility; }
            set { _target.Visibility = value; }
        }

        protected override void Cleanup()
        {
            _propertyBinding.Dispose();
        }
    }
}