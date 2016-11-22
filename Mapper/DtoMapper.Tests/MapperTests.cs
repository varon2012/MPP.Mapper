using System;
using NUnit.Framework;

namespace DtoMapper.Tests
{
    [TestFixture]
    public sealed class MapperTests
    {
        private static readonly Source Source = new Source
        {
            SameType = "Test",
            NotSameType = true,
            Convertible = 12,
            NonConvertible = 3.22,
            ThereIsNoSuchPropertyInDestination = 3
        };

        private static readonly Destination Destination = new Destination
        {
            SameType = Source.SameType,
            Convertible = Source.Convertible
        };

        private static readonly Source SourceForCacheTest = new Source
        {
            SameType = "TestWithCaching",
            NotSameType = false,
            Convertible = 21,
            NonConvertible = 7.22,
            ThereIsNoSuchPropertyInDestination = 11
        };

        private static readonly Destination DestinationForCahceTest = new Destination
        {
            SameType = SourceForCacheTest.SameType,
            Convertible = SourceForCacheTest.Convertible
        };

        [Test]
        public void Map_NullPassed_ExceptionThrown()
        {
            var mapper = new Mapper();

            Assert.Throws<ArgumentNullException>(() => mapper.Map<object, object>(null));
        }

        [Test]
        public void Map_SingleMapping()
        {
            var mapper = new Mapper();
            var result = mapper.Map<Source, Destination>(Source);

            Assert.AreEqual(Destination, result);
        }

        [Test]
        public void Map_MappingWithCache()
        {
            var mapper = new Mapper();

            // Let's create map Source -> Destination in cache
            mapper.Map<Source, Destination>(Source);

            var result = mapper.Map<Source, Destination>(SourceForCacheTest);

            Assert.AreEqual(DestinationForCahceTest, result);
        }

        [Test]
        public void Map_MappingFromEmptyObject_GettingPureObject()
        {
            var mapper = new Mapper();

            var result = mapper.Map<Empty, Destination>(new Empty());

            Assert.AreEqual(new Destination(), result);
        }

        [Test]
        public void Map_MappingToEmptyObject_GettingEmptyObject()
        {
            var mapper = new Mapper();

            var result = mapper.Map<Source, Empty>(Source);

            Assert.AreEqual(new Empty(), result);
        }
    }
}
