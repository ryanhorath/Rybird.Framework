using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace Rybird.Framework
{
    public class WindowLayoutTypeVisibleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value != null)
            {
                var paramValue = parameter != null
                    ? (WindowLayoutType)Enum.Parse(typeof(WindowLayoutType), parameter.ToString())
                    : WindowLayoutType.Normal;
                var enumValue = (WindowLayoutType)value;
                return enumValue == paramValue
                    ? Visibility.Visible
                    : Visibility.Collapsed;
            }
            return Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotSupportedException("ConvertBack not supported.");
        }
    }
}
