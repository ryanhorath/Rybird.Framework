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
    public abstract class ViewBinding : BindableBase, IDisposable
    {
        private bool _disposed = false;

        protected ViewBinding(View view)
        {
            _view = view;
            var activity = view.Context as IFrameworkActivity;
        }

        private View _view;
        public View View
        {
            get { return _view; }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                _disposed = true;
                if (disposing)
                {
                    Cleanup();
                }
            }
        }

        protected abstract void Cleanup();
    }
}