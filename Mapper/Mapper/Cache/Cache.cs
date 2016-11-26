using System;
using System.Collections.Generic;

namespace Mapper.Cache
{
    internal sealed class Cache : ICache
    {
        private readonly Dictionary<TypePair, Delegate> _cache;

        public Cache()
        {
            _cache = new Dictionary<TypePair, Delegate>();
        }

        public void Add<TSource, TDestination>(TypePair key, Func<TSource, TDestination> value)
        {
            _cache.Add(key, value);
        }

        public bool ContainsKey(TypePair key)
        {
            return _cache.ContainsKey(key);
        }

        public Func<TSource, TDestination> GetValue<TSource, TDestination>(TypePair key)
        {
            return (Func<TSource, TDestination>)_cache[key];
        }
    }
}