using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mapper.Tests.ConfigurationTests.MapperConfigurationTests
{
    internal class Source
    {
        public int FirstProperty { get; set; }
        public string SecondProperty { get; set; }
        public double ThirdProperty { get; set; }
        public float FourthProperty { get; set; }
        public DateTime FifthProperty { get; set; }

        public int IntPropertyWithAnotherName { get; set; }

        public int SomeField;

        public int GetSomeInt()
        {
            return 42;
        }
    }
}
