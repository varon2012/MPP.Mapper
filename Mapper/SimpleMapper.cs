using System;
using System.Collections.Generic;
using System.Reflection;
using Mapper.Cache;
using Mapper.Compilers;
using Mapper.Configuration;
using Mapper.Utils;

namespace Mapper
{
    public class SimpleMapper : IMapper
    {
        private readonly ICachedMapperCollection cachedCollection;
        private readonly IMapperCompiler compiler;
        public SimpleMapper()
        {
            cachedCollection = new CachedMapperCollection();
            compiler = new ExpressionTreeCompiler();
        }

        public TDestination Map<TSource, TDestination>(TSource source) where TDestination : new()
        {
            return Map<TSource, TDestination>(source, null);
        }

        public TDestination Map<TSource, TDestination>(
            TSource source, 
            IGenericMapperConfiguration<TSource, TDestination> configuration)
            where TDestination : new()
        {

            MapperUnit mapperUnit = CreateMapperUnit<TSource, TDestination>(configuration);

            Func<TSource, TDestination> compiledFunc = GetCompiledFunc<TSource, TDestination>(mapperUnit);

            return compiledFunc(source);
        }

        private MapperUnit CreateMapperUnit<TSource, TDestination>(IMapperConfiguration config)
        {
            Type sourceType = typeof(TSource);
            Type destinationType = typeof(TDestination);
            MapperUnit mapperUnit = new MapperUnit()
            {
                Source = sourceType,
                Destination = destinationType,
                Config = config
            };
            return mapperUnit;
        }

        private Func<TSource, TDestination> GetCompiledFunc<TSource, TDestination>(MapperUnit mapperUnit)
        {
            if (IsCached(mapperUnit))
            {
                return ((Func<TSource, TDestination>)cachedCollection.GetValue(mapperUnit));
            }

            var properties = GetAllMappablePropertiesPairs(mapperUnit);

            Func<TSource, TDestination> compiledFunc = compiler.Compile<TSource, TDestination>(properties);

            cachedCollection.Add(mapperUnit, compiledFunc);

            return compiledFunc;
        }
        
        private bool IsCached(MapperUnit mapperUnit)
        {
            return cachedCollection.ContainsKey(mapperUnit);
        }

        private List<KeyValuePair<PropertyInfo, PropertyInfo>> GetAllMappablePropertiesPairs(MapperUnit mapperUnit)
        {
            var properties = TypeUtils.GetMappablePropertiesPairs(mapperUnit.Source, mapperUnit.Destination);
            if (mapperUnit.Config != null)
            {
                properties.AddRange(mapperUnit.Config.Value);
            }

            return properties;
        }
    }
}
