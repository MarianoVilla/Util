using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;

namespace DBUtil.SQL
{
    public static class Update
    {
        public static int ByAttribute<T, U>(T Entity, string ConnectionString, string ConditionColumn, object ConditionValue, U AttributeType, string TableName = null) where U : Type
        {
            Type EntityType = Entity.GetType();
            string EntityName = EntityType.Name;
            IEnumerable<PropertyInfo> Props = EntityType.GetProperties().Where(x => Attribute.IsDefined(x, AttributeType));

            TableName = Dager.StringUtil.Coalesce(TableName, EntityName);
            List<SqlParameter> Parameters = new List<SqlParameter>() { new SqlParameter(ConditionColumn, ConditionValue) };

            string Set = "SET ";
            string Where = $"WHERE [{ConditionColumn}] = @{ConditionColumn};";

            foreach (var p in Props)
            {
                var Value = p.GetValue(Entity, null);
                if (Value == null || p.Name == ConditionColumn)
                    continue;

                Set += $"{p.Name}=@{p.Name}, ";
                Parameters.Add(new SqlParameter(p.Name, Value));
            }

            Set = Set.Substring(0, Set.Length - 2);
            string Query = $"UPDATE {TableName} {Set} {Where}";

            return InnerUtil.ExNonQuery(Query, Parameters.ToArray(), ConnectionString);
        }
        public static int ByAttribute<T, U>(T Entity, string ConnectionString, string[] ConditionColumns, object[] ConditionValues, U AttributeType, string TableName = null) where U : Type
        {
            Type EntityType = Entity.GetType();
            string EntityName = EntityType.Name;
            IEnumerable<PropertyInfo> Props = EntityType.GetProperties().Where(x => Attribute.IsDefined(x, AttributeType));

            TableName = Dager.StringUtil.Coalesce(TableName, EntityName);

            string Set = "SET ";
            string Where = $"WHERE ";
            SqlParameter[] Parameters = new SqlParameter[Props.Count()+ConditionColumns.Count()];
            for (int i = 0; i < ConditionColumns.Length; i++)
            {
                Parameters[i] = new SqlParameter("ID"+ConditionColumns[i], ConditionValues[i]);
                Where += $"[{ConditionColumns[i]}]=@ID{ConditionColumns[i]} AND ";
            }
            int j = ConditionColumns.Length-1;
            foreach (var p in Props)
            {
                j++;
                var Value = p.GetValue(Entity, null);
                if (Value == null)
                    continue;
                Set += $"{p.Name}=@{p.Name}, ";
                Parameters[j] = new SqlParameter(p.Name, Value);
            }
            Array.Resize(ref Parameters, Parameters.Where(x => x != null).Count());
            if(Set.EndsWith(", "))
                Set = Set.Substring(0, Set.Length - 2);
            if (Where.EndsWith("AND "))
                Where = Where.Substring(0, Where.Length - 5);
            string Query = $"UPDATE {TableName} {Set} {Where}";

            return InnerUtil.ExNonQuery(Query, Parameters, ConnectionString);
        }

        public static decimal? ByAttributeGetID<T, U>(T Entity, string ConnectionString, string ConditionColumn, object ConditionValue, U AttributeType, string TableName = null) where U : Type
        {
            Type EntityType = Entity.GetType();
            string EntityName = EntityType.Name;
            IEnumerable<PropertyInfo> Props = EntityType.GetProperties().Where(x => Attribute.IsDefined(x, AttributeType));

            TableName = Dager.StringUtil.Coalesce(TableName, EntityName);
            List<SqlParameter> Parameters = new List<SqlParameter>() { new SqlParameter(ConditionColumn, ConditionValue) };

            string Set = "SET ";
            string Where = $" OUTPUT INSERTED.{ConditionColumn} WHERE [{ConditionColumn}] = @{ConditionColumn};";

            foreach (var p in Props)
            {
                var Value = p.GetValue(Entity, null);
                if (Value == null || Value == ConditionValue)
                    continue;

                Set += $"{p.Name} = @{p.Name}, ";
                Parameters.Add(new SqlParameter(p.Name, Value));
            }

            Set = Set.Substring(0, Set.Length - 2);
            string Query = $"UPDATE {TableName} {Set} {Where}";

            return InnerUtil.ExScalarGetID(Query, Parameters.ToArray(), ConnectionString);
        }

    }
}
