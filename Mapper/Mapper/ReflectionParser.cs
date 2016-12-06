using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Mapper.Contracts;

namespace Mapper
{
    internal class ReflectionParser : IReflectionParser
    {
        public List<PropertyInfo> GetSameProperties<TSource, TDestination>(TSource sourceType, TDestination destinationType)
        {
            var list = new List<PropertyInfo>();
            var sourceProperties = GetProperties(typeof(TSource));
            var destinationSource = GetProperties(typeof(TDestination));
            foreach (var sourceProperty in sourceProperties)
            {
                foreach (var destinationProperty in destinationSource)
                {
                    if (IsCanMap(sourceProperty, destinationProperty))
                    {
                        list.Add(destinationProperty);
                        break;
                    }
                }
            }
            return list;
        }

        private PropertyInfo[] GetProperties(Type type)
        {
            return type.GetProperties(BindingFlags.Public
                                                | BindingFlags.SetProperty
                                                | BindingFlags.Instance);
        }

        private bool IsCanMap(PropertyInfo sourceProperty, PropertyInfo destinationProperty)
        {
            if (!sourceProperty.Name.Equals(destinationProperty.Name))
            {
                return false;
            }
            if (destinationProperty.GetSetMethod() == null)
            {
                return false;
            }
            return true;
        }
    }
}
