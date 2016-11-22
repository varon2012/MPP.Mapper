using System.Collections.Generic;
using System.Reflection;
using DtoMapper.Mapping;

namespace DtoMapperTest
{
    internal static class TestDataFactory
    {
        public static IEnumerable<MappingPair> CreatePropertyList()
        {
            const BindingFlags flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;
            return new List<MappingPair>()
            {
                new MappingPair()
                {
                    Source = typeof(Source).GetProperty("EqualValueTypeProperty", flags),
                    Destination = typeof(Destination).GetProperty("EqualValueTypeProperty", flags)
                },
                new MappingPair()
                {
                    Source = typeof(Source).GetProperty("CastValueTypeProperty", flags),
                    Destination = typeof(Destination).GetProperty("CastValueTypeProperty", flags)
                },
                new MappingPair()
                {
                    Source = typeof(Source).GetProperty("EqualRefTypeProperty", flags),
                    Destination = typeof(Destination).GetProperty("EqualRefTypeProperty", flags)
                },
                new MappingPair()
                {
                    Source = typeof(Source).GetProperty("BaseRefTypeProperty", flags),
                    Destination = typeof(Destination).GetProperty("BaseRefTypeProperty", flags)
                }
            };
        }
    }
}
