using System;
using System.Collections.Generic;
using System.Reflection;
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
            IMapper mapper = CreateMapper();
            Assert.Catch(() => { mapper.Map<object, object>(null, null); });
        }

        [Test]
        public void Map_CachedCollectionPassed_ContainsKeyCalled()
        {
            ICachedMapperCollection cachedCollection = Substitute.For<ICachedMapperCollection>();
            
            IMapper mapper = CreateMapper(null, cachedCollection);

            mapper.Map<Source, Destination>(new Source(), null);

            cachedCollection.ReceivedWithAnyArgs().ContainsKey(null);
        }

        [Test]
        public void Map_CacheHit_GetFuncFromCacheCalled()
        {
            ICachedMapperCollection cachedCollection = Substitute.For<ICachedMapperCollection>();

            IMapper mapper = CreateMapper(null, cachedCollection);

            cachedCollection.ContainsKey(null).ReturnsForAnyArgs(true);

            mapper.Map<Source, Destination>(new Source(), null);
            
            cachedCollection.ReceivedWithAnyArgs().GetValue(null);
        }

        [Test]
        public void Map_CacheMiss_NewRecordAddedToCache()
        {
            ICachedMapperCollection cachedCollection = Substitute.For<ICachedMapperCollection>();

            IMapper mapper = CreateMapper(null, cachedCollection);

            cachedCollection.ContainsKey(null).ReturnsForAnyArgs(false);

            mapper.Map<Source, Destination>(new Source(), null);

            cachedCollection.ReceivedWithAnyArgs().Add(null, null);
        }

        [Test]
        public void Map_CacheHit_CompilerDidNotReceiveCall()
        {
            IMapperCompiler compilerMock = Substitute.For<IMapperCompiler>();
            ICachedMapperCollection cachedCollectionStub = Substitute.For<ICachedMapperCollection>();
            
            IMapper mapper = CreateMapper(compilerMock, cachedCollectionStub);

            cachedCollectionStub.ContainsKey(null).ReturnsForAnyArgs(true);

            mapper.Map<Source, Destination>(new Source(), null);

            compilerMock.DidNotReceiveWithAnyArgs().Compile<Source, Destination>(null);
        }

        [Test]
        public void Map_CacheMiss_CompileReceivedCall()
        {
            IMapperCompiler compilerMock = Substitute.For<IMapperCompiler>();
            ICachedMapperCollection cachedCollectionStub = Substitute.For<ICachedMapperCollection>();

            IMapper mapper = CreateMapper(compilerMock, cachedCollectionStub);

            cachedCollectionStub.ContainsKey(null).ReturnsForAnyArgs(false);

            mapper.Map<Source, Destination>(new Source(), null);

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
