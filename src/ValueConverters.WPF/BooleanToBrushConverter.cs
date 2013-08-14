using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;
using ReflectionExtensions;

namespace ValueConverters.WPF
{
    [ValueConversion(typeof(bool), typeof(Brush))]
    public class BooleanToBrushConverter : IValueConverter
    {
        public Brush TrueBrush { get; set; }

        public Brush FalseBrush { get; set; }

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (!targetType.Implements<Brush>(ImplementationFlags.Concrete))
                throw new ArgumentException("Expected targetType Brush but was " + targetType.Name);

            if (value is bool)
                return (bool)value ? TrueBrush : FalseBrush;

            throw new ArgumentException("Expected value of type bool but was " + value.GetType().Name);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
 	        throw new NotImplementedException();
        }
    }
}
