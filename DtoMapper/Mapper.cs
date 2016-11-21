using System;
using DtoMapper.FunctionCompiler;

namespace DtoMapper
{
    public class Mapper : IMapper
    {
        private readonly IFunctionCompiler functionCompiler;

        public Mapper()
        {
            functionCompiler = new CashFunctionCompiler();
        }

        public TDestination Map<TSource, TDestination>(TSource source) where TDestination : new()
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            Func<TSource, TDestination> function = functionCompiler.CompileMappingFunction<TSource, TDestination>();
            return function.Invoke(source);
        }
    }
}
