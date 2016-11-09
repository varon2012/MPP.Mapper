using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Mapper
{
    internal class MappingFunctionsFactory
    {
        private static readonly Dictionary<Type, HashSet<Type>> ImplicitNumberConversions = new Dictionary<Type, HashSet<Type>> () {
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

        public Func<TSource, TDestination> CreateMappingFunction<TSource, TDestination>(MappingEntryInfo mappingEntryInfo) where TDestination : new()
        {
            KeyValuePair<PropertyInfo, PropertyInfo>[] mappingProperties = GetMappingProperties(mappingEntryInfo);

            ParameterExpression parameterExpression = Expression.Parameter(typeof(TSource), "source");        
            List<MemberBinding> memberBindings = new List<MemberBinding>(mappingProperties.Length);

            foreach (KeyValuePair<PropertyInfo, PropertyInfo> currentMapping in mappingProperties)
            {
                Expression propertyAccessExpression = Expression.Property(parameterExpression, currentMapping.Key);
                Expression convertExpression = Expression.Convert(propertyAccessExpression, currentMapping.Value.PropertyType);
                memberBindings.Add(Expression.Bind(currentMapping.Value, convertExpression));
            }

            Expression memberInitExpression = Expression.MemberInit(Expression.New(typeof(TDestination)), memberBindings);
            return Expression.Lambda<Func<TSource, TDestination>>(memberInitExpression, parameterExpression).Compile();
        }

        // Internals

        private KeyValuePair<PropertyInfo, PropertyInfo>[] GetMappingProperties(MappingEntryInfo mappingEntryInfo)
        {
            IEnumerable<KeyValuePair<PropertyInfo, PropertyInfo>> result =
            (from sourceProperty in mappingEntryInfo.Source.GetProperties()
                join destinationProperty in mappingEntryInfo.Destination.GetProperties()
                    on sourceProperty.Name equals destinationProperty.Name
                where destinationProperty.CanWrite && CanAssign(sourceProperty.PropertyType, destinationProperty.PropertyType)
                select new KeyValuePair<PropertyInfo, PropertyInfo>(sourceProperty, destinationProperty));
            return result.ToArray();
        }

        // Static internals

        private static bool CanAssign(Type sourceType, Type destinationType)
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