using DBUtil.DA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DBUtil
{
    public class UsageTestDA : BaseDA
    {
        public UsageTestDA(string ConnectionString) : base(ConnectionString) { }
    }
    public class Test
    {
        UsageTestDA test = new UsageTestDA("TestConnection");

        void TestMethod()
        {
            var T = test.Get(new Object(), "Bla", "Bla");
        }
    }
}
