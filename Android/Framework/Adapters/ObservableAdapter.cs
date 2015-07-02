using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using Android.Views;
using Android.Widget;

namespace Rybird.Framework
{
    public class ObservableAdapter<T> : BaseAdapter<T>, IObservableAdapter
    {
        private bool _disposed;
        private readonly Action<ObservableAdapterItem<T>> _bindViewCallback;
        private readonly Action<ObservableAdapterItem<T>> _unbindViewCallback;
        private readonly IList<ObservableAdapterItem<T>> _items = new List<ObservableAdapterItem<T>>();
        private readonly ILayoutInflaterProvider _inflater;
        private readonly IEnumerable<T> _sourceList;
        private readonly IEnumerable<T> _unfilteredList;
        private readonly Func<T, int> _itemLayoutIdCallback;
        private readonly Func<string, IEnumerable<T>, IEnumerable<T>> _filterCallback;
        private const string _currentFilterValue = "";
        private IEnumerable<T> _filteredList;

        public ObservableAdapter(
                IEnumerable<T> list,
                ILayoutInflaterProvider inflater,
                Func<T, int> itemLayoutIdCallback,
                Action<ObservableAdapterItem<T>> bindViewCallback,
                Action<ObservableAdapterItem<T>> unbindViewCallback,
                Func<string, IEnumerable<T>, IEnumerable<T>> filterCallback = null)
        {
            list.ThrowIfNull("list");
            inflater.ThrowIfNull("inflater");
            itemLayoutIdCallback.ThrowIfNull("itemLayoutIdCallback");
            PauseUpdates = false;
            _inflater = inflater;
            _sourceList = list;
            _filteredList = new List<T>(list);
            _unfilteredList = list;
            _bindViewCallback = bindViewCallback != null ? bindViewCallback : EmptyBindCallback;
            _unbindViewCallback = unbindViewCallback != null ? unbindViewCallback : EmptyUnbindCallback;
            _itemLayoutIdCallback = itemLayoutIdCallback;
            _filterCallback = filterCallback;
            if (_filterCallback == null)
            {
                _filterCallback = EmptyFilter;
            }
            var notifyCollection = list as INotifyCollectionChanged;
            if (notifyCollection != null)
            {
                notifyCollection.CollectionChanged += notifyCollection_CollectionChanged;
            }
        }

        protected bool PauseUpdates { get; set; }

        private void notifyCollection_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (!PauseUpdates)
            {
                RefreshList();
            }
        }

        protected void RefreshList()
        {
            var currentFilter = _filter != null ? _filter.CurrrentFilter : "";
            _filteredList = _filterCallback(currentFilter, _unfilteredList).ToList();
            NotifyDataSetChanged();
            OnDataSetChanged();
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View view = convertView;
            var item = _filteredList.ElementAt(position);
            ViewHolder recycledViewHolder = null;
            if (convertView == null)
            {
                view = _inflater.LayoutInflater.Inflate(_itemLayoutIdCallback.Invoke(item), parent, false);
            }
            else
            {
                var adapterItem = _items.SingleOrDefault(i => i.Item.Equals(item));
                if (adapterItem != null)
                {
                    recycledViewHolder = adapterItem.ViewHolder;
                    _items.Remove(adapterItem);
                    _unbindViewCallback(adapterItem);                    
                    adapterItem.Dispose();
                }
            }
            var newAdapterItem = new ObservableAdapterItem<T>(convertView, item, recycledViewHolder);
            _items.Add(newAdapterItem);
            _bindViewCallback(newAdapterItem);
            return view;
        }

        public T ItemFromView(View view)
        {
            var item = _items.SingleOrDefault(i => i.View == view);
            return item != null ? item.Item : default(T);
        }

        public int GetPosition(T item)
        {
            for (int i = 0; i < _filteredList.Count(); i++)
            {
                if (_filteredList.ElementAt(i).Equals(item))
                {
                    return i;
                }
            }
            return -1;
        }

        private void DisposeItems()
        {
            foreach (var item in _items)
            {
                _unbindViewCallback(item);
                item.Dispose();
            }
            _items.Clear();
        }

        public new void Dispose()
        {
            Dispose(true);
            base.Dispose();
            GC.SuppressFinalize(this);
        }

        protected new virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    DisposeItems();
                    var notifyList = _sourceList as INotifyCollectionChanged;
                    if (notifyList != null)
                    {
                        notifyList.CollectionChanged -= notifyCollection_CollectionChanged;
                    }
                }
                _disposed = true;
            }
        }

        public override int Count { get { return _filteredList.Count(); } }

        public override Java.Lang.Object GetItem(int position)
        {
            return new JavaWrapper<T>(_filteredList.ElementAt(position));
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override T this[int position] { get { return _filteredList.ElementAt(position); } }

        internal IEnumerable<T> UnfilteredItems { get { return _unfilteredList; } }

        internal IEnumerable<T> FilteredItems { set { _filteredList = value.ToList(); } }

        private ObservableAdapterFilter<T> _filter;
        public Filter Filter
        {
            get { return _filter ?? (_filter = new ObservableAdapterFilter<T>(this, s => _filterCallback.Invoke(s, _unfilteredList))); }
        }

        private IEnumerable<T> EmptyFilter(string filterString, IEnumerable<T> unfilteredList)
        {
            return unfilteredList;
        }

        private void EmptyBindCallback(ObservableAdapterItem<T> item)
        {

        }

        private void EmptyUnbindCallback(ObservableAdapterItem<T> item)
        {

        }

        public event EventHandler<EventArgs> DataSetChanged;

        protected virtual void OnDataSetChanged()
        {
            EventHandler<EventArgs> handler = DataSetChanged;
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }
    }
}
