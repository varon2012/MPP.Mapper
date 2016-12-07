using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Mapper.PropertyInfoStorage;

namespace Mapper.Contracts
{
    interface IReflectionParser
    {
        List<TwoValuesPair<PropertyInfo, PropertyInfo>> GetSameProperties<TSource, TDestination>();
    }
}
