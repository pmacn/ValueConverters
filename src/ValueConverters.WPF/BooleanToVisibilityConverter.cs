using System;
using System.Windows;
using System.Windows.Data;

namespace ValueConverters
{
    [ValueConversion(typeof(bool), typeof(Visibility))]
    public class BooleanToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var expectedTargetType = typeof(Visibility);
            if (targetType != expectedTargetType)
                throw new InvalidOperationException("Expected targetType " + expectedTargetType.Name + " but was " + targetType.Name);

            if (value is bool)
                return (bool)value ? Visibility.Visible : Visibility.Collapsed;

            throw new ArgumentException("value must be a boolean");
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var expectedTargetType = typeof(bool);
            if (targetType != expectedTargetType)
                throw new InvalidOperationException("Expected targetType " + expectedTargetType.Name + " but was " + targetType.Name);

            if (value is Visibility)
                return (Visibility)value == Visibility.Visible;

            throw new ArgumentException("value must be of type Visibility");
        }
    }
}
