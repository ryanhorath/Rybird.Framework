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
using Android.Graphics;
using Android.Util;

namespace Rybird.Framework
{
    public class ItemsView : LinearLayout
    {
        public ItemsView(Context context, IAttributeSet attrs, int defStyle) : base(context, attrs, defStyle)
        {
        }

        public ItemsView(Context context, IAttributeSet attrs) : base(context, attrs)
        {
        }

        public ItemsView(Context context)
            : base(context)
        {
        }

        private IObservableAdapter _adapter;
        public IObservableAdapter Adapter
        {
            get { return _adapter; }
            set
            {
                _adapter = value;
                SetChildViews();
            }
        }

        private void SetChildViews()
        {
            RemoveAllViews();
            for (int i = 0; i < Adapter.Count; i++)
            {
                AddView(_adapter.GetView(i, null, null));
            }
        }
    }
}