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

namespace DrawaD
{
    public partial class Form1 : Form
    {
        // Folder path
        string folderpath;

        //Random
        Random rng;

        // Directory Info and Master File List
        DirectoryInfo di;
        List<FileInfo> fi;

        List<string> ext;

        // Search option (to search sub directories or not)
        SearchOption so;

        public Form1(string p)
        {
            InitializeComponent();

            if (Directory.Exists(p))
                Properties.Settings.Default.folderpath = p;

            // Set path
            folderpath = Properties.Settings.Default.folderpath;

            // Init Random
            rng = new Random();

            // Create the list of extensions
            ext = new List<string> { "*.jpg", "*.jpeg", "*.gif", "*.png", "*.bmp" };

            // Set Search Option to search all directories
            so = SearchOption.AllDirectories;

            setFileInfo();

            FileInfo f = getRandom();

            if (!(Directory.Exists(folderpath + "\\..\\today\\")))
                Directory.CreateDirectory(folderpath + "\\..\\today\\");
            else
            {
                // Move everything in \today\ back to folderpath
                DirectoryInfo ddi = new DirectoryInfo(folderpath + "\\..\\today\\");
                List<FileInfo> ffi = new List<FileInfo>();

                foreach (string s in ext)
                    ffi.AddRange(ddi.EnumerateFiles(s, so).ToList());

                /*if (!(Directory.Exists(folderpath + "\\..\\done\\")))
                    Directory.CreateDirectory(folderpath + "\\..\\done\\");*/
                foreach (FileInfo ff in ffi)
                    ff.MoveTo(folderpath + "\\" + ff.Name);
            }
            f.MoveTo(folderpath + "\\..\\today\\" + f.Name);

            Close();
        }

        // Gets all files in a directory with the given extensions
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
            }
            else
            {
                di = null;
                fi = null;
            }

            return;
        }

        // Get a random image from the Master File List
        //  Return the FileInfo for the image or null if no images exist
        public FileInfo getRandom()
        {
            if (fi.Count > 0)
                return fi[rng.Next(0, fi.Count)];
            else
                return null;
        }
    }

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
