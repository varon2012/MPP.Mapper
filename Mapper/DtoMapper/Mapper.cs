using System;

namespace DtoMapper
{
    public sealed class Mapper : IMapper
    {
        private readonly IMapLambdaFactory _lambdaFactory;

        public Mapper()
        {
            _lambdaFactory = new CacheableMapLambdaFactory();
        }

        public TDestination Map<TSource, TDestination>(TSource source) where TDestination : new()
        {
            if (source == null) throw new ArgumentNullException(nameof(source));

            var lambda = _lambdaFactory.CreateLambda<TSource, TDestination>();

            return lambda(source);
        }
    }
}
