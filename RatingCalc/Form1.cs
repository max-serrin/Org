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

namespace RatingCalc
{
    public partial class Form1 : Form
    {
        List<FileInfo> fi;
        List<TrackBar> sliders;
        string root = Properties.Settings.Default.root;
        int index, hindex;
        List<int> indexHistory = new List<int>();
        Stack<FileInfo> undoHistory = new Stack<FileInfo>();
        Stack<string> undoFolderHistory = new Stack<string>();
        Stack<int> undoIndexHistory = new Stack<int>();

        bool mult = true;

        string folder = "";
        string format = String.Format("#.{0}", new string('#', 2));

        Random rng;

        public Form1()
        {
            InitializeComponent();

            rng = new Random();

            sliders = new List<TrackBar>();
            foreach (Control gb in flowLayoutPanel1.Controls)
            {
                if (gb.Name != "bFinish")
                    sliders.Add((TrackBar)gb.Controls[0]);
            }
            
            foreach (TrackBar tb in sliders)
            {
                tb.KeyDown += tb_KeyDown;
            }

            foreach (Control gb in flowLayoutPanel1.Controls)
            {
                gb.Enabled = false;
            }

            tbRating.KeyDown += tbRating_KeyDown;

            lFilename.Text = "";
            lIndex.Text = "";

            editToolStripMenuItem.Enabled = false;
        }

        void tbRating_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                button1_Click(sender, new EventArgs());
            }
            else if (e.KeyCode == Keys.F2)
            {
                button2_Click(sender, new EventArgs());
            }
            else if (e.KeyCode == Keys.F3)
            {
                button3_Click(sender, new EventArgs());
            }
            if (e.KeyCode == Keys.F12)
            {
                NextBatch();
            }
            else if (e.KeyCode == Keys.Add)
            {
                index++;
                if (index > fi.Count)
                    index = 0;
                NextBatch(index);
            }
            else if (e.KeyCode == Keys.Subtract)
            {
                index--;
                if (index < 0)
                    index = fi.Count - 1;
                NextBatch(index);
            }
            else if (e.KeyCode == Keys.F4)
            {
                Remove(this.index);
            }
            else if (e.KeyCode == Keys.O)
            {
                if (e.Modifiers == Keys.Control)
                {
                    openCollectionToolStripMenuItem_Click(sender, null);
                }
            }
            else if (e.KeyCode == Keys.Multiply)
            {

                if (mult)
                {
                    sliders[0].Focus();
                }
                else
                {
                    tbRating.Focus();
                    tbRating.SelectAll();
                }

                mult = !mult;

            }
            else
                e.Handled = false;
            return;
        }

        void tb_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F12)
            {
                NextBatch();
            }
            else if (e.KeyCode == Keys.Add)
            {
                index++;
                if (index > fi.Count)
                    index = 0;
                NextBatch(index);
            }
            else if (e.KeyCode == Keys.Subtract)
            {
                index--;
                if (index < 0)
                    index = fi.Count - 1;
                NextBatch(index);
            }
            else if (e.KeyCode == Keys.F11)
                Remove(this.index);
            else if (e.KeyCode == Keys.O)
            {
                if (e.Modifiers == Keys.Control)
                {
                    openCollectionToolStripMenuItem_Click(sender, null);
                }
            }
            else if (e.KeyCode == Keys.Multiply)
            {

                if (mult)
                {
                    sliders[0].Focus();
                }
                else
                {
                    tbRating.Focus();
                    tbRating.SelectAll();
                }

                mult = !mult;

            }
            else
            {
                ((TrackBar)sender).Value = getKeyValue(e);

                int index = sliders.IndexOf((TrackBar)sender) + 1;

                if (index < sliders.Count)
                {
                    sliders[sliders.IndexOf((TrackBar)sender) + 1].Focus();
                    computeRating();
                }
                else
                {
                    computeRating();
                }
            }
        }

        private void computeRating()
        {
            double count = 0.0;
            double total = 0.0;
            double rating = 0.0;

            foreach (TrackBar tb in sliders)
            {
                if (tb.Value > 0)
                {
                    count++;
                    total += tb.Value;
                }
            }

            rating = Math.Round(total / count, 2);
            //MessageBox.Show(rating.ToString());

            //Popup pop = new Popup();

            //string folder = pop.GetValue(rating, format);
            tbRating.Text = rating.ToString();
            if (tbRating.Text.Length == 1)
                tbRating.Text += ".00";
            else if (tbRating.Text.Length == 3)
                tbRating.Text += "0";
        }

        private void done()
        {

            indexHistory.RemoveAll((int a) => { return a == index; });

            if (folder.Equals("redo"))
            {
                NextBatch(index);
            }
            else
            {
                string move = root + "\\rated" + folder + "_" + fi[index].Name;

                // Move the file
                File.Move(fi[index].FullName, move);

                // Save undo history
                saveHistory(fi[index], move, index);

                // Remove the file from our list
                fi.RemoveAt(index);

                //
                foreach (int i in indexHistory)
                {
                    if (i > index)
                        index--;
                }

                NextBatch();
            }

        }

        private void NextBatch()
        {
            NextBatch(rng.Next(0, fi.Count));
            indexHistory.RemoveRange(hindex, indexHistory.Count - hindex);
        }

        private void NextBatch(int index)
        {
            tbRating.Text = "";
            this.index = index;
            
            indexHistory.Add(index);
            hindex = indexHistory.Count - 1;

            foreach (TrackBar tb in sliders)
            {
                tb.Value = 0;
            }

            if (mult)
                tbRating.Focus();
            else
                sliders[0].Focus();

            if (fi.Count > 0)
            {
                //index = rng.Next(0, fi.Count);

                // TODO
                // Check if image exists in rated folder

                pictureBox1.ImageLocation = fi[index].FullName;
                lFilename.Text = "Filename: " + fi[index].Name;
                lIndex.Text = "Index: " + (index + 1).ToString() + "/" + fi.Count.ToString();
            }
            else
            {
                pictureBox1.ImageLocation = "";

                foreach (Control gb in flowLayoutPanel1.Controls)
                {
                    if (gb.Name != "bFinish")
                        gb.Enabled = false;
                }

                lFilename.Text = "";
                lIndex.Text = "";

                editToolStripMenuItem.Enabled = false;
            }
        }

        private void sFace_KeyDown(object sender, KeyEventArgs e)
        {
            
                sFace.Value = getKeyValue(e);
        }

        private int getKeyValue(KeyEventArgs e)
        {
            int returnVal = 0;

            switch (e.KeyCode)
            {
                case Keys.NumPad1:
                    returnVal = 1;
                    break;
                case Keys.NumPad2:
                    returnVal = 2;
                    break;
                case Keys.NumPad3:
                    returnVal = 3;
                    break;
                case Keys.NumPad4:
                    returnVal = 4;
                    break;
                case Keys.NumPad5:
                    returnVal = 5;
                    break;
                case Keys.D1:
                    returnVal = 1;
                    break;
                case Keys.D2:
                    returnVal = 2;
                    break;
                case Keys.D3:
                    returnVal = 3;
                    break;
                case Keys.D4:
                    returnVal = 4;
                    break;
                case Keys.D5:
                    returnVal = 5;
                    break;
            }

            return returnVal;
        }

        private void openCollectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Open folder dialog
            folderBrowserDialog1.SelectedPath = root;
            if (folderBrowserDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                // Store the folder as the new root
                root = folderBrowserDialog1.SelectedPath;
                Properties.Settings.Default.root = this.root;
                Properties.Settings.Default.Save();

                // Reset undo history
                undoHistory = new Stack<FileInfo>();
                undoFolderHistory = new Stack<string>();
                undoIndexHistory = new Stack<int>();

                // Get all the image files in the folder (not recursive)
                DirectoryInfo di = new DirectoryInfo(root);
                fi = di.EnumerateFiles("*.jpg").ToList();
                fi.AddRange(di.EnumerateFiles("*.jpeg").ToList());
                fi.AddRange(di.EnumerateFiles("*.png").ToList());
                fi.AddRange(di.EnumerateFiles("*.bmp").ToList());
                fi.AddRange(di.EnumerateFiles("*.gif").ToList());

                if (!Directory.Exists(root + "\\rated"))
                {
                    Directory.CreateDirectory(root + "\\rated");
                    Directory.CreateDirectory(root + "\\rated\\explicit");
                    Directory.CreateDirectory(root + "\\rated\\questionable");
                    Directory.CreateDirectory(root + "\\rated\\safe");
                }

                foreach (Control gb in flowLayoutPanel1.Controls)
                {
                    gb.Enabled = true;
                }

                editToolStripMenuItem.Enabled = true;

                NextBatch();
            }

            
        }

        private void skipToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NextBatch();
        }

        private void removeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Remove(this.index);
        }

        private void Remove(int index)
        {
            if (!Directory.Exists(root + "\\remove"))
            {
                Directory.CreateDirectory(root + "\\remove");
            }

            string move = root + "\\remove\\" + fi[index].Name;

            // Remove the file from the list and move it into the remove folder
            File.Move(fi[index].FullName, move);

            // Save undo history
            saveHistory(fi[index], move, index);

            // Remove the file from the list
            fi.RemoveAt(index);

            NextBatch();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Delete(this.index);
        }

        private void Delete(int index)
        {
            // Remove the file from the list and delete it
            File.Delete(fi[index].FullName);
            fi.RemoveAt(index);

            NextBatch();
        }

        private void saveHistory(FileInfo ff, string m, int i)
        {
            // Save history information
            undoHistory.Push(ff);
            undoFolderHistory.Push(m);
            undoIndexHistory.Push(i);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void bFinish_Click(object sender, EventArgs e)
        {
            computeRating();
        }

        private void goToIndexToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IndexPopup2 pop = new IndexPopup2();

            int i = pop.GetValue(fi, index);
            NextBatch(i);
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.O)
            {
                if (e.Modifiers == Keys.Control)
                {
                    openCollectionToolStripMenuItem_Click(sender, null);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (tbRating.Text.Length == 1)
                tbRating.Text += ".00";
            else if (tbRating.Text.Length == 3)
                tbRating.Text += "0";
            folder = "\\explicit\\" + tbRating.Text;
            done();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (tbRating.Text.Length == 1)
                tbRating.Text += ".00";
            else if (tbRating.Text.Length == 3)
                tbRating.Text += "0";
            folder = "\\questionable\\" + tbRating.Text;;
            done();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (tbRating.Text.Length == 1)
                tbRating.Text += ".00";
            else if (tbRating.Text.Length == 3)
                tbRating.Text += "0";
            folder = "\\safe\\" + tbRating.Text;
            done();
        }

        private void bUndo_Click(object sender, EventArgs e)
        {
            // Check to make sure that there is something to undo
            if (undoHistory.Count > 0)
            {
                FileInfo ff = undoHistory.Pop();
                string m = undoFolderHistory.Pop();
                int i = undoIndexHistory.Pop();

                // Move the file back to its original location
                File.Move(m, ff.FullName);

                // Re-insert the file information into our list at the specific index
                fi.Insert(i, ff);

                NextBatch(i);
            }
            else
            {
                if (mult)
                    tbRating.Focus();
                else
                    sliders[0].Focus();
            }
        }
        
    }
}
