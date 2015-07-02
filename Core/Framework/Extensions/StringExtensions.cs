using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Xml;
using System.IO;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Reflection;
using Rybird.Framework;

namespace System
{
    public static class StringExtensions
    {
        #region Common string extensions

        public static void ThrowIfNullOrEmpty(this string value, string propertyName)
        {
            value.ThrowIfNull(propertyName);
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentException(propertyName);
            }
        }

        public static string TrimToMaxLength(this string value, int maxLength)
        {
            return (value == null || value.Length <= maxLength ? value : value.Substring(0, maxLength));
        }

        public static string TrimToMaxLength(this string value, int maxLength, string suffix)
        {
            return (value == null || value.Length <= maxLength ? value : string.Concat(value.Substring(0, maxLength), suffix));
        }

        public static bool Contains(this string inputValue, string comparisonValue, StringComparison comparisonType)
        {
            return (inputValue.IndexOf(comparisonValue, comparisonType) != -1);
        }

        public static string PadBoth(this string value, int width, char padChar, bool truncate = false)
        {
            int diff = width - value.Length;
            if (diff == 0 || diff < 0 && !(truncate))
            {
                return value;
            }
            else if (diff < 0)
            {
                return value.Substring(0, width);
            }
            else
            {
                return value.PadLeft(width - diff / 2, padChar).PadRight(width, padChar);
            }
        }

        public static XDocument ToXDocument(this string xml)
        {
            return XDocument.Parse(xml);
        }

        public static XElement ToXElement(this string xml)
        {
            return XElement.Parse(xml);
        }

        public static string Reverse(this string value)
        {
            if (string.IsNullOrEmpty(value) || (value.Length == 1))
            {
                return value;
            }
            var chars = value.ToCharArray();
            Array.Reverse(chars);
            return new string(chars);
        }

        public static string EnsureStartsWith(this string value, string prefix)
        {
            return value.StartsWith(prefix) ? value : string.Concat(prefix, value);
        }

        public static string EnsureEndsWith(this string value, string suffix)
        {
            return value.EndsWith(suffix) ? value : string.Concat(value, suffix);
        }

        public static string Repeat(this string value, int repeatCount)
        {
            if (value.Length == 1)
            {
                return new string(value[0], repeatCount);
            }
            var sb = new StringBuilder(repeatCount * value.Length);
            while (repeatCount-- > 0)
            {
                sb.Append(value);
            }
            return sb.ToString();
        }

        public static bool IsNumeric(this string value)
        {
            float output;
            return float.TryParse(value, out output);
        }

        public static string ConcatWith(this string value, params string[] values)
        {
            return string.Concat(value, string.Concat(values));
        }

        public static string GetBefore(this string value, string x)
        {
            var xPos = value.IndexOf(x);
            return xPos == -1 ? String.Empty : value.Substring(0, xPos);
        }

        public static string GetBetween(this string value, string x, string y)
        {
            var xPos = value.IndexOf(x);
            var yPos = value.LastIndexOf(y);

            if (xPos == -1 || xPos == -1)
            {
                return String.Empty;
            }
            var startIndex = xPos + x.Length;
            return startIndex >= yPos ? String.Empty : value.Substring(startIndex, yPos - startIndex).Trim();
        }

        public static string GetAfter(this string value, string x)
        {
            var xPos = value.LastIndexOf(x);

            if (xPos == -1)
            {
                return String.Empty;
            }
            var startIndex = xPos + x.Length;
            return startIndex >= value.Length ? String.Empty : value.Substring(startIndex).Trim();
        }

        public static string ToUpperFirstLetter(this string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return string.Empty;
            }
            char[] valueChars = value.ToCharArray();
            valueChars[0] = char.ToUpper(valueChars[0]);
            return new string(valueChars);
        }

        public static byte[] GetBytes(this string data)
        {
            return Encoding.Unicode.GetBytes(data);
        }

        public static byte[] GetBytes(this string data, Encoding encoding)
        {
            return encoding.GetBytes(data);
        }

        public static string ToPlural(this string singular)
        {
            // Multiple words in the form A of B : Apply the plural to the first word only (A)
            int index = singular.LastIndexOf(" of ");
            if (index > 0) return (singular.Substring(0, index)) + singular.Remove(0, index).ToPlural();

            // single Word rules
            //sibilant ending rule
            if (singular.EndsWith("sh")) return singular + "es";
            if (singular.EndsWith("ch")) return singular + "es";
            if (singular.EndsWith("us")) return singular + "es";
            if (singular.EndsWith("ss")) return singular + "es";
            //-ies rule
            if (singular.EndsWith("y")) return singular.Remove(singular.Length - 1, 1) + "ies";
            // -oes rule
            if (singular.EndsWith("o")) return singular.Remove(singular.Length - 1, 1) + "oes";
            // -s suffix rule
            return singular + "s";
        }

        public static string ReplaceAll(this string value, IEnumerable<string> oldValues, Func<string, string> replacePredicate)
        {
            var sbStr = new StringBuilder(value);
            foreach (var oldValue in oldValues)
            {
                var newValue = replacePredicate(oldValue);
                sbStr.Replace(oldValue, newValue);
            }
            return sbStr.ToString();
        }

        public static string ReplaceAll(this string value, IEnumerable<string> oldValues, string newValue)
        {
            var sbStr = new StringBuilder(value);
            foreach (var oldValue in oldValues)
            {
                sbStr.Replace(oldValue, newValue);
            }
            return sbStr.ToString();
        }

        public static string ReplaceAll(this string value, IEnumerable<string> oldValues, IEnumerable<string> newValues)
        {
            var sbStr = new StringBuilder(value);
            var newValueEnum = newValues.GetEnumerator();
            foreach (var old in oldValues)
            {
                if (!newValueEnum.MoveNext())
                {
                    throw new ArgumentOutOfRangeException("newValues", "newValues sequence is shorter than oldValues sequence");
                }
                sbStr.Replace(old, newValueEnum.Current);
            }
            if (newValueEnum.MoveNext())
            {
                throw new ArgumentOutOfRangeException("newValues", "newValues sequence is longer than oldValues sequence");
            }
            return sbStr.ToString();
        }

        #endregion

        #region Bytes & Base64

        public static byte[] ToBytes(this string value)
        {
            return value.ToBytes(null);
        }

        public static byte[] ToBytes(this string value, Encoding encoding)
        {
            encoding = (encoding ?? Encoding.Unicode);
            return encoding.GetBytes(value);
        }

        public static string EncodeBase64(this string value)
        {
            return value.EncodeBase64(null);
        }

        public static string EncodeBase64(this string value, Encoding encoding)
        {
            encoding = (encoding ?? Encoding.UTF8);
            var bytes = encoding.GetBytes(value);
            return Convert.ToBase64String(bytes);
        }

        public static string DecodeBase64(this string encodedValue)
        {
            return encodedValue.DecodeBase64(null);
        }

        public static string DecodeBase64(this string encodedValue, Encoding encoding)
        {
            encoding = (encoding ?? Encoding.UTF8);
            var bytes = Convert.FromBase64String(encodedValue);
            return encoding.GetString(bytes, 0, bytes.Length);
        }

        #endregion

        #region String to Enum

        public static T ParseEnumFromValue<T>(this string enumValue, bool ignorecase = default(bool))
                where T : struct
        {
            return enumValue.IsItemInEnum<T>() ? default(T) : (T)Enum.Parse(typeof(T), enumValue, default(bool));
        }

        public static bool IsItemInEnum<T>(this string dataToCheck)
            where T : struct
        {
            return (string.IsNullOrEmpty(dataToCheck) || !Enum.IsDefined(typeof(T), dataToCheck));
        }

        public static T ParseEnumFromDescription<T>(this string enumDescription, bool ignorecase = default(bool))
            where T : struct
        {
            IDictionary<string, T> valuesMap = CreateFieldMap<T>(enumDescription, ignorecase);
            T enumValue = default(T);
            if (valuesMap.TryGetValue(enumDescription, out enumValue))
            {
                return enumValue;
            }
            throw new Exception(string.Format("Description '{0}' is not a valid description for the enum type {1}", enumDescription, typeof(T).FullName));
        }

        private static IDictionary<string, T> CreateFieldMap<T>(this string enumDescription, bool ignorecase = default(bool))
            where T : struct
        {
            Type type = typeof(T);
            var fields = type.GetRuntimeFields();
            Dictionary<string, T> fieldDescriptionMap = new Dictionary<string, T>();
            foreach (var field in fields)
            {
                EnumDescriptionAttribute attribute = field.GetCustomAttributes(typeof(T), false).FirstOrDefault() as EnumDescriptionAttribute;
                string descriptionName = field.Name;
                if (attribute != null)
                {
                    descriptionName = attribute.Description;
                }
                fieldDescriptionMap.Add(descriptionName, field.Name.ParseEnumFromValue<T>(ignorecase));
            }
            return fieldDescriptionMap;
        }

        #endregion

        public static IDictionary<string, string> ParseQueryParameters(this string uri, bool mergeDuplicateKeys = false)
        {
            var uriParts = uri.Split('?');
            var query = uriParts.Length > 1 ? uriParts[1] : null;
            var parameters = new Dictionary<string, string>();
            if (query != null)
            {
                var queryParts = query.Split('&');
                foreach (var queryPart in queryParts)
                {
                    var parts = queryPart.Split('=');
                    var key = parts[0];
                    var value = parts.Length > 1 ? parts[1] : string.Empty;
                    if (parameters.ContainsKey(key))
                    {
                        if (mergeDuplicateKeys && !String.IsNullOrEmpty(value))
                        {
                            parameters[key] += "," + value;
                        }
                    }
                    else
                    {
                        parameters.Add(key, parts.Length > 1 ? parts[1] : string.Empty);
                    }
                }
            }

            return parameters;
        }
    }
}
