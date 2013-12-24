using System;
using System.Windows;
using System.Windows.Data;
using RequireThat;

namespace ValueConverters
{
    [ValueConversion(typeof(bool), typeof(Visibility))]
    public class BooleanToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            Require.That(targetType, "targetType").Is<Visibility>();
            Require.That(value, "value").IsOfType<bool>();
            
            return (bool)value ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            Require.That(targetType, "targetType").Is<bool>();
            Require.That(value, "value").IsOfType<Visibility>();

            return (Visibility)value == Visibility.Visible;
        }
    }
}
