using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;
using System.Collections.ObjectModel;

namespace Rybird.Framework
{
    public class SharedPreferencesFacade : IDictionary<string, object>
    {
        private ISharedPreferences _sharedPreferences;
        private ISharedPreferencesEditor _editor;

        public SharedPreferencesFacade(ISharedPreferences sharedPreferences)
        {
            _sharedPreferences = sharedPreferences;
        }

        public void Edit()
        {
            _editor = _sharedPreferences.Edit();
        }

        public void Commit()
        {
            _editor.Commit();
            _editor = null;
        }

        public void Add(string key, object value)
        {
            _editor.PutString(key, JsonConvert.SerializeObject(value));
        }

        public bool ContainsKey(string key)
        {
            return _sharedPreferences.Contains(key);
        }

        public ICollection<string> Keys
        {
            get { return _sharedPreferences.All.Select(p => p.Key).ToList(); }
        }

        public bool Remove(string key)
        {
            bool contains = false;
            if (ContainsKey(key))
            {
                contains = true;
                _editor.Remove(key);
            }
            return contains;
        }

        public bool TryGetValue(string key, out object value)
        {
            if (key == null)
            {
                throw new ArgumentNullException("key");
            }
            if (ContainsKey(key))
            {
                value = this[key];
                return true;
            }
            value = null;
            return false;
        }

        public ICollection<object> Values
        {
            get
            {
                Collection<object> values = new Collection<object>();
                foreach (var key in Keys)
                {
                    values.Add(this[key]);
                }
                return values;
            }
        }

        public object this[string key]
        {
            get
            {
                return JsonConvert.DeserializeObject(_sharedPreferences.GetString(key, null));
            }
            set
            {
                _editor.PutString(key, JsonConvert.SerializeObject(value));
            }
        }

        public void Add(KeyValuePair<string, object> item)
        {
            Add(item.Key, item.Value);
        }

        public void Clear()
        {
            _editor.Clear();
        }

        public bool Contains(KeyValuePair<string, object> item)
        {
            object value = null;
            if (TryGetValue(item.Key, out value))
            {
                return value.Equals(item.Value);
            }
            return false;
        }

        public void CopyTo(KeyValuePair<string, object>[] array, int arrayIndex)
        {
            if (array == null)
            {
                throw new ArgumentNullException("array");
            }
            if (arrayIndex < 0)
            {
                throw new ArgumentOutOfRangeException("arrayIndex");
            }
            if (arrayIndex + Count > array.Length)
            {
                throw new ArgumentException("arrayIndex");
            }
            int currentIndex = arrayIndex;
            foreach (var key in Keys)
            {
                array[currentIndex] = new KeyValuePair<string, object>(key, this[key]);
            }
        }

        public int Count
        {
            get { return _sharedPreferences.All.Count; }
        }

        public bool IsReadOnly
        {
            get { return (_editor == null); }
        }

        public bool Remove(KeyValuePair<string, object> item)
        {
            return Remove(item.Key);
        }

        public IEnumerator<KeyValuePair<string, object>> GetEnumerator()
        {
            List<KeyValuePair<string, object>> pairs = new List<KeyValuePair<string, object>>();
            foreach (var key in Keys)
            {
                pairs.Add(new KeyValuePair<string, object>(key, this[key]));
            }
            return pairs.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}