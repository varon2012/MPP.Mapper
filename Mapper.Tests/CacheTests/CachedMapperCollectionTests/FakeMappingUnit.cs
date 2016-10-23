using System;
using Mapper.Configuration;
using Mapper.UnitsForMapping;

namespace Mapper.Tests.CacheTests.CachedMapperCollectionTests
{
    internal class FakeMappingUnit : IMappingUnit
    {
        public Type Source { get; set; }
        public Type Destination { get; set; }
        public IMapperConfiguration Config { get; set; }
    }
}
