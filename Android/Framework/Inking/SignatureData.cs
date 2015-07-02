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

namespace Rybird.Framework
{
    public class SignatureData
    {
        public List<List<Point>> Paths { get { return _Paths; } }
        public event EventHandler PointAdded;

        public Point LastPoint()
        {
            if (currentPath != null && currentPath.Count > 0)
            {
                return currentPath.Last();
            }
            return new Point(0, 0);
        }

        public void Clear()
        {
            _Paths = new List<List<Point>>();
            currentPath = new List<Point>();
        }

        public void AddPoint(SignatureState state, int x, int y)
        {
            if (state == SignatureState.Start)
            {
                currentPath = new List<Point>();
            }
            if (x != 0 && y != 0)
            {
                currentPath.Add(new Point(x, y));
            }
            if (state == SignatureState.End)
            {
                if (currentPath != null)
                {
                    _Paths.Add(currentPath);
                }
            }
            OnPointAdded(EventArgs.Empty);
        }

        public int Length
        {
            get { return _Paths.Count; }
        }

        #region Implementation

        protected void OnPointAdded(EventArgs e)
        {
            if (PointAdded != null)
            {
                PointAdded(this, e);
            }
        }

        private List<Point> currentPath = new List<Point>();
        private List<List<Point>> _Paths = new List<List<Point>>();

        #endregion

    }
}