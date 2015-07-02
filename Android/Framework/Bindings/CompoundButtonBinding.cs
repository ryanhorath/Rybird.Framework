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
    internal class CompoundButtonBinding<TSource> : ViewBinding
    {
        private CompoundButton _target;
        private PropertyBinding<bool, TSource> _propertyBinding;

        public CompoundButtonBinding(CompoundButton target, INotifyPropertyChanged sourceObject, string sourceProperty, IBindingConverter<TSource, bool> converter)
            : base(target)
        {
            _target = target;
            _propertyBinding = new PropertyBinding<bool, TSource>(this, "IsChecked", sourceObject, sourceProperty, converter);
            target.CheckedChange += target_CheckedChange;
        }

        private void target_CheckedChange(object sender, CompoundButton.CheckedChangeEventArgs e)
        {
            RaisePropertyChangedEvent("IsChecked");
        } 

        public bool IsChecked
        {
            get { return _target.Checked; }
            set { _target.Checked = value; }
        }

        protected override void Cleanup()
        {
            _target.CheckedChange -= target_CheckedChange;
            _propertyBinding.Dispose();
        }
    }
}