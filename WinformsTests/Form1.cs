using System;
using System.Collections;
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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnCallChanger_Click(object sender, EventArgs e)
        {
            frmChange changer = new frmChange(this);
            toolTip1.Show("Hi!",btnCallChanger);
            changer.Show();


            Dictionary<string, int> keyValuePairs;
            keyValuePairs = new Dictionary<string, int>();
             
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }
    }
}
