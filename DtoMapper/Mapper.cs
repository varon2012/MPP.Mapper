using System;
using DtoMapper.FunctionCompiler;

namespace DtoMapper
{
    public class Mapper : IMapper
    {
        private readonly IFunctionCompiler functionCompiler;

        public Mapper()
        { 
            functionCompiler = new MappingFunctionCompiler();
        }

        public TDestination Map<TSource, TDestination>(TSource source) where TDestination : new()
        {

            Func<TSource, TDestination> function = functionCompiler.GetMappingFunction<TSource, TDestination>();
            return function.Invoke(source);

        }
    }
}
