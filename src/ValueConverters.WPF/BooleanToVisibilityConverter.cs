using System;
using System.Windows;
using System.Windows.Data;
using EnsureThat;

namespace ValueConverters
{
    [ValueConversion(typeof(bool), typeof(Visibility))]
    public class BooleanToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            Ensure.That(targetType, "targetType").Is<Visibility>();
            Ensure.ThatTypeFor(value, "value").IsBool();
            
            return (bool)value ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            Ensure.That(targetType, "targetType").Is<bool>();
            Ensure.ThatTypeFor(value, "value").IsOfType(typeof (Visibility));

            return (Visibility)value == Visibility.Visible;
        }
    }
}
