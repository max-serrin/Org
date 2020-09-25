using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Org_
{
    public partial class FullScreen : Form
    {
        PictureBox pb1, pb2;
        Form1 dad;

        public FullScreen(ref PictureBox _pb1, ref PictureBox _pb2, Form1 _dad)
        {
            InitializeComponent();
            pb1 = _pb1;
            pb2 = _pb2;
            dad = _dad;
        }

        public void updateFS()
        {
            pictureBox1.ImageLocation = pb1.ImageLocation;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            dad.pictureBox1_Click(sender, e);
        }

        private void FullScreen_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)27)
                this.Hide();
            else
                dad.Form1_KeyPress(sender, e);
        }


    }
}
