using NUnit.Framework;
using QueryFactory.Queries.Implementations;
using QueryFactory.StatementBuilders;
using QueryFactoryTests.InnerHelpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QueryFactoryTests.QueryBuilderTests.UpdateQueryBuilderTest
{
    [TestFixture]
    class UpdateQueryBuilderTest
    {
        QueryBuilder Builder = new QueryBuilder(Constants.ConnectionString);

        [Test]
        public void NullOrWhiteSpaceThrow()
        {
            Assert.Throws<ArgumentNullException>(() => Builder.Update(null));
            Assert.Throws<ArgumentNullException>(() => Builder.Update(""));
        }
        [Test]
        public void OneTable()
        {
            Builder.Update("TABLE");

            string Expected = "UPDATE [Alpha2000].[dbo].[TABLE]";
            string Actual = Builder.Query.QueryString;

            Assert.AreEqual(Expected, Actual);
        }
    }
}
