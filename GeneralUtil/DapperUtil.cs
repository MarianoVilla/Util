using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Dapper;

namespace GeneralUtil
{
    public class DapperWrapper
    {
        string ConnectionString;
        public DapperWrapper(string ConnectionString)
        {
            this.ConnectionString = ConnectionString;
        }
        public static IEnumerable<T> MapEntity<T>(T Entity, dynamic DapperResult) where T : new()
        {
            List<T> Output = new List<T>();
            foreach (var row in DapperResult)
            {
                T Ent = new T();
                var PropsOfT = Entity.GetType().GetProperties();
                foreach (var p in PropsOfT)
                {
                    var data = (IDictionary<string, object>)row;
                    var DataInRow = data[p.Name];
                    if (DataInRow == null)
                        continue;
                    p.SetValue(Ent, DataInRow, null);
                }
                Output.Add(Ent);
            }
            return Output;
        }

        public IEnumerable<dynamic> Query(string QueryString)
        {
            using (IDbConnection db = new SqlConnection(ConnectionString))
            {
                return db.Query(QueryString);
            }
        }
        public IEnumerable<dynamic> Query(string QueryString, DynamicParameters Parameters)
        {
            using (IDbConnection db = new SqlConnection(ConnectionString))
            {
                return db.Query(QueryString, Parameters);
            }
        }
        public IEnumerable<dynamic> Query(string QueryString, object Parameters)
        {
            using (IDbConnection db = new SqlConnection(ConnectionString))
            {
                return db.Query(QueryString, Parameters);
            }
        }
        public int ExecuteScalar(string QueryString)
        {
            using (IDbConnection db = new SqlConnection(ConnectionString))
            {
                return db.ExecuteScalar<int>(QueryString);
            }
        }
        public int ExecuteScalar(string QueryString, DynamicParameters Parameters)
        {
            using (IDbConnection db = new SqlConnection(ConnectionString))
            {
                return db.ExecuteScalar<int>(QueryString, Parameters);
            }
        }
        public int ExecuteScalar(string QueryString, object Parameters)
        {
            using (IDbConnection db = new SqlConnection(ConnectionString))
            {
                return db.ExecuteScalar<int>(QueryString, Parameters);
            }
        }
        public int Execute(string QueryString, DynamicParameters Parameters)
        {
            using (IDbConnection db = new SqlConnection(ConnectionString))
            {
                return db.Execute(QueryString, Parameters);
            }
        }
        public int Execute(string QueryString, object Parameters)
        {
            using (IDbConnection db = new SqlConnection(ConnectionString))
            {
                return db.Execute(QueryString, Parameters);
            }
        }
    }
}
