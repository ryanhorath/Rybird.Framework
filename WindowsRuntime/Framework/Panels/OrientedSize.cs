using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace Rybird.Framework
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct OrientedSize
    {
        public OrientedSize(Orientation orientation) :
            this(orientation, 0.0, 0.0)
        {
        }

        public OrientedSize(Orientation orientation, double width, double height)
        {
            _orientation = orientation;

            // All fields must be initialized before we access the this pointer
            _direct = 0.0;
            _indirect = 0.0;

            Width = width;
            Height = height;
        }

        private Orientation _orientation;
        public Orientation Orientation
        {
            get { return _orientation; }
        }

        private double _direct;
        public double Direct
        {
            get { return _direct; }
            set { _direct = value; }
        }

        private double _indirect;
        public double Indirect
        {
            get { return _indirect; }
            set { _indirect = value; }
        }

        public double Width
        {
            get
            {
                return (Orientation == Orientation.Horizontal) ?
                    Direct :
                    Indirect;
            }
            set
            {
                if (Orientation == Orientation.Horizontal)
                {
                    Direct = value;
                }
                else
                {
                    Indirect = value;
                }
            }
        }

        public double Height
        {
            get
            {
                return (Orientation != Orientation.Horizontal) ?
                    Direct :
                    Indirect;
            }
            set
            {
                if (Orientation != Orientation.Horizontal)
                {
                    Direct = value;
                }
                else
                {
                    Indirect = value;
                }
            }
        }
    }
}
