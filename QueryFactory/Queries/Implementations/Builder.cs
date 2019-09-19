using QueryFactory.Queries.BaseAndInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryFactory.Queries.Implementations
{
    public class QueryBuilder : BaseQueryBuilder
    {
        public QueryBuilder(string ConnectionString, string InitialCatalog = null) : base(ConnectionString, InitialCatalog)
        {
        }
    }
}
