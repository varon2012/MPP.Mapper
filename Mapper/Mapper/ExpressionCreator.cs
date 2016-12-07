using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Mapper.Contracts;

namespace Mapper
{
    internal class ExpressionCreator : IExpressionCreator
    {
        public Func<TSource, TDestination> CreateLambdaExpression<TSource, TDestination>(IEnumerable<TwoValuesPair<PropertyInfo, PropertyInfo>> properties) where TDestination : new ()
        {
            var parameter = Expression.Parameter(typeof(TSource), "source");
            IEnumerable<MemberBinding> bindings = CreateBindings(properties, parameter);

            var body = Expression.MemberInit(Expression.New(typeof(TDestination)), bindings);
            var expr = Expression.Lambda<Func<TSource, TDestination>>(body, parameter);
            return expr.Compile();
        }

        private IEnumerable<MemberBinding> CreateBindings(IEnumerable<TwoValuesPair<PropertyInfo, PropertyInfo>> properties, 
                                                                                 Expression expression)
        {
            var bindings = new List<MemberBinding>();

            foreach (var propertyInfo in properties)
            {
                Expression exp = Expression.Property(expression, propertyInfo.Source);
                exp = Expression.Convert(exp, propertyInfo.Destination.PropertyType);
                var bind = Expression.Bind(propertyInfo.Destination, exp);
                bindings.Add(bind);
            }

            return bindings;
        }
    }
}
