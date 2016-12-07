using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Mapper.Cache;
using Mapper.Contracts;
using Mapper.PropertyInfoStorage;
using Mapper.Reflection;

namespace Mapper
{
    public class Mapper : IMapper
    {
        private static readonly Lazy<Mapper> instance = new Lazy<Mapper>( () => new Mapper() );

        private Mapper()
        {
            cachedLambdas = CachedLambdas.GetInstance();
        }

        public static Mapper GetInstance()
        {
            return instance.Value;
        }

        private readonly CachedLambdas cachedLambdas;
        private TwoValuesPair<Type, Type> cachedTypes;


        public TDestination Map<TSource, TDestination>(TSource source) where TDestination : new()
        {
            if (source == null)
            {
                return default(TDestination);
            }

            cachedTypes = new TwoValuesPair<Type, Type>(typeof(TSource), typeof(TDestination));

            if (IsCacheExist())
            {
                return GetCachedFunc<TSource, TDestination>().Invoke(source);
            }

            var properties = new ReflectionParser().GetSameProperties<TSource, TDestination>();
            var function = new ExpressionCreator.ExpressionCreator().CreateLambdaExpression<TSource, TDestination>(properties);
            AddFunc(function);

            return function.Invoke(source);
        }

        private bool IsCacheExist()
        {
            if (cachedLambdas.ContainsKey(cachedTypes))
            {
                return true;
            }
            return false;
        }

        private Func<TSource, TDestination> GetCachedFunc<TSource, TDestination>()
        {
            return (Func<TSource, TDestination>) cachedLambdas.GetLambda(cachedTypes);
        }

        private void AddFunc(Delegate func)
        {
            cachedLambdas.AddLambda(cachedTypes, func);
        }
    }
}
