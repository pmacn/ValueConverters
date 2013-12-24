using RequireThat;
using ReflectionExtensions;
using System;
using System.Windows.Data;
using System.Windows.Media;

namespace ValueConverters
{
    [ValueConversion(typeof(bool), typeof(Brush))]
    public class BooleanToBrushConverter : IValueConverter
    {
        public Brush TrueBrush { get; set; }

        public Brush FalseBrush { get; set; }

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            Require.That(targetType, "targetType").Is<Brush>();
            Require.That(value, "value").IsOfType<bool>();

            return (bool)value ? TrueBrush : FalseBrush;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            // Feels like it doesn't really make sense to have a convert back for this
 	        throw new NotImplementedException();
        }
    }
}
