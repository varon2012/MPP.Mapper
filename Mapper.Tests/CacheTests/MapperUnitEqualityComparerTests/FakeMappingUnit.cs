using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mapper.Configuration;
using Mapper.UnitsForMapping;

namespace Mapper.Tests.CacheTests.MapperUnitEqualityComparerTests
{
    internal class FakeMappingUnit : IMappingUnit
    {
        public Type Source { get; set; }
        public Type Destination { get; set; }
        public IMapperConfiguration Config { get; set; }
    }
}
