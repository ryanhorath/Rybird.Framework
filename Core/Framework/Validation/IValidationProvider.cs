using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rybird.Framework
{
    public interface IValidationProvider
    {
        IEnumerable<string> ValidateProperty<T>(string propertyName, T value);
        IEnumerable<IGrouping<string, string>> ValidateObject(object obj);
    }
}
