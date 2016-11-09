using System;

namespace Mapper
{
    internal class MappingFunctionsFactory
    {
        public Func<TSource, TDestination> CreateMappingFunction<TSource, TDestination>(MappingEntryInfo mappingEntryInfo) where TDestination : new()
        {
            throw new NotImplementedException();
        }
    }
}