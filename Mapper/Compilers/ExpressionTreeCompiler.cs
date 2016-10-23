using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Mapper.Compilers
{
    internal class ExpressionTreeCompiler : IMapperCompiler
    {
        public Func<TInput, TOutput> Compile<TInput, TOutput>(IEnumerable<KeyValuePair<PropertyInfo, PropertyInfo>> properties)
             where TOutput : new()
        {
            if (properties == null) throw new ArgumentNullException(nameof(properties));

            var source = Expression.Parameter(typeof(TInput), "source");

            IEnumerable<MemberBinding> bindings = CreatePropertiesBinding<TInput, TOutput>(properties, source);

            var body = Expression.MemberInit(Expression.New(typeof(TOutput)), bindings);
            var expr = Expression.Lambda<Func<TInput, TOutput>>(body, source);
            return expr.Compile();
        }

        private IEnumerable<MemberBinding> CreatePropertiesBinding<TInput, TOutput>
            (IEnumerable<KeyValuePair<PropertyInfo, PropertyInfo>> propertiesPairs, Expression propertyExpr)
        {
            List<MemberBinding> bindings = new List<MemberBinding>();

            Type inType = typeof(TInput);
            Type outType = typeof(TOutput);

            foreach (KeyValuePair<PropertyInfo, PropertyInfo> propertyPair in propertiesPairs)
            {

                ThrowIfPropertyNotMemberOfType(propertyPair.Key, inType);
                ThrowIfPropertyNotMemberOfType(propertyPair.Value, outType);

                Expression exprProp = Expression.Property(propertyExpr, propertyPair.Key);
                exprProp = Expression.Convert(exprProp, propertyPair.Value.PropertyType);
                bindings.Add(Expression.Bind(propertyPair.Value, exprProp));

            }

            return bindings;
        }

        private void ThrowIfPropertyNotMemberOfType(PropertyInfo property, Type type)
        {
            if (!(type == property.ReflectedType || type.IsSubclassOf(property.ReflectedType) ||
                property.ReflectedType.IsAssignableFrom(type)))
            {
                throw new InvalidOperationException($"Property {property.Name} is not a member of the class {type.Name}");
            }
        }
    }
}
