using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mapper
{
    internal class TwoValuesPair<TSource, TDestination>
    {
        public TSource Source { get; set; }
        public TDestination Destination { get; set; }
    }
}
