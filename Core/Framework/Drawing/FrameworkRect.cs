using System;
using System.ComponentModel;

namespace Rybird.Framework
{
    public struct FrameworkRect
    {
        public static readonly FrameworkRect Empty = new FrameworkRect();

        [EditorBrowsable(EditorBrowsableState.Never)]
        public bool IsEmpty
        {
            get
            {
                return (_width <= 0 || _height <= 0);
            }
        }

        public FrameworkPoint Location
        {
            get
            {
                return new FrameworkPoint(_x, _y);
            }
            set
            {
                _x = value.X;
                _y = value.Y;
            }
        }

        public FrameworkSize Size
        {
            get
            {
                return new FrameworkSize(_width, _height);
            }
            set
            {
                _width = value.Width;
                _height = value.Height;
            }
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public double Left { get { return _x; } }
        [EditorBrowsable(EditorBrowsableState.Never)]
        public double Right { get { return _x + _width; } }
        [EditorBrowsable(EditorBrowsableState.Never)]
        public double Top { get { return _y; } }
        [EditorBrowsable(EditorBrowsableState.Never)]
        public double Bottom { get { return _y + _height; } }
        private double _width;
        public double Width { get { return _width; } set { _width = value; } }
        private double _height;
        public double Height { get { return _height; } set { _height = value; } }
        private double _x;
        public double X { get { return _x; } set { _x = value; } }
        private double _y;
        public double Y { get { return _y; } set { _y = value; } }

        public FrameworkRect(FrameworkPoint location, FrameworkSize size)
        {
            _x = location.X;
            _y = location.Y;
            _width = size.Width;
            _height = size.Height;
        }

        public FrameworkRect(double x, double y, double width, double height)
        {
            this._x = x;
            this._y = y;
            this._width = width;
            this._height = height;
        }

        public static FrameworkRect FromLTRB(double left, double top, double right, double bottom)
        {
            return new FrameworkRect(left, top, right - left, bottom - top);
        }

        public static FrameworkRect Inflate(FrameworkRect rect, double x, double y)
        {
            FrameworkRect ir = new FrameworkRect(rect.X, rect.Y, rect.Width, rect.Height);
            ir.Inflate(x, y);
            return ir;
        }

        public void Inflate(double x, double y)
        {
            Inflate(new FrameworkSize(x, y));
        }

        public void Inflate(FrameworkSize size)
        {
            _x -= size.Width;
            _y -= size.Height;
            _width += size.Width * 2;
            _height += size.Height * 2;
        }

        public static FrameworkRect Intersect(FrameworkRect a, FrameworkRect b)
        {
            // MS.NET returns a non-empty rectangle if the two rectangles
            // touch each other
            if (!a.IntersectsWithInclusive(b))
                return Empty;

            return FromLTRB(
                Math.Max(a.Left, b.Left),
                Math.Max(a.Top, b.Top),
                Math.Min(a.Right, b.Right),
                Math.Min(a.Bottom, b.Bottom));
        }

        public void Intersect(FrameworkRect rect)
        {
            this = FrameworkRect.Intersect(this, rect);
        }

        public static FrameworkRect Union(FrameworkRect left, FrameworkRect right)
        {
            return FromLTRB(Math.Min(left.Left, right.Left), Math.Min(left.Top, right.Top), Math.Max(left.Right, right.Right), Math.Max(left.Bottom, right.Bottom));
        }

        public static bool operator ==(FrameworkRect left, FrameworkRect right)
        {
            return (left.X == right.X) && (left.Y == right.Y) && (left.Width == right.Width) && (left.Height == right.Height);
        }

        public static bool operator !=(FrameworkRect left, FrameworkRect right)
        {
            return (left.X != right.X) || (left.Y != right.Y) || (left.Width != right.Width) || (left.Height != right.Height);
        }

        public bool Contains(double x, double y)
        {
            return ((x >= Left) && (x < Right) && (y >= Top) && (y < Bottom));
        }

        public bool Contains(FrameworkPoint point)
        {
            return Contains(point.X, point.Y);
        }

        public bool Contains(FrameworkRect rect)
        {
            return (rect == Intersect(this, rect));
        }

        public override bool Equals(object obj)
        {
            return ((obj is FrameworkRect)) && (this == (FrameworkRect)obj);
        }

        public override int GetHashCode()
        {
            return (int)(_x + _y + _width + _height);
        }

        public bool IntersectsWith(FrameworkRect rect)
        {
            return !((Left >= rect.Right) || (Right <= rect.Left) || (Top >= rect.Bottom) || (Bottom <= rect.Top));
        }

        private bool IntersectsWithInclusive(FrameworkRect rect)
        {
            return !((Left > rect.Right) || (Right < rect.Left) || (Top > rect.Bottom) || (Bottom < rect.Top));
        }

        public void Offset(double x, double y)
        {
            _x += x;
            _y += y;
        }

        public void Offset(FrameworkPoint position)
        {
            Offset(position.X, position.Y);
        }

        public override string ToString()
        {
            return String.Format("{{X={0},Y={1},Width={2},Height={3}}}", _x, _y, _width, _height);
        }
    }
}
