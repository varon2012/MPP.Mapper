using System;

namespace Mapper
{
    public interface IMappingFunctionsFactory
    {
        Func<TSource, TDestination> CreateMappingFunction<TSource, TDestination>(MappingEntryInfo mappingEntryInfo) where TDestination : new();
    }
}