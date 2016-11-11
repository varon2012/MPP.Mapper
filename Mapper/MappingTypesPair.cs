using System;

namespace Mapper
{
    internal class MappingTypesPair
    {
        public Type Source { get; set; }
        public Type Destination { get; set; }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof(MappingTypesPair)) return false;
            return Equals((MappingTypesPair) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((Source != null ? Source.GetHashCode() : 0)*397) ^ (Destination != null ? Destination.GetHashCode() : 0);
            }
        }

        // Internals

        protected bool Equals(MappingTypesPair other)
        {
            return Source == other.Source && Destination == other.Destination;
        }

       
    }
}
