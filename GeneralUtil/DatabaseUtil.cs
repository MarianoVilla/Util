using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace CertificadoOrigenDigitalLib
{
    public static class DBInsert
    {
        public static bool InsertEntity<T>(T Entity, string ConnectionString, out decimal? Identity)
        {
            Identity = null;
            try
            {
                GeneralUtils.CheckNullArgs(Entity, ConnectionString);
            }
            catch (Exception ex)
            {
                Utils.setError(ex, "Parámetros nulos en InsertEntity.");
                return false;
            }
            Utils.Loguear("Preparando insert para " + Entity.GetType().Name, "DBInsert");

            string Columns = "(";
            string Values = "VALUES (";

            List<SqlParameter> Parameters = new List<SqlParameter>();

            var EntityName = Entity.GetType().Name;
            var Props = Entity.GetType().GetProperties();

            string TableName = $"{AccesoDB.DBAlpha}[COD_{EntityName}]";
            string Query = $"INSERT INTO {TableName} ";

            foreach (var p in Props)
            {
                var Value = p.GetValue(Entity, null);

                if (Value != null)
                {
                    Columns = AddColumn(p.Name, Columns);
                    if (Value.GetType() == typeof(DateTime))
                        Values = AddDateTimeValue(Value, Values);
                    else if (Value.GetType() == typeof(decimal))
                        Values = AddDecimalValue(Value, Values);
                    else if (Value.GetType() != typeof(string))
                        Values = AddValueNotString(Value.ToString(), Values);
                    else
                        Values = AddValue(Value.ToString(), Values);

                }
            }
            Columns = Columns.Substring(0, Columns.Length - 2) + ")\n";
            Values = Values.Substring(0, Values.Length - 2) + ");";
            Query += Columns + Values;
            return DBInnerUtil.PerformRawGetIdentity(Query, ConnectionString, out Identity);

        }
        public static bool InsertEntity<T>(T Entity, string ConnectionString)
        {
            try
            {
                GeneralUtils.CheckNullArgs(Entity, ConnectionString);
            }
            catch (Exception ex)
            {
                Utils.setError(ex, "Parámetros nulos en InsertEntity.");
                return false;
            }
            Utils.Loguear("Preparando insert para " + Entity.GetType().Name, "DBInsert");

            string Columns = "(";
            string Values = "VALUES (";

            List<SqlParameter> Parameters = new List<SqlParameter>();

            var EntityName = Entity.GetType().Name;
            var Props = Entity.GetType().GetProperties();

            string TableName = $"{AccesoDB.DBAlpha}[COD_{EntityName}]";
            string Query = $"INSERT INTO {TableName} ";

            foreach (var p in Props)
            {
                var Value = p.GetValue(Entity, null);

                if (Value != null)
                {
                    Columns = AddColumn(p.Name, Columns);
                    if (Value.GetType() == typeof(DateTime))
                        Values = AddDateTimeValue(Value, Values);
                    else if (Value.GetType() == typeof(decimal))
                        Values = AddDecimalValue(Value, Values);
                    else if (Value.GetType() != typeof(string))
                        Values = AddValueNotString(Value.ToString(), Values);
                    else
                        Values = AddValue(Value.ToString(), Values);

                }
            }
            Columns = Columns.Substring(0, Columns.Length - 2) + ")\n";
            Values = Values.Substring(0, Values.Length - 2) + ");";
            Query += Columns + Values;
            return DBInnerUtil.PerformRawNonQuery(Query, ConnectionString);

        }
        public static bool InsertEntityByAttribute<T, U>(T Entity, string ConnectionString, U AttributeType, out decimal? Identity) where U : Type
        {
            Identity = null;
            try
            {
                GeneralUtils.CheckNullArgs(Entity, ConnectionString);
            }
            catch (Exception ex)
            {
                Utils.setError(ex, "Parámetros nulos en InsertEntity.");
                return false;
            }
            Utils.Loguear("Preparando insert para " + Entity.GetType().Name, "DBInsert");

            string Columns = "(";
            string Values = "VALUES (";

            List<SqlParameter> Parameters = new List<SqlParameter>();

            var EntityName = Entity.GetType().Name;
            var Props = Entity.GetType().GetProperties().Where(x => Attribute.IsDefined(x, AttributeType)).ToList();

            string TableName = $"{AccesoDB.DBAlpha}[COD_{EntityName}]";
            string Query = $"INSERT INTO {TableName} ";

            foreach (var p in Props)
            {
                var Value = p.GetValue(Entity, null);

                if (Value != null)
                {
                    Columns = AddColumn(p.Name, Columns);
                    Parameters.Add(new SqlParameter(p.Name, Value));
                    Values += $"@{p.Name}, ";

                }
            }
            Columns = Columns.Substring(0, Columns.Length - 2) + ")\n";
            Values = Values.Substring(0, Values.Length - 2) + ")";
            Query += Columns + Values;
            //return DBInnerUtil.PerformRawGetIdentity(Query, ConnectionString, out Identity);
            return DBInnerUtil.PerformNonQueryGetIdentity(Query, Parameters, ConnectionString, out Identity);

        }

        public static bool InsertEntityByAttribute<T, U>(T Entity, string ConnectionString, U AttributeType) where U : Type
        {
            try
            {
                GeneralUtils.CheckNullArgs(Entity, ConnectionString);
            }
            catch (Exception ex)
            {
                Utils.setError(ex, "Parámetros nulos en InsertEntity.");
                return false;
            }
            Utils.Loguear("Preparando insert para " + Entity.GetType().Name, "DBInsert");

            string Columns = "(";
            string Values = "VALUES (";

            var EntityName = Entity.GetType().Name;
            var Props = Entity.GetType().GetProperties().Where(x => Attribute.IsDefined(x, AttributeType)).ToList();

            string TableName = $"{AccesoDB.DBAlpha}[COD_{EntityName}]";
            string Query = $"INSERT INTO {TableName} ";
            List<SqlParameter> Parameters = new List<SqlParameter>();

            foreach (var p in Props)
            {
                var Value = p.GetValue(Entity, null);

                if (Value != null)
                {
                    Columns = AddColumn(p.Name, Columns);
                    Parameters.Add(new SqlParameter(p.Name, Value));
                    Values += $"@{p.Name}, ";

                }
            }
            Columns = Columns.Substring(0, Columns.Length - 2) + ")\n";
            Values = Values.Substring(0, Values.Length - 2) + ")";
            Query += Columns + Values;
            return DBInnerUtil.PerformNonQuery(Query, Parameters, ConnectionString);

        }
        private static string AddSetterParam(string Name, string Value, string Set)
        {
            return Set += $"{Name} = @{Name}, ";

        }

        public static bool InsertSelect(string DBFrom, string DBTo, string ConnectionString, string ConditionColumnTo, string ConditionColumnFrom)
        {
            try
            {
                GeneralUtils.CheckNullArgs(DBFrom, DBTo, ConnectionString);
            }
            catch (Exception ex)
            {
                Utils.setError(ex, "Parámetros nulos en InsertSelect.");
                return false;
            }
            Utils.Loguear($"Preparando insert select de {DBFrom} a {DBTo}", "DBInsert");

            string Query = $"INSERT {DBTo} SELECT * FROM {DBFrom} WHERE {ConditionColumnTo} NOT IN (SELECT {ConditionColumnFrom} FROM {DBTo});";

            return DBInnerUtil.PerformRawNonQuery(Query, ConnectionString);
        }

        #region Insert Helpers
        private static string AddColumn(string Name, string Columns)
        {
            return Columns += $"{Name}, ";

        }

        private static string AddValue(string Value, string Values)
        {
            return Values += $"'{Value}', ";
        }
        private static string AddValueNotString(string Value, string Values)
        {
            return Values += $"{Value}, ";
        }
        private static string AddDecimalValue(object Value, string Values)
        {
            if (Value.ToString().Contains(','))
                Value = Value.ToString().Replace(',', '.');
            return Values += $"{Value}, ";
        }
        private static string AddBooleanValue(object Value, string Values)
        {
            var ValueAsBool = (bool)Value;
            if (ValueAsBool)
                return Values += "1, ";
            return Values += "0, ";
        }
        private static string AddDateTimeValue(object Value, string Values)
        {
            return Values += $"CONVERT(datetime,'{Value}'), ";
        }
        #endregion
    }

    public static class DBDelete
    {
        public static bool DeleteEntity<T>(T EntityInstance, string ConnectionString, string ConditionColumn, string ConditionValue)
        {
            try
            {
                GeneralUtils.CheckNullArgs(EntityInstance, ConnectionString, ConditionColumn, ConditionValue);
            }
            catch (Exception ex)
            {
                Utils.setError(ex, "Parámetros nulos en DeleteEntity.");
                return false;
            }
            Utils.Loguear("Eliminando " + EntityInstance.GetType().ToString());

            var EntityName = EntityInstance.GetType().Name;
            var Props = EntityInstance.GetType().GetProperties();


            List<SqlParameter> Parameters = new List<SqlParameter>();
            string TableName = $"{AccesoDB.DBAlpha}[COD_{EntityName}]";
            string Query = $"DELETE FROM {TableName} ";
            string Where = $"WHERE [{ConditionColumn}] = '{ConditionValue}';";



            Query += Where;
            return DBInnerUtil.PerformNonQuery(Query, Parameters, ConnectionString);

        }
    }

    public static class DBUpdate
    {
        public static bool UpdateEntity<T>(T EntityInstance, string ConnectionString, string ConditionColumn, object ConditionValue)
        {
            try
            {
                GeneralUtils.CheckNullArgs(EntityInstance, ConnectionString, ConditionColumn, ConditionValue);
            }
            catch (Exception ex)
            {
                Utils.setError(ex, "Parámetros nulos en UpdateEntity.");
                return false;
            }
            Utils.Loguear("Actualizando entidad " + EntityInstance.GetType().ToString());

            var EntityName = EntityInstance.GetType().Name;
            var Props = EntityInstance.GetType().GetProperties();

            List<SqlParameter> Parameters = new List<SqlParameter>();
            string Set = "SET ";
            string TableName = $"{AccesoDB.DBAlpha}[COD_{EntityName}]";
            string Query = $"UPDATE {TableName} ";
            string Where = $"WHERE [{ConditionColumn}] = '{ConditionValue}';";

            foreach (var p in Props)
            {
                var Value = p.GetValue(EntityInstance, null);

                if (Value != null)
                {
                    if (Value == ConditionValue)
                        continue;

                    Set = AddSetterParam(p.Name, Value.ToString(), Set);
                    Parameters.Add(new SqlParameter(p.Name, Value.ToString()));
                }
            }
            Set = Set.Substring(0, Set.Length - 2);

            Query += Set + " " + Where;
            return DBInnerUtil.PerformNonQuery(Query, Parameters, ConnectionString);

        }

        private static string AddSetterParam(string Name, string Value, string Set)
        {
            return Set += $"{Name} = @{Name}, ";

        }

        private static string AddValue(string Value, string Values)
        {
            return Values += $"'{Value}', ";
        }

        public static string GetUpdateQuery<T>(T Entity, string Column, string Value, string ConditionColumn, string ConditionValue)
        {
            try
            {
                GeneralUtils.CheckNullArgs(Entity, Column, Value, ConditionColumn, ConditionValue);
            }
            catch (Exception ex)
            {
                Utils.setError(ex, "Parámetros nulos en GetUpdateQuery.");
                return string.Empty;
            }
            Utils.Loguear("Obteniendo query para " + Entity.GetType().ToString());

            var EntityName = Entity.GetType().Name;

            string TableName = $"{AccesoDB.DBAlpha}[COD_{EntityName}]";
            string Query = $"UPDATE {TableName} ";
            string Set = $"SET {Column} = '{Value}' ";
            string Where = $"WHERE {ConditionColumn} = '{ConditionValue}';";

            return Query + Set + Where;
        }

        public static bool BulkUpdate(List<string> Queries, string ConnectionString)
        {
            try
            {
                GeneralUtils.CheckNullArgs(Queries, ConnectionString);
            }
            catch (Exception ex)
            {
                Utils.setError(ex, "Parámetros nulos en BulkUpdate.");
                return false;
            }
            string BulkQuery = "";
            foreach (var q in Queries)
            {
                BulkQuery += q;
            }
            return DBInnerUtil.PerformRawNonQuery(BulkQuery, ConnectionString);
        }
    }

    public static class DBInnerUtil
    {
        public static bool PerformRawNonQuery(string Query, string ConnectionString)
        {

            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand())
                    {
                        conn.Open();
                        command.Connection = conn;
                        command.CommandType = CommandType.Text;
                        command.CommandTimeout = 0;
                        command.CommandText = Query;
                        command.ExecuteNonQuery();
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                Utils.setError(ex);
                return false;
            }
        }
        public static bool PerformRawGetIdentity(string Query, string ConnectionString, out decimal? Identity)
        {
            Identity = null;
            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand())
                    {
                        conn.Open();
                        command.Connection = conn;
                        command.CommandType = CommandType.Text;
                        command.CommandTimeout = 0;
                        command.CommandText = Query + "SELECT SCOPE_IDENTITY()";
                        Identity = (decimal?)command.ExecuteScalar();
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                Utils.setError(ex);
                return false;
            }
        }

        public static bool PerformNonQuery(string Query, List<SqlParameter> Parameters, string ConnectionString)
        {

            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand())
                    {
                        conn.Open();
                        command.Connection = conn;
                        command.CommandText = Query;
                        Parameters.ForEach(x => command.Parameters.Add(x));
                        command.ExecuteNonQuery();
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                Utils.setError(ex);
                return false;
            }
        }
        public static bool PerformNonQueryGetIdentity(string Query, List<SqlParameter> Parameters, string ConnectionString, out decimal? Identity)
        {
            Identity = null;
            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand())
                    {
                        conn.Open();
                        command.Connection = conn;
                        Parameters.ForEach(x => command.Parameters.Add(x));
                        command.CommandText = Query + "SELECT SCOPE_IDENTITY()";
                        Identity = (decimal?)command.ExecuteScalar();
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                Utils.setError(ex);
                return false;
            }
        }

    }
}
