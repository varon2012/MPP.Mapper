using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using DtoMapper;

namespace DtoMapperTest
{
    public class MapperTests
    {
        [Fact]
        public void Map_WhenValueTypesAreEqual_ShouldAssignTheSameValue()
        {
            //arrange
            int valueToTest = 1234;
            Mapper underTest = new Mapper();
            Source source = new Source() {EqualValueTypeProperty = valueToTest};

            //act
            var result = underTest.Map<Source, Destination>(source);

            //assert
            var expected = valueToTest;
            var actual = result.EqualValueTypeProperty;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Map_WhenValueTypesCanBeCast_ShouldAssignTheSameValue()
        {
            //arrange
            float valueToTest = 1234.4321F;
            Mapper underTest = new Mapper();
            Source source = new Source() { CastValueTypeProperty = valueToTest };

            //act
            var result = underTest.Map<Source, Destination>(source);

            //assert
            var expected = valueToTest;
            var actual = result.CastValueTypeProperty;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Map_WhenValueTypesCanNotBeCast_ShouldBeZero()
        {
            //arrange
            long valueToTest = 123456789;
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

        [Fact]
        public void Create_WhenInputDataIncorrect_ShouldThrowException()
        {
            //arrange
            Mapper underTest = new Mapper(); ;

            //assert
            Assert.Throws<ArgumentNullException>(() => underTest.Map<Source, Destination>(null));
        }

    }
}
