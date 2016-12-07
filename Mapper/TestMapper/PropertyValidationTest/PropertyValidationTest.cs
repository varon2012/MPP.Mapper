using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Mapper.Contracts;
using Mapper.Reflection;
using NUnit.Framework;

namespace TestMapper.PropertyValidationTest
{
    [TestFixture]
    internal class PropertyValidationTest
    {
        [Test]
        public void GetProperties_NullPassed_ReturnNull()
        {
            var validator = GetValidator();
            PropertyInfo[] expected = null;

            var result = validator.GetProperties(null);

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void GetProperties_ClassWithoutPropertiesPassed_Return0Elements()
        {
            var validator = GetValidator();
            var expected = 0;

            var result = validator.GetProperties(typeof(Source));

            Assert.AreEqual(expected, result.Length);
        }

        [Test]
        public void GetProperties_ValueTypePassed_Return0Elements()
        {
            var validator = GetValidator();
            var expected = 0;

            var result = validator.GetProperties(typeof(int));

            Assert.AreEqual(expected, result.Length);
        }

        [Test]
        public void GetProperties_StructWithPropertiesPassed_ReturnNonZeroElements()
        {
            var validator = GetValidator();
            var notExpected = 0;

            var result = validator.GetProperties(typeof(TestStruct));

            Assert.AreNotEqual(notExpected, result.Length);
        }

        [Test]
        public void IsCanMap_PropertyWithoutSetterInDestinationPassed_ReturnFalse()
        {
            var validator = GetValidator();
            PropertyInfo propertyWithoutSetter = typeof(ClassWithPropertyWithoutSetter).GetProperty("Property");
            PropertyInfo propertyWithSetter = typeof(ClassWithPropertyWithSetter).GetProperty("Property");

            var result = validator.IsCanMap(propertyWithSetter, propertyWithoutSetter);

            Assert.False(result);
        }

        [Test]
        public void IsCanMap_PropertySetterInDestinationPassed_ReturnFalse()
        {
            var validator = GetValidator();
            PropertyInfo propertyWithoutSetter = typeof(ClassWithPropertyWithoutSetter).GetProperty("Property");
            PropertyInfo propertyWithSetter = typeof(ClassWithPropertyWithSetter).GetProperty("Property");

            var result = validator.IsCanMap(propertyWithoutSetter, propertyWithSetter);

            Assert.True(result);
        }

        [Test]
        public void IsCanMap_PropertiesWithDifferentNamesPassed_ReturnFalse()
        {
            var validator = GetValidator();
            PropertyInfo propertySource = typeof(TestSource).GetProperty("Property");
            PropertyInfo propertydestination = typeof(TestDestination).GetProperty("OtherProperty");

            var result = validator.IsCanMap(propertySource, propertydestination);

            Assert.False(result);
        }

        [Test]
        public void IsCanMap_InvalidCastPropertiesPassed_ReturnFalse()
        {
            var validator = GetValidator();
            PropertyInfo propertySource = typeof(TestSource).GetProperty("Property");
            PropertyInfo propertydestination = typeof(TestDestination).GetProperty("Property");

            var result = validator.IsCanMap(propertySource, propertydestination);

            Assert.False(result);
        }

        private IValidator GetValidator()
        {
            return PropertyValidation.GetInstance();
        }
    }

    internal class Source
    {
        private int field;
        public float publicField;
    }

    internal struct TestStruct
    {
        public int FirstProperty { get; set; }
        public Type SecondProperty { get; set; }
    }

    internal class ClassWithPropertyWithoutSetter
    {
        public int Property { get; }
    }

    internal class ClassWithPropertyWithSetter
    {
        public int Property { get; set; }
    }

    internal class TestSource
    {
        public int Property { get; set; }
    }

    internal class TestDestination
    {
        public int OtherProperty { get; set; }
        public byte Property { get; set; }
    }
}
