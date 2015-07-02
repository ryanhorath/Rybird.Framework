using System;
using System.Globalization;
using System.Runtime.InteropServices;
using System.ComponentModel;

namespace Rybird.Framework
{
    public struct FrameworkSize
    {
        public static readonly FrameworkSize Empty = new FrameworkSize();

        private double _width;
        public double Width { get { return _width; } set { _width = value; } }
        private double _height;
        public double Height { get { return _height; } set { _height = value; } }

        public FrameworkSize(FrameworkPoint point)
        {
            _width = point.X;
            _height = point.Y;
        }

        public FrameworkSize(double width, double height)
        {
            _width = width;
            _height = height;
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public bool IsEmpty
        {
            get
            {
                return ((_width == 0.0) && (_height == 0.0));
            }
        }

        public static explicit operator FrameworkPoint(FrameworkSize size)
        {
            return new FrameworkPoint(size.Width, size.Height);
        }

        public static FrameworkSize operator +(FrameworkSize left, FrameworkSize right)
        {
            return new FrameworkSize(left.Width + right.Width, left.Height + right.Height);
        }

        public static bool operator ==(FrameworkSize left, FrameworkSize right)
        {
            return ((left.Width == right.Width) && (left.Height == right.Height));
        }

        public static bool operator !=(FrameworkSize left, FrameworkSize right)
        {
            return ((left.Width != right.Width) || (left.Height != right.Height));
        }

        public static FrameworkSize operator -(FrameworkSize left, FrameworkSize right)
        {
            return new FrameworkSize(left.Width - right.Width,
                      left.Height - right.Height);
        }

        public override bool Equals(object obj)
        {
            return ((obj is FrameworkSize) && (this == (FrameworkSize)obj));
        }

        public override int GetHashCode()
        {
            return (int)_width ^ (int)_height;
        }

        public override string ToString()
        {
            return string.Format("{{Width={0}, Height={1}}}", _width.ToString(CultureInfo.CurrentCulture),
                _height.ToString(CultureInfo.CurrentCulture));
        }
    }
}
