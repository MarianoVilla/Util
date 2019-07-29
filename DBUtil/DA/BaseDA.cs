using DBUtil.Table;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;

namespace DBUtil.DA
{
    public abstract class BaseDA
    {
        #region Props.

        public string ConnectionString { get; set; }
        private string InitialCatalog { get; set; }
        public Type SelectAttribute { get; set; }
        public Type InsertAttribute { get; set; }

        #endregion

        #region Ctor.

        protected BaseDA(string ConnectionString, Type SelectAttribute = null, Type InsertAttribute = null)
        {
            this.ConnectionString = ConnectionString;
            this.SelectAttribute = SelectAttribute ?? typeof(Selectable);
            this.InsertAttribute = InsertAttribute ?? typeof(Insertable);
            InitialCatalog = LogicHelper.GetInitialCatalog(ConnectionString);
            TableExtensionMethods.BaseDA = this;
        }

        #endregion

        #region Select.

        public T Select<T>(T Entity, string ConditionColumn, object ConditionValue, string TableName = null)
            where T : class, new() => SQL.Select.ByAttribute(Entity, SelectAttribute, ConditionColumn, ConditionValue, ConnectionString, $"{InitialCatalog}[{TableName}");

        public List<T> Select<T>(T Entity, string TableName = null)
            where T : class, new() => SQL.Select.ByAttribute(Entity, SelectAttribute, ConnectionString, $"{InitialCatalog}[{TableName}");

        #endregion

        #region Delete.

        public int Delete<T>(T Entity, string ConditionColumn, object ConditionValue, string TableName = null)
            => SQL.Delete.DeleteEntity(Entity, ConnectionString, ConditionColumn, ConditionValue, $"{InitialCatalog}[{TableName}");

        public int DeleteFrom<T>(T Entity, string TableName = null)
            => SQL.Delete.DeleteFrom(Entity, ConnectionString, $"{InitialCatalog}[{TableName}");

        #endregion

        #region Insert.

        public int Insert<T>(T Entity, string TableName = null)
            => SQL.Insert.ByAttribute(Entity, InsertAttribute, ConnectionString, $"{InitialCatalog}[{TableName}");

        public decimal? InsertGetID<T>(T Entity, string TableName = null)
            => SQL.Insert.ByAttributeGetID(Entity, InsertAttribute, ConnectionString, $"{InitialCatalog}[{TableName}");

        #endregion

        #region Update.

        public int Update<T>(T Entity, string ConditionColumn, object ConditionValue, string TableName = null)
            => SQL.Update.ByAttribute(Entity, ConnectionString, ConditionColumn, ConditionValue, InsertAttribute, $"{InitialCatalog}[{TableName}");

        public decimal? UpdateGetID<T>(T Entity, string ConditionColumn, object ConditionValue, string TableName = null)
            => SQL.Update.ByAttributeGetID(Entity, ConnectionString, ConditionColumn, ConditionValue, InsertAttribute, $"{InitialCatalog}[{TableName}");

        #endregion
    }
}
