using System;

namespace DtoMapper
{
    internal interface IMapLambdaFactory
    {
        Func<TSource, TDestination> CreateLambda<TSource, TDestination>();
    }
}
