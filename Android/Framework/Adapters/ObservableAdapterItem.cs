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

namespace Rybird.Framework
{
    public class ObservableAdapterItem<T> : IDisposable
    {
        public ObservableAdapterItem(View view, T item)
        {
            View = view;
            Item = item;
        }

        public ObservableAdapterItem(View view, T item, ViewHolder viewHolder) : this(view, item)
        {
            _viewHolder = viewHolder;
        }

        public View View { get; private set; }
        public T Item { get; private set; }

        private ViewHolder _viewHolder;
        public ViewHolder ViewHolder
        {
            get { return _viewHolder ?? (_viewHolder = new ViewHolder(View)); }
        }

        private bool _disposed = false;
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
                    if (_viewHolder != null)
                    {
                        _viewHolder.Dispose();
                    }
                    Item = default(T);
                    View = null;
                }
                _disposed = true;
            }
        }
    }
}