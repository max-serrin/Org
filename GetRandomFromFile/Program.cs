using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Reflection;

namespace GetRandomFromFile
{
    class Program
    {
        static void Main(string[] args)
        {
            // Get current working directory
            string p = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            // Random
            Random rng;

            // Directory Info (di) and Master File List (fi)
            DirectoryInfo di;
            List<FileInfo> fi;

            // Extensions to search
            List<string> ext;

            // Search option (to search sub directories or not)
            SearchOption so;

            List<string> filePaths = new List<String>(File.ReadAllLines(p + "\\PullList.txt"));

            // Init Random
            rng = new Random();

            // Create the list of extensions
            ext = new List<string> { "*.jpg", "*.jpeg", "*.gif", "*.png", "*.bmp" };

            // Set Search Option to search all directories
            so = SearchOption.AllDirectories;

            di = new DirectoryInfo(p);
            fi = new List<FileInfo>();

            foreach (string s in ext)
                fi.AddRange(di.EnumerateFiles(s, so).ToList());

            foreach (FileInfo f in fi)
                f.Delete();

            for (int i = 0; i < filePaths.Count; i++)
            {
                string name = filePaths[i];
                string path = filePaths[++i];

                // Get random image from directory
                di = new DirectoryInfo(path);
                fi = new List<FileInfo>();

                foreach (string s in ext)
                    fi.AddRange(di.EnumerateFiles(s, so).ToList());

                // Move the image to the current directory and rename it
                FileInfo f = fi[rng.Next(0, fi.Count)];
                File.Copy(f.FullName, p + "\\" + name + f.Extension);
            }
        }
    }
}
