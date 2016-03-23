using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace Rybird.Framework
{
    public class MinutesConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            // No format provided.
            if (parameter == null)
            {
                return value;
            }
            else
            {
                var format = parameter as string;
                var minutes = (int)value;
                return minutes.ToString(format);
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return value;
        }
    }
}
