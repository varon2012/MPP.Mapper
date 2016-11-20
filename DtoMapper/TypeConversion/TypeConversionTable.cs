using System;
using System.Collections.Generic;

namespace DtoMapper.TypeConversion
{
    internal class TypeConversionTable
    {
        private static readonly Dictionary<Type, HashSet<Type>> castTable;

        static TypeConversionTable()
        {

            castTable = new Dictionary<Type, HashSet<Type>>() {
            { typeof(sbyte), new HashSet<Type> { typeof(short), typeof(int), typeof(long), typeof(float), typeof(double), typeof(decimal) } },
            { typeof(byte), new HashSet<Type> { typeof(short), typeof(ushort), typeof(int), typeof(uint), typeof(long), typeof(ulong), typeof(float), typeof(double), typeof(decimal) } },
            { typeof(short), new HashSet<Type> { typeof(int), typeof(long), typeof(float), typeof(double), typeof(decimal) } },
            { typeof(ushort), new HashSet<Type> { typeof(int), typeof(uint), typeof(long), typeof(ulong), typeof(float), typeof(double), typeof(decimal) } },
            { typeof(int), new HashSet<Type> { typeof(long), typeof(float), typeof(double), typeof(decimal) } },
            { typeof(uint), new HashSet<Type> { typeof(long), typeof(ulong), typeof(float), typeof(double), typeof(decimal) } },
            { typeof(long), new HashSet<Type> { typeof(float), typeof(double), typeof(decimal) } },
            { typeof(char), new HashSet<Type> { typeof(ushort), typeof(int), typeof(uint), typeof(long), typeof(ulong), typeof(float), typeof(double), typeof(decimal) } },
            { typeof(float), new HashSet<Type> { typeof(double) } },
            { typeof(ulong), new HashSet<Type> { typeof(float), typeof(double), typeof(decimal) } }
            };

        }

        public static bool TypeCanBeCast(Type sourceType, Type destinationType)
        {

            if(sourceType == null)
                throw new ArgumentNullException(nameof(sourceType));
            if(destinationType == null)
                throw new ArgumentNullException(nameof(destinationType));

            if (!destinationType.IsAssignableFrom(sourceType))
            {
                return castTable.ContainsKey(sourceType) && castTable[sourceType].Contains(destinationType);
            }
            return true;

        }
    }
}
