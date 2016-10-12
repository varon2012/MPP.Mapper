using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Mapper.Compilers
{
    public class ExpressionTreeCompiler : IMapperCompiler
    {
        public Func<TInput, TOutput> Compile<TInput, TOutput>(IEnumerable<KeyValuePair<PropertyInfo, PropertyInfo>> properties)
        {
            var source = Expression.Parameter(typeof(TInput), "source");

            var body = Expression.MemberInit(Expression.New(typeof(TOutput)),
                properties.
                Select(
                    p =>
                    {
                        Expression exprProp = Expression.Property(source, p.Key);
                        exprProp = Expression.Convert(exprProp, p.Value.PropertyType);
                        return Expression.Bind(p.Value, exprProp);
                    }));
            var expr = Expression.Lambda<Func<TInput, TOutput>>(body, source);
            return expr.Compile();
        }
    }
}
