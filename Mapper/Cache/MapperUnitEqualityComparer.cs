using System;
using System.Collections.Generic;
using Mapper.UnitsForMapping;

namespace Mapper.Cache
{
    internal class MapperUnitEqualityComparer : IEqualityComparer<IMappingUnit>
    {
        public bool Equals(IMappingUnit x, IMappingUnit y)
        {
            if ((x == null) && (y == null))
                return true;
            if ((x == null) || (y == null))
                return false;

            if ((x.Source == y.Source) && (x.Destination == y.Destination) && (Equals(x.Config, y.Config)))
            {
                return true;
            }

            return false;
        }

        public int GetHashCode(IMappingUnit obj)
        {
            if (obj == null) throw new ArgumentNullException(nameof(obj));

            int sourceHash = obj.Source?.GetHashCode() ?? 0;
            int destinationHash = obj.Destination?.GetHashCode() ?? 0;
            int configHash = obj.Config?.GetHashCode() ?? 0;

            unchecked
            {
                int primeNumForMultiply = 29;

                int hash = 17;

                hash = hash * primeNumForMultiply + sourceHash;
                hash = hash * primeNumForMultiply + destinationHash;
                hash = hash * primeNumForMultiply + configHash;

                return hash;
            }
        }

    }
}
