using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Markup;

namespace Rybird.Framework
{
    public class ConverterGroup : DependencyObject, IValueConverter
    {
        public static readonly DependencyProperty ConvertersProperty = DependencyProperty.Register(
            "Converters",
            typeof(Collection<IValueConverter>),
            typeof(ConverterGroup),
            new PropertyMetadata(null));

        public Collection<IValueConverter> Converters
        {
            get
            {
                return GetValue(ConvertersProperty) as Collection<IValueConverter>;
            }

            private set
            {
                SetValue(ConvertersProperty, value);
            }
        }

        public ConverterGroup()
        {
            this.Converters = new Collection<IValueConverter>();
        }

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (this.Converters.Count == 0)
            {
                return DependencyProperty.UnsetValue;
            }

            var convertedValue = value;

            foreach (var valueConverter in this.Converters)
            {
                convertedValue = valueConverter.Convert(convertedValue, targetType, parameter, language);
            }

            return convertedValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            if (this.Converters.Count == 0)
            {
                return DependencyProperty.UnsetValue;
            }

            var convertedValue = value;

            for (var i = this.Converters.Count - 1; i >= 0; --i)
            {
                convertedValue = this.Converters[i].ConvertBack(convertedValue, targetType, parameter, language);
            }

            return convertedValue;
        }
    }
}
