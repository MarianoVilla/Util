using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Dager
{
    public static class StringUtil
    {

        public static void DirectoryNameRemover(string Path, string toCriteria, string searchPattern = "*", SearchOption searchOption = SearchOption.TopDirectoryOnly, int subAdd = 0)
        {
            foreach (var dir in Directory.GetDirectories(Path, searchPattern, searchOption))
            {
                string root = dir.Substring(0, dir.LastIndexOf("\\"));
                string rootLess = dir.Substring(root.LastIndexOf("\\"));
                string moveTo = rootLess.Substring(rootLess.IndexOf(toCriteria)).Substring(subAdd);
                moveTo = string.Concat(root, "\\", moveTo);
                Directory.Move(dir, moveTo);
            }
        }

        public static void DirectoryMultiStringReplace(string Path, string[] ToReplace, string[] Replacements, string searchPattern = "*", SearchOption searchOption = SearchOption.TopDirectoryOnly)
        {
            foreach(var dir in Directory.GetDirectories(Path, searchPattern, searchOption))
            {
                for(int i = 0; i < ToReplace.Length; i++)
                {
                    for(int j = 0; j < Replacements.Length; j++)
                    {
                        dir.Replace(ToReplace[i], Replacements[j]);
                    }

                }
            }
        }

        public static string Concat(string Separator = "", bool EndWithSeparator = false, params string[] Strings)
        {
            string Concat = string.Empty;
            for (int i = 0; i < Strings.Count(); i++)
            {
                if (!string.IsNullOrEmpty(Strings[i]))
                    Concat += Strings[i];
                if (i == Strings.Count() - 1)
                {
                    if (EndWithSeparator)
                        Concat += Separator;
                    continue;
                }
                else
                    Concat += Separator;
            }
            return Concat;
        }

        public static string Coalesce(params string[] strings) => strings.FirstOrDefault(s => !string.IsNullOrEmpty(s));

        public static string GetConnectionStringValue(string ConnectionString, string Key)
        {
            System.Data.Common.DbConnectionStringBuilder builder = new System.Data.Common.DbConnectionStringBuilder();

            builder.ConnectionString = ConnectionString;

            return builder[Key] as string;
        }

        //XUnit.
        public static Dictionary<string, string> StringToKeyValuePairs(string ToSplit, char ValueSplit, char KVSplit)
        {
            return ToSplit.Split(KVSplit)
                .Select(v => v.Split(ValueSplit))
                .ToDictionary(pair => pair[0], pair => pair[1]);
        }

        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            return Convert.ToBase64String(plainTextBytes);
        }

        public static string Base64Decode(string base64EncodedData)
        {
            var base64EncodedBytes = Convert.FromBase64String(base64EncodedData);
            return Encoding.UTF8.GetString(base64EncodedBytes);
        }

        public static void Regexear(Dictionary<Regex, string> RegexReplacer, List<string> Strings)
        {
            foreach (var s in Strings)
            {
                foreach (var rr in RegexReplacer)
                {
                    rr.Key.Replace(s, rr.Value);
                }
            }
        }


    }
}
