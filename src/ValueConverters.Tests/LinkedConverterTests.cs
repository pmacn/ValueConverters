using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
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
}
