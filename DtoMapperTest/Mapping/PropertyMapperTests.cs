using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using DtoMapper.Mapping;
using Xunit;

namespace DtoMapperTest.Mapping
{
    public class PropertyMapperTests
    {
        [Fact]
        public void Mapping_WhenMappingPropertiesExists_ShouldReturnMappingProperties()
        {
            //arrange
            PropertyMapper<Source, Destination> underTest = new PropertyMapper<Source, Destination>();

            //act
            var result = underTest.PerformMapping();

            //assert
            var actual = result.OrderBy(element => element.Source.Name);
            IEnumerable<MappingPair> expected = TestDataFactory.CreatePropertyList().OrderBy(element => element.Source.Name);

            Assert.Equal(expected, actual);
        }
    }
}
