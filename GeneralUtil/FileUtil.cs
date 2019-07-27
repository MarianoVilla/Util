using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dager
{
    public static class FileUtil
    {
        public static string GetFileExtension(string NombreArchivo)
        {
            return NombreArchivo.Substring(NombreArchivo.LastIndexOf("."));
        }

    }
}
