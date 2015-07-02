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
    public abstract class ViewModelBase : BindableBase, INotifyDataErrorInfo
    {
        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;
        private IDictionary<string, IList<string>> _errors = new Dictionary<string, IList<string>>();

        [OnDeserializing]
        private void Initialize()
        {
            _errors = new Dictionary<string, IList<string>>();
        }

        protected override bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            var result = false;
            if (!EqualityComparer<T>.Default.Equals(storage, value))
            {
                storage = value;
                RaisePropertyChangedEvent(propertyName);
                ValidateProperty<T>(propertyName, value);
                result = true;
            }
            return result;
        }

        protected bool ValidateProperty<T>(string propertyName, T value)
        {
            var errors = GetValidationResultsForProperty<T>(propertyName, value);
            if (errors != null && errors.Any())
            {
                SetErrors(propertyName, errors);
                return false;
            }
            else
            {
                ClearErrors(propertyName);
                return true;
            }
        }

        public IEnumerable GetErrors(string propertyName)
        {
            IList<string> list;
            return _errors.TryGetValue(propertyName, out list) ? list : Enumerable.Empty<string>();
        }

        public bool HasErrors
        {
            get { return _errors.Any(); }
        }

        private void ClearErrors(string propertyName)
        {
            var hasErrors = HasErrors;
            _errors.Remove(propertyName);
            if (hasErrors)
            {
                RaisePropertyChangedEvent("HasErrors");
            }
            RaiseErrorsChanged(propertyName);
        }

        private void SetErrors(string propertyName, IEnumerable<string> propertyErrors)
        {
            var hasErrors = HasErrors;
            _errors[propertyName] = propertyErrors.ToList();
            if (!hasErrors)
            {
                RaisePropertyChangedEvent("HasErrors");
            }
            RaiseErrorsChanged(propertyName);
        }

        private void RaiseErrorsChanged(string propertyName)
        {
            EventHandler<DataErrorsChangedEventArgs> handler = ErrorsChanged;
            if (handler != null)
            {
                handler(this, new DataErrorsChangedEventArgs(propertyName));
            }
        }

        protected void ValidateObject()
        {
            var results = GetValidationResultsForObject(this);
            if (results != null && results.Any())
            {
                foreach (var memberResults in results)
                {
                    SetErrors(memberResults.Key, memberResults);
                }
            }
            else
            {
                ClearErrors(string.Empty);
            }
        }

        private IEnumerable<IGrouping<string, string>> GetValidationResultsForObject(object obj)
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
