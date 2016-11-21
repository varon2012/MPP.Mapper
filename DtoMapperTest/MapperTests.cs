using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using DtoMapper;

namespace DtoMapperTest
{
    public class MapperTests
    {
        [Theory]
        [InlineData(1234)]
        public void Map_WhenValueTypesAreEqual_ShouldAssignTheSameValue(int valueToTest)
        {
            //arrange
            Mapper underTest = new Mapper();
            Source source = new Source() {EqualValueTypeProperty = valueToTest};

            //act
            var result = underTest.Map<Source, Destination>(source);

            //assert
            var expected = valueToTest;
            var actual = result.EqualValueTypeProperty;

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(1234.4321F)]
        public void Map_WhenValueTypesCanBeCast_ShouldAssignTheSameValue(float valueToTest)
        {
            //arrange
            Mapper underTest = new Mapper();
            Source source = new Source() { CastValueTypeProperty = valueToTest };

            //act
            var result = underTest.Map<Source, Destination>(source);

            //assert
            var expected = valueToTest;
            var actual = result.CastValueTypeProperty;

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(123456789)]
        public void Map_WhenValueTypesCanNotBeCast_ShouldBeZero(long valueToTest)
        {
            //arrange
            Mapper underTest = new Mapper();
            Source source = new Source() { NotCastTypeProperty = valueToTest };

            //act
            var result = underTest.Map<Source, Destination>(source);

            //assert
            var expected = 0;
            var actual = result.NotCastTypeProperty;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Map_WhenRefTypesAreEqual_ShouldAssignTheSameValue()
        {
            //arrange
            List<int> valueToTest = new List<int>();
            Mapper underTest = new Mapper();
            Source source = new Source() { EqualRefTypeProperty = valueToTest };

            //act
            var result = underTest.Map<Source, Destination>(source);

            //assert
            var expected = valueToTest;
            var actual = result.EqualRefTypeProperty;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Map_WhenRefTypesCanBeCast_ShouldAssignTheSameValue()
        {
            //arrange
            List<int> valueToTest = new List<int>();
            Mapper underTest = new Mapper();
            Source source = new Source() { BaseRefTypeProperty = valueToTest };

            //act
            var result = underTest.Map<Source, Destination>(source);

            //assert
            var expected = valueToTest;
            var actual = result.BaseRefTypeProperty;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Map_WhenRefTypesCanNotBeCast_ShouldBeNull()
        {
            //arrange
            DateTime valueToTest = new DateTime();
            Mapper underTest = new Mapper();
            Source source = new Source() { DifferentRefTypeProperty = valueToTest };

            //act
            var result = underTest.Map<Source, Destination>(source);

            //assert
            StringBuilder expected = null;
            var actual = result.DifferentRefTypeProperty;

            Assert.Equal(expected, actual);
        }

    }
}
