using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace DBUtil.SQL
{
    public static class Delete
    {
        public static int DeleteEntity<T>(T EntityInstance, string ConnectionString, string ConditionColumn, object ConditionValue, string TableName = null)
        {
            var EntityName = EntityInstance.GetType().Name;
            TableName = Dager.StringUtil.Coalesce(TableName, EntityName);

            SqlParameter[] Parameters = new SqlParameter[] { new SqlParameter(ConditionColumn, ConditionValue) };

            string Where = $"WHERE [{ConditionColumn}] = @{ConditionColumn};";
            string Query = $"DELETE FROM {TableName} {Where}";

            return InnerUtil.ExNonQuery(Query, Parameters, ConnectionString);
        }
        public static int DeleteFrom<T>(T EntityInstance, string ConnectionString, string TableName = null)
        {
            var EntityName = EntityInstance.GetType().Name;
            TableName = Dager.StringUtil.Coalesce(TableName, EntityName);

            string Query = $"DELETE FROM {TableName}";

            return InnerUtil.ExNonQuery(Query, ConnectionString);
        }
    }
}
