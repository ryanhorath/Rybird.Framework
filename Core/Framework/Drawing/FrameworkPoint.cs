using System;
using System.Globalization;
using System.Runtime.InteropServices;
using System.ComponentModel;

namespace Rybird.Framework
{
    public struct FrameworkPoint
    {
        public static readonly FrameworkPoint Empty = new FrameworkPoint();

        public FrameworkPoint(double x, double y)
        {
            _x = x;
            _y = y;
        }

        public FrameworkPoint(FrameworkSize size)
        {
            _x = size.Width;
            _y = size.Height;
        }
        
        private double _x;
        public double X { get { return _x; } set { _x = value; } }
        private double _y;
        public double Y { get { return _y; } set { _y = value; } }

        public static explicit operator FrameworkSize(FrameworkPoint point)
        {
            return new FrameworkSize(point.X, point.Y);
        }

        public static FrameworkPoint operator +(FrameworkPoint point, FrameworkSize size)
        {
            return new FrameworkPoint(point.X + size.Width, point.Y + size.Height);
        }

        public static bool operator ==(FrameworkPoint left, FrameworkPoint right)
        {
            return ((left.X == right.X) && (left.Y == right.Y));
        }

        public static bool operator !=(FrameworkPoint left, FrameworkPoint right)
        {
            return !(left == right);
        }

        public static FrameworkPoint operator -(FrameworkPoint point, FrameworkSize size)
        {
            return new FrameworkPoint(point.X - size.Width, point.Y - size.Height);
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public bool IsEmpty
        {
            get
            {
                return ((X == 0.0) && (Y == 0.0));
            }
        }

        public override bool Equals(object obj)
        {
            return ((obj is FrameworkPoint) && (this == (FrameworkPoint)obj));
        }

        public override int GetHashCode()
        {
            return (int)_x ^ (int)_y;
        }

        public override string ToString()
        {
            return String.Format("{{X={0}, Y={1}}}", _x.ToString(CultureInfo.CurrentCulture), _y.ToString(CultureInfo.CurrentCulture));
        }
    }
}
