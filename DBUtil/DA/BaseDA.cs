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
            TableExtension.BaseDA = this;
        }

        #endregion

        #region Select.

        public T SelectSingle<T>(T Entity, string ConditionColumn, object ConditionValue, string TableName = null)
            where T : class, new() => SQL.Select.ByAttributeSingle(Entity, SelectAttribute, ConditionColumn, ConditionValue, ConnectionString, $"{InitialCatalog}[{TableName ?? Entity.GetType().Name}]");

        public List<T> SelectDistinct<T>(T Entity, string[] ConditionColumns, object[] ConditionValues, string[] DistinctBy, string TableName = null, string ANDOR = "AND")
            where T : class, new() => SQL.Select.ByAttribute(Entity, SelectAttribute, ConditionColumns, ConditionValues, ConnectionString, DistinctBy, $"{InitialCatalog}[{TableName ?? Entity.GetType().Name}]", ANDOR);

        public List<T> Select<T>(T Entity, string TableName = null)
            where T : class, new() => SQL.Select.ByAttribute(Entity, SelectAttribute, ConnectionString, $"{InitialCatalog}[{TableName ?? Entity.GetType().Name}]");

        public List<T> Select<T>(T Entity, string[] ConditionColumns, object[] ConditionValues, string TableName = null)
            where T : class, new() => SQL.Select.ByAttribute(Entity, SelectAttribute, ConditionColumns, ConditionValues, ConnectionString, $"{InitialCatalog}[{TableName ?? Entity.GetType().Name}]");

        public List<T> Select<T>(T Entity, string[] ConditionColumns, object[] ConditionValues, string TableName = null, string[] Columns = null)
            where T : class, new() => SQL.Select.ByAttribute(Entity, SelectAttribute, ConditionColumns, ConditionValues, ConnectionString, $"{InitialCatalog}[{TableName ?? Entity.GetType().Name}]", Columns);

        #endregion

        #region Delete.

        public int Delete<T>(T Entity, string ConditionColumn, object ConditionValue, string TableName = null)
            => SQL.Delete.DeleteEntity(Entity, ConnectionString, ConditionColumn, ConditionValue, $"{InitialCatalog}[{TableName ?? Entity.GetType().Name}]");

        public int DeleteFrom<T>(T Entity, string TableName = null)
            => SQL.Delete.DeleteFrom(Entity, ConnectionString, $"{InitialCatalog}[{TableName}]");

        #endregion

        #region Insert.

        public int Insert<T>(T Entity, string TableName = null)
            => SQL.Insert.ByAttribute(Entity, InsertAttribute, ConnectionString, $"{InitialCatalog}[{TableName ?? Entity.GetType().Name}]");

        public decimal? InsertGetID<T>(T Entity, string TableName = null)
            => SQL.Insert.ByAttributeGetID(Entity, InsertAttribute, ConnectionString, $"{InitialCatalog}[{TableName ?? Entity.GetType().Name}]");

        #endregion

        #region Update.

        public int Update<T>(T Entity, string ConditionColumn, object ConditionValue, string TableName = null)
            => SQL.Update.ByAttribute(Entity, ConnectionString, ConditionColumn, ConditionValue, InsertAttribute, $"{InitialCatalog}[{TableName ?? Entity.GetType().Name}]");

        public int Update<T>(T Entity, string[] ConditionColumns, object[] ConditionValues, string TableName = null)
            => SQL.Update.ByAttribute(Entity, ConnectionString, ConditionColumns, ConditionValues, InsertAttribute, $"{InitialCatalog}[{TableName ?? Entity.GetType().Name}]");

        public decimal? UpdateGetID<T>(T Entity, string ConditionColumn, object ConditionValue, string TableName = null)
            => SQL.Update.ByAttributeGetID(Entity, ConnectionString, ConditionColumn, ConditionValue, InsertAttribute, $"{InitialCatalog}[{TableName ?? Entity.GetType().Name}]");

        #endregion
    }
}
