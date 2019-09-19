using QueryFactory.BaseAndInterfaces;
using QueryFactory.Queries.BaseAndInterfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryFactory.Statements
{
    public class UpdateStatement : IUQuery
    {
        public IList<string> Tables { get; set; }
        public IList<string> Columns { get; set; }
        public ISqlQuery Query { get; set; }
        public IList<SqlParameter> SqlParameters { get; set; }
    }
}
