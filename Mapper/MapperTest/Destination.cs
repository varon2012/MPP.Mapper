using System.Collections.Generic;

namespace MapperTest
{
    internal class Destination
    {
        public bool SameBaseType { get; set; }
        public int CanConvertBaseType { get; set; }
        public float CanNotConvertBaseType { get; set; }
        public string SomeDestinationBaseType { get; set; }
        public short NoSetterInDestination { get; }

        public List<int> SameRefType { get; set; }
        public IList<string> NotSameRefType { get; set; }

        public override bool Equals(object obj)
        {
            var destination = obj as Destination;

            if (ReferenceEquals(null, destination)) return false;
            if (ReferenceEquals(this, destination)) return true;

            return Equals(SameBaseType, destination.SameBaseType) &&
                   Equals(CanConvertBaseType, destination.CanConvertBaseType) &&
                   Equals(CanNotConvertBaseType, destination.CanNotConvertBaseType) &&
                   Equals(SomeDestinationBaseType, destination.SomeDestinationBaseType) &&
                   Equals(NoSetterInDestination, destination.NoSetterInDestination) &&
                   Equals(SameRefType, destination.SameRefType) &&
                   Equals(NotSameRefType, destination.NotSameRefType);
        }

        public override int GetHashCode()
        {
            return 24;
        }
    }
}