using System;
using System.Collections.Generic;
using Mapper;

namespace Mapper
{
    internal static class TypesHelper
    {
        private static readonly Dictionary<Type, HashSet<Type>> ImplicitNumberConversions = new Dictionary<Type, HashSet<Type>>() {
            { typeof(sbyte), new HashSet<Type> { typeof(short), typeof(int), typeof(long), typeof(float), typeof(double), typeof(decimal) } },
            { typeof(byte), new HashSet<Type> { typeof(short), typeof(ushort), typeof(int), typeof(uint), typeof(long), typeof(ulong), typeof(float), typeof(double), typeof(decimal) } },
            { typeof(short), new HashSet<Type> { typeof(int), typeof(long), typeof(float), typeof(double), typeof(decimal) } },
            { typeof(ushort), new HashSet<Type> { typeof(int), typeof(uint), typeof(long), typeof(ulong), typeof(float), typeof(double), typeof(decimal) } },
            { typeof(int), new HashSet<Type> { typeof(long), typeof(float), typeof(double), typeof(decimal) } },
            { typeof(uint), new HashSet<Type> { typeof(long), typeof(ulong), typeof(float), typeof(double), typeof(decimal) } },
            { typeof(long), new HashSet<Type> { typeof(float), typeof(double), typeof(decimal) } },
            { typeof(char), new HashSet<Type> { typeof(ushort), typeof(int), typeof(uint), typeof(long), typeof(ulong), typeof(float), typeof(double), typeof(decimal) } },
            { typeof(float), new HashSet<Type> { typeof(double) } },
            { typeof(ulong), new HashSet<Type> { typeof(float), typeof(double), typeof(decimal) } },

        };

        internal static bool CanAssign(Type sourceType, Type destinationType)
        {
            var result = sourceType == destinationType;
            if (!result && sourceType.IsPrimitive && destinationType.IsPrimitive)
            {
                result = CanImplicitConvertPrimitives(sourceType, destinationType);
            }
            return result;
        }

        private static bool CanImplicitConvertPrimitives(Type sourceType, Type destinationType)
        {
            bool result = false;

            if (ImplicitNumberConversions.ContainsKey(sourceType))
            {
                result = ImplicitNumberConversions[sourceType].Contains(destinationType);
            }

            return result;
        }
    }
}
