using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace Mapper
{
    public class MapperConfiguration
    {
        private readonly Dictionary<MappingTypesPair, List<MappingPropertiesPair>> _configuration = 
            new Dictionary<MappingTypesPair, List<MappingPropertiesPair>>();

        public MapperConfiguration Register<TSource, TDestination, TPropertyType>(
            Expression<Func<TSource, TPropertyType>> sourcePropertyAccessor, 
            Expression<Func<TDestination, TPropertyType>> destinationPropertyAccessor) where TDestination : new()
        {
            if (sourcePropertyAccessor == null)
            {
                throw new ArgumentNullException(nameof(sourcePropertyAccessor));
            }
            if (destinationPropertyAccessor == null)
            {
                throw new ArgumentNullException(nameof(destinationPropertyAccessor));
            }

            PropertyInfo sourceProperty = GetPropertyInfo(sourcePropertyAccessor);
            PropertyInfo destinationProperty = GetPropertyInfo(destinationPropertyAccessor);            

            if (!destinationProperty.CanWrite)
            {
                throw new ArgumentException("Destination property doesn't have setter.");
            }

            Type sourceType = sourceProperty.PropertyType;
            Type destinationType = destinationProperty.PropertyType;

            if (!TypesHelper.CanAssign(sourceType, destinationType))
            {
                throw new ArgumentException("Incompatible types.");
            }

            var mappingTypesPair = new MappingTypesPair
            {
                Source = typeof(TSource),
                Destination = typeof(TDestination)
            };

            var newMapping = new MappingPropertiesPair
            {
                SourceProperty = sourceProperty,
                DestinationProperty = destinationProperty
            };

            List<MappingPropertiesPair> registeredMappings;
            if (!_configuration.TryGetValue(mappingTypesPair, out registeredMappings))
            {
                registeredMappings = new List<MappingPropertiesPair>();
                _configuration[mappingTypesPair] = registeredMappings;
            }

            registeredMappings.Add(newMapping);

            return this;
        }

        // Internals

        internal IEnumerable<MappingPropertiesPair> GetRegisteredMappings<TSource, TDestination>()
        {
            var mappingEntryInfo = new MappingTypesPair()
            {
                Source = typeof(TSource),
                Destination = typeof(TDestination)
            };

            List<MappingPropertiesPair> result;

            if (!_configuration.TryGetValue(mappingEntryInfo, out result))
            {
                result = new List<MappingPropertiesPair>();
            }

            return result;
        }

        private PropertyInfo GetPropertyInfo<TSource, TPropertyType>(Expression<Func<TSource, TPropertyType>> propertyAccessor)
        {
            MemberExpression propertyAccessExpression = propertyAccessor.Body as MemberExpression;
            if (propertyAccessExpression == null)
            {
                throw new ArgumentException("Expression doesn't represent property accessor.");
            }

            var propertyInfo = propertyAccessExpression.Member as PropertyInfo;
            if (propertyInfo == null)
            {
                throw new ArgumentException("Expression doesn't represent property accessor.");
            }
            return propertyInfo;
        }
    }
}