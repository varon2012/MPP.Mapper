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

        private static readonly Dictionary<TypeCode, TypeCode[]> ConvertableTypes =
            new Dictionary<TypeCode, TypeCode[]>
            {
                {
                    TypeCode.Char,
                    new[]
                    {
                        TypeCode.UInt16, TypeCode.Int32, TypeCode.UInt32,
                        TypeCode.Int64, TypeCode.UInt64, TypeCode.Single,
                        TypeCode.Double, TypeCode.Decimal
                    }
                },
                {
                    TypeCode.SByte,
                    new[]
                    {
                        TypeCode.Int16, TypeCode.Int32, TypeCode.Int64,
                        TypeCode.Single, TypeCode.Double, TypeCode.Decimal
                    }
                },
                {
                    TypeCode.Byte,
                    new[]
                    {
                        TypeCode.Int16, TypeCode.UInt16, TypeCode.Int32,
                        TypeCode.UInt32, TypeCode.Int64, TypeCode.UInt64,
                        TypeCode.Single, TypeCode.Double, TypeCode.Decimal
                    }
                },
                {
                    TypeCode.Int16,
                    new[]
                    {
                        TypeCode.Int32, TypeCode.Int64, TypeCode.Single,
                        TypeCode.Double, TypeCode.Decimal
                    }
                },
                {
                    TypeCode.UInt16,
                    new[]
                    {
                        TypeCode.Int32, TypeCode.UInt32, TypeCode.Int64,
                        TypeCode.UInt64, TypeCode.Single, TypeCode.Double,
                        TypeCode.Decimal
                    }
                },
                {
                    TypeCode.Int32,
                    new[]
                    {
                        TypeCode.Int64, TypeCode.Single, TypeCode.Double,
                        TypeCode.Decimal
                    }
                },
                {
                    TypeCode.UInt32,
                    new[]
                    {
                        TypeCode.Int64, TypeCode.UInt64, TypeCode.Single,
                        TypeCode.Double, TypeCode.Decimal
                    }
                },
                {
                    TypeCode.Int64,
                    new[]
                    {
                        TypeCode.Single, TypeCode.Double, TypeCode.Decimal
                    }
                },
                {
                    TypeCode.UInt64,
                    new[]
                    {
                        TypeCode.Single, TypeCode.Double, TypeCode.Decimal
                    }
                },
                {
                    TypeCode.Single,
                    new[]
                    {
                        TypeCode.Double
                    }
                }
            };

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

            TypeCode[] convertableTypes;
            return ConvertableTypes.TryGetValue(sourceTypeCode, out convertableTypes) && convertableTypes.Contains(destTypeCode);
        }
    }
}
