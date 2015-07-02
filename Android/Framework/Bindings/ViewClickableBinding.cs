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
    internal class ViewClickableBinding : ViewBinding
    {
        private View _target;
        private PropertyBinding<bool, bool> _propertyBinding;

        public ViewClickableBinding(View target, INotifyPropertyChanged sourceObject, string sourceProperty)
            : this(target, sourceObject, sourceProperty, null)
        {
        }

        public ViewClickableBinding(View target, INotifyPropertyChanged sourceObject, string sourceProperty, IBindingConverter<bool, bool> converter)
            : base(target)
        {
            _target = target;
            _propertyBinding = new PropertyBinding<bool, bool>(this, "Clickable", sourceObject, sourceProperty, converter);
        }

        public bool Clickable 
        {
            get { return _target.Clickable; }
            set { _target.Clickable = value; }
        }

        protected override void Cleanup()
        {
            _propertyBinding.Dispose();
        }
    }
}