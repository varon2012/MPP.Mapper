using System;
using Mapper.Configuration;

namespace Mapper.UnitsForMapping
{
    internal class MappingUnit : IMappingUnit
    {
        public Type Source { get; set; }
        public Type Destination { get; set; }
        public IMapperConfiguration Config { get; set; }
    }
}
