using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.Xaml.Data;

namespace Rybird.Framework
{
    public class FrameworkSizeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is FrameworkSize)
            {
                var size = (FrameworkSize)value;
                return new Size(size.Width, size.Height);
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            if (value is Size)
            {
                var size = (Size)value;
                return new FrameworkSize(size.Width, size.Height);
            }
            return value;
        }
    }
}
