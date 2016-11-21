using System;
using System.Collections.Generic;
using DtoMapper.Mapping;

namespace DtoMapper.FunctionCompiler
{
    public class FunctionCompiler : IFunctionCompiler
    {

        public Func<TSource, TDestination> CompileMappingFunction<TSource, TDestination>() where TDestination : new()
        {
            MappingExpressionTree<TSource, TDestination> expressionTree = new MappingExpressionTree<TSource, TDestination>();
            PropertyMapper<TSource, TDestination> propertyMapper = new PropertyMapper<TSource, TDestination>();

            IEnumerable<MappingPair> mapPairs = propertyMapper.PerformMapping();
            return expressionTree.Create(mapPairs).Compile();
        }

    }
}
