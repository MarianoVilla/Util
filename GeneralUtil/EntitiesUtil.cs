using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dager
{
    public static class EntitiesUtil
    {
        public static T FillEntityFromDTRow<T>(T Entity, DataRow DR) where T : new()
        {
            //var props = Entity.GetType().GetProperties();
            //foreach (var prop in props)
            //{
            //    prop.SetValue(Entity, DR[prop.Name]);
            //}
            return Entity;
        }
        public static SqlParameter[] GetParamsByAttribute<T, U>(T Entity, U AttributeType) where U : Type
        {
            var Props = Entity.GetType().GetProperties().Where(x => Attribute.IsDefined(x, AttributeType)).ToArray();
            var Parametros = new SqlParameter[Props.Count()];
            for (int i = 0; i < Props.Count(); i++)
            {
                Parametros[i] = new SqlParameter(Props[i].Name, Props[i].GetValue(Entity, null));
            }
            return Parametros;
        }
        public static object[] GetPropsValueByAttribute<T, U>(T Entity, U AttributeType)
            where U : Type
        {
            var Props = Entity.GetType().GetProperties().Where(x => Attribute.IsDefined(x, AttributeType)).ToList();
            object[] Output = new object[Props.Count()];
            for (int i = 0; i < Props.Count(); i++)
                Output[i] = Props[i].GetValue(Entity, null);
            return Output;
        }

    }
}
