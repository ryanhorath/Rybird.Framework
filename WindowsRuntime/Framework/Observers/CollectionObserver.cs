using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rybird.Framework
{
    public class CollectionObserver : ObserverBase
    {
        private IDisposable _collectionObserver;
        private Action<NotifyCollectionChangedEventArgs> _collectionChanged;

        public CollectionObserver(INotifyCollectionChanged collection, Action<NotifyCollectionChangedEventArgs> collectionChanged)
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

        protected override void OnDispose()
        {
            _collectionObserver.Dispose();
        }
    }
}
