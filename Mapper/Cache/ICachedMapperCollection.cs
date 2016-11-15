using System;
using Mapper.UnitsForMapping;

namespace Mapper.Cache
{
    public interface ICachedMapperCollection
    {
        void Add(IMappingUnit unit, Delegate func);
        bool ContainsKey(IMappingUnit unit);
        Delegate GetValue(IMappingUnit unit);
        long Count { get; }
    }
}
