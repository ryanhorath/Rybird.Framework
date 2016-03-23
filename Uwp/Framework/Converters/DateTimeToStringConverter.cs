using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace Rybird.Framework
{
    public sealed class DateTimeToStringConverter : IValueConverter
    {
        public string Format { get; set; }
        public bool IsDateTimeNullable { get; set; }

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var result = string.Empty;
            if (value is DateTime)
            {
                result = ((DateTime)value).ToString(this.Format ?? "d");
            }

            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            DateTime date;
            if (value != null && DateTime.TryParse(value.ToString(), out date))
            {
                return date;
            }
            else
            {
                if (this.IsDateTimeNullable)
                {
                    return null;
                }
                else
                {
                    return new DateTime();
                }
            }
        }
    }
}
