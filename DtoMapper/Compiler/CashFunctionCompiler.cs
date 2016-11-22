using System;
using System.Collections.Generic;

namespace DtoMapper.Compiler
{
    public class CashFunctionCompiler : IFunctionCompiler
    {
        private readonly IFunctionCompiler functionCompiler;
        private readonly Dictionary<KeyValuePair<Type, Type>, Delegate> functionCash;

        public CashFunctionCompiler() : this(new FunctionCompiler())
        {
            functionCash = new Dictionary<KeyValuePair<Type, Type>, Delegate>();
        }

        public CashFunctionCompiler(IFunctionCompiler functionCompiler)
        {
            this.functionCompiler = functionCompiler;
        }

        public Func<TSource, TDestination> CompileMappingFunction<TSource, TDestination>() where TDestination : new()
        {
            KeyValuePair<Type, Type> key = new KeyValuePair<Type, Type>(typeof(TSource), typeof(TDestination));

            if (IsInCash(key))
            {
                return GetFromCash<TSource, TDestination>(key);
            }

            Func<TSource, TDestination> mappingFunction = functionCompiler.CompileMappingFunction<TSource, TDestination>();
            PushToCash(key, mappingFunction);
            
            return mappingFunction;
        }

        private bool IsInCash(KeyValuePair<Type, Type> key)
        {
            return functionCash.ContainsKey(key);
        }

        private void PushToCash(KeyValuePair<Type, Type> key, Delegate value)
        { 
            functionCash.Add(key, value);
        }

        private Func<TSource, TDestination> GetFromCash<TSource, TDestination>(KeyValuePair<Type, Type> key)
        {
            return (Func<TSource, TDestination>)functionCash[key];
        }
    }
}

