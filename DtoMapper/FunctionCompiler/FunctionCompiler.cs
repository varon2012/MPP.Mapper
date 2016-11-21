using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using DtoMapper.Mapping;
using DtoMapper.TypeConversion;

namespace DtoMapper.FunctionCompiler
{
    public class FunctionCompiler : IFunctionCompiler
    {

        public Func<TSource, TDestination> CompileMappingFunction<TSource, TDestination>() where TDestination : new()
        {
            MappingExpressionTree<TSource, TDestination> expressionTree = new MappingExpressionTree<TSource, TDestination>();
            PropertyMapper<TSource, TDestination> propertyMapper = new PropertyMapper<TSource, TDestination>();
            IEnumerable<MappingPair> mapPairs = propertyMapper.PerformMapping();

            Func<TSource, TDestination> compiledFunction = expressionTree.Create(mapPairs).Compile();
            return compiledFunction;
        }

    }
}
