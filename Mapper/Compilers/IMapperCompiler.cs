using System;
using System.Collections.Generic;
using System.Reflection;

namespace Mapper.Compilers
{
    internal interface IMapperCompiler
    {
        Func<TInput, TOutput> Compile<TInput, TOutput> (IEnumerable<KeyValuePair<PropertyInfo, PropertyInfo>> properties)
             where TOutput : new();
    }
}
