using System;
using System.Collections.Generic;

namespace Mapper.Types
{
    internal sealed class TypeTable
    {
        private static readonly Dictionary<Type, List<Type>> TypeConverterTable = new Dictionary<Type, List<Type>>() {
            { typeof(sbyte), new List<Type> { typeof(short), typeof(int), typeof(long), typeof(float), typeof(double),
                typeof(decimal) }},
            { typeof(byte), new List<Type> { typeof(short), typeof(ushort), typeof(int), typeof(uint), typeof(long),
                typeof(ulong), typeof(float), typeof(double), typeof(decimal) } },
            { typeof(short), new List<Type> { typeof(int), typeof(long), typeof(float), typeof(double), typeof(decimal) } },
            { typeof(ushort), new List<Type> { typeof(int), typeof(uint), typeof(long), typeof(ulong), typeof(float),
                typeof(double), typeof(decimal) } },
            { typeof(int), new List<Type> { typeof(long), typeof(float), typeof(double), typeof(decimal) } },
            { typeof(uint), new List<Type> { typeof(long), typeof(ulong), typeof(float), typeof(double), typeof(decimal) } },
            { typeof(long), new List<Type> { typeof(float), typeof(double), typeof(decimal) } },
            { typeof(ulong), new List<Type> { typeof(float), typeof(double), typeof(decimal) } },
            { typeof(char), new List<Type> { typeof(ushort), typeof(int), typeof(uint), typeof(long), typeof(ulong),
                typeof(float), typeof(double), typeof(decimal) } },
            { typeof(float), new List<Type> { typeof(double) } }
            };

        public bool CanConvert(Type sourceType, Type destinationType)
        {
            if (sourceType == null) throw new ArgumentNullException(nameof(sourceType));
            if (destinationType == null) throw new ArgumentNullException(nameof(destinationType));
            
            if (sourceType == destinationType)
            {
                return true;
            }

            if (TypeConverterTable.ContainsKey(sourceType))
            {
                return TypeConverterTable[sourceType].Contains(destinationType);
            }

            return false;
        }
    }
}