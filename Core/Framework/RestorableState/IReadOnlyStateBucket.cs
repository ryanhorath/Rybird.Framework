using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rybird.Framework
{
    public interface IReadOnlyStateBucket
    {
        bool ContainsKey(string key);
        T GetValue<T>(string key);
        object this[string key] { get; }
    }
}
