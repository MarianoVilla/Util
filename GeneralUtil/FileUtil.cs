using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Text;

namespace Dager
{
    public static class FileUtil
    {
        public static string GetFileExtension(string NombreArchivo)
        {
            return NombreArchivo.Substring(NombreArchivo.LastIndexOf("."));
        }
        public static IEnumerable<string> GetFiles(string Path, string SearchPattern, SearchOption searchOption)
        {
            string[] searchPatterns = SearchPattern.Split('|');
            List<string> Files = new List<string>();
            foreach (string sp in searchPatterns)
                Files.AddRange(Directory.GetFiles(Path, sp, searchOption));
            Files.Sort();
            return Files;
        }
        public static IEnumerable<string> GetDirectories(string Path, string SearchPattern, SearchOption searchOption)
        {
            string[] searchPatterns = SearchPattern.Split('|');
            List<string> Directories = new List<string>();
            foreach (string sp in searchPatterns)
                Directories.AddRange(Directory.GetDirectories(Path, sp, searchOption));
            Directories.Sort();
            return Directories;
        }
        private static bool GrantAccess(string FullPath)
        {
            DirectoryInfo dInfo = new DirectoryInfo(FullPath);
            DirectorySecurity dSecurity = dInfo.GetAccessControl();
            dSecurity.AddAccessRule(new FileSystemAccessRule(
                new SecurityIdentifier(WellKnownSidType.WorldSid, null),
                FileSystemRights.FullControl,
                InheritanceFlags.ObjectInherit | InheritanceFlags.ContainerInherit,
                PropagationFlags.NoPropagateInherit,
                AccessControlType.Allow));

            dInfo.SetAccessControl(dSecurity);
            return true;
        }

    }
}
