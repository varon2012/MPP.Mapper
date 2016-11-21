using System;

namespace DtoMapper.FunctionCompiler
{
    public interface IFunctionCompiler
    {
        Func<TSource, TDestination> CompileMappingFunction<TSource, TDestination>() where TDestination : new();
    }
}
