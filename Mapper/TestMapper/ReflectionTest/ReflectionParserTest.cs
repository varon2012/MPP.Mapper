using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mapper.Contracts;
using Mapper.Reflection;
using NUnit.Framework;
namespace TestMapper.ReflectionTest
{
    [TestFixture]
    internal class ReflectionParserTest
    {
        [Test]
        public void ReflectionParserGetSameProperties_PassDifferenTypes_PropertiesListCountIs0()
        {
            IReflectionParser parser = new ReflectionParser();

            var result = parser.GetSameProperties<Source, Destination>();

            Assert.AreEqual(0, result.Count);
        }
    }

    internal class Source
    {
        public double FirstProperty { get; set; }
        public short SecondProperty { get; set; }
        public Type ThirdProperty { get; set; }
    }

    internal class Destination
    {
        public float FirstProperty { get; set; }
        public DateTime SecondProperty { get; set; }
    }
}
