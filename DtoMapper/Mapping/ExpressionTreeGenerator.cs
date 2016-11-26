using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace DtoMapper.Mapping
{
    public class ExpressionTreeGenerator<TSource, TDestination> where TDestination : new()
    {
        public Expression<Func<TSource, TDestination>> Create(IEnumerable<MappingPair> mappingPairs) 
        {
            if(mappingPairs == null)
                throw new ArgumentNullException();

            ParameterExpression expressionParameter = Expression.Parameter(typeof(TSource), "source");
            NewExpression destinationCreation = Expression.New(typeof(TDestination));
            List<MemberBinding> destinationInitialization = new List<MemberBinding>();

            foreach (MappingPair mapPair in mappingPairs)
            {
                MemberExpression sourceValue = Expression.Property(expressionParameter, mapPair.Source);
                UnaryExpression valueInDestinationType = Expression.Convert(sourceValue, mapPair.Destination.PropertyType);
                MemberBinding assign = Expression.Bind(mapPair.Destination, valueInDestinationType);
                destinationInitialization.Add(assign);
            }

            MemberInitExpression functionBody = Expression.MemberInit(destinationCreation, destinationInitialization);
            Expression<Func<TSource, TDestination>> expressionTree = Expression.Lambda<Func<TSource, TDestination>>(functionBody, expressionParameter);

            return expressionTree;
        }
    }
}
