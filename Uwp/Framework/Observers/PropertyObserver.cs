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
    public class PropertyObserver : ObserverBase
    {
        private IDisposable _propertyObserver;
        private Action<string> _propertyChanged;
        private IEnumerable<string> _propertyNames;

        public PropertyObserver(INotifyPropertyChanged obj, Action<string> propertyChanged, IEnumerable<string> propertyNames)
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

        protected override void OnDispose()
        {
            _propertyObserver.Dispose();
        }
    }
}
