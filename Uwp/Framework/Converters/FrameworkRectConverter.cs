using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.Xaml.Data;

namespace Rybird.Framework
{
    public class FrameworkRectConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is FrameworkRect)
            {
                var rect = (FrameworkRect)value;
                return new Rect(rect.X, rect.Y, rect.Width, rect.Height);
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            if (value is Rect)
            {
                var rect = (Rect)value;
                return new FrameworkRect(rect.X, rect.Y, rect.Width, rect.Height);
            }
            return value;
        }
    }
}
