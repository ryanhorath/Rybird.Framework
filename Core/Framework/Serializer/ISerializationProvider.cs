using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rybird.Framework
{
    public interface ISerializationProvider
    {
        object Deserialize(Type type, string data);
        T Deserialize<T>(string data);
        string Serialize(object instance);
    }
}
