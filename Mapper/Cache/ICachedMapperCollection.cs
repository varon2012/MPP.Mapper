using System;
using System.Collections.Generic;

namespace Mapper.Cache
{
    internal interface ICachedMapperCollection
    {
        Dictionary<MapperUnit, Delegate> CachedData { get;}
        void Add(MapperUnit unit, Delegate func);
        bool ContainsKey(MapperUnit unit);
        Delegate GetValue(MapperUnit unit);
    }
}
