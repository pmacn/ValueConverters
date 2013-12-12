using NUnit.Framework;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace ValueConverters.Tests
{
    [TestFixture]
    public class WithEmptyLinkedConverter
    {
        IValueConverter _converter = new LinkedConverter();
        object value = "value";
        
        [Test]
        public void ConvertReturnsOriginalValue()
        {
            var convertedValue = _converter.Convert(value, value.GetType(), null, CultureInfo.CurrentCulture);
            Assert.AreEqual(value, convertedValue);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void ConvertWithValueNotOfTargetTypeThrowsException()
        {
            _converter.Convert(value, typeof(bool), null, CultureInfo.CurrentCulture);
        }

        [Test]
        public void ConvertBackReturnsOriginalValue()
        {
            var convertedValue = _converter.ConvertBack(value, value.GetType(), null, CultureInfo.CurrentCulture);
            Assert.AreEqual(value, convertedValue);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void ConvertBackWithValueNotOfTargetTypeThrows()
        {
            _converter.ConvertBack(value, typeof(bool), null, CultureInfo.CurrentCulture);
        }
    }

    [TestFixture]
    public class GivenOneLinkedConverterWithASpecificTargetType
    {
        private LinkedConverter sut;

        [SetUp]
        public void Setup()
        {
            sut = new LinkedConverter();
            sut.Converters.Add(new ConverterThatConvertsToInt());
        }

        [Test]
        [ExpectedException(typeof(ConverterTypesMismatchException))]
        public void AddingConverterWithTheWrongSourceTypeThrows()
        {
            sut.Converters.Add(new ConverterThatConvertsDoubles());
        }
    }

    [TestFixture]
    public class GivenTwoLinkedConverters
    {
        private LinkedConverter sut;

        [SetUp]
        public void Setup()
        {
            sut = new LinkedConverter();
            sut.Converters.Add(new BooleanInversionConverter());
            sut.Converters.Add(new BooleanToVisibilityConverter());
        }

        [TestCase(true, Visibility.Collapsed)] 
        [TestCase(false, Visibility.Visible)]
        public void ReturnsExpectedResultWhenConverting(bool input, Visibility expected)
        {
            var result = sut.Convert(input, typeof (Visibility), null, CultureInfo.CurrentCulture);
            Assert.That(result, Is.EqualTo(expected)); // false -> true -> Visible
        }

        [TestCase(Visibility.Collapsed, true)]
        [TestCase(Visibility.Visible, false)]
        public void ReturnsExpectedResultWhenConvertingBack(Visibility input, bool expected)
        {
            var result = sut.ConvertBack(input, typeof(bool), null, CultureInfo.CurrentCulture);
            Assert.That(result, Is.EqualTo(expected)); // false -> true -> Visible
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
