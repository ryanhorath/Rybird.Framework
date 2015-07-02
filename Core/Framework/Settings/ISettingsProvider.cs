using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rybird.Framework
{
    public interface ISettingsProvider
    {
        void AddOrUpdate(string key, object value);
        bool TryGetValue<T>(string key, out T value);
        bool Remove(string key);
        bool ContainsKey(string key);
    }
}
