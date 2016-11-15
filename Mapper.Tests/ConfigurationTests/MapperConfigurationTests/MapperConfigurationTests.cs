using System;
using Mapper.Configuration;
using NUnit.Framework;

namespace Mapper.Tests.ConfigurationTests.MapperConfigurationTests
{
    [TestFixture]
    internal class MapperConfigurationTests
    {

        [Test]
        public void Register_NullsPassed_ExceptionThrown()
        {
            var configUnderTest = CreateMapperConfiguration<Source, Destination>();
            Assert.Catch<ArgumentNullException>(() => { configUnderTest.Register<int, int>(null, null); });
        }

        [Test]
        public void Register_LeftParamNullPassed_ExceptionThrown()
        {
            var configUnderTest = CreateMapperConfiguration<Source, Destination>();
            Assert.Catch<ArgumentNullException>(() => { configUnderTest.Register<int, int>(x => x.FirstProperty, null); });
        }

        [Test]
        public void Register_RightParamNullPassed_ExceptionThrown()
        {
            var configUnderTest = CreateMapperConfiguration<Source, Destination>();
            Assert.Catch<ArgumentNullException>(() => { configUnderTest.Register<int, int>(null, x => x.FirstProperty); });
        }

        [Test]
        public void Register_ParamsWithDifferentTypesPassed_ExceptionThrown()
        {
            var configUnderTest = CreateMapperConfiguration<Source, Destination>();
            Assert.Catch<ArgumentException>(() => { configUnderTest.Register(y => y.FifthProperty, x => x.FirstProperty); });
        }

        [Test]
        public void Register_ParamIsChildOfGenericTypePassed_ExceptionNotThrown()
        {
            var configUnderTest = CreateMapperConfiguration<Source, Destination>();
            var sourceChild = new SourceChild();
            Assert.DoesNotThrow(() => { configUnderTest.Register(y => sourceChild.FirstProperty, x => x.FirstProperty); });
        }

        [Test]
        public void Register_MethodCalled_ExceptionThrown()
        {
            var configUnderTest = CreateMapperConfiguration<Source, Destination>();

            Assert.Catch<ArgumentException>(() => { configUnderTest.Register(y => y.GetSomeInt(), x => x.FirstProperty); });
        }

        [Test]
        public void Register_FieldPassed_ExceptionThrown()
        {
            var configUnderTest = CreateMapperConfiguration<Source, Destination>();

            Assert.Catch<ArgumentException>(() => { configUnderTest.Register(y => y.SomeField, x => x.FirstProperty); });
        }

        [Test]
        public void Register_NoSetterForDestinationProperty_ExceptionThrown()
        {
            var configUnderTest = CreateMapperConfiguration<Source, Destination>();

            Assert.Catch<ArgumentException>(() => { configUnderTest.Register(y => y.FourthProperty, x => x.FourthProperty); });
        }

        [Test]
        public void Register_CorrectParamsPassed_ExceptionNotThrown()
        {
            var configUnderTest = CreateMapperConfiguration<Source, Destination>();

            Assert.DoesNotThrow(() => { configUnderTest.Register(y => y.FirstProperty, x => x.FirstProperty); });
        }

        [Test]
        public void Register_DifferentPropertiesNames_ExceptionNotThrown()
        {
            var configUnderTest = CreateMapperConfiguration<Source, Destination>();

            Assert.DoesNotThrow(() => { configUnderTest.Register(y => y.IntPropertyWithAnotherName, x => x.FirstProperty); });
        }

        [Test]
        public void Register_CorrectParamsPassed_KeyValuePairAddedToList()
        {
            long expectedCount = 1;

            var configUnderTest = CreateMapperConfiguration<Source, Destination>();

            configUnderTest.Register(y => y.IntPropertyWithAnotherName, x => x.FirstProperty);

            long actual = configUnderTest.Value.Count;

           Assert.AreEqual(expectedCount, actual); 
        }

        [Test]
        public void Ctor_ListInitialization_ListOfPropertiesNotNull()
        {
            var configUnderTest = CreateMapperConfiguration<Source, Destination>();
            
            Assert.NotNull(configUnderTest.Value);
        }

        private IGenericMapperConfiguration<TSource, TDestination> CreateMapperConfiguration<TSource, TDestination>()
            where TDestination : new()
        {
            return new MapperConfiguration<TSource, TDestination> ();
        }
    }
}
