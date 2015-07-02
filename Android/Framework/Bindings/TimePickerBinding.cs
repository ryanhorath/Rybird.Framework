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
    internal class TimePickerBinding<TSource> : ViewBinding
    {
        private TimePicker _target;
        private PropertyBinding<TimeSpan, TSource> _propertyBinding;
        private bool _isUpdating = false;

        public TimePickerBinding(TimePicker target, INotifyPropertyChanged sourceObject, string sourceProperty, IBindingConverter<TSource, TimeSpan> converter)
            : base(target)
        {
            _target = target;
            _propertyBinding = new PropertyBinding<TimeSpan, TSource>(this, "Time", sourceObject, sourceProperty, converter);
            target.TimeChanged += target_TimeChanged;
        }

        private void target_TimeChanged(object sender, TimePicker.TimeChangedEventArgs e)
        {
            if (!_isUpdating)
            {
                RaisePropertyChangedEvent("Time");
            }
        }

        public TimeSpan Time
        {
            get { return new TimeSpan(_target.CurrentHour.IntValue(), _target.CurrentMinute.IntValue(), 0); }
            set
            {
                _isUpdating = true;
                _target.CurrentHour = new Java.Lang.Integer(value.Hours);
                _target.CurrentMinute = new Java.Lang.Integer(value.Minutes);
                _isUpdating = false;
            }
        }

        protected override void Cleanup()
        {
            _target.TimeChanged -= target_TimeChanged;
            _propertyBinding.Dispose();
        }
    }
}