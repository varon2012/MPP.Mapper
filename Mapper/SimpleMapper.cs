using System;
using System.Collections.Generic;
using System.Reflection;
using Mapper.Cache;
using Mapper.Compilers;
using Mapper.Configuration;
using Mapper.UnitsForMapping;
using Mapper.Utils;

namespace Mapper
{
    public class SimpleMapper : IMapper
    {
        private readonly ICachedMapperCollection cachedCollection;
        private readonly IMapperCompiler compiler;
        public SimpleMapper() : this(new ExpressionTreeCompiler(), new CachedMapperCollection())
        {
        }

        //for testing purposes
        internal SimpleMapper(IMapperCompiler compiler) : this(compiler, new CachedMapperCollection())
        {
        }

        //for testing purposes
        internal SimpleMapper(IMapperCompiler compiler, ICachedMapperCollection cachedCollection)
        {
            this.compiler = compiler;
            this.cachedCollection = cachedCollection;
        }

        public TDestination Map<TSource, TDestination>(
            TSource source, 
            IGenericMapperConfiguration<TSource, TDestination> configuration = null)
            where TDestination : new()
        {
            if (source == null) throw new ArgumentNullException(nameof(source));

            IMappingUnit mappingUnit = CreateMappingUnit<TSource, TDestination>(configuration);

            Func<TSource, TDestination> compiledFunc = GetCompiledFunc<TSource, TDestination>(mappingUnit);

            return (compiledFunc != null) ? compiledFunc.Invoke(source) : default(TDestination);
        }

        private IMappingUnit CreateMappingUnit<TSource, TDestination>(IMapperConfiguration config)
        {
            Type sourceType = typeof(TSource);
            Type destinationType = typeof(TDestination);
            MappingUnit mappingUnit = new MappingUnit()
            {
                Source = sourceType,
                Destination = destinationType,
                Config = config
            };
            return mappingUnit;
        }

        private Func<TSource, TDestination> GetCompiledFunc<TSource, TDestination>(IMappingUnit mappingUnit)
             where TDestination : new()
        {
            if (IsCached(mappingUnit))
            {
                return ((Func<TSource, TDestination>)cachedCollection.GetValue(mappingUnit));
            }

            var properties = GetAllMappablePropertiesPairs(mappingUnit);

            Func<TSource, TDestination> compiledFunc = compiler.Compile<TSource, TDestination>(properties);

            cachedCollection.Add(mappingUnit, compiledFunc);

            return compiledFunc;
        }
        
        private bool IsCached(IMappingUnit mappingUnit)
        {
            return cachedCollection.ContainsKey(mappingUnit);
        }

        private List<KeyValuePair<PropertyInfo, PropertyInfo>> GetAllMappablePropertiesPairs(IMappingUnit mappingUnit)
        {
            var properties = TypeUtils.GetMappablePropertiesPairs(mappingUnit.Source, mappingUnit.Destination);
            if (mappingUnit.Config != null)
            {
                properties.AddRange(mappingUnit.Config.Value);
            }

            return properties;
        }
    }
}
