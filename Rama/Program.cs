using Dager;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;
using System.Threading;
using System.Net;
using System.Web;
using System.Collections;
using System.ComponentModel;

namespace Rama
{
    class Program
    {

        static void Main(string[] args)
        {

            Console.ReadKey();
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
