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
    public class ViewHolder : IDisposable
    {
        private Dictionary<int, View> _views = new Dictionary<int, View>();
        private View _parentView;

        public ViewHolder(View parentView)
        {
            parentView.ThrowIfNull("parentView");
            _parentView = parentView;
        }

        public T FindViewById<T>(int resourceId) where T : View
        {
            var view = _views.GetValueOrDefault(resourceId) as T;
            if (view == null)
            {
                view = _parentView.FindViewById<T>(resourceId);
                if (view != null)
                {
                    _views.Add(resourceId, view);
                }
                else
                {
                    throw new Exception(string.Format("Could not find view {0} of type {1}", resourceId, typeof(T).FullName));
                }
            }
            return view;
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
                    _parentView = null;
                    _views.Clear();
                }
                _disposed = true;
            }
        }
    }
}