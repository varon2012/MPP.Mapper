using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mapper.UnitsForMapping;

namespace Mapper.Tests.CacheTests.CachedMapperCollectionTests
{
    internal class FakeMapperEqualityComparer : IEqualityComparer<IMappingUnit>
    {
        internal int ReturningHashCode { get; set; } = 117;

        public bool Equals(IMappingUnit x, IMappingUnit y)
        {
            if ((x == null) && (y == null))
                return true;
            if ((x == null) || (y == null))
                return false;

            return true;
        }

        public int GetHashCode(IMappingUnit obj)
        {
            return ReturningHashCode;
        }
    }
}
