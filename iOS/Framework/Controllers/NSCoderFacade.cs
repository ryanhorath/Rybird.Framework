using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;
using Newtonsoft.Json;
using System.Collections.ObjectModel;

namespace Rybird.Framework
{
    public class NSCoderFacade : IStateBucket
    {
        private readonly NSCoder _nsCoder;

        public NSCoderFacade(NSCoder nsCoder)
        {
            nsCoder.ThrowIfNull("nsCoder");
            _nsCoder = nsCoder;
        }

        public void Add<T>(string key, T value)
        {
            var obj = JsonConvert.SerializeObject(value);
            var nsString = new NSString(obj);
            _nsCoder.Encode(nsString, key);
        }

        public bool ContainsKey(string key)
        {
            return _nsCoder.ContainsKey(key);
        }

        public T GetValue<T>(string key)
        {
            return (T)GetValue(key);
        }

        private object GetValue(string key)
        {
            if (_nsCoder.ContainsKey(key))
            {
                var obj = _nsCoder.DecodeObject(key);
                var objString = ((NSString)obj).ToString();
                return JsonConvert.DeserializeObject(objString);
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