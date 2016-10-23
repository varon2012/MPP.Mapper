using System;
using System.Collections.Generic;
using System.Reflection;
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
            Assert.Catch(() => { TypeUtils.GetMappablePropertiesPairs(null, null); });
        }

        [Test]
        public void GetMappablePropertiesPairs_LeftParamNullPassed_ExceptionThrown()
        {
            Assert.Catch(() => { TypeUtils.GetMappablePropertiesPairs(null, typeof(Source)); });
        }

        [Test]
        public void GetMappablePropertiesPairs_RightParamNullPassed_ExceptionThrown()
        {
            Assert.Catch(() => { TypeUtils.GetMappablePropertiesPairs(typeof(Source), null); });
        }

        [Test]
        public void GetMappablePropertiesPairs_SourceAndDestinationTypesPassed_ReturnsCollectionWithLengthThree()
        {
            int expected = 4;
             List<KeyValuePair<PropertyInfo, PropertyInfo>> propertyPairs = 
                TypeUtils.GetMappablePropertiesPairs(typeof(Source), typeof(Destination));

            Assert.AreEqual(expected, propertyPairs.Count);
        }

        [Test]
        public void GetMappablePropertiesPairs_SourceAndDestinationTypesPassed_ReturnsCollectionDateTimeProperty()
        {
            List<KeyValuePair<PropertyInfo, PropertyInfo>> propertyPairs =
               TypeUtils.GetMappablePropertiesPairs(typeof(Source), typeof(Destination));

            var pair = new KeyValuePair<PropertyInfo, PropertyInfo>(
                    typeof(Source).GetProperty("FifthProperty"),
                    typeof(Destination).GetProperty("FifthProperty")
                );


            Assert.True(propertyPairs.Contains(pair));
        }

        [Test]
        public void GetMappablePropertiesPairs_SourceAndDestinationTypesPassed_ReturnsCollectionWithoutReadonlyProperty()
        {
            List<KeyValuePair<PropertyInfo, PropertyInfo>> propertyPairs =
               TypeUtils.GetMappablePropertiesPairs(typeof(Source), typeof(Destination));

            var pair = new KeyValuePair<PropertyInfo, PropertyInfo>(
                    typeof(Source).GetProperty("FourthProperty"),
                    typeof(Destination).GetProperty("FourthProperty")
                );
            
            Assert.False(propertyPairs.Contains(pair));
        }

        [Test]
        public void GetMappablePropertiesPairs_SourceAndDestinationTypesPassed_ReturnsCollectionWithoutTypesUncompatibleProperty()
        {
            List<KeyValuePair<PropertyInfo, PropertyInfo>> propertyPairs =
               TypeUtils.GetMappablePropertiesPairs(typeof(Source), typeof(Destination));

            var pair = new KeyValuePair<PropertyInfo, PropertyInfo>(
                    typeof(Source).GetProperty("ObjProperty"),
                    typeof(Destination).GetProperty("ObjProperty")
                );

            Assert.False(propertyPairs.Contains(pair));
        }

        [Test]
        public void GetMappablePropertiesPairs_SourceAndDestinationTypesPassed_ReturnsCollectionWithImplicitlyConvertibleProperty()
        {
            List<KeyValuePair<PropertyInfo, PropertyInfo>> propertyPairs =
               TypeUtils.GetMappablePropertiesPairs(typeof(Source), typeof(Destination));

            var pair = new KeyValuePair<PropertyInfo, PropertyInfo>(
                    typeof(Source).GetProperty("ImplicitlyConvertibleProperty"),
                    typeof(Destination).GetProperty("ImplicitlyConvertibleProperty")
                );

            Assert.True(propertyPairs.Contains(pair));
        }

        [Test]
        public void IsConvertibleTypes_NullsPassed_ExceptionThrown()
        {
            Assert.Catch(() => { TypeUtils.IsConvertibleTypes(null, null); });
        }

        [Test]
        public void IsConvertibleTypes_LeftParamNullPassed_ExceptionThrown()
        {
            Assert.Catch(() => { TypeUtils.IsConvertibleTypes(null, typeof(int)); });
        }

        [Test]
        public void IsConvertibleTypes_RightParamNullPassed_ExceptionThrown()
        {
            Assert.Catch(() => { TypeUtils.IsConvertibleTypes(typeof(int), null); });
        }

        /*                        case TypeCode.UInt16:
                        case TypeCode.Int32:
                        case TypeCode.UInt32:
                        case TypeCode.Int64:
                        case TypeCode.UInt64:
                        case TypeCode.Single:
                        case TypeCode.Double:
                        case TypeCode.Decimal:*/

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
            Assert.True(TypeUtils.IsConvertibleTypes(x, y));
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
            Assert.True(TypeUtils.IsConvertibleTypes(x, y));
        }

        [TestCase(typeof(long))]
        [TestCase(typeof(float))]
        [TestCase(typeof(double))]
        [TestCase(typeof(decimal))]
        public void IsConvertibleTypes_TypeOfIntAndConvertibleTypePassed_TrueReturned(Type y)
        {
            Type x = typeof(int);
            Assert.True(TypeUtils.IsConvertibleTypes(x, y));
        }
            
        [TestCase(typeof(float))]
        [TestCase(typeof(double))]
        [TestCase(typeof(decimal))]
        public void IsConvertibleTypes_TypeOfLongAndConvertibleTypePassed_TrueReturned(Type y)
        {
            Type x = typeof(long);
            Assert.True(TypeUtils.IsConvertibleTypes(x, y));
        }
        
        [TestCase(typeof(double))]
        public void IsConvertibleTypes_TypeOfFloatAndConvertibleTypePassed_TrueReturned(Type y)
        {
            Type x = typeof(float);
            Assert.True(TypeUtils.IsConvertibleTypes(x, y));
        }

        [Test]
        public void IsConvertibleTypes_SameRefTypesPassed_TrueReturned()
        {
            Type x = typeof(Source);
            Assert.True(TypeUtils.IsConvertibleTypes(x, x));
        }
    }
}
