using System;
using System.Windows.Data;

namespace ValueConverters
{
    [ValueConversion(typeof(bool), typeof(bool))]
    public class BooleanInversionConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (targetType != typeof(bool))
                throw new ArgumentException("Expected targetType to be bool but was " + targetType.Name);

            return !(bool)value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if(targetType != typeof(bool))
                throw new ArgumentException("Expected targetType to be bool but was " + targetType.Name);

            return !(bool)value;
        }
    }
}
