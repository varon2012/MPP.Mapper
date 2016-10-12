using System;
using Mapper.Cache;
using Mapper.Compilers;
using Mapper.Configuration;
using Mapper.Utils;

namespace Mapper
{
    public class SimpleMapper : IMapper
    {
        private readonly ICachedMapperCollection cachedCollection;
        private readonly ExpressionTreeCompiler compiler;
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
            Type sourceType = typeof(TSource);
            Type destinationType = typeof(TDestination);

            MapperUnit mapperUnit = new MapperUnit()
            {
                Source = sourceType,
                Destination = destinationType,
                Config = configuration
            };

            if (cachedCollection.ContainsKey(mapperUnit))
            {
                return ((Func<TSource, TDestination>)cachedCollection.GetValue(mapperUnit)).Invoke(source);
            }

            var properties = TypeUtils.GetMappablePropertiesPairs(sourceType, destinationType);
            if (configuration != null)
            {
                properties.AddRange(configuration.Config);
            }
            
            Func<TSource, TDestination> dest = compiler.Compile<TSource, TDestination>(properties);
            cachedCollection.Add(mapperUnit, dest);

            return dest(source);
        }
    }
}
