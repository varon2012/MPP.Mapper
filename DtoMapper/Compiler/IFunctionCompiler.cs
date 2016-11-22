using System;

namespace DtoMapper.Compiler
{
    public interface IFunctionCompiler
    {
        Func<TSource, TDestination> CompileMappingFunction<TSource, TDestination>() where TDestination : new();
    }
}
