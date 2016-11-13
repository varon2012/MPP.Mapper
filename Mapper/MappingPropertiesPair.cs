using System.Reflection;

namespace Mapper
{
    public class MappingPropertiesPair
    {
        public PropertyInfo SourceProperty { get; set; }
        public PropertyInfo DestinationProperty { get; set; }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((MappingPropertiesPair) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((SourceProperty != null ? SourceProperty.GetHashCode() : 0)*397) ^ (DestinationProperty != null ? DestinationProperty.GetHashCode() : 0);
            }
        }

        // Internals

        protected bool Equals(MappingPropertiesPair other)
        {
            return Equals(SourceProperty, other.SourceProperty) && Equals(DestinationProperty, other.DestinationProperty);
        }
    }
}