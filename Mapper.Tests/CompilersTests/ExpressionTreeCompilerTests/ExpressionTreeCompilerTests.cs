using System;
using System.Collections.Generic;
using System.Reflection;
using FluentAssertions;
using Mapper.Compilers;
using NUnit.Framework;

namespace Mapper.Tests.CompilersTests.ExpressionTreeCompilerTests
{
    [TestFixture]
    internal class ExpressionTreeCompilerTests
    {
        [Test]
        public void Compile_NullPassed_ExceptionThrown()
        {
            IMapperCompiler compilerUnderTest = CreateCompiler();

            Assert.Catch<ArgumentNullException>(() => { compilerUnderTest.Compile<object, object>(null); });
        }

        [Test]
        public void Compile_EmptyCollectionPassed_NotNullValueReturned()
        {
            IMapperCompiler compilerUnderTest = CreateCompiler();

            var actual = compilerUnderTest.Compile<object, object>(new List<KeyValuePair<PropertyInfo, PropertyInfo>>());

            Assert.NotNull(actual);
        }

        [Test]
        public void Compile_UnassignablePropertyPairsPassed_ExceptionThrown()
        {
            IMapperCompiler compilerUnderTest = CreateCompiler();

            var propPairs = new List<KeyValuePair<PropertyInfo, PropertyInfo>>()
            {
                new KeyValuePair<PropertyInfo, PropertyInfo>(
                    typeof(Source).GetProperty("FirstProperty"),
                    typeof(Destination).GetProperty("FifthProperty")
                )
            };

            Assert.Catch<InvalidOperationException>(() => { compilerUnderTest.Compile<Source, Destination>(propPairs); });
        }

        [Test]
        public void Compile_ExistingPropertyFromAnotherObjectPassed_ExceptionThrown()
        {
            IMapperCompiler compilerUnderTest = CreateCompiler();

            var propPairs = new List<KeyValuePair<PropertyInfo, PropertyInfo>>()
            {
                new KeyValuePair<PropertyInfo, PropertyInfo>(
                    typeof(Source).GetProperty("FirstProperty"),
                    typeof(Destination).GetProperty("FirstProperty")
                )
            };

            Assert.Catch<InvalidOperationException>(() => { compilerUnderTest.Compile<Source, Source>(propPairs); });
        }

        [Test]
        public void Compile_NonExistingPropertyFromAnotherObjectPassed_ExceptionThrown()
        {
            IMapperCompiler compilerUnderTest = CreateCompiler();

            var propPairs = new List<KeyValuePair<PropertyInfo, PropertyInfo>>()
            {
                new KeyValuePair<PropertyInfo, PropertyInfo>(
                    typeof(Source).GetProperty("FirstProperty"),
                    typeof(Destination).GetProperty("FirstProperty")
                )
            };

            Assert.Catch<InvalidOperationException>(() => { compilerUnderTest.Compile<object, object>(propPairs); });
        }

        [Test]
        public void Compile_CorrectParamsPassed_ExceptionNotThrown()
        {
            IMapperCompiler compilerUnderTest = CreateCompiler();

            var propPairs = new List<KeyValuePair<PropertyInfo, PropertyInfo>>()
            {
                new KeyValuePair<PropertyInfo, PropertyInfo>(
                    typeof(Source).GetProperty("FirstProperty"),
                    typeof(Destination).GetProperty("FirstProperty")
                )
            };

            Assert.DoesNotThrow(() => { compilerUnderTest.Compile<Source, Destination>(propPairs); });
        }

        [Test]
        public void Compile_CorrectParamsPassed_CompiledFunctionCorrectlyMapProperties()
        {
            int expected = 5;

            IMapperCompiler compilerUnderTest = CreateCompiler();

            var propPairs = new List<KeyValuePair<PropertyInfo, PropertyInfo>>()
            {
                new KeyValuePair<PropertyInfo, PropertyInfo>(
                    typeof(Source).GetProperty("FirstProperty"),
                    typeof(Destination).GetProperty("FirstProperty")
                )
            };

            var source = new Source() {FirstProperty = expected};
            
            Destination destination = compilerUnderTest.Compile<Source, Destination>(propPairs)(source);

            int actual = destination.FirstProperty;

            actual.Should().Be(expected);
        }

        private IMapperCompiler CreateCompiler()
        {
            return new ExpressionTreeCompiler();
        }
    }
}
