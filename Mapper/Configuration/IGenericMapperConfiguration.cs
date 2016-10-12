using System;
using System.Linq.Expressions;

namespace Mapper.Configuration
{
    public interface IGenericMapperConfiguration<TSource, TDestination> : IMapperConfiguration
    {
        IGenericMapperConfiguration<TSource, TDestination> Register<TOutSource, TOutDestination>(
            Expression<Func<TSource, TOutSource>> sourcePropertyLambda,
            Expression<Func<TDestination, TOutDestination>> destinationPropertyLambda);
    }
}
