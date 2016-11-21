using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using DtoMapper.TypeConversion;

namespace DtoMapper.Mapping
{
    class PropertyMapper<TSource, TDestination> where TDestination : new()
    {
        private const BindingFlags flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;
        private IEnumerable<MappingPair> mappingProperties;

        public IEnumerable<MappingPair> PerformMapping()
        {
            if (mappingProperties != null)
            {
                return mappingProperties;
            }

            mappingProperties = (
                     from PropertyInfo sourceProperty in typeof(TSource).GetProperties(flags)
                     join PropertyInfo destinationProperty in typeof(TDestination).GetProperties(flags)
                     on sourceProperty.Name equals destinationProperty.Name
                     where
                     sourceProperty.CanRead && destinationProperty.CanWrite &&
                     TypeConversionTable.TypeCanBeCast(sourceProperty.PropertyType, destinationProperty.PropertyType)
                     select new MappingPair()
                     {
                         Source = sourceProperty,
                         Destination = destinationProperty
                     }
                 ).
                 ToList();

            return mappingProperties;
        }
    }
}

