using System;
using System.Collections.Generic;

namespace Mapper
{
    internal class MappingFunctionsCache : IMappingFunctionsCache
    {
        private readonly Dictionary<MappingEntryInfo, Delegate> _cache = new Dictionary<MappingEntryInfo, Delegate>();

        public void AddToCache<TSource, TDestination>(MappingEntryInfo mappingEntryInfo, Func<TSource, TDestination> mappingFunction)
        {
            _cache.Add(mappingEntryInfo, mappingFunction);
        }

        public Func<TSource, TDestination> GetCacheFor<TSource, TDestination>(MappingEntryInfo mappingEntryInfo) => (Func<TSource, TDestination>) _cache[mappingEntryInfo];

        public bool HasCacheFor(MappingEntryInfo mappingEntryInfo) => _cache.ContainsKey(mappingEntryInfo);
    }
}