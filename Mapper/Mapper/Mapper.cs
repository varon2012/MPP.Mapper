using System;
using System.Linq;
using System.Collections.Generic;
using Mapper.Cache;
using Mapper.Compiler;
using Mapper.Types;

namespace Mapper
{
    public sealed class Mapper : IMapper
    {
        private readonly TypeTable _typeTable;
        private readonly IMapperCompiler _mapperCompiler;
        private readonly ICache _cache;

        public Mapper()
        {
            _typeTable = new TypeTable();
            _mapperCompiler = new MapperCompiler();
            _cache = new Cache.Cache();
        }

        public TDestination Map<TSource, TDestination>(TSource source) where TDestination : new()
        {
            if (source == null) throw new ArgumentNullException(nameof(source));

            Func<TSource, TDestination> compiledFunc;
            var typePair = new TypePair(typeof(TSource), typeof(TDestination));
                                   
            if (_cache.ContainsKey(typePair))
            {
                compiledFunc = _cache.GetValue<TSource, TDestination>(typePair);
            }
            else
            {
                var mappingProperties = FindMappingProperties<TSource, TDestination>();
                compiledFunc = _mapperCompiler.Compile<TSource, TDestination>(mappingProperties);
                _cache.Add(typePair, compiledFunc);
            }

            return compiledFunc(source);
        }

        private List<MappingPropertyPair> FindMappingProperties<TSource, TDestination>()
        {
            return (
                from sourceProperty in typeof(TSource).GetProperties()
                join destinationProperty in typeof(TDestination).GetProperties()
                    on sourceProperty.Name equals destinationProperty.Name
                where
                    sourceProperty.CanRead && destinationProperty.CanWrite &&
                    _typeTable.CanConvert(sourceProperty.PropertyType, destinationProperty.PropertyType)
                select new MappingPropertyPair(sourceProperty, destinationProperty)
                ).ToList();
        }
    }
}