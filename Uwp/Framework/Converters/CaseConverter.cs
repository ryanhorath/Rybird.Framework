using System;
using System.Diagnostics;
using System.Globalization;
using System.Windows;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace Rybird.Framework
{
    public class CaseConverter : DependencyObject, IValueConverter
    {
        public static readonly DependencyProperty CasingProperty = DependencyProperty.Register(
            "Casing",
            typeof(CharacterCasing),
            typeof(CaseConverter),
            new PropertyMetadata(CharacterCasing.Normal)
            );

        public CharacterCasing Casing
        {
            get { return (CharacterCasing)GetValue(CasingProperty); }
            set { SetValue(CasingProperty, value); }
        }

        public CaseConverter()
        {
        }

        public CaseConverter(CharacterCasing casing)
        {
            this.Casing = casing;
        }

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var str = value as string;

            if (str != null)
            {
                switch (this.Casing)
                {
                    case CharacterCasing.Lower:
                        return str.ToLower();
                    case CharacterCasing.Upper:
                        return str.ToUpper();
                    case CharacterCasing.Normal:
                        return str;
                }
            }

            return DependencyProperty.UnsetValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return DependencyProperty.UnsetValue;
        }

        private static bool ValidateCasing(object value)
        {
            Debug.Assert(value is CharacterCasing);
            return true;
        }
    }
}
