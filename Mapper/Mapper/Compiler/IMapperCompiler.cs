using System;
using System.Collections.Generic;

namespace Mapper.Compiler
{
    internal interface IMapperCompiler
    {
        Func<TSource, TDestination> Compile<TSource, TDestination>(IEnumerable<MappingPropertyPair> propertiesPair);
    }
}