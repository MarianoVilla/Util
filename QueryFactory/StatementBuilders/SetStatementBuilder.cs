using QueryFactory.BaseAndInterfaces;
using QueryFactory.Helpers;
using QueryFactory.Queries.BaseAndInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryFactory.StatementBuilders
{
    public static class SetStatementBuilder
    {
        public static IRunnableQuery Set(this IUQuery Query, string ConditionColumn, object ConditionValue)
        {
            ArgsValidator.ThrowIfNull(ConditionValue);
            ArgsValidator.ThrowIfNullOrWhiteSpace(ConditionColumn);


            Query.SqlParameters.Add(new System.Data.SqlClient.SqlParameter { ParameterName = ConditionColumn, SqlValue = ConditionValue });
            Query.Query.QueryString += $" SET ConditionColumn=@ConditionValue";
            return Query;
        }
    }
}
