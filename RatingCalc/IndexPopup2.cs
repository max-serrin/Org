using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace RatingCalc
{
    public partial class IndexPopup2 : Form
    {
        List<FileInfo> fi;
        int i = 0;

        public IndexPopup2()
        {
            InitializeComponent();

        }

        internal int GetValue(List<FileInfo> fi, int index)
        {
            this.fi = fi;
            i = index;
            textBox1.Text = index.ToString();
            pictureBox1.ImageLocation = fi[index].FullName;

            this.ShowDialog();

            return i;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(textBox1.Text, out i))
            {
                if (i < 1)
                {
                    i = 0;
                    textBox1.Text = i.ToString();
                }
                else if (i > fi.Count)
                {
                    i = fi.Count - 1;
                    textBox1.Text = i.ToString();
                }
                else
                    i -= 1;

                pictureBox1.ImageLocation = fi[i].FullName;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
