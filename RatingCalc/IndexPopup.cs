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
    public partial class IndexPopup : Form
    {
        List<FileInfo> fi;
        int index;

        public IndexPopup()
        {
            InitializeComponent();
            this.Focus();

        }

        internal int GetValue(List<FileInfo> fi)
        {
            this.fi = fi;
            int count = 0;
            foreach (FileInfo f in fi)
            {
                PictureBox pb = new PictureBox();
                pb.ImageLocation = f.FullName;
                pb.TabIndex = count;
                pb.Height = 100;
                pb.Width = 100;
                pb.SizeMode = PictureBoxSizeMode.Zoom;
                pb.MouseClick += pb_MouseClick;
                flowLayoutPanel1.Controls.Add(pb);
            }

            flowLayoutPanel1.MouseEnter += flowLayoutPanel1_MouseEnter;
            flowLayoutPanel1.MouseWheel += flowLayoutPanel1_MouseWheel;

            this.ShowDialog();

            return index;
        }

        void flowLayoutPanel1_MouseEnter(object sender, EventArgs e)
        {
            ((FlowLayoutPanel)sender).Focus();
        }

        void flowLayoutPanel1_MouseWheel(object sender, MouseEventArgs e)
        {
            flowLayoutPanel1.VerticalScroll.Value += e.Delta;
        }

        void pb_MouseClick(object sender, MouseEventArgs e)
        {
            index = flowLayoutPanel1.Controls.IndexOf((PictureBox)sender);
            this.Close();
        }


    }
}
