using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System
{
    public static class IComparableExtensions
    {
        public static bool IsBetween<T>(this T value, T minValue, T maxValue, bool useExclusiveComparison) where T : IComparable<T>
        {
            return IsBetween(value, minValue, maxValue, useExclusiveComparison, Comparer<T>.Default);
        }

        public static bool IsBetween<T>(this T value, T minValue, T maxValue, bool useExclusiveComparison, IComparer<T> comparer) where T : IComparable<T>
        {
            if (comparer == null)
            {
                throw new ArgumentNullException("comparer");
            }
            int minMaxCompare = comparer.Compare(minValue, maxValue);
            if (minMaxCompare < 0)
            {
                if (useExclusiveComparison)
                {
                    return ((comparer.Compare(value, minValue) > 0) && (comparer.Compare(value, maxValue) < 0));
                }
                else
                {
                    return ((comparer.Compare(value, minValue) >= 0) && (comparer.Compare(value, maxValue) <= 0));
                }
            }
            else
            {
                if (useExclusiveComparison)
                {
                    return ((comparer.Compare(value, maxValue) > 0) && (comparer.Compare(value, minValue) < 0));
                }
                else
                {
                    return ((comparer.Compare(value, maxValue) >= 0) && (comparer.Compare(value, minValue) <= 0));
                }
            }
        }
    }
}
