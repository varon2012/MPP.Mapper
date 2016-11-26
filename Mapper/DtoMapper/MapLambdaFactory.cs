using System;
using System.Linq;
using System.Linq.Expressions;

namespace DtoMapper
{
    internal sealed class MapLambdaFactory : IMapLambdaFactory
    {
        public Func<TSource, TDestination> CreateLambda<TSource, TDestination>()
        {
            var sourceType = typeof(TSource);
            var destinationType = typeof(TDestination);

            var source = Expression.Parameter(sourceType);

            var validator = new ConditionsValidator(
                sourceType.GetProperties(),
                destinationType.GetProperties());

            var memberBindings =
                (from propertiesPair in validator
                 let sourceProperty = Expression.Property(source, propertiesPair.Source.Name)
                 let destinationValue = Expression.Convert(sourceProperty, propertiesPair.Destination.PropertyType)

                 select Expression.Bind(propertiesPair.Destination, destinationValue)).ToList();

            var memberInit = Expression.MemberInit(Expression.New(typeof(TDestination)), memberBindings);
            var expression = Expression.Lambda<Func<TSource, TDestination>>(memberInit, source);

            return expression.Compile();
        }
    }
}
