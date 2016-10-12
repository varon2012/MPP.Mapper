using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mapper.Cache
{
    class MapperUnitEqualityComparer : IEqualityComparer<MapperUnit>
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
            var hashCode = obj.Source.GetHashCode() ^ obj.Destination.GetHashCode() ^ obj.Config.GetHashCode();
            return hashCode;
        }
    }
}
