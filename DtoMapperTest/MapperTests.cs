using System;
using System.Collections.Generic;
using Xunit;
using DtoMapper;

namespace DtoMapperTest
{
    public class MapperTests
    {
        [Fact]
        public void Map_WhenValueTypesAreEqual_ShouldAssignTheSameValue()
        {

            Mapper mapper = new Mapper();
            Source source = new Source();

            var expected = 1234;
            source.EqualValueTypeProperty = expected;

            var actual = mapper.Map<Source, Destination>(source).EqualValueTypeProperty;

            Assert.Equal(expected, actual);

        }

        [Fact]
        public void Map_WhenValueTypesCanBeCast_ShouldAssignTheSameValue()
        {

            Mapper mapper = new Mapper();
            Source source = new Source();

            var expected = 1234.4321F;
            source.CastValueTypeProperty = expected;

            var actual = mapper.Map<Source, Destination>(source).CastValueTypeProperty;

            Assert.Equal(expected, actual);

        }

        [Fact]
        public void Map_WhenValueTypesCanNotBeCast_ShouldBeZero()
        {

            Mapper mapper = new Mapper();
            Source source = new Source();

            var expected = 0;
            source.NotCastTypeProperty = 123456789;

            var actual = mapper.Map<Source, Destination>(source).NotCastTypeProperty;

            Assert.Equal(expected, actual);

        }

        [Fact]
        public void Map_WhenRefTypesAreEqual_ShouldAssignTheSameValue()
        {

            Mapper mapper = new Mapper();
            Source source = new Source();

            var expected = new List<int>();
            source.EqualRefTypeProperty = expected;

            var actual = mapper.Map<Source, Destination>(source).EqualRefTypeProperty;

            Assert.Equal(expected, actual);

        }

        [Fact]
        public void Map_WhenRefTypesCanBeCast_ShouldAssignTheSameValue()
        {

            Mapper mapper = new Mapper();
            Source source = new Source();

            var expected = new List<int>();
            source.BaseRefTypeProperty = expected;

            var actual = mapper.Map<Source, Destination>(source).BaseRefTypeProperty;

            Assert.Equal(expected, actual);

        }

        [Fact]
        public void Map_WhenRefTypesCanNotBeCast_ShouldBeNull()
        {

            Mapper mapper = new Mapper();
            Source source = new Source();

            source.DifferentRefTypeProperty = new DateTime();

            var actual = mapper.Map<Source, Destination>(source).DifferentRefTypeProperty;
            Assert.Equal(null, actual);

        }

    }
}
