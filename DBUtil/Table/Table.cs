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

        public Table()
        {
            var IDColumn = typeof(T).GetProperties().FirstOrDefault(x => Attribute.IsDefined(x, typeof(ID)));
            if (IDColumn == null)
                throw new NoIDColumnException("Every table has to have an [ID] property.");
            Columns.Add(IDColumn.Name);
        }

    }
}
