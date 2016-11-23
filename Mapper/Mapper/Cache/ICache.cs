using System;

namespace Mapper.Cache
{
    internal interface ICache
    {
        void Add<TSource, TDestination>(TypePair key, Func<TSource, TDestination> value);
        Func<TSource, TDestination> GetValue<TSource, TDestination>(TypePair key);
        bool ContainsKey(TypePair key);
    }
}