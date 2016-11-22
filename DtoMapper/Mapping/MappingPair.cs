using System.Reflection;

namespace DtoMapper.Mapping
{
    public class MappingPair
    {
        public PropertyInfo Source { get; set; }

        public PropertyInfo Destination { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            if (obj == this)
                return true;
            if (obj.GetType() != typeof(MappingPair))
                return false;

            MappingPair comparingPair = obj as MappingPair;

            bool sourceComparing = comparingPair.Source != null 
                ? comparingPair.Source.Name.Equals(Source.Name) 
                : Source == null;
            bool destinationComparing = comparingPair.Destination != null
                ? comparingPair.Destination.Name.Equals(Destination.Name)
                : Destination == null;

            return sourceComparing && destinationComparing;
        }

        public override int GetHashCode()
        {
            int result = 17;
            result = 31*result + Source.Name.GetHashCode();
            result = 31*result + Destination.Name.GetHashCode();

            return result;
        }

    }
}
