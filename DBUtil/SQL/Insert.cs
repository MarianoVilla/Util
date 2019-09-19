using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;

namespace DBUtil.SQL
{
    public static class Insert
    {

        public static int ByAttribute<T, U>(T Entity, U AttributeType, string ConnectionString, string TableName = null) where U : Type
        {
            Type EntityType = Entity.GetType();
            string EntityName = EntityType.Name;
            IEnumerable<PropertyInfo> InsertableProps = EntityType.GetProperties().Where(x => Attribute.IsDefined(x, AttributeType));

            TableName = Dager.StringUtil.Coalesce(TableName, EntityName);
            string Columns = "(";
            string Values = "VALUES (";

            var Parameters = Dager.EntitiesUtil.GetParamsByAttribute(Entity, AttributeType);

            foreach (var p in InsertableProps)
            {
                Columns += $"{p.Name}, ";
                Values += $"@{p.Name}, ";
            }
            Columns = Columns.Substring(0, Columns.Length - 2) + ")\n";
            Values = Values.Substring(0, Values.Length - 2) + ")";
            string Query = $"INSERT INTO {TableName} {Columns} {Values}";

            return InnerUtil.ExNonQuery(Query, Parameters, ConnectionString);
        }

        public static decimal? ByAttributeGetID<T, U>(T Entity, U AttributeType, string ConnectionString, string TableName = null) where U : Type
        {

            Type EntityType = Entity.GetType();
            string EntityName = EntityType.Name;
            IEnumerable<PropertyInfo> InsertableProps = EntityType.GetProperties().Where(x => Attribute.IsDefined(x, AttributeType));

            TableName = Dager.StringUtil.Coalesce(TableName, EntityName);
            string Columns = "(";
            string Values = "VALUES (";

            var Parameters = Dager.EntitiesUtil.GetParamsByAttribute(Entity, AttributeType);

            foreach (var p in InsertableProps)
            {
                Columns += $"{p.Name}, ";
                Values += $"@{p.Name}, ";
            }
            Columns = Columns.Substring(0, Columns.Length - 2) + ")\n";
            Values = Values.Substring(0, Values.Length - 2) + ")";
            string Query = $"INSERT INTO {TableName} {Columns} {Values} SELECT SCOPE_IDENTITY()";

            return InnerUtil.ExScalarGetID(Query, Parameters, ConnectionString);
        }


    }
}
