using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rybird.Framework
{
    // This class is ugly and results in a static class, but will hopefully go away with Windows Phone 8.1
    // It is necessary because Microsoft has not included DataAnnotations in Windows Phone 8
    // The alternative to the static class is to force every ValidatableBindableBase object
    // to accept a validation strategy in its constructor. That is just too painful.
    public static class ValidatorFacade
    {
        private static IValidationProvider _validationFacade = new AlwaysValidValidationProvider();
        public static void SetValidationFacade(IValidationProvider validationFacade)
        {
            _validationFacade = validationFacade;
        }

        public static IEnumerable<string> ValidateProperty<T>(string propertyName, T value)
        {
            return _validationFacade.ValidateProperty<T>(propertyName, value);
        }

        public static IEnumerable<IGrouping<string, string>> ValidateObject(object obj)
        {
            return _validationFacade.ValidateObject(obj);
        }
    }
}
