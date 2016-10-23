using System;
using System.Collections.Generic;
using Mapper.UnitsForMapping;


namespace Mapper.Cache
{
    internal class CachedMapperCollection : ICachedMapperCollection
    {
        private readonly Dictionary<IMappingUnit, Delegate> cachedData;
        internal CachedMapperCollection()
        {
            cachedData = new Dictionary<IMappingUnit, Delegate>(new MapperUnitEqualityComparer());
        }

        internal CachedMapperCollection(IEqualityComparer<IMappingUnit> equalityComparer)
        {
            cachedData = new Dictionary<IMappingUnit, Delegate>(equalityComparer);
        }

        public bool ContainsKey(IMappingUnit unit)
        {
            return cachedData.ContainsKey(unit);
        }

        public void Add(IMappingUnit unit, Delegate func)
        {
            cachedData.Add(unit, func);
        }

        public Delegate GetValue(IMappingUnit unit)
        {
            return cachedData[unit];
        }

        public long Count => cachedData.Count;
    }
}
