using System;
using Mapper.Configuration;

namespace Mapper
{
    internal class MapperUnit
    {
        internal Type Source { get; set; }
        internal Type Destination { get; set; }
        internal IMapperConfiguration Config { get; set; }
    }
}
