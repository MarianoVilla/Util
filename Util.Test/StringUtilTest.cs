using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Dager.Test
{
    public class StringUtilTest
    {

        [Theory]
        [InlineData("Hi?Bye,Bla?Bla", '?', ',')]
        public void StringToKeyValuePairs(string ToSplit, char ValueSplit, char KVSplit)
        {
            Dictionary<string, string> Expected = new Dictionary<string, string>() { { "Hi", "Bye" }, { "Bla", "Bla"} };

            var Actual = StringUtil.StringToKeyValuePairs(ToSplit, ValueSplit, KVSplit);

            Assert.Equal(Expected, Actual);
        }




    }
}
