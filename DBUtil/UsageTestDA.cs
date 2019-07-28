using DBUtil.DA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DBUtil
{
    public class Customer
    {
        [ID]
        public int ClientNumber { get; set; }
    }
    public class UsageTestDA : BaseDA
    {
        public UsageTestDA(string ConnectionString) : base(ConnectionString) { }
        public Table<Customer> Customers { get; set; }
    }
    public class Test
    {
        UsageTestDA test = new UsageTestDA("TestConnection");

        void TestMethod()
        {
            var customer = new Customer() { ClientNumber = 1 };
            test.Customers.Select(1);
        }
    }
}
