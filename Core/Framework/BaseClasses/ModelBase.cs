using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using System.ComponentModel;
using System.Linq;
using System.Collections;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace Rybird.Framework
{
    public abstract class ModelBase : BindableBase
    {
        protected override bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            var result = false;
            if (!EqualityComparer<T>.Default.Equals(storage, value))
            {
                var errors = GetValidationResultsForProperty<T>(propertyName, value);
                if (errors != null && errors.Any())
                {
                    throw new ValidationException(propertyName, new List<string>(errors));
                }
                storage = value;
                RaisePropertyChangedEvent(propertyName);
                result = true;
            }
            return result;
        }

        private IEnumerable<string> GetValidationResultsForProperty<T>(string propertyName, T value)
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
    }
}
