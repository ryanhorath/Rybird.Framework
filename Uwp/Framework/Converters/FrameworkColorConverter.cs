using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml.Data;

namespace Rybird.Framework
{
    public class FrameworkColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is FrameworkColor)
            {
                var color = (FrameworkColor)value;
                return Color.FromArgb(color.A, color.R, color.G, color.B);
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            if (value is Color)
            {
                var color = (Color)value;
                return FrameworkColor.FromArgb(color.A, color.R, color.G, color.B);
            }
            return value;
        }
    }
}
