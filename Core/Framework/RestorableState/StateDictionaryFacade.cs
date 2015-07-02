using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rybird.Framework
{
    public class StateDictionaryFacade : IStateBucket
    {
        private readonly IDictionary<string, object> _dictionary;

        public StateDictionaryFacade(IDictionary<string, object> dictionary)
        {
            dictionary.ThrowIfNull("dictionary");
            _dictionary = dictionary;
        }

        public void Add<T>(string key, T value)
        {
            _dictionary.Add(key, value);
        }

        public bool ContainsKey(string key)
        {
            return _dictionary.ContainsKey(key);
        }

        public T GetValue<T>(string key)
        {
            return (T)GetValue(key);
        }

        private object GetValue(string key)
        {
            if (ContainsKey(key))
            {
                return _dictionary[key];
            }
            throw new Exception("Key not found");
        }

        public object this[string key]
        {
            get { return _dictionary[key]; }
            set { _dictionary[key] = value; }
        }
    }
}
