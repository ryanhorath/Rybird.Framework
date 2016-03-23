using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace Rybird.Framework
{
    public class VisibleIfTrueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is bool)
            {
                Visibility notVisible = Visibility.Collapsed;
                if (parameter != null && parameter is Visibility)
                {
                    notVisible = (Visibility)parameter;
                }
                if ((bool)value)
                {
                    return Visibility.Visible;
                }
                return notVisible;
            }
            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            if (value is Visibility)
            {
                Visibility visibility = (Visibility)value;
                if (visibility == Visibility.Visible)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }
    }
}
