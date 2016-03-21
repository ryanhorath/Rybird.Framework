using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace Rybird.Framework
{
    public class EnumValueToDescriptionConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is Enum == false)
            {
                throw new ArgumentException("'value' must be an enum.");
            }
            return ((Enum)value).ToDescription();
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotSupportedException("ConvertBack not supported.");
        }
    }
}
