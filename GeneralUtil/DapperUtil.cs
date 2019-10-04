using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeneralUtil
{

    public class DapperWrapper
    {
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
    }
}
