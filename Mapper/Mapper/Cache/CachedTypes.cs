using System;

namespace Mapper.Cache
{
    internal struct CachedTypes
    {
        public CachedTypes(Type source, Type destination)
        {
            Source = source;
            Destination = destination;
        }

        public Type Source { get; set; }
        public Type Destination { get; set; }
    }
}
