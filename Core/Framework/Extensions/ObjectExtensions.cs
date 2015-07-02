using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace System
{
    public static class ObjectExtensions
    {
        public static bool EqualsAny<T>(this T obj, params T[] values)
        {
            return values.Any(v => v.Equals(obj));
        }

        public static void ThrowIfNull<T>(this T obj, string propertyName) where T : class
        {
            if (obj == null)
            {
                throw new ArgumentNullException(propertyName);
            }
        }

        public static IEnumerable<T> GetAttributesForClass<T>(this object obj, bool includeInherited) where T : Attribute
        {
            return (obj as Type ?? obj.GetType())
                .GetTypeInfo()
                .GetCustomAttributes(typeof(T), includeInherited).OfType<T>()
                .Select(attribute => attribute);
        }

        public static IEnumerable<T> GetAttributesForField<T>(this object obj, string fieldName, bool includeInherited) where T : Attribute
        {
            return (obj as Type ?? obj.GetType())
                .GetRuntimeField(fieldName)
                .GetCustomAttributes(typeof(T), includeInherited)
                .Cast<T>();
        }

        public static IEnumerable<T> GetAttributesForProperty<T>(this object obj, string propertyName, bool includeInherited) where T : Attribute
        {
            return (obj as Type ?? obj.GetType())
                .GetRuntimeProperty(propertyName)
                .GetCustomAttributes(typeof(T), includeInherited)
                .Cast<T>();
        }

        public static IDictionary<PropertyInfo, IEnumerable<T>> GetAttributesForAllProperties<T>(this object obj, bool includeInherited) where T : Attribute
        {
            Dictionary<PropertyInfo, IEnumerable<T>> attributes = new Dictionary<PropertyInfo, IEnumerable<T>>();
            var properties = (obj as Type ?? obj.GetType())
                .GetRuntimeProperties();
            foreach (var property in properties)
            {
                IEnumerable<T> propertyAttributes = property.GetCustomAttributes(typeof(T), includeInherited).Cast<T>();
                attributes.Add(property, propertyAttributes);
            }
            return attributes;
        }
    }
}
