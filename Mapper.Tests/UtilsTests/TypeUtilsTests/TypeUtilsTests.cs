using System;
using System.Collections.Generic;
using System.Reflection;
using FluentAssertions;
using Mapper.Utils;
using NUnit.Framework;

namespace Mapper.Tests.UtilsTests.TypeUtilsTests
{
    [TestFixture]
    internal class TypeUtilsTests
    {
        [Test]
        public void GetMappablePropertiesPairs_NullsPassed_ExceptionThrown()
        {
            Assert.Catch<ArgumentNullException>(() => { TypeUtils.GetMappablePropertiesPairs(null, null); });
        }

        [Test]
        public void GetMappablePropertiesPairs_LeftParamNullPassed_ExceptionThrown()
        {
            Assert.Catch<ArgumentNullException>(() => { TypeUtils.GetMappablePropertiesPairs(null, typeof(Source)); });
        }

        [Test]
        public void GetMappablePropertiesPairs_RightParamNullPassed_ExceptionThrown()
        {
            Assert.Catch<ArgumentNullException>(() => { TypeUtils.GetMappablePropertiesPairs(typeof(Source), null); });
        }

        [Test]
        public void GetMappablePropertiesPairs_SourceAndDestinationTypesPassed_ReturnsCollectionWithLengthThree()
        {
            long expected = 4;
             List<KeyValuePair<PropertyInfo, PropertyInfo>> propertyPairs = 
                TypeUtils.GetMappablePropertiesPairs(typeof(Source), typeof(Destination));

            long actual = propertyPairs.Count;

            actual.Should().Be(expected);
        }

        [Test]
        public void GetMappablePropertiesPairs_SourceAndDestinationTypesPassed_ReturnsCollectionDateTimeProperty()
        {
            List<KeyValuePair<PropertyInfo, PropertyInfo>> propertyPairs =
               TypeUtils.GetMappablePropertiesPairs(typeof(Source), typeof(Destination));

            var pairForTest = new KeyValuePair<PropertyInfo, PropertyInfo>(
                    typeof(Source).GetProperty("FifthProperty"),
                    typeof(Destination).GetProperty("FifthProperty")
                );

            bool actual = propertyPairs.Contains(pairForTest);

            Assert.True(actual);
        }

        [Test]
        public void GetMappablePropertiesPairs_SourceAndDestinationTypesPassed_ReturnsCollectionWithoutReadonlyProperty()
        {
            List<KeyValuePair<PropertyInfo, PropertyInfo>> propertyPairs =
               TypeUtils.GetMappablePropertiesPairs(typeof(Source), typeof(Destination));

            var pairForTest = new KeyValuePair<PropertyInfo, PropertyInfo>(
                    typeof(Source).GetProperty("FourthProperty"),
                    typeof(Destination).GetProperty("FourthProperty")
                );

            bool actual = propertyPairs.Contains(pairForTest);

            Assert.False(actual);
        }

        [Test]
        public void GetMappablePropertiesPairs_SourceAndDestinationTypesPassed_ReturnsCollectionWithoutTypesUncompatibleProperty()
        {
            List<KeyValuePair<PropertyInfo, PropertyInfo>> propertyPairs =
               TypeUtils.GetMappablePropertiesPairs(typeof(Source), typeof(Destination));

            var pairForTest = new KeyValuePair<PropertyInfo, PropertyInfo>(
                    typeof(Source).GetProperty("ObjProperty"),
                    typeof(Destination).GetProperty("ObjProperty")
                );

            bool actual = propertyPairs.Contains(pairForTest);
            
            Assert.False(actual);
        }

        [Test]
        public void GetMappablePropertiesPairs_SourceAndDestinationTypesPassed_ReturnsCollectionWithImplicitlyConvertibleProperty()
        {
            List<KeyValuePair<PropertyInfo, PropertyInfo>> propertyPairs =
               TypeUtils.GetMappablePropertiesPairs(typeof(Source), typeof(Destination));

            var pairForTest = new KeyValuePair<PropertyInfo, PropertyInfo>(
                    typeof(Source).GetProperty("ImplicitlyConvertibleProperty"),
                    typeof(Destination).GetProperty("ImplicitlyConvertibleProperty")
                );

            bool actual = propertyPairs.Contains(pairForTest);

            Assert.True(actual);
        }

        [Test]
        public void IsConvertibleTypes_NullsPassed_ExceptionThrown()
        {
            Assert.Catch<ArgumentNullException>(() => { TypeUtils.IsConvertibleTypes(null, null); });
        }

        [Test]
        public void IsConvertibleTypes_LeftParamNullPassed_ExceptionThrown()
        {
            Assert.Catch<ArgumentNullException>(() => { TypeUtils.IsConvertibleTypes(null, typeof(int)); });
        }

        [Test]
        public void IsConvertibleTypes_RightParamNullPassed_ExceptionThrown()
        {
            Assert.Catch<ArgumentNullException>(() => { TypeUtils.IsConvertibleTypes(typeof(int), null); });
        }

        [TestCase(typeof(ushort))]
        [TestCase(typeof(int))]
        [TestCase(typeof(uint))]
        [TestCase(typeof(long))]
        [TestCase(typeof(ulong))]
        [TestCase(typeof(float))]
        [TestCase(typeof(double))]
        [TestCase(typeof(decimal))]
        public void IsConvertibleTypes_TypeOfCharAndConvertibleTypePassed_TrueReturned(Type y)
        {
            Type x = typeof(char);

            bool actual = TypeUtils.IsConvertibleTypes(x, y);

            Assert.True(actual);
        }

        [TestCase(typeof(int))]
        [TestCase(typeof(uint))]
        [TestCase(typeof(long))]
        [TestCase(typeof(ulong))]
        [TestCase(typeof(float))]
        [TestCase(typeof(double))]
        [TestCase(typeof(decimal))]
        public void IsConvertibleTypes_TypeOfByteAndConvertibleTypePassed_TrueReturned(Type y)
        {
            Type x = typeof(byte);

            bool actual = TypeUtils.IsConvertibleTypes(x, y);

            Assert.True(actual);
        }

        [TestCase(typeof(long))]
        [TestCase(typeof(float))]
        [TestCase(typeof(double))]
        [TestCase(typeof(decimal))]
        public void IsConvertibleTypes_TypeOfIntAndConvertibleTypePassed_TrueReturned(Type y)
        {
            Type x = typeof(int);

            bool actual = TypeUtils.IsConvertibleTypes(x, y);

            Assert.True(actual);
        }
            
        [TestCase(typeof(float))]
        [TestCase(typeof(double))]
        [TestCase(typeof(decimal))]
        public void IsConvertibleTypes_TypeOfLongAndConvertibleTypePassed_TrueReturned(Type y)
        {
            Type x = typeof(long);

            bool actual = TypeUtils.IsConvertibleTypes(x, y);

            Assert.True(actual);
        }
        
        [TestCase(typeof(double))]
        public void IsConvertibleTypes_TypeOfFloatAndConvertibleTypePassed_TrueReturned(Type y)
        {
            Type x = typeof(float);

            bool actual = TypeUtils.IsConvertibleTypes(x, y);

            Assert.True(actual);
        }

        [Test]
        public void IsConvertibleTypes_SameRefTypesPassed_TrueReturned()
        {
            Type x = typeof(Source);

            bool actual = TypeUtils.IsConvertibleTypes(x, x);

            Assert.True(actual);
        }
    }
}
