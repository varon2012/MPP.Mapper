using System;
using System.Collections.Generic;

namespace DtoMapper.FunctionCompiler
{
    internal class FunctionCash 
    {
        private readonly Dictionary<KeyValuePair<Type, Type>, Delegate> cash;

        public FunctionCash()
        {
            cash = new Dictionary<KeyValuePair<Type, Type>, Delegate>();
        }

        public void PushToCash(KeyValuePair<Type, Type> key, Delegate value)
        {

            if (value == null)
                throw new ArgumentNullException(nameof(value));

            if (!cash.ContainsKey(key))
            {
                cash.Add(key, value);
            }

        }

        public Delegate GetFromCash(KeyValuePair<Type, Type> key)
        {
            return cash.ContainsKey(key) ? cash[key] : null;
        }
    }
}
