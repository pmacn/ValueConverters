using System;
using EnsureThat;

namespace ValueConverters
{
    public static class ParamTypeExtensions
    {
        public static Param<Type> Is<T>(this Param<Type> @this)
        {
            var expectedType = typeof (T);
            if (@this.Value != expectedType)
            {
                throw new ArgumentException(String.Format("Expected {0} to be of type {1} but was {2}", @this.Name, expectedType.Name, @this.Value.Name));
            }

            return @this;
        }
    }
}