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


namespace MoveFiles
{
    public partial class Form1 : Form
    {
        string filepath = @"c:\file.txt";
        string folderpath = @"c:\289324";

        public Form1()
        {
            InitializeComponent();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DirectoryInfo di = new DirectoryInfo(folderpath);
            List<FileInfo> fi = new List<FileInfo>();

            fi.AddRange(di.EnumerateFiles("*.jpg").ToList());
            fi.AddRange(di.EnumerateFiles("*.jpeg").ToList());
            fi.AddRange(di.EnumerateFiles("*.png").ToList());
            fi.AddRange(di.EnumerateFiles("*.bmp").ToList());
            fi.AddRange(di.EnumerateFiles("*.gif").ToList());

            List<string> li = new List<string>();
            foreach (FileInfo f in fi)
            {
                li.Add(f.FullName);
            }
            

            string[] text = File.ReadAllLines(filepath);

            foreach (string s in text)
            {
                foreach (string l in li)
                {
                    if (l.Contains(s))
                    {
                        File.Move(l, @"c:\g\move\" + s + "." + l.Substring(l.IndexOf('.')));
                        break;
                    }
                }
            }
        }
    }
}
