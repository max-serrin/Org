using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GetRandom
{
    public partial class Popup : Form
    {
        int returnValue;

        public Popup()
        {
            InitializeComponent();
            this.Focus();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            returnValue = (int)numericUpDown1.Value;
            this.Close();
        }

        public int GetValue()
        {
            return returnValue;
        }
    }
}
