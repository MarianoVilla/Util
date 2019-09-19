using NUnit.Framework;
using QueryFactory.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryFactoryTests.HelperTests
{
    [TestFixture]
    class NormalizerTest
    {
        [Test]
        public void TableName()
        {
            string Expected = "[Database].[dbo].[TableName]";
            string Actual = Normalizer.TableName("[Database].[dbo].", "TableName");

            Assert.AreEqual(Expected, Actual);
        }
    }
}
