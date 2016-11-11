using System;

namespace Mapper
{
    internal interface IMappingFunctionsCache
    {
        void AddToCache<TSource, TDestination>(MappingEntryInfo mappingEntryInfo, Func<TSource, TDestination> mappingFunction);
        Func<TSource, TDestination> GetCacheFor<TSource, TDestination>(MappingEntryInfo mappingEntryInfo);
        bool HasCacheFor(MappingEntryInfo mappingEntryInfo);
    }
}