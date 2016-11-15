using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Mapper
{
    internal class MappingFunctionsFactory : IMappingFunctionsFactory
    {
        public Func<TSource, TDestination> CreateMappingFunction<TSource, TDestination>(List<MappingPropertiesPair> mappingProperties) where TDestination : new()
        {
            ParameterExpression parameterExpression = Expression.Parameter(typeof(TSource), "source");        
            List<MemberBinding> memberBindings = new List<MemberBinding>(mappingProperties.Count);

            foreach (MappingPropertiesPair currentMapping in mappingProperties)
            {
                Expression propertyAccessExpression = Expression.Property(parameterExpression, currentMapping.SourceProperty);
                Expression convertExpression = Expression.Convert(propertyAccessExpression, currentMapping.DestinationProperty.PropertyType);
                memberBindings.Add(Expression.Bind(currentMapping.DestinationProperty, convertExpression));
            }

            Expression memberInitExpression = Expression.MemberInit(Expression.New(typeof(TDestination)), memberBindings);
            return Expression.Lambda<Func<TSource, TDestination>>(memberInitExpression, parameterExpression).Compile();
        }        
    }
}