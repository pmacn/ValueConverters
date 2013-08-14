using System;

namespace ValueConverters
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    sealed class ValueConversionAttribute : Attribute
    {
        private readonly Type _sourceType;

        private readonly Type _targetType;

        public ValueConversionAttribute(Type sourceType, Type targetType)
        {
            _sourceType = sourceType;
            _targetType = targetType;
        }

        public Type SourceType { get { return _sourceType; } }

        public Type TargetType { get { return _targetType; } }
    }
}
