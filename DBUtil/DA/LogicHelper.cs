using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DBUtil.DA
{
    internal static class LogicHelper
    {
        public static string GetInitialCatalog(string ConnectionString)
        {
            string InitialCatalog = Dager.StringUtil.GetConnectionStringValue(ConnectionString, "Initial Catalog");

            return $"[{InitialCatalog}].[dbo].";
        }

    }
}
