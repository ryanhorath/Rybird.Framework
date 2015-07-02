using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Collections;

namespace Rybird.Framework
{
    public class CollectionSyncer<SourceType, TargetType> : IDisposable
    {
        public delegate TargetType SourceTypeToTargetTypeCallback(SourceType item);

        private IEnumerable<SourceType> _sourceCollection;
        private ICollection<TargetType> _targetCollection;
        private SourceTypeToTargetTypeCallback _addItemCallback;
        private SourceTypeToTargetTypeCallback _removeItemCallback;
        private CollectionChangedObserver _observer;

        public CollectionSyncer(IEnumerable<SourceType> sourceCollection, ICollection<TargetType> targetCollection,
            SourceTypeToTargetTypeCallback sourceItemAddedCallback, SourceTypeToTargetTypeCallback sourceItemRemovedCallback, bool initalizeTargetList)
        {
            Guard.AgainstNull(sourceCollection, "sourceCollection");
            Guard.AgainstNull(targetCollection, "targetCollection");
            if (!(sourceCollection is INotifyCollectionChanged))
            {
                throw new Exception("Source collection must implement INotifyCollectionChanged");
            }
            _observer = new CollectionChangedObserver((INotifyCollectionChanged)sourceCollection, (e) => CollectionChanged(e));
            _sourceCollection = sourceCollection;
            _targetCollection = targetCollection;
            _addItemCallback = sourceItemAddedCallback;
            _removeItemCallback = sourceItemRemovedCallback;
            if (initalizeTargetList)
            {
                InitializeTarget();
            }
        }

        private void sourceCollection_CollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            CollectionChanged(e);
        }

        private void InitializeTarget()
        {
            if (_addItemCallback != null)
            {
                foreach (SourceType item in _sourceCollection)
                {
                    var targetItem = _addItemCallback.Invoke(item);
                    _targetCollection.AddUnique(targetItem);
                }
            }
        }

        private void CollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Reset)
            {
                _targetCollection.Clear();
            }
            else
            {
                if (e.OldItems != null)
                {
                    foreach (SourceType item in e.OldItems)
                    {
                        var targetItem = _removeItemCallback.Invoke(item);
                        _targetCollection.Remove(targetItem);
                    }
                }
                if (e.NewItems != null)
                {
                    foreach (SourceType item in e.NewItems)
                    {
                        var targetItem = _addItemCallback.Invoke(item);
                        _targetCollection.AddUnique(targetItem);
                    }
                }
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }

        private bool _isDisposed = false;
        private void Dispose(bool isDisposing)
        {
            if (!_isDisposed)
            {
                if (isDisposing && _observer != null)
                {
                    _observer.Dispose();
                }
                _isDisposed = true;
            }
        }
    }
}
