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
            var config = CreateMapperConfiguration<Source, Destination>();
            Assert.Catch(() => { config.Register<int, int>(null, null); });
        }

        [Test]
        public void Register_LeftParamNullPassed_ExceptionThrown()
        {
            var config = CreateMapperConfiguration<Source, Destination>();
            Assert.Catch(() => { config.Register<int, int>(x => x.FirstProperty, null); });
        }

        [Test]
        public void Register_RightParamNullPassed_ExceptionThrown()
        {
            var config = CreateMapperConfiguration<Source, Destination>();
            Assert.Catch(() => { config.Register<int, int>(null, x => x.FirstProperty); });
        }

        [Test]
        public void Register_ParamsWithDifferentTypesPassed_ExceptionThrown()
        {
            var config = CreateMapperConfiguration<Source, Destination>();
            Assert.Catch(() => { config.Register(y => y.FifthProperty, x => x.FirstProperty); });
        }

        [Test]
        public void Register_ParamIsChildOfGenericTypePassed_ExceptionNotThrown()
        {
            var config = CreateMapperConfiguration<Source, Destination>();
            var sourceChild = new SourceChild();
            Assert.DoesNotThrow(() => { config.Register(y => sourceChild.FirstProperty, x => x.FirstProperty); });
        }

        [Test]
        public void Register_MethodCalled_ExceptionThrown()
        {
            var config = CreateMapperConfiguration<Source, Destination>();

            Assert.Catch(() => { config.Register(y => y.GetSomeInt(), x => x.FirstProperty); });
        }

        [Test]
        public void Register_FieldPassed_ExceptionThrown()
        {
            var config = CreateMapperConfiguration<Source, Destination>();

            Assert.Catch(() => { config.Register(y => y.SomeField, x => x.FirstProperty); });
        }

        [Test]
        public void Register_NoSetterForDestinationProperty_ExceptionThrown()
        {
            var config = CreateMapperConfiguration<Source, Destination>();

            Assert.Catch(() => { config.Register(y => y.FourthProperty, x => x.FourthProperty); });
        }

        [Test]
        public void Register_CorrectParamsPassed_ExceptionNotThrown()
        {
            var config = CreateMapperConfiguration<Source, Destination>();

            Assert.DoesNotThrow(() => { config.Register(y => y.FirstProperty, x => x.FirstProperty); });
        }

        [Test]
        public void Register_DifferentPropertiesNames_ExceptionNotThrown()
        {
            var config = CreateMapperConfiguration<Source, Destination>();

            Assert.DoesNotThrow(() => { config.Register(y => y.IntPropertyWithAnotherName, x => x.FirstProperty); });
        }

        [Test]
        public void Register_CorrectParamsPassed_KeyValuePairAddedToList()
        {
            int expectedCount = 1;

            var config = CreateMapperConfiguration<Source, Destination>();

            config.Register(y => y.IntPropertyWithAnotherName, x => x.FirstProperty);

           Assert.AreEqual(expectedCount, config.Value.Count); 
        }

        [Test]
        public void Ctor_ListInitialization_ListOfPropertiesNotNull()
        {
            var config = CreateMapperConfiguration<Source, Destination>();
            
            Assert.NotNull(config.Value);
        }

        private IGenericMapperConfiguration<TSource, TDestination> CreateMapperConfiguration<TSource, TDestination>()
            where TDestination : new()
        {
            return new MapperConfiguration<TSource, TDestination> ();
        }
    }
}
