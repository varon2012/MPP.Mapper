using System;

namespace DtoMapper.FunctionCompiler
{
    internal interface IFunctionCompiler
    {
        Func<TSource, TDestination> GetMappingFunction<TSource, TDestination>() where TDestination : new();
    }
}
