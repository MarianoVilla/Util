using DBUtil.DA;
using DBUtil.Table;
using System;
using System.ComponentModel;
using System.Linq;

namespace Rama
{
    public class Customer
    {
        [ID]
        [Selectable]
        [Insertable]
        public int ClientNumber { get; set; }

        [Selectable]
        [Insertable]
        public string SomeOtherProp { get; set; }

        public abstract class BaseNCM
        {
            public virtual string IDARTICULO { get; set; }
            public virtual string ncm { get; set; }
            public virtual bool Priorizar { get; set; }
        }
        public class NCMXML : BaseNCM
        {
            [ID]
            [Selectable]
            //[Browsable(false)]
            public override string IDARTICULO { get; set; }

        }
        public class NCMCatalogo : BaseNCM, ICloneable
        {
            [ID]
            [Selectable]
            //[Browsable(false)]
            public override string IDARTICULO { get; set; }
            [ID]
            [Selectable]
            [Browsable(false)]
            public string IdCatalogo { get; set; }
            [Selectable]
            [DisplayName("NCM")]
            public override string ncm { get; set; }

            public object Clone()
            {
                return MemberwiseClone();
            }
        }
        public class NCMValidaXML : BaseNCM
        {
            [Selectable]
            [Insertable]
            [ID]
            [DisplayName("Catálogo")]
            public string IdCatalogo { get; set; }
            [Selectable]
            [Insertable]
            [DisplayName("NCM (priorizada)")]
            public override string ncm { get; set; }
            [Selectable]
            [Insertable]
            [DisplayName("NCM (no priorizada)")]
            public string ncmDiferencia { get; set; }
            [Selectable]
            [Insertable]
            public DateTime Fecha { get; set; }
            [Browsable(false)]
            public override bool Priorizar { get; set; }

        }
        public class ncm
        {
            [Selectable]
            [DisplayName("Posición arancelaria")]
            public string POSICION_ARANCELARIA { get; set; }
        }
        public class Catalogo
        {
            [ID]
            [Selectable]
            public string idcatalogo { get; set; }
            [Selectable]
            public string descripcion { get; set; }
        }
        public class subitems
        {
            [ID]
            [Selectable]
            public string interno { get; set; }
        }
        public class JSONBase
        {
            [Selectable]
            public string NombreArchivoJson { get; set; }
            [Selectable]
            public string NroDespacho { get; set; }
        }
        public class UsageTestDA : BaseDA
        {
            public Table<Customer> Customers { get; set; }
            public Table<NCMCatalogo> NCMsCatalogo { get; set; }
            public Table<NCMValidaXML> NCMsBase { get; set; }
            public Table<Catalogo> Catalogos { get; set; }
            public Table<ncm> Posiciones { get; set; }
            public Table<JSONBase> Jsons;

            public UsageTestDA(string ConnectionString) : base(ConnectionString)
            {
                Customers = new Table<Customer>();
                NCMsCatalogo = new Table<NCMCatalogo>();
                NCMsBase = new Table<NCMValidaXML>();
                Catalogos = new Table<Catalogo>();
                Posiciones = new Table<ncm>();
                Jsons = new Table<JSONBase>();
            }

        }
        class Program
        {

            static void Main(string[] args)
            {
                TestMethod();
                Console.ReadKey();
            }

            static void TestMethod()
            {
                UsageTestDA DB = new UsageTestDA(@"Password=alpha2000;Persist Security Info=True;User ID=sintiaw;Data Source=DESKTOP-5LNC589\SQLEXPRESS;Initial Catalog=Alpha2000;");
                var CustomerObject = new Customer { ClientNumber = 2, SomeOtherProp = "Test" };

                //var NCMsCatalogo = DB.NCMsCatalogo.Select(new string[] { "NEOLOG" }, new string[] { "IdCatalogo" }, null, new string[] { "IDARTICULO", "IdCatalogo", "ncm" });
                //var NCMsFromDB = DB.NCMsBase.Select(new string[] { "00043517372A", "NEOLOG" }, null, "NCMValidaXML");
                //var NCMsFromDB = DB.NCMsBase.Select(new string[] { "ble" }, new string[] { "ble" }, "NCMValidaXML");
                //var Test = new NCMValidaXML() { IdCatalogo = "00", Fecha = DateTime.Now, ncm = "8708.50.99.990U", ncmDiferencia = "8708.29.99.990J", Priorizar = false };
                //var Affected = DB.NCMsBase.Update(Test, new object[] { "8708.29.99.990J", "8708.50.99.990U" }, new string[] { "ncm", "ncmDiferencia" });
                var Posiciones = DB.Posiciones.SelectDistinct(new object[] { "ConditionValue", "ConditionValue2" }, new string[] { "DistinctBy1" }, new string[] { "ConditionColumn" }, "Posiciones", "OR");

            }


            private static string RemoverPathCalificado(string FileName)
            {
                int IndexBarra = FileName.LastIndexOf('\\') + 1;

                if (IndexBarra < 0)
                    return string.Empty;

                return FileName.Substring(IndexBarra);
            }
            public static long GetLastLine(string filePath)
            {
                long Lineas = 0;

                if (!filePath.Contains("_LINEASPROCESADAS_"))
                {
                    return Lineas;
                }

                filePath = RemoverPathCalificado(filePath);

                int IndiceGuion = filePath.LastIndexOf('_');

                int IndicePunto = filePath.LastIndexOf('.');

                string NumLineas = filePath.Substring(IndiceGuion, IndicePunto);

                long.TryParse(NumLineas, out Lineas);

                return Lineas;

            }
            private static string ExtraerPeriodo(string FileName)
            {
                FileName = @"Alpha.bla.sisi\bla.bla\impo_2018_201901201901.zip.lst.PROCESADO";

                FileName = RemoverPathCalificado(FileName);

                int IndexPunto = FileName.IndexOf('.');

                int IndexPuntoMenosFecha = IndexPunto - 6;

                var DateFromFile = FileName.Substring(IndexPuntoMenosFecha, 6);

                return DateFromFile;
            }

        }
    }
}
