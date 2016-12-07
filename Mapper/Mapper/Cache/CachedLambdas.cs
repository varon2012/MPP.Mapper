using System;
using System.Collections.Generic;
using Mapper.Contracts;

namespace Mapper.Cache
{
    internal class CachedLambdas : ICacheLambdas
    {
        private static readonly Lazy<CachedLambdas> instance = new Lazy<CachedLambdas>(() => new CachedLambdas());

        private CachedLambdas()
        {
            cachedLambdas = new Dictionary<CachedTypes, Delegate>();
        }

        public static CachedLambdas GetInstance()
        {
            return instance.Value;
        }

        private readonly Dictionary<CachedTypes, Delegate> cachedLambdas;

        public Delegate GetLambda(CachedTypes key)
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

        public void AddLambda(CachedTypes key, Delegate value)
        {
            cachedLambdas.Add(key, value);
        }

        public bool ContainsKey(CachedTypes key)
        {
            return cachedLambdas.ContainsKey(key);
        }
    }
}
