using System;
using System.Collections.Generic;

namespace DtoMapperTest
{
    public sealed class Source
    {
        public int EqualValueTypeProperty { get; set; }
        public float CastValueTypeProperty { get; set; }
        public long NotCastTypeProperty { get; set; }

        public List<int> EqualRefTypeProperty { get; set; }
        public List<int> BaseRefTypeProperty { get; set; }
        public DateTime DifferentRefTypeProperty { get; set; }

    }
}
