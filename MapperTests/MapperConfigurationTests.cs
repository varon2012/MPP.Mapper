using Xunit;
using Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mapper.Tests
{
    public class MapperConfigurationTests
    {
        [Fact]
        public void Register_NullPassed_ExceptionThrown()
        {
            MapperConfiguration mapperConfiguration = new MapperConfiguration();
            Assert.Throws<ArgumentNullException>(() => mapperConfiguration.Register<object, object, object>(null, null));
        }

        [Fact]
        public void Register_IncompatiblePrimitiveTypes_ExceptionThrown()
        {
            MapperConfiguration mapperConfiguration = new MapperConfiguration();
            Assert.Throws<ArgumentException>(
                () =>
                    mapperConfiguration.Register<Source, Destination, double>(source => source.CantConvert,
                        destination => destination.CantConvert));
        }

        [Fact]
        public void Register_IncompatibeNonprimitiveTypes_ExceptionThrown()
        {
            MapperConfiguration mapperConfiguration = new MapperConfiguration();
            Assert.Throws<ArgumentException>(
                () =>
                    mapperConfiguration.Register<Source, Destination, Foo>(source => source.SubclassAndClass,
                        destination => destination.SubclassAndClass));
        }

        [Fact]
        public void Register_ReadonlyDestinationProperty_ExceptionThrown()
        {
            MapperConfiguration mapperConfiguration = new MapperConfiguration();
            Assert.Throws<ArgumentException>(
                () =>
                    mapperConfiguration.Register<Source, Destination, object>(source => source.CantAssign,
                        destination => destination.CantAssign));
        }
    }
}