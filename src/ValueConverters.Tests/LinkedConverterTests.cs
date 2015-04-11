using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using Xunit;

namespace ValueConverters.Tests
{
    public class WithEmptyLinkedConverter
    {
        IValueConverter _converter = new LinkedConverter();
        object value = "value";
        
        [Fact]
        public void ConvertReturnsOriginalValue()
        {
            var convertedValue = _converter.Convert(value, value.GetType(), null, CultureInfo.CurrentCulture);
            Assert.Equal(value, convertedValue);
        }

        [Fact]
        public void ConvertWithValueNotOfTargetTypeThrowsException()
        {
            Assert.Throws<ArgumentException>(
                () => _converter.Convert(value, typeof(bool), null, CultureInfo.CurrentCulture));
        }

        [Fact]
        public void ConvertBackReturnsOriginalValue()
        {
            var convertedValue = _converter.ConvertBack(value, value.GetType(), null, CultureInfo.CurrentCulture);
            Assert.Equal(value, convertedValue);
        }

        [Fact]
        public void ConvertBackWithValueNotOfTargetTypeThrows()
        {
            Assert.Throws<ArgumentException>(
                ()=> _converter.ConvertBack(value, typeof(bool), null, CultureInfo.CurrentCulture));
        }
    }

    public class GivenOneLinkedConverterWithASpecificTargetType
    {
        private LinkedConverter sut;

        public GivenOneLinkedConverterWithASpecificTargetType()
        {
            sut = new LinkedConverter();
            sut.Converters.Add(new ConverterThatConvertsToInt());
        }

        [Fact]
        public void AddingConverterWithTheWrongSourceTypeThrows()
        {
            Assert.Throws<ConverterTypesMismatchException>(
                () => sut.Converters.Add(new ConverterThatConvertsDoubles()));
        }
    }

    public class GivenTwoLinkedConverters
    {
        private LinkedConverter sut;

        public GivenTwoLinkedConverters()
        {
            sut = new LinkedConverter();
            sut.Converters.Add(new BooleanInversionConverter());
            sut.Converters.Add(new BooleanToVisibilityConverter());
        }

        [Theory]
        [InlineData(true, Visibility.Collapsed)]
        [InlineData(false, Visibility.Visible)]
        public void ReturnsExpectedResultWhenConverting(bool input, Visibility expected)
        {
            var result = sut.Convert(input, typeof (Visibility), null, CultureInfo.CurrentCulture);
            Assert.Equal(expected, result); // false -> true -> Visible
        }

        [Theory]
        [InlineData(Visibility.Collapsed, true)]
        [InlineData(Visibility.Visible, false)]
        public void ReturnsExpectedResultWhenConvertingBack(Visibility input, bool expected)
        {
            var result = sut.ConvertBack(input, typeof(bool), null, CultureInfo.CurrentCulture);
            Assert.Equal(expected, result); // false -> true -> Visible
        }
    }

    [ValueConversion(typeof(int), typeof(int))]
    public class ConverterThatConvertsToInt : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    [ValueConversion(typeof(double), typeof(int))]
    public class ConverterThatConvertsDoubles : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
