using System;
using Mapper.Configuration;

namespace Mapper.UnitsForMapping
{
    internal interface IMappingUnit
    {
        Type Source { get; set; }
        Type Destination { get; set; }
        IMapperConfiguration Config { get; set; }
    }
}
