using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Org
{
    public partial class FullScreen : Form
    {
        private PictureBox parentPictureBox;
        private Org org;

        public FullScreen(PictureBox _pictureBox, Org _parent)
        {
            InitializeComponent();
            pictureBox.MouseWheel += PictureBox_MouseWheel;
            parentPictureBox = _pictureBox;
            org = _parent;
        }

        private void PictureBox_MouseWheel(object sender, MouseEventArgs e)
        {
            org.PictureBox_MouseWheel(sender, e);
            UpdatePictureBox();
        }

        public void UpdatePictureBox()
        {
            pictureBox.ImageLocation = parentPictureBox.ImageLocation;
        }

        private void pictureBox_Click(object sender, MouseEventArgs e)
        {
            org.pictureBox_MouseClick(sender, e);
            UpdatePictureBox();
        }

        private void pictureBox_DoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button.Equals(MouseButtons.Left))
            {
                Hide();
            }
        }
    }
}
