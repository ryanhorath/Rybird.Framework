using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rybird.Framework
{
    public class ValidationException : Exception
    {
        public ValidationException(string propertyName, IReadOnlyList<string> validationErrors)
        {
            PropertyName = propertyName;
            ValidationErrors = validationErrors;
        }

        public string PropertyName { get; private set; }
        public IReadOnlyList<string> ValidationErrors { get; private set; }
    }
}
