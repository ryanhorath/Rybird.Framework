using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rybird.Framework
{
    public interface IStateBucket : IReadOnlyStateBucket
    {
        void Add<T>(string key, T value);
        new object this[string key] { get;  set; }
    }
}
