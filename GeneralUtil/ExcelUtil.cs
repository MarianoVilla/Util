using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dager
{
    public static class ExcelUtil
    {
        public static void LeerExcel(string con, string sheetName)
        {
            try
            {
                using (OleDbConnection connection = new OleDbConnection(con))
                {
                    connection.Open();
                    DataTable dt = connection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                    List<string> sheetNames = new List<string>();
                    foreach (DataRow row in dt.Rows)
                    {
                        sheetNames.Add(row["TABLE_NAME"].ToString());
                    }
                    if (!sheetNames.Any(x => x == sheetName))
                        return;
                    OleDbCommand command = new OleDbCommand("SELECT * FROM [Hoja1$]", connection);
                    using (OleDbDataReader dr = command.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            var row1Col0 = dr[0];
                            Console.WriteLine(row1Col0);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }

        }

        public static void SaveDT(DataTable DT)
        {
            
        }
    }
}
