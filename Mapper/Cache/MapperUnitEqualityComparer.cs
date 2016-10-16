using System.Collections.Generic;

namespace Mapper.Cache
{
    internal class MapperUnitEqualityComparer : IEqualityComparer<MapperUnit>
    {
        public bool Equals(MapperUnit x, MapperUnit y)
        {
            if ((x == null) && (y == null))
                return true;
            if ((x == null) || (y == null))
                return false;
            if ((x.Source == y.Source) && (x.Destination == y.Destination) && (x.Config.Equals(y.Config)))
            {
                return true;
            }

            return false;
        }

        public int GetHashCode(MapperUnit obj)
        {
            unchecked
            {
                int primeNumForMultiply = 29;

                int hash = 17;

                hash = hash * primeNumForMultiply + obj.Source.GetHashCode();
                hash = hash * primeNumForMultiply + obj.Destination.GetHashCode();
                hash = hash * primeNumForMultiply + obj.Config.GetHashCode();

                return hash;
            }
        }
    }
}
