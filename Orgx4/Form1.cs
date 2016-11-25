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

namespace Org_
{
    public partial class Form1 : Form
    {
        string folderPath;
        string imageFolderPath;
        List<string> ext;

        Dictionary<string, string> folders;
        Dictionary<string, char> keys;

        // Directory Info and Master File List
        DirectoryInfo dif;
        DirectoryInfo dii;
        List<FileInfo> fi;
        List<PictureBox> pbl;

        // Search option (to search sub directories or not)
        SearchOption so;

        int pos;

        Stack<string> history;

        FullScreen FS;
        Settings S;

        public Form1(String path)
        {
            InitializeComponent();

            folders = new Dictionary<string, string>();
            folders.Add("left_click", "Move23");
            folders.Add("right_click", "Move01");
            folders.Add("middle_click", "tass");
            folders.Add("delete", "Delete");

            keys = new Dictionary<string, char>();
            keys.Add("left_click", '=');
            keys.Add("right_click", '-');
            keys.Add("middle_click", '\\');
            keys.Add("delete", '`');

            if (Directory.Exists(path))
                Properties.Settings.Default.folderPath = path;

            if (!(Directory.Exists(Properties.Settings.Default.folderPath)))
                Properties.Settings.Default.folderPath = "C:\\";

            folderPath = Properties.Settings.Default.folderPath;
            imageFolderPath = folderPath;

            pbl = new List<PictureBox>();
            pbl.Add(pictureBox1);
            pbl.Add(pictureBox2);
            pbl.Add(pictureBox3);
            pbl.Add(pictureBox4);

            // Create the list of extensions
            ext = new List<string> { "*.jpg", "*.jpeg", "*.gif", "*.png", "*.bmp" };

            // Set Search Option to search all directories
            so = SearchOption.TopDirectoryOnly;

            FS = new FullScreen(ref pictureBox1, this);
            S = new Settings(ref folders, ref keys);

            if (Directory.Exists(folderPath))
                openFolder();
        }

        private void openFolder()
        {
            dif = new DirectoryInfo(folderPath);

            getDirectories();
            getImages();
        }

        private void getDirectories()
        {
            folderList.Items.Clear();
            folderList.Items.Add(".");
            folderList.Items.Add("..");
            foreach (DirectoryInfo d in dif.GetDirectories())
            {
                folderList.Items.Add(d.ToString());
            }
        }

        private void getImages()
        {
            pos = 0;
            dii = new DirectoryInfo(imageFolderPath);
            fi = new List<FileInfo>();

            imageList.Items.Clear();

            foreach (string s in ext)
                fi.AddRange(dii.EnumerateFiles(s, so).ToList());

            foreach (FileInfo f in fi)
                imageList.Items.Add(f.Name);

            toolStripProgressBar1.Value = 0;
            toolStripProgressBar1.Maximum = fi.Count;
            if (fi.Count > 0)
                imageList.SelectedIndex = 0;
            else
                setImage();

            history = new Stack<string>();
        }

        private void setImage()
        {
            if (fi.Count > 0)
            {
                pictureBox1.ImageLocation = fi[pos].FullName;
                toolStripStatusLabel1.Text = fi[pos].Name;
                toolStripStatusLabel2.Text = (pos + 1).ToString() + @"/" + fi.Count;
                if (fi.Count > 1)
                    pictureBox2.ImageLocation = fi[pos + 1].FullName;
                if (fi.Count > 2)
                    pictureBox3.ImageLocation = fi[pos + 2].FullName;
                if (fi.Count > 3)
                    pictureBox4.ImageLocation = fi[pos + 3].FullName;
                FS.updateFS();
            }
            else
            {
                pictureBox1.ImageLocation = "";
                pictureBox2.ImageLocation = "";
                pictureBox3.ImageLocation = "";
                pictureBox4.ImageLocation = "";
                toolStripStatusLabel1.Text = "";
                toolStripStatusLabel2.Text = @"0/0";
                toolStripProgressBar1.Maximum = 100;
                toolStripProgressBar1.Value = 100;
                FS.updateFS();
            }
        }

        private void folderList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (folderList.SelectedIndex <= 0)
            {
                imageFolderPath = folderPath;
                getImages();
            }
            else if (folderList.SelectedIndex == 1)
            {
                string path = folderPath.Substring(0,folderPath.LastIndexOf(@"\"));
                if (Directory.Exists(path))
                {
                    imageFolderPath = path;
                    getImages();
                }
                else
                {
                    MessageBox.Show("Directory does not exist.");
                    folderList.Items.Remove(folderList.SelectedItem);
                    folderList.SelectedIndex = 0;
                }
            }
            else
            {
                string path = folderPath + "\\" + folderList.Items[folderList.SelectedIndex].ToString();
                if (Directory.Exists(path))
                {
                    imageFolderPath = path;
                    getImages();
                }
                else
                {
                    MessageBox.Show("Directory does not exist.");
                    folderList.Items.Remove(folderList.SelectedItem);
                    folderList.SelectedIndex = 0;
                }
            }
            focus();
        }

        private void imageList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (imageList.SelectedIndex >= 0)
            {
                pos = imageList.SelectedIndex;
                setImage();
            }
        }

        private void folderList_DoubleClick(object sender, EventArgs e)
        {
            if (folderList.SelectedIndex <= 0)
            {
                imageFolderPath = folderPath;
                getImages();
            }
            else if (folderList.SelectedIndex == 1)
            {
                string path = folderPath.Substring(0, folderPath.LastIndexOf(@"\"));
                if (Directory.Exists(path))
                {
                    Properties.Settings.Default.folderPath = path;
                    folderPath = Properties.Settings.Default.folderPath;
                    imageFolderPath = folderPath;
                    openFolder();
                }
                else
                {
                    MessageBox.Show("Directory does not exist.");
                    folderList.Items.Remove(folderList.SelectedItem);
                    folderList.SelectedIndex = 0;
                }
            }
            else
            {
                string path = folderPath + "\\" + folderList.SelectedItem.ToString();
                if (Directory.Exists(path))
                {
                    Properties.Settings.Default.folderPath = path;
                    folderPath = Properties.Settings.Default.folderPath;
                    imageFolderPath = folderPath;
                    openFolder();
                }
                else
                {
                    MessageBox.Show("Directory does not exist.");
                    folderList.Items.Remove(folderList.SelectedItem);
                    folderList.SelectedIndex = 0;
                }
            }
            focus();
        }

        public void pictureBox1_Click(object sender, EventArgs e)
        {
            pbClick(0, (MouseEventArgs)e);
        }

        public void pictureBox2_Click(object sender, EventArgs e)
        {
            pbClick(1, (MouseEventArgs)e);
            setImage();
        }

        public void pictureBox3_Click(object sender, EventArgs e)
        {
            pbClick(2, (MouseEventArgs)e);
            setImage();
        }

        public void pictureBox4_Click(object sender, EventArgs e)
        {
            pbClick(3, (MouseEventArgs)e);
            setImage();
        }

        public void pbClick(int pbn, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.XButton2)
                forward();
            else if (e.Button == System.Windows.Forms.MouseButtons.XButton1)
                back();
            else if (e.Button == System.Windows.Forms.MouseButtons.Left)
                moveImageToFolder(folders["left_click"], pbn);
            else if (e.Button == System.Windows.Forms.MouseButtons.Right)
                moveImageToFolder(folders["right_click"], pbn);
            else if (e.Button == System.Windows.Forms.MouseButtons.Middle)
                moveImageToFolder(folders["middle_click"], pbn);
        }

        private void back()
        {
            if (fi.Count > 0)
            {
                pos--;
                if (pos < 0)
                    pos = fi.Count - 1;
                imageList.SelectedIndex = pos;
            }
        }

        private void forward()
        {
            if (fi.Count > 0)
            {
                pos++;
                if (pos == fi.Count)
                    pos = 0;
                imageList.SelectedIndex = pos;
            }
        }

        private void focus()
        {
            this.Focus();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            focus();
        }

        public void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == keys["left_click"])
            {
                moveImageToFolder(folders["left_click"], 0);
                e.Handled = true;
            }
            else if (e.KeyChar == keys["right_click"])
            {
                moveImageToFolder(folders["right_click"], 0);
                e.Handled = true;
            }
            else if (e.KeyChar == keys["middle_click"])
            {
                moveImageToFolder(folders["middle_click"], 0);
                e.Handled = true;
            }
            else if (e.KeyChar == keys["delete"])
            {
                    moveImageToFolder(folders["delete"], 0);
                    e.Handled = true;
            }
            else if (e.KeyChar == 'z')
            {
                undo();
                e.Handled = true;
            }
        }

        private void moveImageToFolder(string p, int pp)
        {
            if (fi.Count > 0)
            {
                string path = imageFolderPath + @"\" + p;
                string dest = path + @"\" + fi[pos+pp].Name;
                if (!(Directory.Exists(path)))
                {
                    Directory.CreateDirectory(path);
                    getDirectories();
                }
                File.Move(fi[pos+pp].FullName, dest);
                incrementProgress();
                history.Push(dest);

                fi.RemoveAt(pos+pp);
                imageList.Items.RemoveAt(pos+pp);
                if (pos == fi.Count)
                    pos = 0;
                if (fi.Count > 0)
                    imageList.SelectedIndex = pos;
            }
        }

        private void incrementProgress()
        {
            toolStripProgressBar1.Value++;
            if (toolStripProgressBar1.Value > toolStripProgressBar1.Maximum)
                toolStripProgressBar1.Value = toolStripProgressBar1.Maximum;
        }

        private void decrementProgress()
        {
            toolStripProgressBar1.Value--;
            if (toolStripProgressBar1.Value < 0)
                toolStripProgressBar1.Value = 0;
        }

        private void undo()
        {
            if (history.Count > 0)
            {
                decrementProgress();
                FileInfo f = new FileInfo(history.Pop());
                File.Move(f.FullName, imageFolderPath + @"\" + f.Name);
                fi.Insert(pos, new FileInfo(imageFolderPath + @"\" + f.Name));
                imageList.Items.Insert(pos, fi[pos].Name);
                imageList.SelectedIndex = pos;
                setImage();
            }
        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            undo();
        }

        private void toolStripDropDownButton1_Click(object sender, EventArgs e)
        {
            FS.ShowDialog();
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            S.ShowDialog();
        }

        private void openGetRandomHereToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GetRandom.Form1 f = new GetRandom.Form1(imageFolderPath);
            f.Show();
        }
    }
}
