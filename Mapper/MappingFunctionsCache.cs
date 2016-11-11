using System;
using System.Collections.Generic;

namespace Mapper
{
    internal class MappingFunctionsCache : IMappingFunctionsCache
    {
        private readonly Dictionary<MappingTypesPair, Delegate> _cache = new Dictionary<MappingTypesPair, Delegate>();

        public void AddToCache<TSource, TDestination>(MappingTypesPair mappingEntryInfo, Func<TSource, TDestination> mappingFunction)
        {
            _cache.Add(mappingEntryInfo, mappingFunction);
        }

        public Func<TSource, TDestination> GetCacheFor<TSource, TDestination>(MappingTypesPair mappingEntryInfo) => (Func<TSource, TDestination>) _cache[mappingEntryInfo];

        public bool HasCacheFor(MappingTypesPair mappingEntryInfo) => _cache.ContainsKey(mappingEntryInfo);
    }
}