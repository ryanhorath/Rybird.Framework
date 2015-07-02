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
using System.Reflection;

namespace Rybird.Framework
{
    public class PropertyBinding<TTarget, TSource> : IDisposable
    {
        private bool _disposed = false;
        private object _targetObject;
        private INotifyPropertyChanged _sourceObject;
        private PropertyInfo _targetProperty;
        private PropertyInfo _sourceProperty;
        private bool _updateFromSource = false;
        private bool _updateFromTarget = false;
        private IBindingConverter<TSource, TTarget> _converter;

        public PropertyBinding(object targetObject, string targetProperty, INotifyPropertyChanged sourceObject, string sourceProperty)
           : this(targetObject, targetProperty, sourceObject, sourceProperty, null)
        {
        }

        public PropertyBinding(object targetObject, string targetProperty, INotifyPropertyChanged sourceObject, string sourceProperty, IBindingConverter<TSource, TTarget> converter)
        {
            _converter = converter;
            _targetProperty = targetObject.GetType().GetProperty(targetProperty);
            if (_targetProperty == null)
            {
                throw new Exception(string.Format("Property {0} does not exist on object {1}", targetProperty, targetObject.GetType().FullName));
            }
            _sourceProperty = sourceObject.GetType().GetProperty(sourceProperty);
            if (_sourceProperty == null)
            {
                throw new Exception(string.Format("Property {0} does not exist on object {1}", sourceProperty, sourceObject.GetType().FullName));
            }
            _targetObject = targetObject;
            _sourceObject = sourceObject;
            Initialize();
            var targetNotify = targetObject as INotifyPropertyChanged;
            if (targetNotify != null)
            {
                targetNotify.PropertyChanged += targetObject_PropertyChanged;
            }
            sourceObject.PropertyChanged += sourceObject_PropertyChanged;
        }

        private void sourceObject_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            UpdateTarget();
        }

        private void UpdateTarget()
        {
            if (!_updateFromTarget)
            {
                _updateFromSource = true;
                _targetProperty.SetValue(_targetObject, GetValueFromSource());
                _updateFromSource = false;
            }
        }

        private void targetObject_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            UpdateSource();
        }

        private void UpdateSource()
        {
            try
            {
                if (!_updateFromSource)
                {
                    _updateFromTarget = true;
                    _sourceProperty.SetValue(_sourceObject, GetValueFromTarget());
                    _updateFromTarget = false;
                }
            }
            catch(Exception)
            {
                ;
            }
        }

        private TTarget GetValueFromSource()
        {
            if (_converter != null)
            {
                return _converter.Convert((TSource)_sourceProperty.GetValue(_sourceObject));
            }
            else
            {
                return (TTarget)_sourceProperty.GetValue(_sourceObject);
            }
        }

        private TSource GetValueFromTarget()
        {
            if (_converter != null)
            {
                return _converter.ConvertBack((TTarget)_targetProperty.GetValue(_targetObject));
            }
            else
            {
                return (TSource)_targetProperty.GetValue(_targetObject);
            }
        }

        private void Initialize()
        {
            UpdateTarget();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    var targetNotify = _targetObject as INotifyPropertyChanged;
                    if (targetNotify != null)
                    {
                        targetNotify.PropertyChanged -= targetObject_PropertyChanged;
                    }
                    _sourceObject.PropertyChanged -= sourceObject_PropertyChanged;
                }
                _disposed = true;
            }
        }
    }
}