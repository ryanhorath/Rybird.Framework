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
    public class BundleFacade : IStateBucket
    {
        private readonly Bundle _bundle;

        public BundleFacade(Bundle bundle)
        {
            bundle.ThrowIfNull("bundle");
            _bundle = bundle;
        }

        public void Add<T>(string key, T value)
        {
            _bundle.PutString(key, JsonConvert.SerializeObject(value));
        }

        public bool ContainsKey(string key)
        {
            return _bundle.ContainsKey(key);
        }

        public T GetValue<T>(string key)
        {
            return (T)GetValue(key);
        }

        private object GetValue(string key)
        {
            if (_bundle.ContainsKey(key))
            {
                var obj = _bundle.Get(key);
                return JsonConvert.DeserializeObject(obj.ToString());
            }
            throw new Exception("Key not found");
        }

        public object this[string key]
        {
            get
            {
                return GetValue(key);
            }
            set
            {
                Add(key, value);
            }
        }
    }
}