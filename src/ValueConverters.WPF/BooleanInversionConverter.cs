using System;
using System.Windows.Data;

namespace ValueConverters.WPF
{
    [ValueConversion(typeof(bool), typeof(bool))]
    //[ValueConverter(typeof(bool), typeof(bool))]
    public class BooleanInversionConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (!targetType.Equals(typeof(bool)))
                throw new InvalidOperationException();

            return !(bool)value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return !(bool)value;
        }
    }
}
