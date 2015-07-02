using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rybird.Framework
{
    public class AlwaysValidValidationProvider : IValidationProvider
    {
        public IEnumerable<string> ValidateProperty<T>(string propertyName, T value)
        {
            return new List<string>();
        }

        public IEnumerable<IGrouping<string, string>> ValidateObject(object obj)
        {
            return new List<IGrouping<string, string>>();
        }
    }
}
