using System;
using System.Collections.Generic;
using System.Reflection;
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
            IMapperCompiler compiler = CreateCompiler();

            Assert.Catch(() => { compiler.Compile<object, object>(null); });
        }

        [Test]
        public void Compile_EmptyCollectionPassed_NotNullValueReturned()
        {
            IMapperCompiler compiler = CreateCompiler();
            
            Assert.NotNull(compiler.Compile<object, object>(new List<KeyValuePair<PropertyInfo, PropertyInfo>>()));
        }

        [Test]
        public void Compile_UnassignablePropertyPairsPassed_ExceptionThrown()
        {
            IMapperCompiler compiler = CreateCompiler();

            var propPairs = new List<KeyValuePair<PropertyInfo, PropertyInfo>>()
            {
                new KeyValuePair<PropertyInfo, PropertyInfo>(
                    typeof(Source).GetProperty("FirstProperty"),
                    typeof(Destination).GetProperty("FifthProperty")
                )
            };

            Assert.Catch(() => { compiler.Compile<Source, Destination>(propPairs); });
        }

        [Test]
        public void Compile_ExistingPropertyFromAnotherObjectPassed_ExceptionThrown()
        {
            IMapperCompiler compiler = CreateCompiler();

            var propPairs = new List<KeyValuePair<PropertyInfo, PropertyInfo>>()
            {
                new KeyValuePair<PropertyInfo, PropertyInfo>(
                    typeof(Source).GetProperty("FirstProperty"),
                    typeof(Destination).GetProperty("FirstProperty")
                )
            };

            Assert.Catch(() => { compiler.Compile<Source, Source>(propPairs); });
        }

        [Test]
        public void Compile_NonExistingPropertyFromAnotherObjectPassed_ExceptionThrown()
        {
            IMapperCompiler compiler = CreateCompiler();

            var propPairs = new List<KeyValuePair<PropertyInfo, PropertyInfo>>()
            {
                new KeyValuePair<PropertyInfo, PropertyInfo>(
                    typeof(Source).GetProperty("FirstProperty"),
                    typeof(Destination).GetProperty("FirstProperty")
                )
            };

            Assert.Catch(() => { compiler.Compile<object, object>(propPairs); });
        }

        [Test]
        public void Compile_CorrectParamsPassed_ExceptionNotThrown()
        {
            IMapperCompiler compiler = CreateCompiler();

            var propPairs = new List<KeyValuePair<PropertyInfo, PropertyInfo>>()
            {
                new KeyValuePair<PropertyInfo, PropertyInfo>(
                    typeof(Source).GetProperty("FirstProperty"),
                    typeof(Destination).GetProperty("FirstProperty")
                )
            };

            Assert.DoesNotThrow(() => { compiler.Compile<Source, Destination>(propPairs); });
        }

        [Test]
        public void Compile_CorrectParamsPassed_CompiledFunctionCorrectlyMapProperties()
        {
            IMapperCompiler compiler = CreateCompiler();

            var propPairs = new List<KeyValuePair<PropertyInfo, PropertyInfo>>()
            {
                new KeyValuePair<PropertyInfo, PropertyInfo>(
                    typeof(Source).GetProperty("FirstProperty"),
                    typeof(Destination).GetProperty("FirstProperty")
                )
            };

            var source = new Source() {FirstProperty = 5};

            Destination destination =  compiler.Compile<Source, Destination>(propPairs)(source);

            Assert.AreEqual(source.FirstProperty, destination.FirstProperty);
        }

        private IMapperCompiler CreateCompiler()
        {
            return new ExpressionTreeCompiler();
        }
    }
}
