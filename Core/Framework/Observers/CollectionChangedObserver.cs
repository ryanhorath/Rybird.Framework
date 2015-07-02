using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rybird.Framework
{
    public class CollectionChangedObserver : IDisposable
    {
        private IDisposable _collectionObserver;
        private Action<NotifyCollectionChangedEventArgs> _collectionChanged;

        public CollectionChangedObserver(INotifyCollectionChanged collection, Action<NotifyCollectionChangedEventArgs> collectionChanged)
        {
            _collectionChanged = collectionChanged;
            _collectionObserver = Observable.FromEventPattern<NotifyCollectionChangedEventHandler, NotifyCollectionChangedEventArgs>(
                ev => collection.CollectionChanged += ev, ev => collection.CollectionChanged -= ev)
                .Subscribe(e => CollectionChanged(e.EventArgs));
        }

        private void CollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            _collectionChanged(e);
        }

        private void OnDispose()
        {
            _collectionObserver.Dispose();
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
