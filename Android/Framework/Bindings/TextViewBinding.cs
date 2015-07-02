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
    internal class TextViewBinding<TSource> : ViewBinding
    {
        private TextView _target;
        private PropertyBinding<string, TSource> _propertyBinding;

        public TextViewBinding(TextView target, INotifyPropertyChanged sourceObject, string sourceProperty)
            : this(target, sourceObject, sourceProperty, null)
        {
        }

        public TextViewBinding(TextView target, INotifyPropertyChanged sourceObject, string sourceProperty, IBindingConverter<TSource, string> converter)
            : base(target)
        {
            _target = target;
            _propertyBinding = new PropertyBinding<string, TSource>(this, "Text", sourceObject, sourceProperty, converter);
            target.TextChanged += target_TextChanged;
        }

        private void target_TextChanged(object sender, global::Android.Text.TextChangedEventArgs e)
        {
            RaisePropertyChangedEvent("Text");
        }

        public string Text
        {
            get { return _target.Text; }
            set { _target.Text = value; }
        }

        protected override void Cleanup()
        {
            _target.TextChanged -= target_TextChanged;
            _propertyBinding.Dispose();
        }
    }
}