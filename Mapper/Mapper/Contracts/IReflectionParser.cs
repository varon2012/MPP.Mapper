using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Mapper.Contracts
{
    interface IReflectionParser
    {
        List<PropertyInfo> GetSameProperties<TSource, TDestination>(TSource sourceType, TDestination destinationType);
    }
}
