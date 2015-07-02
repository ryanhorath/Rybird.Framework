using System;
using System.Net;
using System.Windows;
using System.Windows.Input;

namespace System
{
    public static class Int32Extensions
    {
        public static bool IsEven(this long value)
        {
            return value % 2 == 0;
        }

        public static bool IsOdd(this long value)
        {
            return value % 2 != 0;
        }

        public static bool InBetweenExclusive(this int value, long minValue, long maxValue)
        {
            return (value >= minValue && value <= maxValue);
        }

        public static bool IsBetweenInclusive(this int value, long minValue, long maxValue)
        {
            return (value > minValue && value < maxValue);
        }
    }
}
