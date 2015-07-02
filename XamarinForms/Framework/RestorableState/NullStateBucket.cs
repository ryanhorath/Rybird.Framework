using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rybird.Framework
{
    public class NullStateBucket : IStateBucket
    {
        public void Add<T>(string key, T value)
        {
            
        }

        public object this[string key]
        {
            get
            {
                return null;
            }
            set
            {
                
            }
        }

        public bool ContainsKey(string key)
        {
            return false;
        }

        public T GetValue<T>(string key)
        {
            return default(T);
        }
    }
}
