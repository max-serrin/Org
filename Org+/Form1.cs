using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
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

        BackgroundWorker backgroundWorker1;

        // Search option (to search sub directories or not)
        SearchOption so;

        int pos, _pos;

        Stack<string> history;

        FullScreen FS;
        Settings S;
        HotkeySettings hs;

        public Form1(string path)
        {
            InitializeComponent();
            
            foreach (var drive in DriveInfo.GetDrives())
            {
                fileToolStripMenuItem.DropDownItems.Add(drive.Name, null, ChangeDrive);
            }

            folders = new Dictionary<string, string>();
            folders.Add("left_click", Properties.Settings.Default.left_click);
            folders.Add("right_click", Properties.Settings.Default.right_click);
            folders.Add("middle_click", Properties.Settings.Default.middle_click);
            folders.Add("delete", Properties.Settings.Default.delete);

            keys = new Dictionary<string, char>();
            keys.Add("left_click", '=');
            keys.Add("right_click", '-');
            keys.Add("middle_click", '\\');
            keys.Add("delete", '`');

            if (path != null && Directory.Exists(path))
                Properties.Settings.Default.folderPath = path;
            
            folderPath = Properties.Settings.Default.folderPath;

            if (!(Directory.Exists(Properties.Settings.Default.folderPath)))
                Properties.Settings.Default.folderPath = "C:\\";

            imageFolderPath = folderPath;

            pictureBox1.ImageLocation = "";
            pictureBox2.ImageLocation = "";
            pictureBox2.Tag = false;
            toolStripStatusLabel1.Text = "";
            toolStripStatusLabel2.Text = @"0/0";
            toolStripProgressBar1.Maximum = 100;
            toolStripProgressBar1.Value = 100;

            backgroundWorker1 = new BackgroundWorker();
            backgroundWorker1.DoWork += backgroundWorker1_DoWork;
            backgroundWorker1.RunWorkerCompleted += backgroundWorker1_RunWorkerCompleted;  //Tell the user how the process went
            backgroundWorker1.WorkerSupportsCancellation = true;

            pictureBox2.LoadCompleted += PictureBox2_LoadCompleted;

            // Create the list of extensions
            ext = new List<string> { "*.jpg", "*.jpeg", "*.gif", "*.png", "*.bmp" };

            // Set Search Option to search all directories
            so = SearchOption.TopDirectoryOnly;

            FS = new FullScreen(ref pictureBox1, ref pictureBox2, this);
            S = new Settings(ref folders, ref keys);
            hs = new HotkeySettings();

            hs.hotkeys[Keys.D1] = "1♥";
            hs.hotkeys[Keys.D1 | Keys.Control] = "1♥-";
            hs.hotkeys[Keys.D1 | Keys.Alt] = "1♥+";
            hs.hotkeys[Keys.D2] = "2♥";
            hs.hotkeys[Keys.D2 | Keys.Control] = "2♥-";
            hs.hotkeys[Keys.D2 | Keys.Alt] = "2♥+";
            hs.hotkeys[Keys.D3] = "3♥";
            hs.hotkeys[Keys.D3 | Keys.Control] = "3♥-";
            hs.hotkeys[Keys.D3 | Keys.Alt] = "3♥+";
            hs.hotkeys[Keys.D4] = "4♥";
            hs.hotkeys[Keys.D4 | Keys.Control] = "4♥-";
            hs.hotkeys[Keys.D4 | Keys.Alt] = "4♥+";
            hs.hotkeys[Keys.D5] = "5♥";
            hs.hotkeys[Keys.D5 | Keys.Control] = "5♥-";
            hs.hotkeys[Keys.D5 | Keys.Alt] = "5♥+";
            hs.hotkeys[Keys.L] = "Special";
            hs.hotkeys[Keys.Oemtilde] = "Delete";
            hs.hotkeys[Keys.F1] = "Bondage (1-3♥)";
            hs.hotkeys[Keys.F2] = "Bondage (4-5♥)";

            if (Directory.Exists(folderPath))
            {
                openFolder();
                fileToolStripMenuItem.DropDownItems.OfType<ToolStripMenuItem>().FirstOrDefault(o => o.Text.Equals(folderPath.Substring(0, 3))).Checked = true;
            }

            focus();
        }

        private void ChangeDrive(object sender, EventArgs e)
        {
            var _folderPath = folderPath;
            folderPath = ((ToolStripMenuItem)sender).Text;
            imageFolderPath = folderPath;

            try
            {
                openFolder();

                // Set checkmark
                var ch = fileToolStripMenuItem.DropDownItems.OfType<ToolStripMenuItem>().FirstOrDefault(o => o.Checked.Equals(true))?.Checked;
                ch = false;
                ((ToolStripMenuItem)sender).Checked = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("There was an issue opening the drive: " + ex.ToString());

                // Reset to previous folder
                folderPath = _folderPath;
                openFolder();
            }
        }

        private void PictureBox2_LoadCompleted(object sender, AsyncCompletedEventArgs e)
        {
            pictureBox2.Tag = true;
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
                if (!(bool)pictureBox2.Tag || pictureBox2.ImageLocation.Equals("") || !fi[pos].FullName.Equals(pictureBox2.ImageLocation))
                    pictureBox1.ImageLocation = fi[pos].FullName;
                else
                {
                    PictureBox temp = pictureBox1;
                    pictureBox1 = pictureBox2;
                    pictureBox1.BringToFront();
                    pictureBox2 = temp;
                }
                
                _pos = pos + 1;
                if (_pos >= fi.Count)
                    _pos = 0;

                pictureBox2.Tag = false;
                if (pos == _pos)
                    pictureBox2.ImageLocation = "";
                else
                    pictureBox2.ImageLocation = fi[_pos].FullName;

                if (!pictureBox2.ImageLocation.Equals(""))
                    pictureBox2.LoadAsync();
                
                toolStripStatusLabel1.Text = fi[pos].Name;
                toolStripStatusLabel2.Text = (pos + 1).ToString() + @"/" + fi.Count;
                FS.updateFS();
            }
            else
            {
                pictureBox1.ImageLocation = "";
                pictureBox2.ImageLocation = "";
                toolStripStatusLabel1.Text = "";
                toolStripStatusLabel2.Text = @"0/0";
                toolStripProgressBar1.Maximum = 100;
                toolStripProgressBar1.Value = 100;
                FS.updateFS();
            }
        }

        private void backgroundWorker1_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            pictureBox2.Load();
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                pictureBox2.ImageLocation = "";
            }
            else if (e.Error != null)
            {
                pictureBox2.ImageLocation = "";
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
            if (((MouseEventArgs)e).Button == System.Windows.Forms.MouseButtons.XButton2)
                forward();
            else if (((MouseEventArgs)e).Button == System.Windows.Forms.MouseButtons.XButton1)
                back();
            //else if (((MouseEventArgs)e).Button == System.Windows.Forms.MouseButtons.Left)
            //{
            //    moveImageToFolder(folders["left_click"]);
            //}
            //else if (((MouseEventArgs)e).Button == System.Windows.Forms.MouseButtons.Right)
            //{
            //    moveImageToFolder(folders["right_click"]);
            //}
            //else if (((MouseEventArgs)e).Button == System.Windows.Forms.MouseButtons.Middle)
            //{
            //    moveImageToFolder(folders["middle_click"]);
            //}
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
            //if (e.KeyChar == keys["left_click"])
            //{
            //    moveImageToFolder(folders["left_click"]);
            //    e.Handled = true;
            //}
            //else if (e.KeyChar == keys["right_click"])
            //{
            //    moveImageToFolder(folders["right_click"]);
            //    e.Handled = true;
            //}
            //else if (e.KeyChar == keys["middle_click"])
            //{
            //    moveImageToFolder(folders["middle_click"]);
            //    e.Handled = true;
            //}
            //else if (e.KeyChar == keys["delete"])
            //{
            //        moveImageToFolder(folders["delete"]);
            //        e.Handled = true;
            //}
            //else 
            if (e.KeyChar == 'z')
            {
                undo();
                e.Handled = true;
            }
        }

        private void moveImageToFolder(string p)
        {
            if (fi.Count > 0)
            {
                string path = imageFolderPath + @"\" + p;
                string dest = path + @"\" + fi[pos].Name;
                if (!(Directory.Exists(path)))
                {
                    Directory.CreateDirectory(path);
                    getDirectories();
                }

                bool fileMoved = false;
                if (!(File.Exists(dest)))
                {
                    File.Move(fi[pos].FullName, dest);
                    fileMoved = true;
                }
                else
                {
                    DialogResult result = MessageBox.Show("File already exists, overwrite?.", "File move error.", MessageBoxButtons.YesNo);
                    if (result == DialogResult.Yes)
                    {
                        File.Delete(dest);
                        File.Move(fi[pos].FullName, dest);
                        fileMoved = true;
                    }
                }

                if (fileMoved)
                {
                    incrementProgress();
                    history.Push(dest);

                    fi.RemoveAt(pos);
                    imageList.Items.RemoveAt(pos);
                    if (pos == fi.Count)
                        pos = 0;
                    if (fi.Count > 0)
                        imageList.SelectedIndex = pos;
                    else
                        setImage();
                }
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
            if (history?.Count > 0)
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
            //S.ShowDialog();
            hs.ShowDialog();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            pictureBox1_Click(sender, e);
        }

        private void openFolderInExplorerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start(folderPath);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.folderPath = imageFolderPath;
            Properties.Settings.Default.left_click = folders["left_click"];
            Properties.Settings.Default.right_click = folders["right_click"];
            Properties.Settings.Default.middle_click = folders["middle_click"];
            Properties.Settings.Default.delete = folders["delete"];
            Properties.Settings.Default.Save();
        }

        private void pictureBox1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (hs.hotkeys.ContainsKey(e.KeyData))
            {
                moveImageToFolder(hs.hotkeys[e.KeyData]);
            }

        }

        private void openGetRandomHereToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GetRandom.Form1 f = new GetRandom.Form1(imageFolderPath);
            f.Show();
        }
    }
}
