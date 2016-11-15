using System;
using Mapper.Configuration;

namespace Mapper.UnitsForMapping
{
    public interface IMappingUnit
    {
        Type Source { get; set; }
        Type Destination { get; set; }
        IMapperConfiguration Config { get; set; }
    }
}
