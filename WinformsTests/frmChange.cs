using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinformsTests
{
    public partial class frmChange : Form
    {
        public Form1 MyForm;
        public frmChange(Form1 form)
        {
            MyForm = form;
            InitializeComponent();
        }

        private void btnChangeColor_Click(object sender, EventArgs e)
        {
            MyForm.BackColor = Color.Black;
        }

        private void frmChange_Load(object sender, EventArgs e)
        {

        }
    }
}
