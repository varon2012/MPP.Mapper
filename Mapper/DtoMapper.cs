using System;

namespace Mapper
{
    
    public class DtoMapper : IMapper
    {
        private readonly IMappingFunctionsCache _mappingFunctionsCache;
        private readonly IMappingFunctionsFactory _mappingFunctionsFactory;

        public DtoMapper() : this(new MappingFunctionsCache(), new MappingFunctionsFactory()) { }

        public TDestination Map<TSource, TDestination>(TSource source) where TDestination : new()
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            var mappingEntryInfo = new MappingEntryInfo()
            {
                Source = typeof(TSource),
                Destination = typeof(TDestination)
            };

            Func<TSource, TDestination> mappingFunc = GetMappingFunc<TSource, TDestination>(mappingEntryInfo);

            return mappingFunc(source);
        }

        //Internals
        internal DtoMapper(IMappingFunctionsCache mappingFunctionsCache, IMappingFunctionsFactory mappingFunctionsFactory)
        {
            _mappingFunctionsCache = mappingFunctionsCache;
            _mappingFunctionsFactory = mappingFunctionsFactory;
        }

        private Func<TSource, TDestination> GetMappingFunc<TSource, TDestination>(MappingEntryInfo mappingEntryInfo) where TDestination : new()
        {
            Func<TSource, TDestination> result;
            if (_mappingFunctionsCache.HasCacheFor(mappingEntryInfo))
            {
                result = _mappingFunctionsCache.GetCacheFor<TSource, TDestination>(mappingEntryInfo);
            }
            else
            {
                result = _mappingFunctionsFactory.CreateMappingFunction<TSource, TDestination>(mappingEntryInfo);
                _mappingFunctionsCache.AddToCache(mappingEntryInfo, result);
            }

            return result;
        }
    }
}
