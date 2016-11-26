using System;
using System.Collections.Generic;
using DtoMapper.Mapping;

namespace DtoMapper.Compiler
{
    public class FunctionCompiler : IFunctionCompiler
    {
        public Func<TSource, TDestination> CompileMappingFunction<TSource, TDestination>() where TDestination : new()
        {
            ExpressionTreeGenerator<TSource, TDestination> expressionTreeGenerator = new ExpressionTreeGenerator<TSource, TDestination>();
            PropertyMapper<TSource, TDestination> propertyMapper = new PropertyMapper<TSource, TDestination>();

            IEnumerable<MappingPair> mapPairs = propertyMapper.PerformMapping();
            return expressionTreeGenerator.Create(mapPairs).Compile();
        }
    }
}
