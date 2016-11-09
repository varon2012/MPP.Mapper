using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mapper
{
    public class DtoMapper : IMapper
    {
        public TDestination Map<TSource, TDestination>(TSource source) where TDestination : new()
        {
            throw new NotImplementedException();
        }
    }
}
