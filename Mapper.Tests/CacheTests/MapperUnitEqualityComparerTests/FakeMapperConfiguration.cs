using System.Collections.Generic;
using System.Reflection;
using Mapper.Configuration;

namespace Mapper.Tests.CacheTests.MapperUnitEqualityComparerTests
{
    internal class FakeMapperConfiguration: IMapperConfiguration
    {
        public List<KeyValuePair<PropertyInfo, PropertyInfo>> Value { get; }

        internal FakeMapperConfiguration(List<KeyValuePair<PropertyInfo, PropertyInfo>> value)
        {
            Value = value;
        }
    }
}
