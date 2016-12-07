using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Mapper.Contracts
{
    public interface IValidator
    {
        PropertyInfo[] GetProperties(Type type);
        bool IsCanMap(PropertyInfo sourceProperty, PropertyInfo destinationProperty);
    }
}
