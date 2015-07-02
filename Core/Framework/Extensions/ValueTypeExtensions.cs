using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System
{
    public static class ValueTypeExtensions
    {
        public static bool IsDefault<T>(this T value) where T : struct
        {
            return value.Equals(default(T));
        }

        public static bool IsNotDefault<T>(this T value) where T : struct
        {
            return (value.IsDefault() == false);
        }

        public static T? ToNullable<T>(this T value) where T : struct
        {
            return (value.IsDefault() ? null : (T?)value);
        }
    }
}
