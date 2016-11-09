using System;

namespace Mapper
{
    public class DtoMapper : IMapper
    {
        private readonly MappingFunctionsCache _mappingFunctionsCache = new MappingFunctionsCache();
        private readonly MappingFunctionsFactory _mappingFunctionsFactory = new MappingFunctionsFactory();

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
