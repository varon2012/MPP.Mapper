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
        public List<TwoValuesPair<PropertyInfo, PropertyInfo>> GetSameProperties<TSource, TDestination>()
        {
            var list = new List<PropertyInfo>();
            var resultProperties = new List<TwoValuesPair<PropertyInfo, PropertyInfo>>();
            var sourceProperties = GetProperties(typeof(TSource));
            var destinationSource = GetProperties(typeof(TDestination));
            foreach (var sourceProperty in sourceProperties)
            {
                foreach (var destinationProperty in destinationSource)
                {
                    if (IsCanMap(sourceProperty, destinationProperty))
                    {
                        resultProperties.Add(new TwoValuesPair<PropertyInfo, PropertyInfo>()
                        {
                            Source = sourceProperty,
                            Destination = destinationProperty
                        });
                        break;
                    }
                }
            }
            return resultProperties;
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
            if (!IsValidCast(sourceProperty.PropertyType, destinationProperty.PropertyType))
            {
                return false;
            }
            return true;
        }

        private bool IsValidCast(Type source, Type destination)
        {
            return true;
        }
    }
}
