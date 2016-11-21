using System;
using System.Reflection;

namespace DtoMapper.Mapping
{
    public class MappingPair
    {
        public PropertyInfo Source { get; set; }

        public PropertyInfo Destination { get; set; }
    }
}
