using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Mapper.Compiler
{
    internal sealed class MapperCompiler : IMapperCompiler
    {
        public Func<TSource, TDestination> Compile<TSource, TDestination>(IEnumerable<MappingPropertyPair> propertiesPair)
        {
            if (propertiesPair == null) throw new ArgumentNullException(nameof(propertiesPair));

            ParameterExpression source = Expression.Parameter(typeof(TSource), nameof(source));
            var memberBindings = GenerateProperties(source, propertiesPair);
            var memberInit = Expression.MemberInit(Expression.New(typeof(TDestination)), memberBindings);
            var expression = Expression.Lambda<Func<TSource, TDestination>>(memberInit, source);

            return expression.Compile();
        }

        private List<MemberAssignment> GenerateProperties(Expression source, IEnumerable<MappingPropertyPair> propertiesPair)
        {
            var memberBindings = new List<MemberAssignment>();

            foreach (var propertyPair in propertiesPair)
            {
                var sourceProperty = Expression.Property(source, propertyPair.Source.Name);
                var destinationValue = Expression.Convert(sourceProperty, propertyPair.Destination.PropertyType);
                var currentExpression = Expression.Bind(propertyPair.Destination, destinationValue);
                memberBindings.Add(currentExpression);
            }

            return memberBindings;
        }
    }
}