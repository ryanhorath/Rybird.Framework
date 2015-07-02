using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rybird.Framework
{
    public struct FrameworkColor
    {
        public static readonly FrameworkColor Empty = new FrameworkColor();
        private static short StateARGBValueValid = (short)2;
        private static short StateValueMask = FrameworkColor.StateARGBValueValid;
        private static long NotDefinedValue = 0L;
        private readonly long value;
        private readonly short state;
        private const int ARGBAlphaShift = 24;
        private const int ARGBRedShift = 16;
        private const int ARGBGreenShift = 8;
        private const int ARGBBlueShift = 0;

        public static FrameworkColor Transparent
        {
            get
            {
                return new FrameworkColor();
            }
        }

        public byte R
        {
            get
            {
                return (byte)((ulong)(this.Value >> 16) & (ulong)byte.MaxValue);
            }
        }

        public byte G
        {
            get
            {
                return (byte)((ulong)(this.Value >> 8) & (ulong)byte.MaxValue);
            }
        }

        public byte B
        {
            get
            {
                return (byte)((ulong)this.Value & (ulong)byte.MaxValue);
            }
        }

        public byte A
        {
            get
            {
                return (byte)((ulong)(this.Value >> 24) & (ulong)byte.MaxValue);
            }
        }

        public bool IsEmpty
        {
            get
            {
                return (int)this.state == 0;
            }
        }

        private long Value
        {
            get
            {
                if (((int)this.state & (int)FrameworkColor.StateValueMask) != 0)
                {
                    return this.value;
                }
                else
                {
                    return FrameworkColor.NotDefinedValue;
                }
            }
        }

        private FrameworkColor(long value, short state)
        {
            this.value = value;
            this.state = state;
        }

        public static bool operator ==(FrameworkColor left, FrameworkColor right)
        {
            return !(left.value != right.value || (int)left.state != (int)right.state);
        }

        public static bool operator !=(FrameworkColor left, FrameworkColor right)
        {
            return !(left == right);
        }

        public static FrameworkColor FromArgb(int argb)
        {
            return new FrameworkColor((long)argb & (long)uint.MaxValue, FrameworkColor.StateARGBValueValid);
        }

        public static FrameworkColor FromArgb(int alpha, int red, int green, int blue)
        {
            FrameworkColor.CheckByte(alpha, "alpha");
            FrameworkColor.CheckByte(red, "red");
            FrameworkColor.CheckByte(green, "green");
            FrameworkColor.CheckByte(blue, "blue");
            return new FrameworkColor(FrameworkColor.MakeArgb((byte)alpha, (byte)red, (byte)green, (byte)blue), FrameworkColor.StateARGBValueValid);
        }

        public static FrameworkColor FromArgb(int alpha, FrameworkColor baseColor)
        {
            FrameworkColor.CheckByte(alpha, "alpha");
            return new FrameworkColor(FrameworkColor.MakeArgb((byte)alpha, baseColor.R, baseColor.G, baseColor.B), FrameworkColor.StateARGBValueValid);
        }

        public static FrameworkColor FromArgb(int red, int green, int blue)
        {
            return FrameworkColor.FromArgb((int)byte.MaxValue, red, green, blue);
        }

        public float GetBrightness()
        {
            float num1 = (float)this.R / (float)byte.MaxValue;
            float num2 = (float)this.G / (float)byte.MaxValue;
            float num3 = (float)this.B / (float)byte.MaxValue;
            float num4 = num1;
            float num5 = num1;
            if ((double)num2 > (double)num4)
                num4 = num2;
            if ((double)num3 > (double)num4)
                num4 = num3;
            if ((double)num2 < (double)num5)
                num5 = num2;
            if ((double)num3 < (double)num5)
                num5 = num3;
            return (float)(((double)num4 + (double)num5) / 2.0);
        }

        public float GetHue()
        {
            if ((int)this.R == (int)this.G && (int)this.G == (int)this.B)
                return 0.0f;
            float num1 = (float)this.R / (float)byte.MaxValue;
            float num2 = (float)this.G / (float)byte.MaxValue;
            float num3 = (float)this.B / (float)byte.MaxValue;
            float num4 = 0.0f;
            float num5 = num1;
            float num6 = num1;
            if ((double)num2 > (double)num5)
                num5 = num2;
            if ((double)num3 > (double)num5)
                num5 = num3;
            if ((double)num2 < (double)num6)
                num6 = num2;
            if ((double)num3 < (double)num6)
                num6 = num3;
            float num7 = num5 - num6;
            if ((double)num1 == (double)num5)
                num4 = (num2 - num3) / num7;
            else if ((double)num2 == (double)num5)
                num4 = (float)(2.0 + ((double)num3 - (double)num1) / (double)num7);
            else if ((double)num3 == (double)num5)
                num4 = (float)(4.0 + ((double)num1 - (double)num2) / (double)num7);
            float num8 = num4 * 60f;
            if ((double)num8 < 0.0)
                num8 += 360f;
            return num8;
        }

        public float GetSaturation()
        {
            float num1 = (float)this.R / (float)byte.MaxValue;
            float num2 = (float)this.G / (float)byte.MaxValue;
            float num3 = (float)this.B / (float)byte.MaxValue;
            float num4 = 0.0f;
            float num5 = num1;
            float num6 = num1;
            if ((double)num2 > (double)num5)
                num5 = num2;
            if ((double)num3 > (double)num5)
                num5 = num3;
            if ((double)num2 < (double)num6)
                num6 = num2;
            if ((double)num3 < (double)num6)
                num6 = num3;
            if ((double)num5 != (double)num6)
                num4 = ((double)num5 + (double)num6) / 2.0 > 0.5 ? (float)(((double)num5 - (double)num6) / (2.0 - (double)num5 - (double)num6)) : (float)(((double)num5 - (double)num6) / ((double)num5 + (double)num6));
            return num4;
        }

        public int ToArgb()
        {
            return (int)this.Value;
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder(32);
            stringBuilder.Append(this.GetType().Name);
            stringBuilder.Append(" [");
            if (((int)this.state & (int)FrameworkColor.StateValueMask) != 0)
            {
                stringBuilder.Append("A=");
                stringBuilder.Append(this.A);
                stringBuilder.Append(", R=");
                stringBuilder.Append(this.R);
                stringBuilder.Append(", G=");
                stringBuilder.Append(this.G);
                stringBuilder.Append(", B=");
                stringBuilder.Append(this.B);
            }
            else
            {
                stringBuilder.Append("Empty");
            }
            stringBuilder.Append("]");
            return ((object)stringBuilder).ToString();
        }

        public override bool Equals(object obj)
        {
            if (obj is FrameworkColor)
            {
                FrameworkColor color = (FrameworkColor)obj;
                return (this.value == color.value && (int)this.state == (int)color.state);
            }
            return false;
        }

        public override int GetHashCode()
        {
            return this.value.GetHashCode() ^ this.state.GetHashCode();
        }

        private static void CheckByte(int value, string name)
        {
            if (!(value >= 0 && value <= (int)byte.MaxValue))
            {
                throw new ArgumentException(string.Format("Value {0} is outside the acceptable range.", name));
            }
        }

        private static long MakeArgb(byte alpha, byte red, byte green, byte blue)
        {
            return (long)(uint)((int)red << 16 | (int)green << 8 | (int)blue | (int)alpha << 24) & (long)uint.MaxValue;
        }
    }
}
