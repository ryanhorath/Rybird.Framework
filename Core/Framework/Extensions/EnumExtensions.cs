using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.ComponentModel;
using Rybird.Framework;

namespace System
{
    public static class EnumExtensions
    {
        public static string ToDescription(this Enum value)
        {
            var attribute = value.GetType().GetRuntimeField(value.ToString()).GetCustomAttribute<EnumDescriptionAttribute>();
            return attribute == null ? value.ToString() : attribute.Description;
        }

        public static TEnum FromDescription<TEnum>(string description) where TEnum : struct
        {
            if (!(typeof(TEnum).GetTypeInfo().IsEnum))
            {
                throw new InvalidOperationException("Type provided must be an enum type.");
            }
            foreach (var field in typeof(TEnum).GetRuntimeFields())
            {
                var attribute = field.GetAttributesForClass<EnumDescriptionAttribute>(true).FirstOrDefault();
                string value = attribute != null ? attribute.Description : field.Name;
                if (description == value)
                {
                    return (TEnum)field.GetValue(null);
                }
            }
            throw new ArgumentException("Enum value not found.", "description");
        }

        public static bool IsFlagSet<T>(this Enum value, T flag)
            where T : struct
        {
            // do not refactor to: var v = (int) (object) value;
            // this causes a strange exception in WP7.
            var valueObj = (object)value;
            var v = (int)valueObj;

            var flagObj = (object)flag;
            var f = (int)flagObj;

            return (v & f) == f;
        }

        public static T ReadEnum<T>(string value, T defaultValue = default(T))
            where T : struct
        {
            try
            {
                return (T)Enum.Parse(typeof(T), value, true);

            }
            catch (Exception)
            {
                return defaultValue;
            }
        }
    }
}
