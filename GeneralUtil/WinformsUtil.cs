using System;
using System.Collections.Generic;
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
    }
}
