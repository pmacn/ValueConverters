using System;
using System.Windows;
using System.Windows.Data;

namespace ValueConverters.WPF
{
    [ValueConverter(typeof(bool), typeof(Visibility))]
    public class BooleanToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (!targetType.Equals(typeof(Visibility)))
                throw new InvalidOperationException("Target type has to be Visibility");

            if (!(value is bool))
                throw new ArgumentException("value must be a boolean");

            return (bool)value ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (!targetType.Equals(typeof(bool)))
                throw new InvalidOperationException("Target type has to be bool");

            if (!(value is Visibility))
                throw new ArgumentException("value must be of type Visibility");

            return (Visibility)value == Visibility.Visible;
        }
    }
}
