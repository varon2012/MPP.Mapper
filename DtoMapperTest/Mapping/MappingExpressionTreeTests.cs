using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using DtoMapper.Mapping;
using Xunit;

namespace DtoMapperTest.Mapping
{

    public class MappingExpressionTreeTests
    {
        [Fact]
        public void Create_WhenInputDataCorrect_ShouldReturnCorrectFunction()
        {
            //arrange
            MappingExpressionTree<Source, Destination> underTest = new MappingExpressionTree<Source, Destination>();
            var valueToTest = TestDataFactory.CreatePropertyList();
            Source parameter = new Source()
            {
                EqualValueTypeProperty = 12,
                CastValueTypeProperty = 13.5F,
                NotCastTypeProperty = 48,

                EqualRefTypeProperty = new List<int>(),
                BaseRefTypeProperty = new List<int>(),
                DifferentRefTypeProperty = new DateTime()
            };
            Expression<Func<Source, Destination>> expectedBehavior = source => new Destination()
            {
                EqualValueTypeProperty = source.EqualValueTypeProperty,
                CastValueTypeProperty = source.CastValueTypeProperty,

                EqualRefTypeProperty = source.EqualRefTypeProperty,
                BaseRefTypeProperty = source.BaseRefTypeProperty
            };

            //act
            var actualBehavior = underTest.Create(valueToTest);

            //assert
            var actual = actualBehavior.Compile().Invoke(parameter);
            var expect = expectedBehavior.Compile().Invoke(parameter);

            Assert.Equal(actual, expect, Destination.DestinationComparer);
        }

        [Fact]
        public void Create_WhenInputDataIncorrect_ShouldThrowException()
        {
            //arrange
            MappingExpressionTree<Source, Destination> underTest = new MappingExpressionTree<Source, Destination>();

            //assert
            Assert.Throws<ArgumentNullException>(() => underTest.Create(null));
        }
    }

}
