using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Mapper.Contracts;
using Mapper.Reflection;

namespace Mapper
{
    public class Mapper : IMapper
    {
        public TDestination Map<TSource, TDestination>(TSource source) where TDestination : new()
        {
            var properties = new ReflectionParser().GetSameProperties<TSource, TDestination>();
            Func<TSource, TDestination> func = new ExpressionCreator.ExpressionCreator().CreateLambdaExpression<TSource, TDestination>(properties);
            return func.Invoke(source);
        }
    }
}
