using System;
using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;
using Mapper.Cache;
using Mapper.Configuration;
using Mapper.UnitsForMapping;

namespace Mapper.Tests.CacheTests.CachedMapperCollectionTests
{
    [TestFixture]
    internal class CachedMapperCollectionTests
    {
        [Test]
        public void Count_EmptyCollection_CountIsZero()
        {
            long expected = 0;
            ICachedMapperCollection collectionUnderTest = CreateCachedMapperCollection();

            long actual = collectionUnderTest.Count;

            actual.Should().Be(expected);
        }

        [Test]
        public void Count_AddOneElement_CountIsOne()
        {
            long expected = 1;

            ICachedMapperCollection collectionUnderTest = CreateCachedMapperCollection();
            
            collectionUnderTest.Add(CreateMappingUnit(), (Action)(() => { }));

            long actual = collectionUnderTest.Count;

            actual.Should().Be(expected);
        }

        [Test]
        public void Add_AddNullKey_ExceptionThrown()
        {
            ICachedMapperCollection collectionUnderTest = CreateCachedMapperCollection();
            
            Assert.Catch<ArgumentException>(() => { collectionUnderTest.Add(null, (Action) (() => { })); });
        }

        [Test]
        public void Add_AddNullValue_ExceptionNotThrown()
        {
            ICachedMapperCollection collectionUnderTest = CreateCachedMapperCollection();

            Assert.DoesNotThrow(() => { collectionUnderTest.Add(CreateMappingUnit(), null); });
        }

        [Test]
        public void ContainsKey_NullPassed_ExceptionThrown()
        {
            ICachedMapperCollection collectionUnderTest = CreateCachedMapperCollection();

            Assert.Catch<ArgumentNullException>(() => { collectionUnderTest.ContainsKey(null); });
        }

        [Test]
        public void ContainsKey_EmptyCollection_FalseReturned()
        {
            ICachedMapperCollection collectionUnderTest = CreateCachedMapperCollection();

            bool actual = collectionUnderTest.ContainsKey(CreateMappingUnit());

            Assert.False(actual);
        }

        [Test]
        public void ContainsKey_MissingElementPassed_FalseReturned()
        {
            FakeMapperEqualityComparer comparerStub = new FakeMapperEqualityComparer();

            ICachedMapperCollection collectionUnderTest = CreateCachedMapperCollection(comparerStub);
            collectionUnderTest.Add(CreateMappingUnit(), (Action)(() => { }));

            comparerStub.ReturningHashCode = 113; //new random value

            bool actual = collectionUnderTest.ContainsKey(CreateMappingUnit());

            Assert.False(actual);
        }

        [Test]
        public void ContainsKey_ExistingElementPassed_TrueReturned()
        {
            FakeMapperEqualityComparer comparerStub = new FakeMapperEqualityComparer();

            ICachedMapperCollection collectionUnderTest = CreateCachedMapperCollection(comparerStub);
            collectionUnderTest.Add(CreateMappingUnit(), (Action)(() => { }));

            IMappingUnit unitToFind = CreateMappingUnit();
            comparerStub.ReturningHashCode = 113; //new random value
            collectionUnderTest.Add(unitToFind, (Action)(() => { }));

            bool actual = collectionUnderTest.ContainsKey(unitToFind);

            Assert.True(actual);
        }


        [Test]
        public void GetValue_NullPassed_ExceptionThrown()
        {
            ICachedMapperCollection collection = CreateCachedMapperCollection();

            Assert.Catch<ArgumentNullException>(() => { collection.GetValue(null); });
        }

        [Test]
        public void GetValue_EmptyCollection_ExceptionThrown()
        {
            ICachedMapperCollection collectionUnderTest = CreateCachedMapperCollection();

            Assert.Catch<KeyNotFoundException>(() => { collectionUnderTest.GetValue(CreateMappingUnit()); });
        }


        [Test]
        public void GetValue_MissingKey_ExceptionThrown()
        {
            FakeMapperEqualityComparer comparerStub = new FakeMapperEqualityComparer();

            ICachedMapperCollection collectionUnderTest = CreateCachedMapperCollection(comparerStub);

            collectionUnderTest.Add(CreateMappingUnit(), (Action)(() => { }));

            comparerStub.ReturningHashCode = 113; //new random value
            
            Assert.Catch<KeyNotFoundException>(() => { collectionUnderTest.GetValue(CreateMappingUnit()); });
        }

        private ICachedMapperCollection CreateCachedMapperCollection(IEqualityComparer<IMappingUnit> equalityComparer = null  )
        {
            return new CachedMapperCollection(equalityComparer ?? new FakeMapperEqualityComparer());
        }

        
        private static IMappingUnit CreateMappingUnit(Type source = null, Type destination = null, IMapperConfiguration config = null)
        {
            return new FakeMappingUnit()
            {
                Source = source,
                Config = config,
                Destination = destination
            };
        }
    }
}
