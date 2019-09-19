using DBUtil.DA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DBUtil.Table
{
    public class Table<T> where T : class, new()
    {
        internal List<string> Columns { get; } = new List<string>();
        internal string[] IDColumns { get; }

        public Table()
        {
            var IDProps = typeof(T).GetProperties().Where(x => Attribute.IsDefined(x, typeof(ID))).ToArray();
            if (IDProps == null)
                return;
            IDColumns = new string[IDProps.Count()];
            for(int i = 0; i < IDProps.Count(); i++)
                IDColumns[i] = IDProps[i].Name;
        }

    }
}
