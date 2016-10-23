using System;
using System.Collections;
using System.Collections.Generic;
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
            int expected = 0;
            ICachedMapperCollection collection = CreateCachedMapperCollection();
            Assert.AreEqual(expected, collection.Count);
        }

        [Test]
        public void Count_AddOneElement_CountIsOne()
        {
            int expected = 1;
            
            IEqualityComparer<IMappingUnit> comparer = new FakeMapperEqualityComparer();
            
            ICachedMapperCollection collection = CreateCachedMapperCollection(comparer);

            collection.Add(CreateMappingUnit(), (Action)(() => { }));
            
            Assert.AreEqual(expected, collection.Count);
        }

        [Test]
        public void Add_AddNullKey_ExceptionThrown()
        {
            IEqualityComparer<IMappingUnit> comparer = new FakeMapperEqualityComparer();

            ICachedMapperCollection collection = CreateCachedMapperCollection(comparer);
            
            Assert.Catch(() => { collection.Add(null, (Action) (() => { })); });
        }

        [Test]
        public void Add_AddNullValue_ExceptionNotThrown()
        {
            IEqualityComparer<IMappingUnit> comparer = new FakeMapperEqualityComparer();

            ICachedMapperCollection collection = CreateCachedMapperCollection(comparer);

            Assert.DoesNotThrow(() => { collection.Add(CreateMappingUnit(), null); });
        }

        [Test]
        public void ContainsKey_NullPassed_ExceptionThrown()
        {
            IEqualityComparer<IMappingUnit> comparer = new FakeMapperEqualityComparer();

            ICachedMapperCollection collection = CreateCachedMapperCollection(comparer);

            Assert.Catch(() => { collection.ContainsKey(null); });
        }

        [Test]
        public void ContainsKey_EmptyCollection_FalseReturned()
        {
            IEqualityComparer<IMappingUnit> comparer = new FakeMapperEqualityComparer();

            ICachedMapperCollection collection = CreateCachedMapperCollection(comparer);

            Assert.False(collection.ContainsKey(CreateMappingUnit()));
        }

        [Test]
        public void ContainsKey_MissingElementPassed_FalseReturned()
        {
            FakeMapperEqualityComparer comparer = new FakeMapperEqualityComparer();

            ICachedMapperCollection collection = CreateCachedMapperCollection(comparer);
            collection.Add(CreateMappingUnit(), (Action)(() => { }));
            
            comparer.ReturningHashCode = 113; //new random value

            Assert.False(collection.ContainsKey(CreateMappingUnit()));
        }

        [Test]
        public void ContainsKey_ExistingElementPassed_TrueReturned()
        {
            FakeMapperEqualityComparer comparer = new FakeMapperEqualityComparer();

            ICachedMapperCollection collection = CreateCachedMapperCollection(comparer);
            collection.Add(CreateMappingUnit(), (Action)(() => { }));

            IMappingUnit unitToFind = CreateMappingUnit();
            comparer.ReturningHashCode = 113; //new random value
            collection.Add(unitToFind, (Action)(() => { }));

            Assert.True(collection.ContainsKey(unitToFind));
        }


        [Test]
        public void GetValue_NullPassed_ExceptionThrown()
        {
            FakeMapperEqualityComparer comparer = new FakeMapperEqualityComparer();

            ICachedMapperCollection collection = CreateCachedMapperCollection(comparer);

            Assert.Catch(() => { collection.GetValue(null); });
        }

        [Test]
        public void GetValue_EmptyCollection_ExceptionThrown()
        {
            FakeMapperEqualityComparer comparer = new FakeMapperEqualityComparer();

            ICachedMapperCollection collection = CreateCachedMapperCollection(comparer);

            Assert.Catch(() => { collection.GetValue(CreateMappingUnit()); });
        }


        [Test]
        public void GetValue_MissingKey_ExceptionThrown()
        {
            FakeMapperEqualityComparer comparer = new FakeMapperEqualityComparer();

            ICachedMapperCollection collection = CreateCachedMapperCollection(comparer);

            collection.Add(CreateMappingUnit(), (Action)(() => { }));

            comparer.ReturningHashCode = 113; //new random value
            
            Assert.Catch(() => { collection.GetValue(CreateMappingUnit()); });
        }

        private ICachedMapperCollection CreateCachedMapperCollection(IEqualityComparer<IMappingUnit> equalityComparer = null  )
        {
            return new CachedMapperCollection(equalityComparer ?? new MapperUnitEqualityComparer());
        }

        
        private IMappingUnit CreateMappingUnit(Type source = null, Type destination = null, IMapperConfiguration config = null)
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
