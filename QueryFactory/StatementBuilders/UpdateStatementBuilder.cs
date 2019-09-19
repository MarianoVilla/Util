using QueryFactory.BaseAndInterfaces;
using QueryFactory.Helpers;
using QueryFactory.Queries.BaseAndInterfaces;
using QueryFactory.Queries.Implementations;
using QueryFactory.Statements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryFactory.StatementBuilders
{
    public static class UpdateStatementBuilder
    {
        public static IUQuery Update(this IQueryBuilder Builder, string Table)
        {
            ArgsValidator.ThrowIfNullOrWhiteSpace(Table);
            Table = Normalizer.TableName(Builder.Query.InitialCatalog, Table);
            Builder.Query.QueryString = $"UPDATE {Table}";
            return new UpdateStatement { Query = Builder.Query, Tables = new List<string> { Table } };
        }
        //public static IUQuery Update(this IQueryBuilder Builder, params string[] Tables)
        //{
        //    ArgumentValidator.ThrowIfNullOrWhiteSpace(Tables);
        //    Tables = Normalizer.TableNames(Builder.Query.InitialCatalog, Tables);
        //}
    }
}
