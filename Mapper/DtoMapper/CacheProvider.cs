using System;
using System.Collections.Generic;

namespace DtoMapper
{
    internal class CacheProvider
    {
        private sealed class TypesPair
        {
            internal Type Source { get; }
            internal Type Destination { get; }

            public TypesPair(Type source, Type destination)
            {
                Source = source;
                Destination = destination;
            }
        }

        private sealed class TypesPairComparer : IEqualityComparer<TypesPair>
        {
            public bool Equals(TypesPair x, TypesPair y)
            {
                if (x == null && y == null) return true;
                if (x == null || y == null) return false;

                return x.Source == y.Source && x.Destination == y.Destination;
            }

            public int GetHashCode(TypesPair obj)
            {
                if (obj == null) throw new ArgumentNullException(nameof(obj));

                var sourceHash = obj.Source?.GetHashCode() ?? 0;
                var destinationHash = obj.Destination?.GetHashCode() ?? 0;

                unchecked
                {
                    return 2147483647 * sourceHash ^ destinationHash;
                }
            }
        }

        private readonly Dictionary<TypesPair, Delegate> _cache;

        private static readonly Lazy<CacheProvider> Lazy =
            new Lazy<CacheProvider>(() => new CacheProvider());

        internal static CacheProvider Instance => Lazy.Value;

        private CacheProvider()
        {
            _cache = new Dictionary<TypesPair, Delegate>(new TypesPairComparer());
        }

        public Func<TSource, TDestination> GetOrAddToCache<TSource, TDestination>(
            Func<Func<TSource, TDestination>> expression)
        {
            Delegate lambda;
            var typesPair = new TypesPair(typeof(TSource), typeof(TDestination));

            if (!_cache.TryGetValue(typesPair, out lambda))
            {
                lambda = expression();
                _cache[typesPair] = lambda;
            }

            return lambda as Func<TSource, TDestination>;
        }
    }
}
