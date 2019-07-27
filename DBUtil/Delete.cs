using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace DBUtil
{
    public static class Delete
    {

        public static int DeleteEntity<T>(T EntityInstance, string ConnectionString, string ConditionColumn, string ConditionValue, string TableName = null)
        {
            var EntityName = EntityInstance.GetType().Name;
            TableName = Dager.StringUtil.Coalesce(EntityName, TableName);

            SqlParameter[] Parameters = new SqlParameter[] { new SqlParameter(ConditionColumn, ConditionValue) };

            string Where = $"WHERE [{ConditionColumn}] = @{ConditionColumn};";
            string Query = $"DELETE FROM {TableName} {Where}";

            return InnerUtil.ExNonQuery(Query, Parameters, ConnectionString);
        }
    }
}
