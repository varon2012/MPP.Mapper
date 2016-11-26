using DtoMapper.Compiler;
using Xunit;

namespace DtoMapperTest.Compiler
{
    public class CashFunctionCompilerTests
    {
        [Fact]
        public void Cash_WhenInvokeSeveralTimes_ShouldReturnEqualRefferences()
        {
            //arrange
            CashFunctionCompiler underTest = new CashFunctionCompiler();

            //act
            var firstInvoke = underTest.CompileMappingFunction<Source, Destination>();
            var secondInvoke = underTest.CompileMappingFunction<Source, Destination>();

            //assert
            Assert.True(firstInvoke == secondInvoke);
        }
    }
}
