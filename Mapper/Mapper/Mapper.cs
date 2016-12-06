using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Mapper.Contracts;

namespace Mapper
{
    public class Mapper : IMapper
    {
        public TDestination Map<TSource, TDestination>(TSource source) where TDestination : new()
        {
            var typeSource = typeof(TSource);
            return default(TDestination);
        }
    }
}
