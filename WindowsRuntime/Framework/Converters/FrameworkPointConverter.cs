using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.Xaml.Data;

namespace Rybird.Framework
{
    public class FrameworkPointConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is FrameworkPoint)
            {
                var point = (FrameworkPoint)value;
                return new Point(point.X, point.Y);
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            if (value is Point)
            {
                var point = (Point)value;
                return new FrameworkPoint(point.X, point.Y);
            }
            return value;
        }
    }
}
