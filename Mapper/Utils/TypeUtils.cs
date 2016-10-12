using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Mapper.Utils
{
    internal static class TypeUtils
    {
        internal static List<KeyValuePair<PropertyInfo, PropertyInfo>> GetMappablePropertiesPairs(Type sourceType, Type destinationType)
        {
            if (sourceType == null) throw new ArgumentNullException(nameof(sourceType));
            if (destinationType == null) throw new ArgumentNullException(nameof(destinationType));

            List<KeyValuePair<PropertyInfo, PropertyInfo>> properties =
                (from sourceProp in sourceType.GetProperties()
                    join destProp in destinationType.GetProperties()
                        on sourceProp.Name equals destProp.Name
                    where
                        destProp.CanWrite &&
                        IsConvertibleTypes(sourceProp.PropertyType, destProp.PropertyType)
                    select new KeyValuePair< PropertyInfo, PropertyInfo > (sourceProp, destProp))
                    .ToList();
            
            return properties;
        }

        internal static bool IsConvertibleTypes(Type source, Type destination)
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

            TypeCode sourceTypeCode = Type.GetTypeCode(source);
            TypeCode destTypeCode = Type.GetTypeCode(destination);
            switch (sourceTypeCode)
            {
                case TypeCode.Char:
                    switch (destTypeCode)
                    {
                        case TypeCode.UInt16:
                        case TypeCode.Int32:
                        case TypeCode.UInt32:
                        case TypeCode.Int64:
                        case TypeCode.UInt64:
                        case TypeCode.Single:
                        case TypeCode.Double:
                        case TypeCode.Decimal:
                            return true;
                    }
                    return false;

                case TypeCode.SByte:
                    switch (destTypeCode)
                    {
                        case TypeCode.Int16:
                        case TypeCode.Int32:
                        case TypeCode.Int64:
                        case TypeCode.Single:
                        case TypeCode.Double:
                        case TypeCode.Decimal:
                            return true;
                    }
                    break;

                case TypeCode.Byte:
                    switch (destTypeCode)
                    {
                        case TypeCode.Int16:
                        case TypeCode.UInt16:
                        case TypeCode.Int32:
                        case TypeCode.UInt32:
                        case TypeCode.Int64:
                        case TypeCode.UInt64:
                        case TypeCode.Single:
                        case TypeCode.Double:
                        case TypeCode.Decimal:
                            return true;
                    }
                    return false;

                case TypeCode.Int16:
                    switch (destTypeCode)
                    {
                        case TypeCode.Int32:
                        case TypeCode.Int64:
                        case TypeCode.Single:
                        case TypeCode.Double:
                        case TypeCode.Decimal:
                            return true;
                    }
                    return false;

                case TypeCode.UInt16:
                    switch (destTypeCode)
                    {
                        case TypeCode.Int32:
                        case TypeCode.UInt32:
                        case TypeCode.Int64:
                        case TypeCode.UInt64:
                        case TypeCode.Single:
                        case TypeCode.Double:
                        case TypeCode.Decimal:
                            return true;
                    }
                    return false;

                case TypeCode.Int32:
                    switch (destTypeCode)
                    {
                        case TypeCode.Int64:
                        case TypeCode.Single:
                        case TypeCode.Double:
                        case TypeCode.Decimal:
                            return true;
                    }
                    return false;

                case TypeCode.UInt32:
                    switch (destTypeCode)
                    {
                        case TypeCode.UInt32:
                        case TypeCode.UInt64:
                        case TypeCode.Single:
                        case TypeCode.Double:
                        case TypeCode.Decimal:
                            return true;
                    }
                    return false;

                case TypeCode.Int64:
                case TypeCode.UInt64:
                    switch (destTypeCode)
                    {
                        case TypeCode.Single:
                        case TypeCode.Double:
                        case TypeCode.Decimal:
                            return true;
                    }
                    return false;

                case TypeCode.Single:
                    return (destTypeCode == TypeCode.Double);

                default:
                    return false;
            }
            return false;
        }
    }
}
