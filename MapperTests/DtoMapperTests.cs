using System;
using Xunit;
using Mapper;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;

namespace Mapper.Tests
{
    public class DtoMapperTests
    {
        private static readonly Source Source = new Source
        {
            FirstProperty = 1,
            SecondProperty = "a",
            ThirdProperty = 3.14,
            FourthProperty = 2
        };

        private static readonly Destination Expected = new Destination
        {
            FirstProperty = Source.FirstProperty,
            SecondProperty = Source.SecondProperty,
        };

        [Fact]
        public void Map_NullPassed_ExceptionThrown()
        {
            IMapper mapper = new DtoMapper();
            Assert.Throws<ArgumentNullException>(() => mapper.Map<object, object>(null));
        }

        [Fact]
        public void Map_MappingWithoutCache()
        {
            IMapper mapper = new DtoMapper();
            Destination actual = mapper.Map<Source, Destination>(Source);
            Assert.Equal(Expected, actual);
        }

        [Fact]
        public void Map_MappingUsingCache()
        {
            IMapper mapper = new DtoMapper();
            mapper.Map<Source, Destination>(Source); //create cache for "Source -> Destination" pair
            Destination actual = mapper.Map<Source, Destination>(Source);
            Assert.Equal(Expected, actual);
        }

        [Fact]
        public void Map_CacheMiss_GetCacheForDidNotCalled_CreateMappingFunctionCalled()
        {
            var mockCache = new Mock<IMappingFunctionsCache>();
            mockCache.Setup(cache => cache.HasCacheFor(It.IsAny<MappingEntryInfo>())).Returns(false);

            Mock<IMappingFunctionsFactory> mockFactory = CreateFakeMappingFunctionsFactory();
            
            IMapper mapper = new DtoMapper(mockCache.Object, mockFactory.Object);
            mapper.Map<object, object>(new object());

            mockCache.Verify(cache => cache.GetCacheFor<object, object>(It.IsAny<MappingEntryInfo>()), Times.Never);
            mockFactory.Verify(factory => factory.CreateMappingFunction<object, object>(It.IsAny<MappingEntryInfo>()), Times.Once);
        }

        [Fact]
        public void Map_CacheHit_GetCacheForCalled_CreateMappingFunctionDidNotCalled()
        {
            var mockCache = new Mock<IMappingFunctionsCache>();
            mockCache.Setup(mock => mock.HasCacheFor(It.IsAny<MappingEntryInfo>())).Returns(true);
            mockCache.Setup(mock => mock.GetCacheFor<object, object>(It.IsAny<MappingEntryInfo>())).Returns(x => x);

            Mock<IMappingFunctionsFactory> mockFactory = CreateFakeMappingFunctionsFactory();

            IMapper mapper = new DtoMapper(mockCache.Object, mockFactory.Object);
            mapper.Map<object, object>(new object());

            mockCache.Verify(cache => cache.GetCacheFor<object, object>(It.IsAny<MappingEntryInfo>()), Times.Once);
            mockFactory.Verify(factory => factory.CreateMappingFunction<object, object>(It.IsAny<MappingEntryInfo>()), Times.Never);
        }

        // Internals
        Mock<IMappingFunctionsFactory> CreateFakeMappingFunctionsFactory()
        {
            var result = new Mock<IMappingFunctionsFactory>();
            result.Setup(factory => factory.CreateMappingFunction<object, object>(It.IsAny<MappingEntryInfo>())).Returns(x => x);

            return result;
        }
    }
}