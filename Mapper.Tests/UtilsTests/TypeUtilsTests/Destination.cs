using System;

namespace Mapper.Tests.UtilsTests.TypeUtilsTests
{
    internal class Destination
    {
        public int FirstProperty { get; set; }
        public string SecondProperty { get; set; }
        public byte ThirdProperty { get; set; }
        public float FourthProperty { get; }
        public DateTime FifthProperty { get; set; }


        public Destination ObjProperty { get; set; }


        public int ImplicitlyConvertibleProperty { get; set; }
    }
}
