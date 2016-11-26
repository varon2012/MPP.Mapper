using System;
using System.Reflection;

namespace DtoMapper.Mapping
{
    public class MappingPair : IEquatable<MappingPair>
    {
        public PropertyInfo Source { get; set; }

        public PropertyInfo Destination { get; set; }

        public bool Equals(MappingPair other)
        {
            bool sourceComparing = other.Source != null
                ? other.Source.Name.Equals(Source.Name)
                : Source == null;
            bool destinationComparing = other.Destination != null
                ? other.Destination.Name.Equals(Destination.Name)
                : Destination == null;

            return sourceComparing && destinationComparing;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            if (obj == this)
                return true;
            if (obj.GetType() != typeof(MappingPair))
                return false;

            return Equals(obj as MappingPair);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int result = 17;
                result = 31*result + Source.Name.GetHashCode();
                result = 31*result + Destination.Name.GetHashCode();

                return result;
            }
            
        }

    }
}
