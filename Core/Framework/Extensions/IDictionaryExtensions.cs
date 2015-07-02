using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace System.Collections.Generic
{
    public static class IDictionaryExtensions
    {
        public static IDictionary<TKey, TValue> Sort<TKey, TValue>(this IDictionary<TKey, TValue> dictionary)
        {
            if (dictionary == null)
            {
                throw new ArgumentNullException("dictionary");
            }
            return new SortedDictionary<TKey, TValue>(dictionary);
        }

        public static IDictionary<TKey, TValue> Sort<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, IComparer<TKey> comparer)
        {
            if (dictionary == null)
            {
                throw new ArgumentNullException("dictionary");
            }
            if (comparer == null)
            {
                throw new ArgumentNullException("comparer");
            }
            return new SortedDictionary<TKey, TValue>(dictionary, comparer);
        }

        public static IDictionary<TKey, TValue> SortByValue<TKey, TValue>(this IDictionary<TKey, TValue> dictionary)
        {
            return (new SortedDictionary<TKey, TValue>(dictionary)).OrderBy(kvp => kvp.Value).ToDictionary(item => item.Key, item => item.Value);
        }

        public static IDictionary<TValue, TKey> Invert<TKey, TValue>(this IDictionary<TKey, TValue> dictionary)
        {
            if (dictionary == null)
            {
                throw new ArgumentNullException("dictionary");
            }
            return dictionary.ToDictionary(pair => pair.Value, pair => pair.Key);
        }

        public static TValue FirstValue<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TValue defaultValue, params TKey[] keys)
        {
            foreach (var key in keys)
            {
                if (dictionary.ContainsKey(key))
                {
                    return dictionary[key];
                }
            }
            return defaultValue;
        }

        public static TValue GetValueOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> source, TKey key, TValue defaultValue = default(TValue))
        {
            TValue value;
            return source.TryGetValue(key, out value) ? value : defaultValue;
        }

        public static TValue GetValueOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> source, TKey key, Func<TValue> defaultValue)
        {
            TValue value;
            if (source.TryGetValue(key, out value))
            {
                return value;
            }
            return defaultValue();
        }

        public static TValue GetValueOrAddDefault<TKey, TValue>(this IDictionary<TKey, TValue> source, TKey key, Func<TValue> defaultValue)
        {
            TValue value;
            if (source.TryGetValue(key, out value))
            {
                return value;
            }
            else
            {
                value = defaultValue();
                source.Add(key, value);
                return value;
            }
        }

        public static TValue GetValueOrThrow<TKey, TValue>(this IDictionary<TKey, TValue> source, TKey key, Exception exception)
        {
            TValue value;
            if (source.TryGetValue(key, out value))
            {
                return value;
            }
            throw exception;
        }
    }
}
