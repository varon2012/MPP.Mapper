using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using Mapper.Utils;

namespace Mapper.Configuration
{
    public class MapperConfiguration<TSource, TDestination> : IGenericMapperConfiguration<TSource, TDestination>
    {
        public List<KeyValuePair<PropertyInfo, PropertyInfo>> Config { get; }

        public MapperConfiguration()
        {
            Config = new List<KeyValuePair<PropertyInfo, PropertyInfo>>();
        }

        public IGenericMapperConfiguration<TSource, TDestination> Register<TOutSource, TOutDestination>(
            Expression<Func<TSource, TOutSource>> sourcePropertyLambda,
            Expression<Func<TDestination, TOutDestination>> destinationPropertyLambda)
        {
            PropertyInfo sourceProp = GetPropertyInfo(sourcePropertyLambda);
            PropertyInfo destProp = GetPropertyInfo(destinationPropertyLambda);

            if (!TypeUtils.IsConvertibleTypes(sourceProp.PropertyType, destProp.PropertyType))
            {
                throw new ArgumentException("Lambdas have unassignable return types");
            }

            if (!destProp.CanWrite)
            {
                throw new ArgumentException("There is no setter for destination property");
            }
            Config.Add(new KeyValuePair<PropertyInfo, PropertyInfo>(sourceProp, destProp));
            return this;
        }

        private PropertyInfo GetPropertyInfo<TObj, TOut>(Expression<Func<TObj, TOut>> propertyLambda)
        {
            Type type = typeof(TObj);

            MemberExpression member = propertyLambda.Body as MemberExpression;
            if (member == null)
            {
                throw new ArgumentException($"Expression '{propertyLambda}' refers to a method, not a property.");
            }

            PropertyInfo propInfo = member.Member as PropertyInfo;
            if (propInfo == null)
            {
                throw new ArgumentException($"Expression '{propertyLambda}' refers to a field, not a property.");
            }

            if (type != propInfo.ReflectedType && !type.IsSubclassOf(propInfo.ReflectedType) && !propInfo.ReflectedType.IsAssignableFrom(type))
            {
                throw new ArgumentException($"Expresion '{propertyLambda}' refers to a property that is not from type {type}.");
            }

            return propInfo;
        }
    }
}
