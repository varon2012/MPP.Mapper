using System;
using System.Collections.Generic;

namespace DtoMapper.FunctionCompiler
{
    public class CashFunctionCompiler : IFunctionCompiler
    {
        private readonly IFunctionCompiler functionCompiler;
        private readonly Dictionary<KeyValuePair<Type, Type>, Delegate> functionCash;

        public CashFunctionCompiler()
        {
            functionCompiler = new FunctionCompiler();
            functionCash = new Dictionary<KeyValuePair<Type, Type>, Delegate>();
        }

        public Func<TSource, TDestination> CompileMappingFunction<TSource, TDestination>() where TDestination : new()
        {
            KeyValuePair<Type, Type> key = new KeyValuePair<Type, Type>(typeof(TSource), typeof(TDestination));
            Func<TSource, TDestination> mappingFunction = GetFromCash<TSource,TDestination>(key);

            if (mappingFunction == null)
            {
                mappingFunction = functionCompiler.CompileMappingFunction<TSource, TDestination>();
                PushToCash(key, mappingFunction);
            }

            return mappingFunction;
        }

        private void PushToCash(KeyValuePair<Type, Type> key, Delegate value)
        { 
            if (value == null)
                throw new ArgumentNullException(nameof(value));

            if (!functionCash.ContainsKey(key))
            {
                functionCash.Add(key, value);
            }
        }

        private Func<TSource, TDestination> GetFromCash<TSource, TDestination>(KeyValuePair<Type, Type> key)
        {
            return functionCash.ContainsKey(key) ? (Func<TSource, TDestination>)functionCash[key] : null;
        }
    }
}

