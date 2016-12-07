using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Mapper.Contracts
{
    internal interface IExpressionCreator
    {
        Func<TSource, TDestination> CreateLambdaExpression<TSource, TDestination>(
            IEnumerable<TwoValuesPair<PropertyInfo, PropertyInfo>> properties) where TDestination : new();
    }
}
