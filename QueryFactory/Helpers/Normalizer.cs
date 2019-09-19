using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryFactory.Helpers
{
    public static class Normalizer
    {
        public static string TableName(string InitialCatalog, string TableName)
        {
            return $"{InitialCatalog}[{TableName}]";
        }

        public static string[] TableNames(string InitialCatalog, params string[] TableNames)
        {
            for (int i = 0; i < TableNames.Length; i++)
                TableNames[i] = $"{InitialCatalog}[{TableNames[i]}]";
            return TableNames;
        }
    }
}
