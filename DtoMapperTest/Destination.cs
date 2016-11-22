using System.Collections.Generic;
using System.Text;

namespace DtoMapperTest
{
    public sealed class Destination
    {
        private sealed class DestinationEqualityComparer : IEqualityComparer<Destination>
        {
            public bool Equals(Destination x, Destination y)
            {
                if (ReferenceEquals(x, y)) return true;
                if (ReferenceEquals(x, null)) return false;
                if (ReferenceEquals(y, null)) return false;
                if (x.GetType() != y.GetType()) return false;
                return x.EqualValueTypeProperty == y.EqualValueTypeProperty && x.CastValueTypeProperty.Equals(y.CastValueTypeProperty) && x.NotCastTypeProperty == y.NotCastTypeProperty && Equals(x.EqualRefTypeProperty, y.EqualRefTypeProperty) && Equals(x.BaseRefTypeProperty, y.BaseRefTypeProperty) && Equals(x.DifferentRefTypeProperty, y.DifferentRefTypeProperty);
            }

            public int GetHashCode(Destination obj)
            {
                unchecked
                {
                    var hashCode = obj.EqualValueTypeProperty;
                    hashCode = (hashCode*397) ^ obj.CastValueTypeProperty.GetHashCode();
                    hashCode = (hashCode*397) ^ obj.NotCastTypeProperty.GetHashCode();
                    hashCode = (hashCode*397) ^ (obj.EqualRefTypeProperty != null ? obj.EqualRefTypeProperty.GetHashCode() : 0);
                    hashCode = (hashCode*397) ^ (obj.BaseRefTypeProperty != null ? obj.BaseRefTypeProperty.GetHashCode() : 0);
                    hashCode = (hashCode*397) ^ (obj.DifferentRefTypeProperty != null ? obj.DifferentRefTypeProperty.GetHashCode() : 0);
                    return hashCode;
                }
            }
        }

        private static readonly IEqualityComparer<Destination> DestinationComparerInstance = new DestinationEqualityComparer();

        public static IEqualityComparer<Destination> DestinationComparer
        {
            get { return DestinationComparerInstance; }
        }

        public int EqualValueTypeProperty { get; set; }
        public double CastValueTypeProperty { get; set; }
        public byte NotCastTypeProperty { get; set; }

        public List<int> EqualRefTypeProperty { get; set; }
        public IEnumerable<int> BaseRefTypeProperty { get; set; }
        public StringBuilder DifferentRefTypeProperty { get; set; }
    }
}
