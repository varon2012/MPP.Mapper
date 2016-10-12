using System;
using System.Collections.Generic;
using System.Reflection;

namespace Mapper.Cache
{
    internal class CachedMapperCollection : ICachedMapperCollection
    {
        public Dictionary<MapperUnit, Delegate> CachedData { get;}
        public CachedMapperCollection()
        {
            CachedData = new Dictionary<MapperUnit, Delegate>(new MapperUnitEqualityComparer());
        }

        public bool ContainsKey(MapperUnit unit)
        {
            return CachedData.ContainsKey(unit);
        }

        public void Add(MapperUnit unit, Delegate func)
        {
            CachedData.Add(unit, func);
        }

        public Delegate GetValue(MapperUnit unit)
        {
            return CachedData[unit];
        }
    }
}
