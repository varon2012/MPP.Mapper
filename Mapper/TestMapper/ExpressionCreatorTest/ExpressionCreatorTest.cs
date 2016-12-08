using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mapper.Contracts;
using Mapper.ExpressionCreator;
using NUnit.Framework;

namespace TestMapper.ExpressionCreatorTest
{
    [TestFixture]
    internal class ExpressionCreatorTest
    {
        [Test]
        public void CreateLambda_NullPassed_ReturnNull()
        {
            var creator = GetCreator();
            Func<OneClass, OtherClass> expected = null;

            var result = creator.CreateLambdaExpression<OneClass, OtherClass>(null);

            Assert.AreEqual(expected, result);
        }

        private IExpressionCreator GetCreator()
        {
            return new ExpressionCreator();
        }
    }

    internal class OneClass
    {
        
    }

    internal class OtherClass
    {
        
    }
}
