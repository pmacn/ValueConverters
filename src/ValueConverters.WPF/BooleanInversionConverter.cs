using System;
using System.Windows.Data;
using RequireThat;

namespace ValueConverters
{
    [ValueConversion(typeof(bool), typeof(bool))]
    public class BooleanInversionConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            Require.That(targetType, "targetType").Is<bool>();
            Require.That(value, "value").IsOfType<bool>();

            return !(bool)value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            Require.That(targetType, "targetType").Is<bool>();
            Require.That(value, "value").IsOfType<bool>();

            return !(bool)value;
        }
    }
}
