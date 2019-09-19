using QueryFactory.Queries.BaseAndInterfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryFactory.BaseAndInterfaces
{
    public interface IUQuery : IRunnableQuery
    {
        IList<string> Tables { get; set; }
        IList<string> Columns { get; set; }
        IList<SqlParameter> SqlParameters { get; set; }
    }
}
