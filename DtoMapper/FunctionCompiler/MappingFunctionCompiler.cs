using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using DtoMapper.TypeConversion;

namespace DtoMapper.FunctionCompiler
{
    internal class MappingFunctionCompiler : IFunctionCompiler
    {
        private struct MapPair
        {
            public PropertyInfo SourceProperty { get; set; }
            public PropertyInfo DestinationProperty { get; set; }
        }

        private readonly FunctionCash functionCash;

        public MappingFunctionCompiler()
        {
            functionCash = new FunctionCash();
        }

        public Func<TSource, TDestination> GetMappingFunction<TSource, TDestination>() where TDestination : new()
        {

            KeyValuePair<Type, Type> key = new KeyValuePair<Type, Type>(typeof(TSource), typeof(TDestination));
            Delegate mappingFunction = functionCash.GetFromCash(key);

            if (mappingFunction == null)
            {
                mappingFunction = CreateCompiledFunction<TSource, TDestination>();
                functionCash.PushToCash(key, mappingFunction);
            }

            return (Func<TSource, TDestination>) mappingFunction;

        }

        private Func<TSource, TDestination> CreateCompiledFunction<TSource, TDestination>()
        {

            IEnumerable<MapPair> mapPairs = FindMapPairs<TSource, TDestination>();
            Expression<Func<TSource, TDestination>> expressionTree =
                CreateExpressionTree<TSource, TDestination>(mapPairs);

            Func<TSource, TDestination> compiledFunction = expressionTree.Compile();
            return compiledFunction;

        }

        private IEnumerable<MapPair> FindMapPairs<TSource, TDestination>()
        {

            const BindingFlags flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;

            List<MapPair> mapProperties = (
                    from PropertyInfo sourceProperty in typeof(TSource).GetProperties(flags)
                    join PropertyInfo destinationProperty in typeof(TDestination).GetProperties(flags)
                    on sourceProperty.Name equals destinationProperty.Name
                    where
                    sourceProperty.CanRead && destinationProperty.CanWrite &&
                    TypeConversionTable.TypeCanBeCast(sourceProperty.PropertyType, destinationProperty.PropertyType)
                    select new MapPair()
                    {
                        SourceProperty = sourceProperty,
                        DestinationProperty = destinationProperty
                    }
                ).
                ToList();

            return mapProperties;

        }

        private Expression<Func<TSource, TDestination>> CreateExpressionTree<TSource, TDestination>(
            IEnumerable<MapPair> mapPairs)
        {

            ParameterExpression functionParameter = Expression.Parameter(typeof(TSource), "source");

            NewExpression destinationCreation = Expression.New(typeof(TDestination));
            List<MemberBinding> destinationInitialization = new List<MemberBinding>();

            foreach (MapPair mapPair in mapPairs)
            {
                MemberExpression sourceValue = Expression.Property(functionParameter, mapPair.SourceProperty);
                UnaryExpression valueInDestinationType = Expression.Convert(sourceValue,
                    mapPair.DestinationProperty.PropertyType);
                MemberBinding assign = Expression.Bind(mapPair.DestinationProperty, valueInDestinationType);
                destinationInitialization.Add(assign);
            }

            MemberInitExpression functionBody = Expression.MemberInit(destinationCreation, destinationInitialization);
            Expression<Func<TSource, TDestination>> expressionTree =
                Expression.Lambda<Func<TSource, TDestination>>(functionBody, functionParameter);

            return expressionTree;

        }
    }
}
