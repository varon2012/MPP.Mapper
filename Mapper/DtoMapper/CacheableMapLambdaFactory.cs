using System;
using System.Collections.Generic;

namespace DtoMapper
{
    internal sealed class CacheableMapLambdaFactory : IMapLambdaFactory
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

                var result = 17;

                unchecked
                {
                    result = 31*result + sourceHash;
                    result = 31*result + destinationHash;
                    return result;
                }
            }
        }

        private readonly Dictionary<TypesPair, Delegate> _cache;
        private readonly IMapLambdaFactory _lambdaFactory;

        public CacheableMapLambdaFactory()
        {
            _cache = new Dictionary<TypesPair, Delegate>(new TypesPairComparer());
            _lambdaFactory = new MapLambdaFactory();
        }

        public Func<TSource, TDestination> CreateLambda<TSource, TDestination>()
        {
            Delegate lambda;
            var typesPair = new TypesPair(typeof(TSource), typeof(TDestination));

            if (!_cache.TryGetValue(typesPair, out lambda))
            {
                lambda = _lambdaFactory.CreateLambda<TSource, TDestination>();
                _cache[typesPair] = lambda;
            }

            return lambda as Func<TSource, TDestination>;
        }
    }
}
