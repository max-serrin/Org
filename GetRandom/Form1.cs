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
using System.Collections.Specialized;

namespace GetRandom
{
    public partial class Form1 : Form
    {
        // Folder path with the images to randomize
        string folderpath;

        // Favorites Collection, saved to options
        StringCollection favorites;

        //Random
        Random rng;

        // Directory Info (di) and Master File List (fi)
        DirectoryInfo di;
        List<FileInfo> fi;

        // Extensions to search
        List<string> ext;

        // Index for image list
        int pos;

        // Search option (to search sub directories or not)
        SearchOption so;
        
        /// <summary>
        /// Initializes the GetRandom Form and enumerates and randomizes files found in directory p.
        /// </summary>
        /// <param name="p"></param>
        public Form1(string p)
        {
            InitializeComponent();

            this.KeyUp += form1_KeyUp;
            pbImage.Click += form1_Click;

            // Init Random
            rng = new Random();

            if (Directory.Exists(p))
                Properties.Settings.Default.folderpath = p;

            // Set path
            folderpath = Properties.Settings.Default.folderpath;
            
            if (Properties.Settings.Default.favorites == null)
            {
                Properties.Settings.Default.favorites = new StringCollection();
                Properties.Settings.Default.Save();
            }
            favorites = Properties.Settings.Default.favorites;

            // Create the list of extensions
            ext = new List<string> { "*.jpg", "*.jpeg", "*.gif", "*.png", "*.bmp" };

            // Set Search Option to search all directories
            so = SearchOption.AllDirectories;
            allDirectoriesToolStripMenuItem.Checked = true;

            setFileInfo();
        }

        /// <summary>
        /// Provides handling should the form itself be clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void form1_Click(object sender, EventArgs e)
        {
            if (((MouseEventArgs)e).Button == System.Windows.Forms.MouseButtons.XButton2)
                doit();
            else if (((MouseEventArgs)e).Button == System.Windows.Forms.MouseButtons.XButton1)
                doback();
        }

        /// <summary>
        /// Provides handling for key presses to the form.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void form1_KeyUp(object sender, KeyEventArgs e)
        {
            if ((ModifierKeys & Keys.Control) == Keys.Control)
                if (e.KeyCode == Keys.F)
                    removeFav();
            else if ((ModifierKeys & Keys.Control) == Keys.Shift) { }
            else if ((ModifierKeys & Keys.Control) == Keys.Alt) { }
            else
            {
                if (e.KeyData == Keys.F)
                    setFav();
                else if (e.KeyData == Keys.Right || e.KeyData == Keys.Up || e.KeyData == Keys.Space)
                    doit();
                else if (e.KeyData == Keys.Left || e.KeyData == Keys.Down)
                    doback();
                else if (e.KeyData == Keys.Home)
                {
                    pos = -1;
                    doit();
                }
                else if (e.KeyData == Keys.End)
                {
                    pos = fi.Count - 2;
                    doit();
                }
            }
        }

        /// <summary>
        /// Display the next image in the random list (wraps to first image after last).
        /// </summary>
        private void doit()
        {
            pos++;
            if (pos == fi.Count)
                pos = 0;

            // Get the next image
            FileInfo fr = fi[pos];
 
            // If one exists, output to the picture box
            if (fi != null)
                pbImage.ImageLocation = fr.FullName;
            else
                pbImage.ImageLocation = "";

            checkFav();
        }

        /// <summary>
        /// Display the previous image in the random list (wraps to last image after first).
        /// </summary>
        private void doback()
        {
            pos--;
            if (pos < 0)
                pos = fi.Count - 1;

            // Get the next image
            FileInfo fr = fi[pos];

            // If one exists, output to the picture box
            if (fi != null)
                pbImage.ImageLocation = fr.FullName;
            else
                pbImage.ImageLocation = "";

            checkFav();
        }

        /// <summary>
        /// Sets the favorite heart to red if the current image is among the favorites list.
        /// </summary>
        private void checkFav()
        {
            if (favorites.Contains(pbImage.ImageLocation))
                menuStrip1.Items[4].Image = GetRandom.Properties.Resources.heart_red;
            else
                menuStrip1.Items[4].Image = GetRandom.Properties.Resources.heart_yellow;
        }

        /// <summary>
        /// Get a random image from the Master File List and return the FileInfo for the image or null if no images exist.
        /// </summary>
        /// <returns></returns>
        public FileInfo getRandom()
        {
            if (fi.Count > 0)
                return fi[rng.Next(0, fi.Count)];
            else
                return null;
        }

        // Set the folder where the images will be pooled
        private void setFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
             // Open folder dialog
            fbdBrowser.SelectedPath = folderpath;
            if (fbdBrowser.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                folderpath = fbdBrowser.SelectedPath;
                setFileInfo();
                doit();
            }
        }
        
        /// <summary>
        /// Display a message box with the image path.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void infoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(pbImage.ImageLocation);
        }

        /// <summary>
        /// Delete the currently selected image.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void deleteImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            File.Delete(pbImage.ImageLocation);
            doit();
        }

        /// <summary>
        /// Perform the next image command.  
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void nextToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            doit();
        }

        /// <summary>
        /// Gets all files in a directory with the given extensions
        /// </summary>
        private void setFileInfo()
        {
            Properties.Settings.Default.folderpath = this.folderpath;
            Properties.Settings.Default.Save();

            if (folderpath != "" && Directory.Exists(folderpath))
            {

                di = new DirectoryInfo(folderpath);
                fi = new List<FileInfo>();

                foreach (string s in ext)
                    fi.AddRange(di.EnumerateFiles(s, so).ToList());

                MyExtensions.Shuffle(fi, rng);
                pos = -1;

                toolsToolStripMenuItem.Enabled = true;
                lastImageToolStripMenuItem.Enabled = true;
                nextImageToolStripMenuItem.Enabled = true;
                toolStripMenuItem1.Enabled = true;
            }
            else
            {
                di = null;
                fi = null;

                toolsToolStripMenuItem.Enabled = false;
                lastImageToolStripMenuItem.Enabled = false;
                nextImageToolStripMenuItem.Enabled = false;
                toolStripMenuItem1.Enabled = false;
            }
        }

        /// <summary>
        /// Cuts the next X images onto the clipboard.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void nextXToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StringCollection get = new StringCollection();
            string check;

            Popup pop = new Popup();
            pop.ShowDialog();
            int num = pop.GetValue();

            setFileInfo();

            if (fi.Count > 0)
                for (int i = 0; i < num; i++)
                {
                    check = fi[rng.Next(0, fi.Count)].FullName;

                    if (get.Contains(check))
                        i--;
                    else
                        get.Add(check);
                }
            else
                MessageBox.Show("The folder does not contain any recognized image files.");

            if (get.Count > 0)
            {
                byte[] moveEffect = new byte[] { 2, 0, 0, 0 };
                MemoryStream dropEffect = new MemoryStream();
                dropEffect.Write(moveEffect, 0, moveEffect.Length);

                DataObject data = new DataObject();
                data.SetFileDropList(get);
                data.SetData("Preferred DropEffect", dropEffect);

                Clipboard.Clear();
                Clipboard.SetDataObject(data, true);
                //Clipboard.SetFileDropList(get);
                MessageBox.Show("Files successfully cut.");
            }
            else
                MessageBox.Show("No images were cut.");
        }

        /// <summary>
        /// Creates a popup for the next X images.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void nextXPopupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StringCollection get = new StringCollection();
            string check;

            Popup pop = new Popup();
            pop.ShowDialog();
            int num = pop.GetValue();

            if (fi.Count > 0)
                for (int i = 0; i < num; i++)
                {
                    check = fi[rng.Next(0, fi.Count)].FullName;

                    if (get.Contains(check))
                    {
                        i--;
                    }
                    else
                    {
                        get.Add(check);
                    }
                }
            else
                MessageBox.Show("The folder does not contain any recognized image files.");

            NextX nx = new NextX(get, this);
            nx.ShowDialog();
        }

        /// <summary>
        /// Moves current image to the directory \L7\ from the current location.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void promoteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.IO.FileInfo f = new FileInfo(pbImage.ImageLocation);

            // Assumes L7 exists...
            f.MoveTo(f.DirectoryName + "\\L7\\" + f.Name);
            doit();
        }

        /// <summary>
        /// Moves the current image to the directory \..\-\ from the current location.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void demoteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.IO.FileInfo f = new FileInfo(pbImage.ImageLocation);

            // Assumes \..\- exists...
            f.MoveTo(f.DirectoryName + "\\..\\-\\" + f.Name);
            doit();
        }

        /// <summary>
        /// Next image tool strip menu item.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void nextImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            doit();
        }

        /// <summary>
        /// Previous image tool strip menu item.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lastImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            doback();
        }

        /// <summary>
        /// Open and navigate to the selected image in explorer.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void openFileLocationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileLocation(pbImage.ImageLocation);
        }

        /// <summary>
        /// Open and navigate to the selected image in explorer.
        /// </summary>
        /// <param name="filePath"></param>
        public void openFileLocation(string filePath)
        {
            System.Diagnostics.Process.Start("explorer.exe", string.Format("/select,\"{0}\"", filePath));
        }

        /// <summary>
        /// Toggle favorite property.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            toggleFav();
        }

        /// <summary>
        /// Toggle favorite property and set the icon. 
        /// </summary>
        private void toggleFav()
        {
            if (favorites.Contains(pbImage.ImageLocation))
                favorites.Remove(pbImage.ImageLocation);
            else
                favorites.Add(pbImage.ImageLocation);
            saveFavorites();
            checkFav();
        }

        /// <summary>
        /// Sets image as favorite and updates icon.
        /// </summary>
        private void setFav()
        {
            if (!(favorites.Contains(pbImage.ImageLocation)))
                favorites.Add(pbImage.ImageLocation);
            saveFavorites();
            checkFav();
        }

        /// <summary>
        /// Unfavorite image and updates icon.
        /// </summary>
        private void removeFav()
        {
            if (favorites.Contains(pbImage.ImageLocation))
                favorites.Remove(pbImage.ImageLocation);
            saveFavorites();
            checkFav();
        }

        /// <summary>
        /// Saves the favorite list.
        /// </summary>
        private void saveFavorites()
        {
            Properties.Settings.Default.favorites = favorites;
            Properties.Settings.Default.Save();
        }

        /// <summary>
        /// Toggles whether or not the favorites are being viewed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void favoritesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem _sender = (ToolStripMenuItem)sender;

            if (!(_sender.Checked))
                setFileInfo();
            else
            {
                fi = new List<FileInfo>();
                foreach (string s in favorites)
                {
                    FileInfo f = new FileInfo(s);
                    fi.Add(f);
                }
                MyExtensions.Shuffle(fi, rng);
            }

            pos = -1;
            doit();
        }

        /// <summary>
        /// Toggles between using only the top directory or all directories when enumerating images
        /// </summary>
        private void allDirectoriesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (allDirectoriesToolStripMenuItem.Checked)
            {
                allDirectoriesToolStripMenuItem.Checked = false;
                // Set Search Option to search only the top directory
                so = SearchOption.TopDirectoryOnly;
            }
            else
            {
                allDirectoriesToolStripMenuItem.Checked = true;
                // Set Search Option to search all directories
                so = SearchOption.AllDirectories;
            }

            setFileInfo();
        }

        private void pbImage_Click(object sender, EventArgs e)
        {
            if (((MouseEventArgs)e).Button == MouseButtons.Right)
            {
                StringCollection get = new StringCollection();

                get.Add(pbImage.ImageLocation);
                
                Clipboard.SetFileDropList(get);
            }
        }
    }

    /// <summary>
    /// Helper extension for shuffling a list.
    /// </summary>
    public static class MyExtensions
    {
        public static void Shuffle<T>(this IList<T> list, Random rnd)
        {
            for (var i = 0; i < list.Count; i++)
                Swap(list, i, rnd.Next(i, list.Count));
        }

        public static void Swap<T>(this IList<T> list, int i, int j)
        {
            var temp = list[i];
            list[i] = list[j];
            list[j] = temp;
        }
    }
}
