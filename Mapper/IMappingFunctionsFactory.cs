using System;

namespace Mapper
{
    internal interface IMappingFunctionsFactory
    {
        Func<TSource, TDestination> CreateMappingFunction<TSource, TDestination>(MappingEntryInfo mappingEntryInfo) where TDestination : new();
    }
}