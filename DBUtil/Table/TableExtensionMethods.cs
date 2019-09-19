using DBUtil.DA;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DBUtil.Table
{
    public static partial class TableExtension
    {
        internal static BaseDA BaseDA { get; set; }

        public static T SelectSingle<T>(this Table<T> Self, object ConditionValue, string TableName = null)
            where T : class, new()
        {
            return BaseDA.SelectSingle(new T(), Self.Columns.First(), ConditionValue, TableName);
        }
        public static List<T> SelectDistinct<T>(this Table<T> Self, object[] ConditionValues, string[] DistinctBy, string[] ConditionColumns = null, string TableName = null, string ANDOR = "AND")
            where T : class, new() => BaseDA.SelectDistinct(new T(), ConditionColumns ?? Self.IDColumns, ConditionValues, DistinctBy, TableName, ANDOR);

        public static List<T> Select<T>(this Table<T> Self, object[] ConditionValues, string[] ConditionColumns = null, string TableName = null)
            where T : class, new() => BaseDA.Select(new T(), ConditionColumns ?? Self.IDColumns, ConditionValues, TableName);

        public static List<T> Select<T>(this Table<T> Self, object[] ConditionValues, string[] ConditionColumns = null, string TableName = null, string[] Columns = null)
            where T : class, new() => BaseDA.Select(new T(), ConditionColumns ?? Self.IDColumns, ConditionValues, TableName, Columns);

        public static List<T> Select<T>(this Table<T> Self, string TableName = null)
            where T : class, new()
        {
            return BaseDA.Select(new T(), TableName);
        }

        public static int Delete<T>(this Table<T> Self, object ConditionValue, string TableName = null)
            where T : class, new()
        {
            return BaseDA.Delete(new T(), Self.Columns.First(), ConditionValue, TableName);
        }
        public static int DeleteFrom<T>(this Table<T> Self, string TableName = null)
            where T : class, new()
        {
            return BaseDA.DeleteFrom(new T(), TableName);
        }
        public static int Insert<T>(this Table<T> Self, T Entity, string TableName = null)
            where T : class, new()
        {
            return BaseDA.Insert(Entity, TableName);
        }
        public static decimal? InsertGetID<T>(this Table<T> Self, T Entity, string TableName = null)
            where T : class, new()
        {
            return BaseDA.InsertGetID(Entity, TableName);
        }

        public static int Update<T>(this Table<T> Self, T Entity, object ConditionValue = null, string TableName = null)
            where T : class, new()
        {
            ConditionValue = Dager.GeneralUtil.Coalesce(ConditionValue, Dager.EntitiesUtil.GetPropsValueByAttribute(Entity, typeof(ID)));
            return BaseDA.Update(Entity, Self.Columns.First(), ConditionValue, TableName);
        }
        public static int Update<T>(this Table<T> Self, T Entity, object[] ConditionValues = null, string[] ConditionColumns = null, string TableName = null)
            where T : class, new()
        {
            return BaseDA.Update(Entity, ConditionColumns ?? Self.IDColumns, ConditionValues ?? Dager.EntitiesUtil.GetPropsValueByAttribute(Entity, typeof(ID)), TableName);
        }
        public static decimal? UpdateGetID<T>(this Table<T> Self, T Entity, object ConditionValue = null, string TableName = null)
            where T : class, new()
        {
            ConditionValue = Dager.GeneralUtil.Coalesce(ConditionValue, Dager.EntitiesUtil.GetPropsValueByAttribute(Entity, typeof(ID)));
            return BaseDA.UpdateGetID(Entity, Self.Columns.First(), ConditionValue, TableName);
        }

    }
}
