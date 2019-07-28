using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;

namespace DBUtil.DA
{
    public static class TableExtensions
    {
        public static BaseDA BaseDA { get; set; }

        public static Table<T> Select<T>(this Table<T> Entity, object ConditionValue, string Table = null)
            where T : class, new()
        {
            return BaseDA.Get(Entity, Entity.Columns.First(), ConditionValue, Table);
        }
    }
    public abstract class BaseDA
    {
        public string ConnectionString { get; set; }
        private string InitialCatalog { get; set; }
        public Type SelectAttribute { get; set; }
        public Type InsertAttribute { get; set; }

        public BaseDA(string ConnectionString, Type SelectAttribute = null, Type InsertAttribute = null)
        {
            this.ConnectionString = ConnectionString;
            this.SelectAttribute = SelectAttribute ?? typeof(Selectable);
            this.InsertAttribute = InsertAttribute ?? typeof(Insertable);
            InitialCatalog = LogicHelper.GetInitialCatalog(ConnectionString);
            TableExtensions.BaseDA = this;
        }


        public T Get<T>(T Entity, string ConditionColumn, object ConditionValue, string Table = null)
            where T : class, new()
        {
            return Select.ByAttribute(Entity, SelectAttribute, ConditionColumn, ConditionValue, ConnectionString, Table);
        }

        public List<T> GetList<T>(T Entity, string TableName = null)
            where T : class, new()
        {
            return Select.ByAttribute(Entity, SelectAttribute, ConnectionString, TableName);
        }

        public int Delete<T>(T Entity, string ConditionColumn, string ConditionValue, string TableName = null)
        {
            return DBUtil.Delete.DeleteEntity(Entity, ConnectionString, ConditionColumn, ConditionValue, TableName);
        }

        public int Insert<T>(T Entity, string TableName = null)
        {
            return DBUtil.Insert.ByAttribute(Entity, InsertAttribute, ConnectionString, TableName);
        }

        public decimal? InsertGetID<T>(T Entity, string TableName = null)
        {
            return DBUtil.Insert.ByAttributeGetID(Entity, InsertAttribute, ConnectionString, TableName);
        }

        public int Update<T>(T Entity, string ConditionColumn, object ConditionValue, string TableName = null)
        {
            return DBUtil.Update.ByAttribute(Entity, ConnectionString, ConditionColumn, ConditionValue, InsertAttribute, TableName);
        }

        public decimal? UpdateGetID<T>(T Entity, string ConditionColumn, object ConditionValue, string TableName = null)
        {
            return DBUtil.Update.ByAttributeGetID(Entity, ConnectionString, ConditionColumn, ConditionValue, InsertAttribute, TableName);
        }

    }
}
