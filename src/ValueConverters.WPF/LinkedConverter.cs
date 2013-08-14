using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Globalization;
using System.Linq;
using System.Windows.Data;
using System.Windows.Markup;

namespace ValueConverters.WPF
{
    [ContentProperty("Converters")]
    public class LinkedConverter : IValueConverter
    {
        public LinkedConverter()
        {
            Converters.CollectionChanged += ConvertersChanged;
            Descriptors = new ObservableCollection<ConverterDescriptor>();
        }

        private ObservableCollection<IValueConverter> _converters = new ObservableCollection<IValueConverter>();
        public ObservableCollection<IValueConverter> Converters { get { return _converters; } set { _converters = value; } }

        private ObservableCollection<ConverterDescriptor> Descriptors { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!Descriptors.Any())
                return value;

            var expectedTargetType = Descriptors.Last().TargetType;
            if (!targetType.Equals(expectedTargetType))
                throw new ArgumentException(String.Format("TargetType is {0}, expected {1}", targetType.Name, expectedTargetType.Name));

            return GroupConvert(value, targetType, parameter, culture);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(!Descriptors.Any())
                return value;
            
            var expectedTargetType = Descriptors.First().SourceType;
            if (!targetType.Equals(expectedTargetType))
                throw new ArgumentException(String.Format("TargetType is {0}, expected {1}", targetType.Name, expectedTargetType.Name));

            return GroupConvertBack(value, targetType, parameter, culture);
        }

        private object GroupConvert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            object intermediateValue = value;
            foreach (var descriptor in Descriptors)
            {
                intermediateValue = descriptor.Converter.Convert(intermediateValue, descriptor.TargetType, parameter, culture);
            }

            return intermediateValue;
        }

        private object GroupConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // TODO : Need to implement this
            throw new NotImplementedException();
        }

        private void ConvertersChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            Descriptors.Clear();
            foreach (var converter in Converters)
                Descriptors.Add(new ConverterDescriptor(converter));

            ValidateConverters();
        }

        private void ValidateConverters()
        {
            for (var i = 0; i < Descriptors.Count - 1; i++)
            {
                var current = Descriptors[i];
                var next = Descriptors[i + 1];
                if (!current.TargetType.Equals(next.SourceType))
                {
                    throw new ConverterTypesMismatchException(String.Format("SourceType of {0} does not match TargetType of {1}", next.SourceType.Name, current.TargetType.Name));
                }
            }
        }
    }

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
            var attributes = converter.GetType().GetCustomAttributes(typeof(ValueConverterAttribute), false);
            if (!attributes.Any())
                throw new Exception("Converter must have a ValueConverterAttribute to be wrapped in a descriptor");

            var attribute = attributes.Single() as ValueConverterAttribute;
            TargetType = attribute.TargetType;
            SourceType = attribute.SourceType;
            Converter = converter;
        }

        public IValueConverter Converter { get; private set; }

        public Type TargetType { get; set; }

        public Type SourceType { get; set; }
    }
}
