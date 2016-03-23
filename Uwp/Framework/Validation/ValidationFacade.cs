using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rybird.Framework
{
    public class ValidationFacade : IValidationProvider
    {
        public IEnumerable<string> ValidateProperty<T>(string propertyName, T value)
        {
            ICollection<ValidationResult> validationResults = new List<ValidationResult>();
            ValidationContext validationContext = CreateValidationContext<T>(propertyName);

            if (!Validator.TryValidateProperty(value, validationContext, validationResults))
            {
                return validationResults.Select(x => x.ErrorMessage);
            }
            return new List<string>();
        }

        private ValidationContext CreateValidationContext<T>(string propertyName)
        {
            var validationContext = new ValidationContext(this)
            {
                MemberName = propertyName
            };
            return validationContext;
        }

        public IEnumerable<IGrouping<string, string>> ValidateObject(object obj)
        {
            ICollection<ValidationResult> validationResults = new List<ValidationResult>();
            bool result = Validator.TryValidateObject(this, new ValidationContext(this), validationResults);
            if (result)
            {
                return validationResults
                    .SelectMany(results => results.MemberNames
                        .Select(member => new KeyValuePair<string, string>(member, results.ErrorMessage)))
                    .GroupBy(x => x.Key, pair => pair.Value);
            }
            else
            {
                return new List<IGrouping<string, string>>();
            }
        }
    }
}
