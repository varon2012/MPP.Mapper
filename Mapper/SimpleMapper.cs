using System;
using Mapper.Cache;
using Mapper.Compilers;
using Mapper.Configuration;
using Mapper.Utils;

namespace Mapper
{
    public class SimpleMapper : IMapper
    {
        private ICachedMapperCollection cachedCollection;
        private ExpressionTreeCompiler compiler;
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

            var properties = TypeUtils.GetMappablePropertiesPairs(sourceType, destinationType);
            if (configuration != null)
            {
                properties.AddRange(configuration.Config);
            }
            
            Func<TSource, TDestination> dest = compiler.Compile<TSource, TDestination>(properties);
            return dest(source);
        }
    }
}
