using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace Rybird.Framework
{
    public class EnumViewModel<TEnum> : ViewModelBase where TEnum : struct
    {
        public EnumViewModel()
        {
            if (!(typeof(TEnum).GetTypeInfo().IsEnum))
            {
                throw new InvalidOperationException("Type provided must be an enum type.");
            }
        }

        public IEnumerable<string> AllDescriptions
        {
            get
            {
                return Enum.GetValues(typeof(TEnum))
                    .Cast<TEnum>()
                    .Select(e => (e as Enum).ToDescription());
            }
        }

        public string SelectedDescription
        {
            get { return (SelectedValue as Enum).ToDescription(); }
            set { SelectedValue = EnumExtensions.FromDescription<TEnum>(value); }
        }

        private TEnum _selectedValue;
        public TEnum SelectedValue
        {
            get { return _selectedValue; }
            set
            {
                SetProperty<TEnum>(ref _selectedValue, value);
                RaisePropertyChangedEvent("SelectedDescription");
            }
        }
    }
}
