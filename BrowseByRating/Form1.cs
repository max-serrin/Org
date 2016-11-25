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

namespace BrowseByRating
{
    public partial class Form1 : Form
    {
        List<FileInfo> fe, fq, fs;
        //string root = Properties.Settings.Default.root;
        string root = "C:\\";
        int index;

        Random rng;

        public Form1()
        {
            InitializeComponent();

            rng = new Random();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Open folder dialog
            folderBrowserDialog1.SelectedPath = root;
            if (folderBrowserDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                // Store the folder as the new root
                root = folderBrowserDialog1.SelectedPath;
                //Properties.Settings.Default.root = this.root;
                //Properties.Settings.Default.Save();

                // Get all the image files in each selected folder
                DirectoryInfo de = new DirectoryInfo(root + "\\explicit");
                DirectoryInfo dq = new DirectoryInfo(root + "\\questionable");
                DirectoryInfo ds = new DirectoryInfo(root + "\\safe");
                fe = new List<FileInfo>();
                fq = new List<FileInfo>();
                fs = new List<FileInfo>();

                if (checkBox1.Checked && Directory.Exists(root + "\\explicit"))
                {
                    fe.AddRange(de.EnumerateFiles("*.jpg").ToList());
                    fe.AddRange(de.EnumerateFiles("*.jpeg").ToList());
                    fe.AddRange(de.EnumerateFiles("*.png").ToList());
                    fe.AddRange(de.EnumerateFiles("*.bmp").ToList());
                    fe.AddRange(de.EnumerateFiles("*.gif").ToList());
                }

                if (checkBox2.Checked && Directory.Exists(root + "\\questionable"))
                {
                    fq.AddRange(dq.EnumerateFiles("*.jpg").ToList());
                    fq.AddRange(dq.EnumerateFiles("*.jpeg").ToList());
                    fq.AddRange(dq.EnumerateFiles("*.png").ToList());
                    fq.AddRange(dq.EnumerateFiles("*.bmp").ToList());
                    fq.AddRange(dq.EnumerateFiles("*.gif").ToList());
                }

                if (checkBox3.Checked && Directory.Exists(root + "\\safe"))
                {
                    fs.AddRange(ds.EnumerateFiles("*.jpg").ToList());
                    fs.AddRange(ds.EnumerateFiles("*.jpeg").ToList());
                    fs.AddRange(ds.EnumerateFiles("*.png").ToList());
                    fs.AddRange(ds.EnumerateFiles("*.bmp").ToList());
                    fs.AddRange(ds.EnumerateFiles("*.gif").ToList());
                }
            }
        }

        private void bGo_Click(object sender, EventArgs e)
        {
            List<FileInfo> ff = new List<FileInfo>();
            List<FileInfo> fi = new List<FileInfo>();
            double r = 0.00, ri;
            bool pass = false;

            if (checkBox1.Checked)
                ff.AddRange(fe);
            if (checkBox2.Checked)
                ff.AddRange(fq);
            if (checkBox3.Checked)
                ff.AddRange(fs);

            if (textBox1.TextLength > 0)
                pass = double.TryParse(textBox1.Text, out r);

            string filerating;
            foreach (FileInfo f in ff)
            {
                filerating = f.Name.Substring(0, 4);
                if (double.TryParse(filerating, out ri))
                {
                    if (ri >= r)
                        fi.Add(f);
                }
            }

            if (fi.Count == 0)
                pictureBox1.ImageLocation = "";
            else
            {
                index = rng.Next(0, fi.Count);

                pictureBox1.ImageLocation = fi[index].FullName;
            }

            
        }
    }
}
