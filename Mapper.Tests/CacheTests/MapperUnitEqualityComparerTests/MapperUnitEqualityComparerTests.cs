using System;
using System.Collections.Generic;
using System.Reflection;
using Mapper.Cache;
using Mapper.Configuration;
using Mapper.UnitsForMapping;
using NUnit.Framework;

namespace Mapper.Tests.CacheTests.MapperUnitEqualityComparerTests
{
    [TestFixture]
    internal class MapperUnitEqualityComparerTests
    {
        [Test]
        public void Equals_NullsPassed_TrueReturned()
        {
            IEqualityComparer<IMappingUnit> comparerUnderTest = CreateEqualityComparer();

            bool actual = comparerUnderTest.Equals(null, null);

            Assert.True(actual);
        }

        [Test]
        public void Equals_NullAndObjPassed_FalseReturned()
        {
            IEqualityComparer<IMappingUnit> comparerUnderTest = CreateEqualityComparer();

            bool actual = comparerUnderTest.Equals(null, CreateFakeMappingUnit());

            Assert.False(actual);
        }

        [Test]
        public void Equals_ObjAndNullPassed_FalseReturned()
        {
            IEqualityComparer<IMappingUnit> comparerUnderTest = CreateEqualityComparer();

            bool actual = comparerUnderTest.Equals(CreateFakeMappingUnit(), null);

            Assert.False(actual);
        }

        [Test]
        public void Equals_TwoEmptyUnitsPassed_TrueReturned()
        {
            IEqualityComparer<IMappingUnit> comparerUnderTest = CreateEqualityComparer();

            bool actual = comparerUnderTest.Equals(CreateFakeMappingUnit(), CreateFakeMappingUnit());

            Assert.True(actual);
        }


        [Test]
        public void Equals_UnitsWithDifferentSourcePassed_FalseReturned()
        {
            IEqualityComparer<IMappingUnit> comparerUnderTest = CreateEqualityComparer();

            IMappingUnit x = CreateFakeMappingUnit(typeof(int));
            IMappingUnit y = CreateFakeMappingUnit(typeof(float));

            bool actual = comparerUnderTest.Equals(x, y);

            Assert.False(actual);
        }

        [Test]
        public void Equals_UnitsWithDifferentDestinationPassed_FalseReturned()
        {
            IEqualityComparer<IMappingUnit> comparerUnderTest = CreateEqualityComparer();

            IMappingUnit x = CreateFakeMappingUnit(null, typeof(float));
            IMappingUnit y = CreateFakeMappingUnit(null, typeof(int));

            bool actual = comparerUnderTest.Equals(x, y);

            Assert.False(actual);
        }

        [Test]
        public void Equals_UnitsWithDifferentEmptyConfigPassed_FalseReturned()
        {
            IEqualityComparer<IMappingUnit> comparerUnderTest = CreateEqualityComparer();

            IMapperConfiguration xConfig = new FakeMapperConfiguration(new List<KeyValuePair<PropertyInfo, PropertyInfo>>());
            IMapperConfiguration yConfig = new FakeMapperConfiguration(new List<KeyValuePair<PropertyInfo, PropertyInfo>>());

            IMappingUnit x = CreateFakeMappingUnit(null, null, xConfig);
            IMappingUnit y = CreateFakeMappingUnit(null, null, yConfig);

            bool actual = comparerUnderTest.Equals(x, y);

            Assert.False(actual);
        }

        [Test]
        public void Equals_UnitsWithDifferentConfigPassed_FalseReturned()
        {
            IEqualityComparer<IMappingUnit> comparerUnderTest = CreateEqualityComparer();

            IMapperConfiguration xConfig = new FakeMapperConfiguration(new List<KeyValuePair<PropertyInfo, PropertyInfo>>());
            IMapperConfiguration yConfig =
                new FakeMapperConfiguration(new List<KeyValuePair<PropertyInfo, PropertyInfo>>()
                {
                    new KeyValuePair<PropertyInfo, PropertyInfo>(
                        typeof(FakeObjectWithProperties).GetProperty("FirstProperty"),
                        typeof(FakeObjectWithProperties).GetProperty("SecondProperty"))
                });
            
            IMappingUnit x = CreateFakeMappingUnit(null, null, xConfig);
            IMappingUnit y = CreateFakeMappingUnit(null, null, yConfig);

            bool actual = comparerUnderTest.Equals(x, y);

            Assert.False(actual);
        }

        [Test]
        public void Equals_UnitsWithSameSourcePassed_TrueReturned()
        {
            IEqualityComparer<IMappingUnit> comparerUnderTest = CreateEqualityComparer();

            IMappingUnit x = CreateFakeMappingUnit(typeof(int));
            IMappingUnit y = CreateFakeMappingUnit(typeof(int));
            
            bool actual = comparerUnderTest.Equals(x, y);

            Assert.True(actual);
        }

        [Test]
        public void Equals_UnitsWithSameDestinationPassed_TrueReturned()
        {
            IEqualityComparer<IMappingUnit> comparerUnderTest = CreateEqualityComparer();

            IMappingUnit x = CreateFakeMappingUnit(null, typeof(float));
            IMappingUnit y = CreateFakeMappingUnit(null, typeof(float));

            bool actual = comparerUnderTest.Equals(x, y);

            Assert.True(actual);
        }

        [Test]
        public void Equals_UnitsWithSameConfigPassed_TrueReturned()
        {
            IEqualityComparer<IMappingUnit> comparerUnderTest = CreateEqualityComparer();

            IMapperConfiguration config = new FakeMapperConfiguration(new List<KeyValuePair<PropertyInfo, PropertyInfo>>());

            IMappingUnit x = CreateFakeMappingUnit(null, null, config);
            IMappingUnit y = CreateFakeMappingUnit(null, null, config);

            bool actual = comparerUnderTest.Equals(x, y);

            Assert.True(actual);
        }

        [Test]
        public void GetHashCode_NullPassed_ExceptionThrown()
        {
            IEqualityComparer<IMappingUnit> comparerUnderTest = CreateEqualityComparer();

            Assert.Catch<ArgumentNullException>(()=> { comparerUnderTest.GetHashCode(null); });
        }

        [Test]
        public void GetHashCode_EmptyUnitPassed_NotNullReturned()
        {
            IEqualityComparer<IMappingUnit> comparerUnderTest = CreateEqualityComparer();

            Assert.DoesNotThrow(()=> { comparerUnderTest.GetHashCode(CreateFakeMappingUnit()); });
        }

        [Test]
        public void GetHashCode_NotEmptyUnitPassed_NotNullReturned()
        {
            IEqualityComparer<IMappingUnit> comparerUnderTest = CreateEqualityComparer();

            Assert.DoesNotThrow(() => { comparerUnderTest.GetHashCode(CreateFakeMappingUnit(typeof(int))); });
        }

        private IMappingUnit CreateFakeMappingUnit(Type source = null, Type destination = null, IMapperConfiguration config = null)
        {
            return new FakeMappingUnit()
            {
                Source = source,
                Config = config,
                Destination = destination
            };
        }

        private IEqualityComparer<IMappingUnit> CreateEqualityComparer()
        {
            return new MapperUnitEqualityComparer();
        }
    }
}
