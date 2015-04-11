namespace ValueConverters
{
    using System;
    using System.Globalization;
    using System.Windows;
    using System.Windows.Data;
    using Krav;

    [ValueConversion(typeof(bool), typeof(Visibility))]
    public class BooleanToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            RequireThat.ThisHolds(typeof(Visibility) == targetType, "targetType");
            RequireThat.IsOfType<bool>(value, "value");
            
            return (bool)value ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            RequireThat.ThisHolds(typeof(bool) == targetType, "targetType");
            RequireThat.IsOfType<Visibility>(value, "value");

            return (Visibility)value == Visibility.Visible;
        }
    }
}
