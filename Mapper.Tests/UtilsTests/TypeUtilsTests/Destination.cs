using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mapper.Tests.UtilsTests.TypeUtilsTests
{
    class Destination
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
