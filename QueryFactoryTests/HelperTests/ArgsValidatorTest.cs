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
    class ArgsValidatorTest
    {
        [Test]
        public void NullArgument()
        {
            Assert.Throws<ArgumentNullException>(() => ArgsValidator.ThrowIfNull(null));
        }
        [Test]
        public void NullString()
        {
            Assert.Throws<ArgumentNullException>(() => ArgsValidator.ThrowIfNullOrWhiteSpace(null));
        }
    }
}
