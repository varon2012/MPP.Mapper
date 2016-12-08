using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mapper.Cache;
using Mapper.Contracts;
using Mapper.PropertyInfoStorage;
using NUnit.Framework;

namespace TestMapper.CacheTest
{
    [TestFixture]
    internal class CachedLambdasTest
    {
        [Test]
        public void AddLambda_PassClassPairToMapper_CachedValueListCountNonZero()
        {
            var mapper = GetMapper();
            var cachedList = GetCachedList();
            var source = GetSource();

            var result = mapper.Map<Source, Destination>(source);
            var unit = new TwoValuesPair<Type, Type>(typeof(Source), typeof(Destination));

            Assert.True(cachedList.ContainsKey(unit));
        }

        [Test]
        public void GetLambda_PassedNonExistingKey_ReturnNull()
        {
            var cachedList = GetCachedList();

            var unit = new TwoValuesPair<Type, Type>(typeof(NotMappedSource), typeof(NotMappedDestination));

            Assert.Null(cachedList.GetLambda(unit));
        }

        private IMapper GetMapper()
        {
            return Mapper.Mapper.GetInstance();
        }

        private ICacheLambdas GetCachedList()
        {
            return CachedLambdas.GetInstance();
        }

        private Source GetSource()
        {
            return new Source()
            {
                First = 1,
                Second = new DateTime(200, 01, 10),
                Third = 12.5
            };
        }
    }

    internal class Source
    {
        public int First { get; set; }
        public DateTime Second { get; set; }
        public double Third { get; set; }
    }

    internal class Destination
    {
        public int First { get; set; }
        public DateTime Second { get; set; }
        public byte Third { get; set; }
    }

    internal class NotMappedSource
    {
        
    }

    internal class NotMappedDestination
    {
        
    }
}
