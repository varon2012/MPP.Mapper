using System;

namespace Mapper.Cache
{
    internal interface ICachedMapperCollection
    {
        void Add(MapperUnit unit, Delegate func);
        bool ContainsKey(MapperUnit unit);
        Delegate GetValue(MapperUnit unit);
    }
}
