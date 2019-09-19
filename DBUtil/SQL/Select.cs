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
            SqlParameter[] Parameters = new SqlParameter[] { new SqlParameter(ConditionColumn, ConditionValue) };
            string Query = $"SELECT * FROM {TableName} WHERE [{ConditionColumn}]=@{ConditionColumn};";
            return InnerUtil.ExReader(Entity, Query, Parameters, ConnectionString, AttributeType);
        }

        public static List<T> ByAttribute<T, U>(T Entity, U AttributeType, string ConnectionString, string TableName = null)
            where T : class, new()
            where U : Type
        {
            string EntityName = Entity.GetType().Name;
            string Query = $"SELECT * FROM {TableName};";
            return InnerUtil.ExReader(Entity, AttributeType, Query, ConnectionString);
        }
        public static List<T> ByAttribute<T, U>(T Entity, U AttributeType, string ConnectionString, string TableName = null, string[] Columns = null)
            where T : class, new()
            where U : Type
        {
            string EntityName = Entity.GetType().Name;
            string WhatToSelect = string.Empty;
            for(int i = 0; i < Columns.Length; i++)
                WhatToSelect += $"[{Columns[i]}], ";
            WhatToSelect = WhatToSelect.Substring(0, WhatToSelect.Length - 2);
            string Query = $"SELECT {WhatToSelect} FROM {TableName};";
            return InnerUtil.ExReader(Entity, AttributeType, Query, ConnectionString);
        }
        public static List<T> ByAttribute<T, U>(T Entity, U AttributeType, string[] ConditionColumns, object[] ConditionValues, string ConnectionString, string TableName = null, string[] Columns = null)
            where T : class, new()
            where U : Type
        {
            string EntityName = Entity.GetType().Name;
            string WhatToSelect = string.Empty;
            string Where = "WHERE ";
            for (int i = 0; i < Columns.Length; i++)
                WhatToSelect += $"[{Columns[i]}], ";
            SqlParameter[] Parameters = new SqlParameter[ConditionColumns.Length];
            if (ConditionColumns.Length > 0)
            {
                for (int i = 0; i < ConditionColumns.Length; i++)
                {
                    Parameters[i] = new SqlParameter(ConditionColumns[i], ConditionValues[i]);
                    Where += $"[{ConditionColumns[i]}]=@{ConditionColumns[i]} AND ";
                }
                Where = Where.Substring(0, Where.Length - 5);
            }
            else
                Where = string.Empty;
            WhatToSelect = WhatToSelect.Substring(0, WhatToSelect.Length - 2);

            string Query = $"SELECT {WhatToSelect} FROM {TableName} {Where};";
            return InnerUtil.ExReader(Entity, AttributeType, Parameters, Query, ConnectionString);
        }

        public static List<T> ByAttribute<T, U>(T Entity, U AttributeType, string[] ConditionColumns, object[] ConditionValues, string ConnectionString, string[] DistinctBy, string TableName = null, string ANDOR = "AND")
            where T : class, new()
            where U : Type
        {
            if (ConditionValues == null)
                return null;
            string EntityName = Entity.GetType().Name;
            string Where = "WHERE ";
            string Distinct = "DISTINCT (";
            if (DistinctBy.Any())
            {
                foreach (var dis in DistinctBy)
                    Distinct += $"{dis}, ";
            }
            else
                Distinct = null;
            if (Distinct != null)
                Distinct = Distinct.Substring(0, Distinct.Length - 2) + ")";
            SqlParameter[] Parameters = null;
            if(ConditionColumns.Length == ConditionValues.Length)
            {
                Parameters = new SqlParameter[ConditionColumns.Length];
                for (int i = 0; i < ConditionColumns.Length; i++)
                {
                    Parameters[i] = new SqlParameter(ConditionColumns[i], ConditionValues[i]);
                    Where += $"[{ConditionColumns[i]}]=@{ConditionColumns[i]} {ANDOR} ";
                }
            }
            else
            {
                Parameters = new SqlParameter[ConditionValues.Length];
                for (int i = 0; i < ConditionValues.Length; i++)
                {
                    string ParamName = $"ConditionColumns{i}";
                    Parameters[i] = new SqlParameter(ParamName, ConditionValues[i]);
                    Where += $"[{ConditionColumns[0]}]=@{ParamName} {ANDOR} ";
                }
            }
            Where = Where.Substring(0, Where.Length - (ANDOR.Length+2));
            string Query = $"SELECT {Distinct ?? "*"} FROM {TableName} {Where};";
            return InnerUtil.ExReader(Entity, AttributeType, Parameters, Query, ConnectionString);
        }

        public static List<T> ByAttribute<T, U>(T Entity, U AttributeType, string ConditionColumn, object ConditionValue, string ConnectionString, string TableName = null)
            where T : class, new()
            where U : Type
        {
            if (ConditionValue == null)
                return null;
            string EntityName = Entity.GetType().Name;
            string Where = $"WHERE {ConditionColumn}=@{ConditionColumn}";
            var Parameters = new SqlParameter[] { new SqlParameter(ConditionColumn, ConditionValue) };
            Where = Where.Substring(0, Where.Length - 5);
            string Query = $"SELECT * FROM {TableName} {Where};";
            return InnerUtil.ExReader(Entity, AttributeType, Parameters, Query, ConnectionString);
        }


    }
}
