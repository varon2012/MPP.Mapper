using System.Collections.Generic;
using System.Reflection;

namespace Mapper.Configuration
{
    public interface IMapperConfiguration
    {
        List<KeyValuePair<PropertyInfo, PropertyInfo>> Config { get;  }
    }
}
