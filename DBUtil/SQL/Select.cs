using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace DBUtil.SQL
{
    public static class Select
    {
        public static T ByAttributeSingle<T, U>(T Entity, U AttributeType, string ConditionColumn, object ConditionValue, string ConnectionString, string TableName = null)
            where T : class, new()
            where U : Type
        {
            if (ConditionValue == null)
                return null;
            string EntityName = Entity.GetType().Name;
            TableName = Dager.StringUtil.Coalesce(TableName, EntityName);
            SqlParameter[] Parameters = new SqlParameter[] { new SqlParameter(ConditionColumn, ConditionValue) };
            string Query = $"SELECT * FROM [{TableName}] WHERE [{ConditionColumn}]=@{ConditionColumn};";
            return InnerUtil.ExReader(Entity, Query, Parameters, ConnectionString, AttributeType);
        }

        public static List<T> ByAttribute<T, U>(T Entity, U AttributeType, string ConnectionStirng, string TableName = null)
            where T : class, new()
            where U : Type
        {
            string EntityName = Entity.GetType().Name;
            TableName = Dager.StringUtil.Coalesce(TableName, EntityName);
            string Query = $"SELECT * FROM [{TableName}];";
            return InnerUtil.ExReader(Entity, AttributeType, Query, ConnectionStirng);
        }

        public static List<T> ByAttribute<T, U>(T Entity, U AttributeType, string ConditionColumn, object ConditionValue, string ConnectionString, string TableName = null)
            where T : class, new()
            where U : Type
        {
            if (ConditionValue == null)
                return null;
            string EntityName = Entity.GetType().Name;
            TableName = Dager.StringUtil.Coalesce(TableName, EntityName);
            SqlParameter[] Parameters = new SqlParameter[] { new SqlParameter(ConditionColumn, ConditionValue) };
            string Query = $"SELECT * FROM [{TableName}] WHERE [{ConditionColumn}]=@{ConditionColumn};";
            return InnerUtil.ExReader(Entity, AttributeType, Parameters, Query, ConnectionString);
        }

    }
}
