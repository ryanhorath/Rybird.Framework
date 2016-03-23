using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rybird.Framework
{
    public class CollectionItemObserver<T> : ObserverBase
    {
        private IDisposable _collectionObserver;
        private Action<T> _itemAdded;
        private Action<T> _itemRemoved;

        public CollectionItemObserver(INotifyCollectionChanged collection, Action<T> itemAdded, Action<T> itemRemoved)
        {
            _itemAdded = itemAdded;
            _itemRemoved = itemRemoved;
            _collectionObserver = Observable.FromEventPattern<NotifyCollectionChangedEventHandler, NotifyCollectionChangedEventArgs>(
                ev => collection.CollectionChanged += ev, ev => collection.CollectionChanged -= ev)
                .Subscribe(e => CollectionChanged(e.EventArgs));
        }

        private void CollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            if (e.OldItems != null)
            {
                foreach (T item in e.OldItems)
                {
                    _itemRemoved(item);
                }
            }
            if (e.NewItems != null)
            {
                foreach (T item in e.NewItems)
                {
                    _itemAdded(item);
                }
            }
        }

        protected override void OnDispose()
        {
            _collectionObserver.Dispose();
        }
    }
}
