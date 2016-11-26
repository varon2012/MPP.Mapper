using System;

namespace Mapper.Cache
{
    internal struct TypePair
    {
        public Type SourceType { get; private set; }
        public Type DestinationType { get; private set; }

        public TypePair(Type sourceType, Type destinationType)
        {
            SourceType = sourceType;
            DestinationType = destinationType;
        }
    }
}
