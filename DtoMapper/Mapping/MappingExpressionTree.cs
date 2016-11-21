using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace DtoMapper.Mapping
{
    public class MappingExpressionTree<TSource, TDestination> where TDestination : new()
    {
        private readonly ParameterExpression expressionParameter;
        private readonly NewExpression destinationCreation;
        private readonly List<MemberBinding> destinationInitialization;

        public MappingExpressionTree()
        {
            expressionParameter = Expression.Parameter(typeof(TSource), "source");
            destinationCreation = Expression.New(typeof(TDestination));
            destinationInitialization = new List<MemberBinding>();
        }

        public Expression<Func<TSource, TDestination>> Create(IEnumerable<MappingPair> mappingPairs) 
        {
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
