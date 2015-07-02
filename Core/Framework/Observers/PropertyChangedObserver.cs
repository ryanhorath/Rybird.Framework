using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rybird.Framework
{
    public class PropertyChangedObserver : IDisposable
    {
        private IDisposable _propertyObserver;
        private Action<string> _propertyChanged;
        private IEnumerable<string> _propertyNames;

        public PropertyChangedObserver(INotifyPropertyChanged obj, Action<string> propertyChanged, string propertyName)
            : this(obj, propertyChanged, new List<string>() { propertyName })
        {
        }

        public PropertyChangedObserver(INotifyPropertyChanged obj, Action<string> propertyChanged, IEnumerable<string> propertyNames)
        {
            _propertyChanged = propertyChanged;
            _propertyNames = propertyNames;
            _propertyObserver = Observable.FromEventPattern<PropertyChangedEventHandler, PropertyChangedEventArgs>(
                ev => obj.PropertyChanged += ev, ev => obj.PropertyChanged -= ev)
                .Subscribe(e => OnPropertyChanged(e.EventArgs.PropertyName));
        }

        private void OnPropertyChanged(string propertyName)
        {
            if (_propertyNames == null || !_propertyNames.Any())
            {
                _propertyChanged(propertyName);
            }
            else if (_propertyNames.Contains(propertyName))
            {
                _propertyChanged(propertyName);
            }
        }

        private void OnDispose()
        {
            _propertyObserver.Dispose();
        }

        #region Dispose Pattern
        public void Dispose()
        {
            Dispose(true);
        }

        private bool _isDisposed = false;
        private void Dispose(bool isDisposing)
        {
            if (!_isDisposed)
            {
                if (isDisposing)
                {
                    OnDispose();
                }
                _isDisposed = true;
            }
        }
        #endregion
    }
}
