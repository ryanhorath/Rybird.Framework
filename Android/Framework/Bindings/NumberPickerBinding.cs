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
    internal class NumberPickerBinding<TSource> : ViewBinding
    {
        private NumberPicker _target;
        private PropertyBinding<int, TSource> _propertyBinding;

        public NumberPickerBinding(NumberPicker target, INotifyPropertyChanged sourceObject, string sourceProperty, IBindingConverter<TSource, int> converter)
            : base(target)
        {
            _target = target;
            _propertyBinding = new PropertyBinding<int, TSource>(this, "Value", sourceObject, sourceProperty, converter);
            target.ValueChanged += target_ValueChanged;
        }

        private void target_ValueChanged(object sender, NumberPicker.ValueChangeEventArgs e)
        {
            RaisePropertyChangedEvent("Value");
        }

        public int Value
        {
            get { return _target.Value; }
            set { _target.Value = value; }
        }

        protected override void Cleanup()
        {
            _target.ValueChanged -= target_ValueChanged;
        }
    }
}