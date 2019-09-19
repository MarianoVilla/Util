using QueryFactory.Queries.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryFactory.Queries.BaseAndInterfaces
{
    public abstract class BaseQueryBuilder : IQueryBuilder
    {
        public IList<string> Columns { get; set; }
        public ISqlQuery Query { get; set; }

        public BaseQueryBuilder(string ConnectionString, string InitialCatalog = null)
        {
            Query = new SqlQuery()
            {
                ConnectionString = ConnectionString,
                InitialCatalog = GetInitialCatalog(ConnectionString)
            };
        }

        static string GetInitialCatalog(string ConnectionString)
        {
            string InitialCatalog = GetConnectionStringValue(ConnectionString, "Initial Catalog");

            return $"[{InitialCatalog}].[dbo].";
        }
        static string GetConnectionStringValue(string ConnectionString, string Key)
        {
            System.Data.Common.DbConnectionStringBuilder builder = new System.Data.Common.DbConnectionStringBuilder();

            builder.ConnectionString = ConnectionString;

            return builder[Key] as string;
        }
    }
}
