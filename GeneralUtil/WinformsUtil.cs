using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Dager
{
    public static class WinformsUtil
    {
        public static void BlanqueaTextBoxes(IEnumerable<Control> controles)
        {
            if (controles == null)
                return;
            controles.ToList().ForEach(x => x.Text = "");
        }
        //ToDo: support multiple extensions.
        public static string OpenFileDialog(string FilterExt, string FilterFriendlyName)
        {
            using (var fbd = new OpenFileDialog())
            {
                fbd.Filter = $"{FilterFriendlyName} (*{FilterExt})|*{FilterExt}";
                if (fbd.ShowDialog() == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.FileName) && fbd.SafeFileName.EndsWith(FilterExt))
                {
                    return fbd.FileName;
                }
            }
            return string.Empty;
        }
        public static void OpenFolder(string FilePath)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo()
            {
                Arguments = "/select, \"" + FilePath,
                FileName = "explorer.exe"
            };
            Process.Start(startInfo);
        }

    }
}
