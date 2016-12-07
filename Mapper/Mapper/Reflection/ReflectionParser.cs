using System;
using System.Collections.Generic;
using System.Reflection;
using Mapper.Contracts;
using Mapper.PropertyInfoStorage;

namespace Mapper.Reflection
{
    public class ReflectionParser : IReflectionParser
    {
        private readonly PropertyValidation validator;

        public ReflectionParser()
        {
            validator = PropertyValidation.GetInstance();
        }

        public List<TwoValuesPair<PropertyInfo, PropertyInfo>> GetSameProperties<TSource, TDestination>()
        {
            var resultProperties = new List<TwoValuesPair<PropertyInfo, PropertyInfo>>();
            var sourceProperties = validator.GetProperties(typeof(TSource));
            var destinationSource = validator.GetProperties(typeof(TDestination));

            foreach (var sourceProperty in sourceProperties)
            {
                foreach (var destinationProperty in destinationSource)
                {
                    if (validator.IsCanMap(sourceProperty, destinationProperty))
                    {
                        resultProperties.Add(new TwoValuesPair<PropertyInfo, PropertyInfo>(sourceProperty, destinationProperty));
                        break;
                    }
                }
            }

            return resultProperties;
        }
    }
}
