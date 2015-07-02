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
    internal class TextViewErrorBinding : ViewBinding
    {
        private TextView _target;
        private INotifyDataErrorInfo _sourceObject;
        private string _sourceProperty;

        public TextViewErrorBinding(TextView target, INotifyDataErrorInfo sourceObject, string sourceProperty)
            : base(target)
        {
            _target = target;
            _sourceObject = sourceObject;
            _sourceProperty = sourceProperty;
            _sourceObject.ErrorsChanged += sourceObject_ErrorsChanged;
        }

        private void sourceObject_ErrorsChanged(object sender, DataErrorsChangedEventArgs e)
        {
            var errors = _sourceObject.GetErrors(_sourceProperty) as IEnumerable<string>;
            if (errors != null && errors.Any())
            {
                _target.Error = errors.First();
            }
            else if (!string.IsNullOrEmpty(_target.Error))
            {
                _target.Error = null;
            }
        }

        public string Text
        {
            get { return _target.Text; }
            set { _target.Text = value; }
        }

        protected override void Cleanup()
        {
            _sourceObject.ErrorsChanged -= sourceObject_ErrorsChanged;
        }
    }
}