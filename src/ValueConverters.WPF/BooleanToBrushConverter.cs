namespace ValueConverters
{
    using System;
    using System.Globalization;
    using System.Windows.Data;
    using System.Windows.Media;
    using Krav;

    [ValueConversion(typeof(bool), typeof(Brush))]
    public class BooleanToBrushConverter : IValueConverter
    {
        public Brush TrueBrush { get; set; }

        public Brush FalseBrush { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            RequireThat.ThisHolds(typeof(Brush).IsAssignableFrom(targetType), "targetType");
            RequireThat.IsOfType<bool>(value, "value");

            return (bool)value ? TrueBrush : FalseBrush;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Feels like it doesn't really make sense to have a convert back for this
 	        throw new NotImplementedException();
        }
    }
}
