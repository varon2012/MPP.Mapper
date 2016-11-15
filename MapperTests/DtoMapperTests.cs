using System;
using Xunit;
using Mapper;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Moq;

namespace Mapper.Tests
{
    public class DtoMapperTests
    {
        private static readonly Source Source = new Source
        {
            CanConvert = 1,
            SameType = "a",
            CantConvert = 3.14,
            SubclassAndClass = new FooSubclass(),
            Name = "a"
        };

        private static readonly Destination ExpectedWithoutConfiguration = new Destination
        {
            CanConvert = Source.CanConvert,
            SameType = Source.SameType,
        };

        private static readonly Destination ExpectedWithConfiguration = new Destination
        {
            CanConvert = Source.CanConvert,
            SameType = Source.SameType,
            FirstName = Source.Name
        };

        [Fact]
        public void Map_NullPassed_ExceptionThrown()
        {
            IMapper mapper = new DtoMapper();

            Assert.Throws<ArgumentNullException>(() => mapper.Map<object, object>(null));
        }

        [Fact]
        public void Map_SimpleMapping()
        {
            IMapper mapper = new DtoMapper();

            Destination actual = mapper.Map<Source, Destination>(Source);

            Assert.Equal(ExpectedWithoutConfiguration, actual);
        }

        [Fact]
        public void Map_MappingUsingCache()
        {
            IMapper mapper = new DtoMapper();
            mapper.Map<Source, Destination>(Source); //create cache for "Source -> Destination" pair

            Destination actual = mapper.Map<Source, Destination>(Source);

            Assert.Equal(ExpectedWithoutConfiguration, actual);
        }

        [Fact]
        public void Map_MappingUsingConfiguration()
        {
            MapperConfiguration mapperConfiguration = new MapperConfiguration();
            mapperConfiguration.Register<Source, Destination, string>(source => source.Name, destination => destination.FirstName);
            IMapper mapper = new DtoMapper(mapperConfiguration);

            Destination actual = mapper.Map<Source, Destination>(Source);

            Assert.Equal(ExpectedWithConfiguration, actual);
        }

        [Fact]
        public void Map_CacheMiss_GetCacheForDidNotCalled_CreateMappingFunctionCalled()
        {
            var mockCache = new Mock<IMappingFunctionsCache>();
            mockCache.Setup(cache => cache.HasCacheFor(It.IsAny<MappingTypesPair>())).Returns(false);
            Mock<IMappingFunctionsFactory> mockFactory = CreateFakeMappingFunctionsFactory();            
            IMapper mapper = new DtoMapper(mockCache.Object, mockFactory.Object);

            mapper.Map<object, object>(new object());

            mockCache.Verify(cache => cache.GetCacheFor<object, object>(It.IsAny<MappingTypesPair>()), Times.Never);
            mockFactory.Verify(factory => factory.CreateMappingFunction<object, object>(It.IsAny<List<MappingPropertiesPair>>()), Times.Once);
        }

        [Fact]
        public void Map_CacheHit_GetCacheForCalled_CreateMappingFunctionDidNotCalled()
        {
            var mockCache = new Mock<IMappingFunctionsCache>();
            mockCache.Setup(mock => mock.HasCacheFor(It.IsAny<MappingTypesPair>())).Returns(true);
            mockCache.Setup(mock => mock.GetCacheFor<object, object>(It.IsAny<MappingTypesPair>())).Returns(x => x);

            Mock<IMappingFunctionsFactory> mockFactory = CreateFakeMappingFunctionsFactory();

            IMapper mapper = new DtoMapper(mockCache.Object, mockFactory.Object);
            mapper.Map<object, object>(new object());

            mockCache.Verify(cache => cache.GetCacheFor<object, object>(It.IsAny<MappingTypesPair>()), Times.Once);
            mockFactory.Verify(factory => factory.CreateMappingFunction<object, object>(It.IsAny<List<MappingPropertiesPair>>()), Times.Never);
        }

        // Internals

        private Mock<IMappingFunctionsFactory> CreateFakeMappingFunctionsFactory()
        {
            var result = new Mock<IMappingFunctionsFactory>();
            result.Setup(factory => factory.CreateMappingFunction<object, object>(It.IsAny<List<MappingPropertiesPair>>())).Returns(x => x);
            
            return result;
        }
    }
}