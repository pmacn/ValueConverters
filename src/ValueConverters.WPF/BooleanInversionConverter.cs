namespace ValueConverters
{
    using System;
    using System.Globalization;
    using System.Windows.Data;
    using Krav;

    [ValueConversion(typeof(bool), typeof(bool))]
    public class BooleanInversionConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            RequireThat.ThisHolds(typeof(bool) == targetType, "targetType");
            RequireThat.IsOfType<bool>(value, "value");

            return !(bool)value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            RequireThat.ThisHolds(typeof(bool) == targetType, "targetType");
            RequireThat.IsOfType<bool>(value, "value");

            return !(bool)value;
        }
    }
}
