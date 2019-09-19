using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryFactory.Queries.BaseAndInterfaces
{
    public interface ISqlQuery
    {
        string InitialCatalog { get; set; }
        string ConnectionString { get; set; }
        string QueryString { get; set; }
        IList<string> Columns { get; set; }
    }
}
