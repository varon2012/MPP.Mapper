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
            IEqualityComparer<IMappingUnit> comparer = CreateEqualityComparer();

            Assert.True(comparer.Equals(null,null));
        }

        [Test]
        public void Equals_NullAndObjPassed_FalseReturned()
        {
            IEqualityComparer<IMappingUnit> comparer = CreateEqualityComparer();

            Assert.False(comparer.Equals(null, CreateMappingUnit()));
        }

        [Test]
        public void Equals_ObjAndNullPassed_FalseReturned()
        {
            IEqualityComparer<IMappingUnit> comparer = CreateEqualityComparer();

            Assert.False(comparer.Equals(CreateMappingUnit(), null));
        }

        [Test]
        public void Equals_TwoEmptyUnitsPassed_TrueReturned()
        {
            IEqualityComparer<IMappingUnit> comparer = CreateEqualityComparer();

            Assert.True(comparer.Equals(CreateMappingUnit(), CreateMappingUnit()));
        }


        [Test]
        public void Equals_UnitsWithDifferentSourcePassed_FalseReturned()
        {
            IEqualityComparer<IMappingUnit> comparer = CreateEqualityComparer();

            IMappingUnit x = CreateMappingUnit(typeof(int));
            IMappingUnit y = CreateMappingUnit(typeof(float));
            
            Assert.False(comparer.Equals(x, y));
        }

        [Test]
        public void Equals_UnitsWithDifferentDestinationPassed_FalseReturned()
        {
            IEqualityComparer<IMappingUnit> comparer = CreateEqualityComparer();

            IMappingUnit x = CreateMappingUnit(null, typeof(float));
            IMappingUnit y = CreateMappingUnit(null, typeof(int));

            Assert.False(comparer.Equals(x, y));
        }

        [Test]
        public void Equals_UnitsWithDifferentEmptyConfigPassed_FalseReturned()
        {
            IEqualityComparer<IMappingUnit> comparer = CreateEqualityComparer();

            IMapperConfiguration xConfig = new FakeMapperConfiguration(new List<KeyValuePair<PropertyInfo, PropertyInfo>>());
            IMapperConfiguration yConfig = new FakeMapperConfiguration(new List<KeyValuePair<PropertyInfo, PropertyInfo>>());

            IMappingUnit x = CreateMappingUnit(null, null, xConfig);
            IMappingUnit y = CreateMappingUnit(null, null, yConfig);

            Assert.False(comparer.Equals(x, y));
        }

        [Test]
        public void Equals_UnitsWithDifferentConfigPassed_FalseReturned()
        {
            IEqualityComparer<IMappingUnit> comparer = CreateEqualityComparer();

            IMapperConfiguration xConfig = new FakeMapperConfiguration(new List<KeyValuePair<PropertyInfo, PropertyInfo>>());
            IMapperConfiguration yConfig =
                new FakeMapperConfiguration(new List<KeyValuePair<PropertyInfo, PropertyInfo>>()
                {
                    new KeyValuePair<PropertyInfo, PropertyInfo>(
                        typeof(FakeObjectWithProperties).GetProperty("FirstProperty"),
                        typeof(FakeObjectWithProperties).GetProperty("SecondProperty"))
                });
            
            IMappingUnit x = CreateMappingUnit(null, null, xConfig);
            IMappingUnit y = CreateMappingUnit(null, null, yConfig);

            Assert.False(comparer.Equals(x, y));
        }

        [Test]
        public void Equals_UnitsWithSameSourcePassed_TrueReturned()
        {
            IEqualityComparer<IMappingUnit> comparer = CreateEqualityComparer();

            IMappingUnit x = CreateMappingUnit(typeof(int));
            IMappingUnit y = CreateMappingUnit(typeof(int));

            Assert.True(comparer.Equals(x, y));
        }

        [Test]
        public void Equals_UnitsWithSameDestinationPassed_TrueReturned()
        {
            IEqualityComparer<IMappingUnit> comparer = CreateEqualityComparer();

            IMappingUnit x = CreateMappingUnit(null, typeof(float));
            IMappingUnit y = CreateMappingUnit(null, typeof(float));

            Assert.True(comparer.Equals(x, y));
        }

        [Test]
        public void Equals_UnitsWithSameConfigPassed_TrueReturned()
        {
            IEqualityComparer<IMappingUnit> comparer = CreateEqualityComparer();

            IMapperConfiguration config = new FakeMapperConfiguration(new List<KeyValuePair<PropertyInfo, PropertyInfo>>());

            IMappingUnit x = CreateMappingUnit(null, null, config);
            IMappingUnit y = CreateMappingUnit(null, null, config);

            Assert.True(comparer.Equals(x, y));
        }

        [Test]
        public void GetHashCode_NullPassed_ExceptionThrown()
        {
            IEqualityComparer<IMappingUnit> comparer = CreateEqualityComparer();

            Assert.Catch(()=> { comparer.GetHashCode(null); });
        }

        [Test]
        public void GetHashCode_EmptyUnitPassed_NotNullReturned()
        {
            IEqualityComparer<IMappingUnit> comparer = CreateEqualityComparer();

            Assert.NotNull(comparer.GetHashCode(CreateMappingUnit()));
        }

        [Test]
        public void GetHashCode_NotEmptyUnitPassed_NotNullReturned()
        {
            IEqualityComparer<IMappingUnit> comparer = CreateEqualityComparer();

            Assert.NotNull(comparer.GetHashCode(CreateMappingUnit(typeof(int))));
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

        private IEqualityComparer<IMappingUnit> CreateEqualityComparer()
        {
            return new MapperUnitEqualityComparer();
        }
    }
}
