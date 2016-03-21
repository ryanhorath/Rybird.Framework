using System;
using System.Diagnostics;
using System.Globalization;
using System.Windows;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace Rybird.Framework
{
    public class DateTimeConverter : DependencyObject, IValueConverter
    {
        public static readonly DependencyProperty SourceKindProperty = DependencyProperty.Register(
            "SourceKind",
            typeof(DateTimeKind),
            typeof(DateTimeConverter),
            new PropertyMetadata(DateTimeKind.Unspecified)
            );

        public static readonly DependencyProperty TargetKindProperty = DependencyProperty.Register(
            "TargetKind",
            typeof(DateTimeKind),
            typeof(DateTimeConverter),
            new PropertyMetadata(DateTimeKind.Unspecified)
            );

        public static readonly DependencyProperty ConversionModeProperty = DependencyProperty.Register(
            "ConversionMode",
            typeof(DateTimeConversionMode),
            typeof(DateTimeConverter),
            new PropertyMetadata(DateTimeConversionMode.DoConversion)
            );

        public static readonly DependencyProperty SourceAdjustmentProperty = DependencyProperty.Register(
            "SourceAdjustment",
            typeof(TimeSpan),
            typeof(DateTimeConverter),
            new PropertyMetadata(TimeSpan.Zero));

        public static readonly DependencyProperty TargetAdjustmentProperty = DependencyProperty.Register(
            "TargetAdjustment",
            typeof(TimeSpan),
            typeof(DateTimeConverter),
            new PropertyMetadata(TimeSpan.Zero));

        public DateTimeKind SourceKind
        {
            get { return (DateTimeKind)GetValue(SourceKindProperty); }
            set { SetValue(SourceKindProperty, value); }
        }

        public DateTimeKind TargetKind
        {
            get { return (DateTimeKind)GetValue(TargetKindProperty); }
            set { SetValue(TargetKindProperty, value); }
        }

        public DateTimeConversionMode ConversionMode
        {
            get { return (DateTimeConversionMode)GetValue(ConversionModeProperty); }
            set { SetValue(ConversionModeProperty, value); }
        }

        public TimeSpan SourceAdjustment
        {
            get { return (TimeSpan)GetValue(SourceAdjustmentProperty); }
            set { SetValue(SourceAdjustmentProperty, value); }
        }

        public TimeSpan TargetAdjustment
        {
            get { return (TimeSpan)GetValue(TargetAdjustmentProperty); }
            set { SetValue(TargetAdjustmentProperty, value); }
        }

        public DateTimeConverter()
        {
        }

        public DateTimeConverter(DateTimeKind sourceKind, DateTimeKind targetKind)
        {
            this.SourceKind = sourceKind;
            this.TargetKind = targetKind;
        }

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is DateTime)
            {
                return DoConversion(this.ConversionMode, (DateTime)value, this.TargetKind, this.TargetAdjustment);
            }

            return DependencyProperty.UnsetValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            if (value is DateTime)
            {
                return DoConversion(this.ConversionMode, (DateTime)value, this.SourceKind, this.SourceAdjustment);
            }

            return DependencyProperty.UnsetValue;
        }

        private static DateTime DoConversion(DateTimeConversionMode conversionMode, DateTime dateTime, DateTimeKind convertTo, TimeSpan adjustment)
        {
            if (adjustment != TimeSpan.Zero)
            {
                dateTime = dateTime.Add(adjustment);
            }

            if (conversionMode == DateTimeConversionMode.DoConversion)
            {
                switch (convertTo)
                {
                    case DateTimeKind.Local:
                        return dateTime.ToLocalTime();
                    case DateTimeKind.Utc:
                        return dateTime.ToUniversalTime();
                    default:
                        return dateTime;
                }
            }
            else
            {
                return DateTime.SpecifyKind(dateTime, convertTo);
            }
        }
    }
}