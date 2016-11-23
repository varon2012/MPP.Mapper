using System.Reflection;

namespace Mapper
{
    internal struct MappingPropertyPair
    {
        public PropertyInfo Source { get; set; }
        public PropertyInfo Destination { get; set; }

        public MappingPropertyPair(PropertyInfo source, PropertyInfo destination)
        {
            Source = source;
            Destination = destination;
        }
    }
}
