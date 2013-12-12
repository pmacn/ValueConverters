using System;
using System.Windows.Data;
using EnsureThat;

namespace ValueConverters
{
    [ValueConversion(typeof(bool), typeof(bool))]
    public class BooleanInversionConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            Ensure.That(targetType, "targetType").Is<bool>();
            Ensure.ThatTypeFor(value).IsBool();

            return !(bool)value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            Ensure.That(targetType, "targetType").Is<bool>();
            Ensure.ThatTypeFor(value).IsBool();

            return !(bool)value;
        }
    }
}
