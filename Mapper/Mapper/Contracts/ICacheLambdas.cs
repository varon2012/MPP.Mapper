using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mapper.Cache;

namespace Mapper.Contracts
{
    interface ICacheLambdas
    {
        Delegate GetLambda(CachedTypes key);
        void AddLambda(CachedTypes key, Delegate value);
        bool ContainsKey(CachedTypes key);
    }
}
