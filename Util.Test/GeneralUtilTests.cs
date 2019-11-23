using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Dager;
using Util.Test.InnerAux;
using Xunit;

namespace Dager.Test
{
    public class GeneralUtilTest
    {

        [Fact]
        public void CheckNullArgs_ShouldThrowNullRefEx()
        {
            Assert.Throws<NullReferenceException>(() => GeneralUtil.CheckNullArgs(null));
        }

        [Fact]
        public void CheckNullArgs_ShouldWork()
        {
            GeneralUtil.CheckNullArgs("Test", 1, 1.5, new Exception());
            GeneralUtil.CheckNullArgs(new List<string> { "Test" });
        }

        [Fact]
        public void CheckEmptyStrings_ShouldThrowEx()
        {
            Assert.Throws<ArgumentException>(() => GeneralUtil.CheckEmptyStrings(""));
            Assert.Throws<NullReferenceException>(() => GeneralUtil.CheckEmptyStrings(null));
        }

        [Fact]
        public void CheckEmptyStrings_ShouldWork()
        {
            GeneralUtil.CheckEmptyStrings("Hi");
            GeneralUtil.CheckEmptyStrings("Hi", "Test");
            GeneralUtil.CheckEmptyStrings();
        }

        [Fact]
        public void GetYearMonths()
        {
            List<DateTime> Months = new List<DateTime>() { DateTime.Now, DateTime.Now.AddMonths(1) };
        }


        [Theory]
        [InlineData(@"Alpha.bla.sisi\bla.bla\Test")]
        [InlineData(@"Blablabla\Test")]
        public void RemoverPathCalificado_ShouldReturnFileName(string FileName)
        {
            int IndexBarra = FileName.LastIndexOf('\\') + 1;

            if (IndexBarra < 0)
                return;

            var Expected = "Test";

            var Actual = FileName.Substring(IndexBarra);

            Assert.Equal(Expected, Actual);
        }

        [Theory]
        [InlineData(3, @"C:\Users\dager\Desktop\Desktop\Alpha\File.txt")]
        [InlineData(4, @"C:\Users\dager\Desktop\Desktop\Alpha\File.txt")]
        [InlineData(50, @"C:\Users\dager\Desktop\Desktop\Alpha\File.txt")]
        [InlineData(1.2, @"C:\Users\dager\Desktop\Desktop\Alpha\File.txt")]
        public static void RemoveLines(long limit, string path)
        {
            if (limit < 0)
                return;
            long i = 0;
            string Renamed = path + "_LINEASPROCESADAS_" + limit + ".lst";
            string line = string.Empty;
            using (StreamReader sr = new StreamReader(path))
            using (StreamWriter sw = new StreamWriter(Renamed))
            {
                while ((line = sr.ReadLine()) != null)
                {
                    if (i >= limit)
                    {
                        sw.WriteLine(line);
                    }
                    i++;
                }
            }

            var LinesFromOriginalFile = File.ReadAllLines(path);
            var LinesAfterReading = File.ReadAllLines(Renamed);
            //File.Delete(path);

            Assert.NotEqual(LinesAfterReading, LinesFromOriginalFile);
        }

        [Theory]
        [InlineData(@"C:\Users\dager\Desktop\Desktop\Alpha\File.txt")]
        public static void ReachedEOF_ShouldReach(string FilePath)
        {
            bool ReachedEOF = false;
            using(StreamReader sr = new StreamReader(FilePath))
            {
                string line = string.Empty;

                while ((line = sr.ReadLine()) != null){}
                ReachedEOF = sr.EndOfStream;
            }
            Assert.True(ReachedEOF);
        }


        [Theory]
        [InlineData(0, typeof(TestEnum))]
        [InlineData("defaultValue", typeof(TestEnum))]
        [InlineData(10.0, typeof(decimal?))]
        [InlineData(null, typeof(string))]
        [InlineData(123, typeof(object))]
        [InlineData("SomeString", typeof(DateTime))]
        public static void ChangeType_ShouldWork(object value, Type t)
        {
            GeneralUtil.ChangeType(value, t);
        }

        [Theory]
        [InlineData(@"C:\Users\dager\Desktop\Desktop\Alpha\FileWithEOF.txt")]
        public static void ReachedEOF_ShouldNotReach(string FilePath)
        {
            bool ReachedEOF = false;
            using (StreamReader sr = new StreamReader(FilePath))
            {
                string line = string.Empty;

                while ((line = sr.ReadLine()) != null)
                {
                    if (line == "Stop")
                        break;
                }
                ReachedEOF = sr.EndOfStream;
            }
            Assert.False(ReachedEOF);
        }

        [Theory]
        [InlineData(@"cmd.exe", null)]
        public static void RunProcess_ShouldWork(string Path, string Params = null)
        {
            GeneralUtil.RunProcess(Path, Params);
        }


        public static List<string> Splittear_ShouldWork(Regex Reg, string line, char CriterioSplit)
        {
            return line.Split(CriterioSplit)
                .Select(x => Reg.Replace(x, string.Empty))
                .ToList();
        }

        [Theory]
        [InlineData(@"Alpha.bla.sisi\bla.bla\impo_2018_201901201901.zip.lst.PROCESADO")]
        [InlineData(@"expo_agregado_201902_Archivo_201902.zip.lst_LINEASPROCESADAS_0.lst")]
        [InlineData(@"C:\Program Files\Alpha 2000\Clasifica\Alpha.EstadisticasComercioExterior.Servicio\Descargas\201902\expo_agregado_201902_Archivo_201902.zip.lst_LINEASPROCESADAS_0.lst")]
        public static void GetLastLine_ShouldWork(string path)
        {
            GetLastLine(path);
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

            int IndiceGuion = filePath.LastIndexOf('_') + 1;

            int IndicePunto = filePath.LastIndexOf('.');

            string NumLineas = filePath.Substring(IndiceGuion, IndicePunto-IndiceGuion);

            long.TryParse(NumLineas, out Lineas);

            return Lineas;

        }
    }
}
