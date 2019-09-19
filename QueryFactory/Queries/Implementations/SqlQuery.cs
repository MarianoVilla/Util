using QueryFactory.Queries.BaseAndInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryFactory.Queries.Implementations
{
    public class SqlQuery : ISqlQuery
    {
        public string InitialCatalog { get; set; }
        public string ConnectionString { get; set; }
        public string QueryString { get; set; }
        public IList<string> Columns { get; set; }
    }
}
