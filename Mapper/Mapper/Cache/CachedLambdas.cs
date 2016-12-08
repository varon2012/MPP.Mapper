using System;
using System.Collections.Generic;
using Mapper.Contracts;
using Mapper.PropertyInfoStorage;

namespace Mapper.Cache
{
    public class CachedLambdas : ICacheLambdas
    {
        private static readonly Lazy<CachedLambdas> instance = new Lazy<CachedLambdas>(() => new CachedLambdas());

        private CachedLambdas()
        {
            cachedLambdas = new Dictionary<TwoValuesPair<Type, Type>, Delegate>();
        }

        public static CachedLambdas GetInstance()
        {
            return instance.Value;
        }

        private readonly Dictionary<TwoValuesPair<Type, Type>, Delegate> cachedLambdas;

        public Delegate GetLambda(TwoValuesPair<Type, Type> key)
        {
            try
            {
                return cachedLambdas[key];
            }
            catch (KeyNotFoundException)
            {
                return null;
            }
        }

        public void AddLambda(TwoValuesPair<Type, Type> key, Delegate value)
        {
            cachedLambdas.Add(key, value);
        }

        public bool ContainsKey(TwoValuesPair<Type, Type> key)
        {
            return cachedLambdas.ContainsKey(key);
        }
    }
}
