using System;

namespace ValueConverters.WPF
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public sealed class ValueConverterAttribute : Attribute
    {
        private readonly Type _sourceType;

        private readonly Type _targetType;

        public ValueConverterAttribute(Type sourceType, Type targetType)
        {
            _sourceType = sourceType;
            _targetType = targetType;
        }

        public Type SourceType { get { return _sourceType; } }

        public Type TargetType { get { return _targetType; } }
    }
}
