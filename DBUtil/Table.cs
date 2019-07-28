using DBUtil.DA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DBUtil
{
    public class Table<T> where T : class, new()
    {
        internal List<string> Columns { get; }

        public Table()
        {
            var IDColumn = GetType().GetProperties().FirstOrDefault(x => Attribute.IsDefined(x, typeof(ID)));
            if (IDColumn == null)
                throw new NoIDColumnException("Every table has to have an [ID] property.");
            Columns.Add(IDColumn.Name);
        }

    }
}
