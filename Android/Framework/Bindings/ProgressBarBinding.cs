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
    internal class ProgressBarBinding<TSource> : ViewBinding
    {
        private ProgressBar _target;
        private PropertyBinding<int, TSource> _propertyBinding;

        public ProgressBarBinding(ProgressBar target, INotifyPropertyChanged sourceObject, string sourceProperty, IBindingConverter<TSource, int> converter)
            : base(target)
        {
            _target = target;
            _propertyBinding = new PropertyBinding<int, TSource>(this, "Progress", sourceObject, sourceProperty, converter);
        }

        public int Progress
        {
            get { return _target.Progress; }
            set { _target.Progress = value; }
        }

        protected override void Cleanup()
        {
            _propertyBinding.Dispose();
        }
    }
}