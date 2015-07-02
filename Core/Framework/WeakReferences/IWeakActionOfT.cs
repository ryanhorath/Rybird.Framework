using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rybird.Framework
{
    public interface IWeakAction<T>
    {
        void Execute(T obj);
    }
}
