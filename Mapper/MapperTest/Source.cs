using System.Collections.Generic;

namespace MapperTest
{
    internal class Source
    {
        public bool SameBaseType { get; set; }
        public byte CanConvertBaseType { get; set; }
        public double CanNotConvertBaseType { get; set; }
        public string SomeSourceBaseType { get; set; }
        public short NoSetterInDestination { get; set; }

        public List<int> SameRefType { get; set; }
        public List<string> NotSameRefType { get; set; }
    }
}