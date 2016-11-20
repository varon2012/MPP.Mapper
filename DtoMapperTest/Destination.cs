using System.Collections.Generic;
using System.Text;

namespace DtoMapperTest
{
    public sealed class Destination
    {
        public int EqualValueTypeProperty { get; set; }
        public double CastValueTypeProperty { get; set; }
        public byte NotCastTypeProperty { get; set; }

        public List<int> EqualRefTypeProperty { get; set; }
        public IEnumerable<int> BaseRefTypeProperty { get; set; }
        public StringBuilder DifferentRefTypeProperty { get; set; }
    }
}
