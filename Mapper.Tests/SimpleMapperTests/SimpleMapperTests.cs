using System;
using Mapper.Cache;
using Mapper.Compilers;
using Mapper.Configuration;
using Mapper.UnitsForMapping;
using NSubstitute;
using NUnit.Framework;

namespace Mapper.Tests.SimpleMapperTests
{
    [TestFixture]
    internal class SimpleMapperTests
    {
        [Test]
        public void Map_SourceNullPassed_ExceptionThrown()
        {
            IMapper mapperUnderTest = CreateMapper();
            Assert.Catch<ArgumentNullException>(() => { mapperUnderTest.Map<object, object>(null, null); });
        }

        [Test]
        public void Map_CachedCollectionPassed_ContainsKeyCalled()
        {
            ICachedMapperCollection cachedCollectionMock = Substitute.For<ICachedMapperCollection>();
            
            IMapper mapperUnderTest = CreateMapper(null, cachedCollectionMock);

            mapperUnderTest.Map<Source, Destination>(new Source(), null);

            cachedCollectionMock.ReceivedWithAnyArgs().ContainsKey(null);
        }

        [Test]
        public void Map_CacheHit_GetFuncFromCacheCalled()
        {
            ICachedMapperCollection cachedCollectionMock = Substitute.For<ICachedMapperCollection>();

            IMapper mapperUnderTest = CreateMapper(null, cachedCollectionMock);

            cachedCollectionMock.ContainsKey(null).ReturnsForAnyArgs(true);

            mapperUnderTest.Map<Source, Destination>(new Source(), null);

            cachedCollectionMock.ReceivedWithAnyArgs().GetValue(null);
        }

        [Test]
        public void Map_CacheMiss_NewRecordAddedToCache()
        {
            ICachedMapperCollection cachedCollectionMock = Substitute.For<ICachedMapperCollection>();

            IMapper mapperUnderTest = CreateMapper(null, cachedCollectionMock);

            cachedCollectionMock.ContainsKey(null).ReturnsForAnyArgs(false);

            mapperUnderTest.Map<Source, Destination>(new Source(), null);

            cachedCollectionMock.ReceivedWithAnyArgs().Add(null, null);
        }

        [Test]
        public void Map_CacheHit_CompilerDidNotReceiveCall()
        {
            IMapperCompiler compilerMock = Substitute.For<IMapperCompiler>();
            ICachedMapperCollection cachedCollectionStub = Substitute.For<ICachedMapperCollection>();
            
            IMapper mapperUnderTest = CreateMapper(compilerMock, cachedCollectionStub);

            cachedCollectionStub.ContainsKey(null).ReturnsForAnyArgs(true);

            mapperUnderTest.Map<Source, Destination>(new Source(), null);

            compilerMock.DidNotReceiveWithAnyArgs().Compile<Source, Destination>(null);
        }

        [Test]
        public void Map_CacheMiss_CompileReceivedCall()
        {
            IMapperCompiler compilerMock = Substitute.For<IMapperCompiler>();
            ICachedMapperCollection cachedCollectionStub = Substitute.For<ICachedMapperCollection>();

            IMapper mapperUnderTest = CreateMapper(compilerMock, cachedCollectionStub);

            cachedCollectionStub.ContainsKey(null).ReturnsForAnyArgs(false);

            mapperUnderTest.Map<Source, Destination>(new Source(), null);

            compilerMock.ReceivedWithAnyArgs().Compile<Source, Destination>(null);
        }

        private IMappingUnit CreateMappingUnit(Type source = null, Type destination = null, IMapperConfiguration config = null)
        {
            return new MappingUnit()
            {
                Source = source,
                Config = config,
                Destination = destination
            };
        }

        private IMapper CreateMapper(IMapperCompiler compiler = null, ICachedMapperCollection cachedCollection = null)
        {
            return new SimpleMapper(compiler ?? new ExpressionTreeCompiler(), cachedCollection ?? new CachedMapperCollection());
        }
    }
}
