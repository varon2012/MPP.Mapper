using System;

namespace Mapper
{
    public class MappingEntryInfo
    {
        public Type Source { get; set; }
        public Type Destination { get; set; }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof(MappingEntryInfo)) return false;
            return Equals((MappingEntryInfo) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((Source != null ? Source.GetHashCode() : 0)*397) ^ (Destination != null ? Destination.GetHashCode() : 0);
            }
        }

        // Internals

        protected bool Equals(MappingEntryInfo other)
        {
            return Source == other.Source && Destination == other.Destination;
        }

       
    }
}
