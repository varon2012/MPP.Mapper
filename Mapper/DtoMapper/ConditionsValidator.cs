using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace DtoMapper
{
    internal struct PropertiesPair
    {
        internal PropertyInfo Source { get; }
        internal PropertyInfo Destination { get; }

        internal PropertiesPair(PropertyInfo source, PropertyInfo destination)
        {
            Source = source;
            Destination = destination;
        }
    }

    internal class ConditionsValidator : IEnumerable<PropertiesPair>
    {
        private readonly IEnumerable<PropertyInfo> _sourceProperties;
        private readonly IEnumerable<PropertyInfo> _destinationProperties;

        internal ConditionsValidator(
            IEnumerable<PropertyInfo> sourceProperties,
            IEnumerable<PropertyInfo> destinationProperties)
        {
            _sourceProperties = sourceProperties;
            _destinationProperties = destinationProperties;
        }

        public IEnumerator<PropertiesPair> GetEnumerator()
        {
            return (from sourceProperty in _sourceProperties
                    join destinationProperty in _destinationProperties
                        on sourceProperty.Name equals destinationProperty.Name
                    where 
                        destinationProperty.CanWrite &&
                        IsConvertibleTypes(sourceProperty.PropertyType, destinationProperty.PropertyType)
                    select new PropertiesPair(sourceProperty, destinationProperty))
                .GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private static bool IsConvertibleTypes(Type source, Type destination)
        {
            return IsEqualRefType(source, destination) ||
                IsEqualValueType(source, destination) ||
                IsImplicitNumericConversion(source, destination);
        }

        private static bool IsEqualValueType(Type source, Type destination)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (destination == null) throw new ArgumentNullException(nameof(destination));

            if (!source.IsValueType || !destination.IsValueType)
            {
                return false;
            }

            return source == destination;
        }

        private static bool IsEqualRefType(Type source, Type destination)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (destination == null) throw new ArgumentNullException(nameof(destination));

            if (!source.IsClass || source.IsAbstract || !destination.IsClass || destination.IsAbstract)
            {
                return false;
            }

            return source == destination;
        }

        private static bool IsImplicitNumericConversion(Type source, Type destination)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (destination == null) throw new ArgumentNullException(nameof(destination));

            var sourceTypeCode = Type.GetTypeCode(source);
            var destTypeCode = Type.GetTypeCode(destination);

            switch (sourceTypeCode)
            {
                case TypeCode.Char:
                    return new[]
                    {
                        TypeCode.UInt16, TypeCode.Int32, TypeCode.UInt32,
                        TypeCode.Int64, TypeCode.UInt64, TypeCode.Single,
                        TypeCode.Double, TypeCode.Decimal
                    }.Contains(destTypeCode);

                case TypeCode.SByte:
                    return new[]
                    {
                        TypeCode.Int16, TypeCode.Int32, TypeCode.Int64,
                        TypeCode.Single, TypeCode.Double, TypeCode.Decimal
                    }.Contains(destTypeCode);

                case TypeCode.Byte:
                    return new[]
                    {
                        TypeCode.Int16, TypeCode.UInt16, TypeCode.Int32,
                        TypeCode.UInt32, TypeCode.Int64, TypeCode.UInt64,
                        TypeCode.Single, TypeCode.Double, TypeCode.Decimal
                    }.Contains(destTypeCode);

                case TypeCode.Int16:
                    return new[]
                    {
                        TypeCode.Int32, TypeCode.Int64, TypeCode.Single,
                        TypeCode.Double, TypeCode.Decimal
                    }.Contains(destTypeCode);

                case TypeCode.UInt16:
                    return new[]
                    {
                        TypeCode.Int32, TypeCode.UInt32, TypeCode.Int64,
                        TypeCode.UInt64, TypeCode.Single, TypeCode.Double,
                        TypeCode.Decimal
                    }.Contains(destTypeCode);

                case TypeCode.Int32:
                    return new[]
                    {
                        TypeCode.Int64, TypeCode.Single, TypeCode.Double,
                        TypeCode.Decimal
                    }.Contains(destTypeCode);

                case TypeCode.UInt32:
                    return new[]
                    {
                        TypeCode.Int64, TypeCode.UInt64, TypeCode.Single,
                        TypeCode.Double, TypeCode.Decimal
                    }.Contains(destTypeCode);

                case TypeCode.Int64:
                case TypeCode.UInt64:
                    return new[]
                    {
                        TypeCode.Single, TypeCode.Double, TypeCode.Decimal
                    }.Contains(destTypeCode);

                case TypeCode.Single:
                    return destTypeCode == TypeCode.Double;

                default:
                    return false;
            }
        }
    }
}
