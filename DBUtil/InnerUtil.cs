using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace DBUtil
{
    internal static class InnerUtil
    {
        public static int ExNonQuery(string Query, SqlParameter[] Parameters, string ConnectionString)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            using (SqlCommand command = new SqlCommand(Query, conn))
            {
                conn.Open();
                command.Parameters.AddRange(Parameters);
                return command.ExecuteNonQuery();
            }
        }
        public static decimal? ExScalarGetID(string Query, SqlParameter[] Parameters, string ConnectionString)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            using (SqlCommand command = new SqlCommand(Query, conn))
            {
                conn.Open();
                command.Parameters.AddRange(Parameters);
                return (decimal?)command.ExecuteScalar();
            }
        }
        public static T ExReader<T, U>(T Entity, string Query, SqlParameter[] Parameters, string ConnectionString, U AttributeType)
            where T : class, new()
            where U : Type
        {
            Entity = null;
            SqlDataReader dr = null;
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            using (SqlCommand command = new SqlCommand(Query, conn))
            {
                conn.Open();
                command.CommandType = CommandType.Text;
                command.CommandTimeout = 0;
                command.Parameters.AddRange(Parameters);
                dr = command.ExecuteReader();
                while (dr.Read())
                {
                    Entity = new T();
                    Entity = LoadByAttribute(dr, Entity, AttributeType);
                    break;
                }
                return Entity;
            }
        }
        public static List<T> ExReader<T, U>(T Entity, U AttributeType, string Query, string ConnectionString)
            where T : class, new()
            where U : Type
        {
            List<T> Entities = new List<T>();
            SqlDataReader dr = null;
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            using (SqlCommand command = new SqlCommand(Query, conn))
            {
                conn.Open();
                command.CommandType = CommandType.Text;
                command.CommandTimeout = 0;
                dr = command.ExecuteReader();
                while (dr.Read())
                {
                    Entities.Add(LoadByAttribute(dr, Entity, AttributeType));
                }
                return Entities;
            }
        }
        static T LoadByAttribute<T, U>(IDataReader dr, T Entity, U AttributeType) where U : Type
        {
            if (Entity == null)
                return Entity;

            var EntityProps = Entity.GetType().GetProperties().Where(x => Attribute.IsDefined(x, AttributeType));

            foreach (var Prop in EntityProps)
            {
                string PropName = Prop.Name;
                if (Convert.IsDBNull(dr[PropName]) || dr[PropName] == null)
                    continue;
                Prop.SetValue(Entity, Dager.GeneralUtil.ChangeType(dr[PropName], Prop.PropertyType), null);
            }
            return Entity;
        }

    }
}
