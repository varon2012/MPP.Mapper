using System;
using System.Collections.Generic;
using System.Reflection;

namespace Mapper
{
    public interface IMappingFunctionsFactory
    {
        Func<TSource, TDestination> CreateMappingFunction<TSource, TDestination>(List<MappingPropertiesPair> mappingProperties) where TDestination : new();
    }
}