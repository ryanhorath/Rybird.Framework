using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rybird.Framework
{
    public class ReadOnlyDictionaryWrapper<TKey, TValue> : IReadOnlyDictionary<TKey, TValue>
    {
        private IDictionary<TKey, TValue> _dictionary;

        public ReadOnlyDictionaryWrapper(IDictionary<TKey, TValue> dictionary)
        {
            _dictionary = dictionary;
        }

        public bool ContainsKey(TKey key) { return _dictionary.ContainsKey(key); }

        public IEnumerable<TKey> Keys { get { return _dictionary.Keys; } }

        public bool TryGetValue(TKey key, out TValue value)
        {
            TValue v;
            var result = _dictionary.TryGetValue(key, out v);
            value = v;
            return result;
        }

        public IEnumerable<TValue> Values { get { return _dictionary.Values; } }

        public TValue this[TKey key] { get { return _dictionary[key]; } }

        public int Count { get { return _dictionary.Count; } }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return _dictionary
                .Select(x => new KeyValuePair<TKey, TValue>(x.Key, x.Value))
                .GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
