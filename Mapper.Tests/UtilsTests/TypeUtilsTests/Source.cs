using System;

namespace Mapper.Tests.UtilsTests.TypeUtilsTests
{
    internal class Source
    {
        public int FirstProperty { get; set; }
        public string SecondProperty { get; set; }
        public double ThirdProperty { get; set; }
        public float FourthProperty { get; set; }
        public DateTime FifthProperty { get; set; }

        public object ObjProperty { get; set; }

        public byte ImplicitlyConvertibleProperty { get; set; }
    }
}
