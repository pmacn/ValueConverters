namespace ValueConverters
{
    using System;
    using System.Collections.ObjectModel;
    using System.Collections.Specialized;
    using System.Globalization;
    using System.Linq;
    using System.Windows.Data;
    using System.Windows.Markup;
    using Krav;

    [ContentProperty("Converters")]
    public class LinkedConverter : IValueConverter
    {
        public LinkedConverter()
        {
            Converters = new ObservableCollection<IValueConverter>();
            Descriptors = new ObservableCollection<ConverterDescriptor>();
            Converters.CollectionChanged += ConvertersChanged;
        }

        public ObservableCollection<IValueConverter> Converters { get; set; }

        private ObservableCollection<ConverterDescriptor> Descriptors { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            RequireThat.NotNull(targetType, "targetType");

            if (!Descriptors.Any())
            {
                if (!targetType.IsInstanceOfType(value))
                {
                    throw new ArgumentException("Empty LinkedConverter must return 'value' which is of type " + value.GetType().Name + " but targetType is " + targetType.Name);
                }

                return value;
            }

            var expectedTargetType = Descriptors.Last().TargetType;
            RequireThat.ThisHolds(expectedTargetType.IsAssignableFrom(targetType), "targetType");

            return GroupConvert(value, parameter, culture);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            RequireThat.NotNull(targetType, "targetType");

            if (!Descriptors.Any())
            {
                if (!targetType.IsInstanceOfType(value))
                {
                    throw new ArgumentException("Empty LinkedConverter must return 'value' which is of type " + value.GetType().Name + " but targetType is " + targetType.Name);
                }

                return value;
            }
            
            var expectedTargetType = Descriptors.First().SourceType;
            RequireThat.ThisHolds(expectedTargetType.IsAssignableFrom(targetType), "targetType");

            return GroupConvertBack(value, parameter, culture);
        }

        private object GroupConvert(object value, object parameter, CultureInfo culture)
        {
            return Descriptors.Aggregate(value,
                                         (current, descriptor) =>
                                         descriptor.Converter.Convert(current, descriptor.TargetType, parameter, culture));
        }

        private object GroupConvertBack(object value, object parameter, CultureInfo culture)
        {
            return Descriptors.Reverse()
                              .Aggregate(value,
                                         (current, descriptor) =>
                                         descriptor.Converter.ConvertBack(current, descriptor.SourceType, parameter, culture));
        }

        private void ConvertersChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            Descriptors.Clear();
            foreach (var descriptor in Converters.Select(c => new ConverterDescriptor(c)))
            {
                Descriptors.Add(descriptor);
            }

            ValidateConverters();
        }

        private void ValidateConverters()
        {
            for (var i = 0; i < Descriptors.Count - 1; i++)
            {
                var current = Descriptors[i];
                var next = Descriptors[i + 1];
                if (current.TargetType != next.SourceType)
                {
                    throw new ConverterTypesMismatchException(String.Format("SourceType of {0} does not match TargetType of {1}", next.SourceType.Name, current.TargetType.Name));
                }
            }
        }
    }

#if !SILVERLIGHT 
    [Serializable]
#endif
    public class ConverterTypesMismatchException : Exception
    {
        public ConverterTypesMismatchException() { }
        public ConverterTypesMismatchException(string message) : base(message) { }
        public ConverterTypesMismatchException(string message, Exception inner) : base(message, inner) { }
    }
    
    internal class ConverterDescriptor
    {
        public ConverterDescriptor(IValueConverter converter)
        {
            var attribute = (ValueConversionAttribute)converter.GetType().GetCustomAttributes(typeof(ValueConversionAttribute), true).SingleOrDefault();
            if (attribute == null)
                throw new Exception("Converter must have a ValueConverterAttribute to be wrapped in a descriptor");

            TargetType = attribute.TargetType;
            SourceType = attribute.SourceType;
            Converter = converter;
        }

        public IValueConverter Converter { get; private set; }

        public Type TargetType { get; private set; }

        public Type SourceType { get; private set; }
    }
}
