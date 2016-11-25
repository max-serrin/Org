using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GetRandom
{
    public partial class NextX : Form
    {
        Form1 _f;

        public NextX(System.Collections.Specialized.StringCollection get, Form1 f)
        {
            InitializeComponent();
            this.Focus();

            _f = f;

            List<PictureBox> pbl = new List<PictureBox>();
            foreach (string s in get)
            {
                addPB(s);
            }

            ContextMenu cml = new System.Windows.Forms.ContextMenu();
            cml.MenuItems.Add("Add Random");
            cml.MenuItems[0].Click += addRandom_Click;

            flowLayoutPanel1.ContextMenu = cml;
        }

        private void addPB(string s)
        {
            PictureBox pb = new PictureBox();
            pb.ImageLocation = s;
            pb.SizeMode = PictureBoxSizeMode.Zoom;
            pb.Size = new Size((int)(flowLayoutPanel1.Size.Height / 2), flowLayoutPanel1.Size.Height);
            pb.Click += imageClick;
            ContextMenu cm = new System.Windows.Forms.ContextMenu();
            cm.MenuItems.Add("Promote");
            cm.MenuItems.Add("Demote");
            cm.MenuItems.Add("Get Random");
            cm.MenuItems.Add("Remove From List");
            cm.MenuItems.Add("Open File Location");
            cm.MenuItems.Add("Cut");
            cm.MenuItems.Add("Cut All");

            cm.MenuItems[0].Click += promoteToolStripMenuItem_Click;
            cm.MenuItems[1].Click += demoteToolStripMenuItem_Click;
            cm.MenuItems[2].Click += getRandomToolStripMenuItem_Click;
            cm.MenuItems[3].Click += removeFromList_Click;
            cm.MenuItems[4].Click += openFileLocation_Click;
            cm.MenuItems[5].Click += cutImage_Click;
            cm.MenuItems[6].Click += cutAllImages_Click;

            pb.ContextMenu = cm;
            flowLayoutPanel1.Controls.Add(pb);
        }

        private void cutImage_Click(object sender, EventArgs e)
        {
            PictureBox pb = (PictureBox)((ContextMenu)((MenuItem)sender).Parent).SourceControl;
            StringCollection get = new StringCollection();

            get.Add(pb.ImageLocation);

            byte[] moveEffect = new byte[] { 2, 0, 0, 0 };
            MemoryStream dropEffect = new MemoryStream();
            dropEffect.Write(moveEffect, 0, moveEffect.Length);

            DataObject data = new DataObject();
            data.SetFileDropList(get);
            data.SetData("Preferred DropEffect", dropEffect);

            Clipboard.Clear();
            Clipboard.SetDataObject(data, true);
            MessageBox.Show("File successfully cut.");

            if (pictureBox1.ImageLocation == pb.ImageLocation)
                pictureBox1.ImageLocation = "";
            flowLayoutPanel1.Controls.Remove(pb);
        }

        private void cutAllImages_Click(object sender, EventArgs e)
        {
            if (flowLayoutPanel1.Controls.Count > 1)
            {
                StringCollection get = new StringCollection();

                foreach (Control c in flowLayoutPanel1.Controls)
                {
                    get.Add(((PictureBox)c).ImageLocation);
                }

                byte[] moveEffect = new byte[] { 2, 0, 0, 0 };
                MemoryStream dropEffect = new MemoryStream();
                dropEffect.Write(moveEffect, 0, moveEffect.Length);

                DataObject data = new DataObject();
                data.SetFileDropList(get);
                data.SetData("Preferred DropEffect", dropEffect);

                Clipboard.Clear();
                Clipboard.SetDataObject(data, true);
                MessageBox.Show("File(s) successfully cut.");

                this.Close();
            }
        }

        private void openFileLocation_Click(object sender, EventArgs e)
        {
            PictureBox pb = (PictureBox)((ContextMenu)((MenuItem)sender).Parent).SourceControl;

            _f.openFileLocation(pb.ImageLocation);
        }

        private void addRandom_Click(object sender, EventArgs e)
        {
            addPB(_f.getRandom().FullName);
        }

        private void removeFromList_Click(object sender, EventArgs e)
        {
            PictureBox pb = (PictureBox)((ContextMenu)((MenuItem)sender).Parent).SourceControl;

            if (pictureBox1.ImageLocation == pb.ImageLocation)
                pictureBox1.ImageLocation = "";
            flowLayoutPanel1.Controls.Remove(pb);
        }

        private void imageClick(object sender, EventArgs e)
        {
            pictureBox1.ImageLocation = ((PictureBox)sender).ImageLocation;
        }

        private void promoteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PictureBox pb = (PictureBox)((ContextMenu)((MenuItem)sender).Parent).SourceControl;
            System.IO.FileInfo f = new System.IO.FileInfo(pb.ImageLocation);
            
            // Assumes L7 exists...
            f.MoveTo(f.DirectoryName + "\\L7\\" + f.Name);
            pictureBox1.ImageLocation = "";
            flowLayoutPanel1.Controls.Remove(pb);
        }

        private void demoteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PictureBox pb = (PictureBox)((ContextMenu)((MenuItem)sender).Parent).SourceControl;
            System.IO.FileInfo f = new System.IO.FileInfo(pb.ImageLocation);

            // Assumes \..\- exists...
            f.MoveTo(f.DirectoryName + "\\..\\-\\" + f.Name);
            pictureBox1.ImageLocation = "";
            flowLayoutPanel1.Controls.Remove(pb);
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PictureBox pb = (PictureBox)((ContextMenu)((MenuItem)sender).Parent).SourceControl;
            System.IO.FileInfo f = new System.IO.FileInfo(pb.ImageLocation);

            f.Delete();
            pictureBox1.ImageLocation = "";
            flowLayoutPanel1.Controls.Remove(pb);
        }

        private void getRandomToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Random rng = new Random();
            pictureBox1.ImageLocation = ((PictureBox)flowLayoutPanel1.Controls[rng.Next(0, flowLayoutPanel1.Controls.Count)]).ImageLocation;
        }
    }
}
