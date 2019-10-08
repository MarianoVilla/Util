using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Dager
{
    public static class GeneralUtil
    {

        //XUnit.
        public static void CheckNullArgs<T>(List<T> Arguments)
        {
            if (Arguments == null)
                throw new ArgumentNullException();
            Arguments.ForEach(x => { if (x == null) { throw new ArgumentNullException(); } });
        }

        //XUnit.
        public static void CheckNullArgs(params dynamic[] Args)
        {
            foreach (var a in Args)
            {
                if (a == null)
                    throw new ArgumentNullException();
            }
        }

        //XUnit.
        public static void CheckEmptyStrings(params string[] Strings)
        {
            CheckNullArgs(Strings);

            Strings.ToList().ForEach(x =>
            {
                if (string.IsNullOrWhiteSpace(x))
                {
                    throw new ArgumentException("Se recibió un string vacío.", x.GetType().Name);
                }
            });
        }

        public static object Coalesce(params object[] objects) => objects.FirstOrDefault(s => s != null);

        public static void CheckEmptyChars(params char[] Chars)
        {
            CheckNullArgs(Chars);

            Chars.ToList().ForEach(x =>
            {
                if (char.IsWhiteSpace(x))
                {
                    throw new ArgumentException("Se recibió un char vacío.", x.GetType().Name);
                }
            });
        }

        public static void AddIfNotInList<T>(List<T> ListTo, T ValueToAdd)
        {
            if (ListTo.Contains(ValueToAdd))
                return;
            ListTo.Add(ValueToAdd);
        }

        public static void AddIfNotNullNorInList<T>(List<T> ListTo, T ValueTo)
        {
            if (ValueTo == null | ListTo.Contains(ValueTo))
                return;

            ListTo.Add(ValueTo);
        }


        public static object ChangeType(object value, Type t)
        {
            if(value == null)
            {
                if (t.IsValueType)
                    throw new InvalidCastException();
                else return null;
            }
            if (t.IsGenericType && t.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
            {
                if (value == null)
                {
                    return null;
                }

                t = Nullable.GetUnderlyingType(t);
            }
            if(value.GetType() == typeof(string) && t.GetType() == typeof(double))
            {
                value = ((string)value).Replace('.', ',');
            }
            if (t == typeof(DateTime))
            {
                DateTime Date;
                return DateTime.TryParse(value.ToString(), out Date) ? Date : default(DateTime);
            }
            if (t.IsEnum)
                return Enum.Parse(t, value.ToString());
            return Convert.ChangeType(value, t);
        }
        public static string GetVersion()
        {
            System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
            System.Diagnostics.FileVersionInfo fvi = System.Diagnostics.FileVersionInfo.GetVersionInfo(assembly.Location);
            return fvi.FileVersion;
        }
        public static Process RunProcess(string Path, string Params = null, bool CreateNoWindow = false)
        {
            Process Proc = new Process();
            Proc.StartInfo = new ProcessStartInfo(Path)
            {
                Arguments = Params,
                UseShellExecute = false,
                CreateNoWindow = CreateNoWindow
            };
            Proc.Start();
            return Proc;
        }

    }
}
