using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Mapper
{
    
    public class DtoMapper : IMapper
    {
        private readonly IMappingFunctionsCache _mappingFunctionsCache;
        private readonly IMappingFunctionsFactory _mappingFunctionsFactory;
        private readonly MapperConfiguration _mapperConfiguration;

        public DtoMapper() : this(new MapperConfiguration()) { }

        public DtoMapper(MapperConfiguration mapperConfiguration) : this(new MappingFunctionsCache(), 
            new MappingFunctionsFactory(), 
            mapperConfiguration) { }

        public TDestination Map<TSource, TDestination>(TSource source) where TDestination : new()
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            var mappingEntryInfo = new MappingTypesPair()
            {
                Source = typeof(TSource),
                Destination = typeof(TDestination)
            };

            Func<TSource, TDestination> mappingFunc = GetMappingFunction<TSource, TDestination>(mappingEntryInfo);

            return mappingFunc(source);
        }

        //Internals

        internal DtoMapper(IMappingFunctionsCache mappingFunctionsCache, 
            IMappingFunctionsFactory mappingFunctionsFactory, 
            MapperConfiguration mapperConfiguration)
        {
            _mappingFunctionsCache = mappingFunctionsCache;
            _mappingFunctionsFactory = mappingFunctionsFactory;
            _mapperConfiguration = mapperConfiguration;
        }

        private Func<TSource, TDestination> GetMappingFunction<TSource, TDestination>(MappingTypesPair mappingEntryInfo) 
            where TDestination : new()
        {
            Func<TSource, TDestination> result;
            if (_mappingFunctionsCache.HasCacheFor(mappingEntryInfo))
            {
                result = _mappingFunctionsCache.GetCacheFor<TSource, TDestination>(mappingEntryInfo);
            }
            else
            {
                List<MappingPropertiesPair> mappingProperties = GetMappingProperties(mappingEntryInfo);

                mappingProperties.AddRange(_mapperConfiguration.GetRegisteredMappings<TSource, TDestination>());
                result = _mappingFunctionsFactory.CreateMappingFunction<TSource, TDestination>(mappingProperties);
                _mappingFunctionsCache.AddToCache(mappingEntryInfo, result);
            }

            return result;
        }

        // Static internals

        private static List<MappingPropertiesPair> GetMappingProperties(MappingTypesPair mappingEntryInfo)
        {
            IEnumerable<MappingPropertiesPair> result =
            (from sourceProperty in mappingEntryInfo.Source.GetProperties()
             join destinationProperty in mappingEntryInfo.Destination.GetProperties()
                 on sourceProperty.Name equals destinationProperty.Name
             where destinationProperty.CanWrite && TypesHelper.CanAssign(sourceProperty.PropertyType, destinationProperty.PropertyType)
             select new MappingPropertiesPair {SourceProperty = sourceProperty, DestinationProperty = destinationProperty});
            return result.ToList();
        }
    }
}
