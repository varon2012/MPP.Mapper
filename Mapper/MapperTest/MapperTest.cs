using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace MapperTest
{
    [TestFixture]
    public sealed class MapperTest
    {
        private static readonly Source Source = new Source
        {
            SameBaseType = true,
            CanConvertBaseType = 123,
            CanNotConvertBaseType = 456,
            SomeSourceBaseType = "source",
            NoSetterInDestination = 8,

            SameRefType = new List<int>(),
            NotSameRefType = new List<string>()
        };

        private static readonly Destination Destination = new Destination
        {
            SameBaseType = Source.SameBaseType,
            CanConvertBaseType = Source.CanConvertBaseType,
            
            SameRefType = Source.SameRefType
        };

        [Test]
        public void Map_Mapping_GetDestination()
        {
            var mapper = new Mapper.Mapper();

            var result = mapper.Map<Source, Destination>(Source);

            Assert.AreEqual(Destination, result);
        }

        [Test]
        public void Map_Mapping_GetDestinationFromCache()
        {
            var mapper = new Mapper.Mapper();

            mapper.Map<Source, Destination>(Source);
            var result = mapper.Map<Source, Destination>(Source);

            Assert.AreEqual(Destination, result);
        }

        [Test]
        public void Map_MappingToEmpty_GetEmpty()
        {
            var mapper = new Mapper.Mapper();

            var result = mapper.Map<Source, Empty>(Source);
            
            Assert.AreEqual(new Empty(), result);
        }

        [Test]
        public void Map_MappingFromEmpty_GetDestinationWithDefaultProperties()
        {
            var mapper = new Mapper.Mapper();

            var result = mapper.Map<Empty, Destination>(new Empty());

            Assert.AreEqual(new Destination(), result);
        }

        [Test]
        public void Map_MappingWhenSourceIsNull_ExceptionThrown()
        {
            var mapper = new Mapper.Mapper();

            Assert.Throws<ArgumentNullException>(() => mapper.Map<Source, Destination>(null));
        }
    }
}
