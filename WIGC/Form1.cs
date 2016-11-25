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

namespace WIGC
{
    public partial class Form1 : Form
    {
        // Delimiters
        char[] delS = new char[] { '[', ']' };
        char[] delC = new char[] { '{', '}' };

        // Start path
        string path = @"C:\";

        // Image extension
        string ext = "jpg";

        List<string> lines;

        public Form1()
        {
            InitializeComponent();
        }

        private void bDownload_Click(object sender, EventArgs e)
        {
            path = tbPath.Text;
            lines = new List<string>();
            lines.Add(@"<body>");
            bw_DoWork();
            lines.Add(@"</body>");
            System.IO.File.WriteAllLines(path, lines);
        }

        private void bw_DoWork()
        {

            // Setup variables
            string url = tbURL.Text;
            List<string> ls = new List<string>(); // In-progress url, built up by pieces
            List<Point> lp = new List<Point>(); // Save fusker limits
            List<int> ld = new List<int>(); // Padding for each fusker

            // Get file save directory, create it if it doesn't exist
            string dir = path.Substring(0, path.LastIndexOf(@"\") + 1);
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);

            // Look for fusker chars
            if (url.Contains('[') && url.Contains(']'))
            {
                // Split up url by fusker delimiter
                string[] al = url.Split(delS);


                // Begin initial loop
                for (int i = 0; i < al.Count(); i++)
                {
                    ls.Add(al[i]);
                    i++;

                    if (i < al.Count())
                    {
                        // Split fusker limits
                        string[] num = al[i].Split('-');
                        ld.Add(Math.Min(num[0].Length, num[1].Length));
                        int x, y;
                        int.TryParse(num[0], out x);
                        int.TryParse(num[1], out y);
                        lp.Add(new Point(x, y));
                    }
                }

                func(ls[0], "", ls, lp, ld, 0);
            }
            else
            {
                // No fusker so just download the file
                lines.Add("<img src=\"" + url + "\">");
            }
        }

        /*
         *  Recursive downloading for fusker
         */
        private void func(string s, string d, List<string> ls, List<Point> lp, List<int> ld, int depth)
        {
            if (depth == lp.Count())
            {
                string file = path + d + "." + ext;
                if (!File.Exists(file))
                {
                    lines.Add("<img src=\"" + s + "\"><br>");
                }

                return;
            }
            for (int i = lp[depth].X; i <= lp[depth].Y; i++)
            {
                string a = "";
                for (int j = i.ToString().Length; j < ld[depth]; j++)
                {
                    a += "0";
                }
                a += i.ToString();
                func(s + a + ls[depth + 1], d + i.ToString() + "-", ls, lp, ld, depth + 1);
            }
        }

        private void bSetPath_Click(object sender, EventArgs e)
        {
            if (fbd.ShowDialog() == DialogResult.OK)
                tbPath.Text = fbd.SelectedPath + @"\";
        }
    }
}
