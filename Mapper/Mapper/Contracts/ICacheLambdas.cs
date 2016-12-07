using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mapper.Cache;
using Mapper.PropertyInfoStorage;

namespace Mapper.Contracts
{
    public interface ICacheLambdas
    {
        Delegate GetLambda(TwoValuesPair<Type, Type> key);
        void AddLambda(TwoValuesPair<Type, Type> key, Delegate value);
        bool ContainsKey(TwoValuesPair<Type, Type> key);
    }
}
