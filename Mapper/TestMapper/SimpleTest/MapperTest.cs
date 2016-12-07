using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mapper.Contracts;
using NUnit.Framework;

namespace TestMapper.SimpleTest
{
    [TestFixture]
    internal class MapperTest
    {
        [Test]
        public void Map_PassSourceObject_FirstPropertySetToSourceValue()
        {
            IMapper mapper = GetMapper();
            var source = GetSourceObject();
            var expected = GetExpectedObject();

            var dest = mapper.Map<Source, Destination>(source);

            Assert.AreEqual(expected.FirstProperty, dest.FirstProperty);
        }

        [Test]
        public void Map_PassSourceObject_SecondPropertySetToSourceValue()
        {
            IMapper mapper = GetMapper();
            var source = GetSourceObject();
            var expected = GetExpectedObject();

            var dest = mapper.Map<Source, Destination>(source);

            Assert.AreEqual(expected.SecondProperty, dest.SecondProperty);
        }

        [Test]
        public void Map_PassSourceObject_ThirdPropertySetDefault()
        {
            IMapper mapper = GetMapper();
            var source = GetSourceObject();
            var expected = GetExpectedObject();

            var dest = mapper.Map<Source, Destination>(source);

            Assert.AreEqual(expected.ThirdProperty, dest.ThirdProperty);
        }

        [Test]
        public void Map_PassSourceObject_FourthPropertySetToDefault()
        {
            IMapper mapper = GetMapper();
            var source = GetSourceObject();
            var expected = GetExpectedObject();

            var dest = mapper.Map<Source, Destination>(source);

            Assert.AreEqual(expected.FourthProperty, dest.FourthProperty);
        }

        [Test]
        public void Map_PassNullObject_ReturnDefaultValue()
        {
            IMapper mapper = GetMapper();
            Source source = null;
            Destination expected = default(Destination);

            var dest = mapper.Map<Source, Destination>(source);

            Assert.AreEqual(expected, dest);
        }

        private IMapper GetMapper()
        {
            return Mapper.Mapper.GetInstance();
        }

        private Source GetSourceObject()
        {
            return new Source()
            {
                FirstProperty = 10,
                SecondProperty = "second",
                ThirdProperty = 25.5,
                FourthProperty = 111
            };
        }

        private Destination GetExpectedObject()
        {
            var source = GetSourceObject();
            return new Destination()
            {
                FirstProperty = source.FirstProperty,
                SecondProperty = source.SecondProperty,
                ThirdProperty = default(float),
                FourthProperty = default(DateTime)
            };
        }
    }
}
