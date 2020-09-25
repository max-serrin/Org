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
//using IWshRuntimeLibrary;

namespace UpdateShortcut
{
    public partial class Form1 : Form
    {


        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_DragDrop(object sender, DragEventArgs e)
        {
            //IWshShell wsh = new WshShellClass();
            Shell32.Shell shell = new Shell32.Shell();
            
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            var shortcuts = files.Where(f => f.EndsWith(".lnk")).ToList();
            foreach (var s in shortcuts)
            {
                Shell32.Folder folder = shell.NameSpace(Path.GetDirectoryName(s));
                Shell32.FolderItem folderItem = folder.Items().Item(Path.GetFileName(s));
                Shell32.ShellLinkObject currentLink = (Shell32.ShellLinkObject)folderItem.GetLink;

                // Assign the new path here. This value is not read-only.
                currentLink.Path = currentLink.Path.Replace(tbSearch.Text, tbReplace.Text);
                currentLink.WorkingDirectory = currentLink.WorkingDirectory.Replace(tbSearch.Text, tbReplace.Text);

                // Save the link to commit the changes.
                currentLink.Save();
            }
            //foreach (var f in shortcuts) MessageBox.Show(f);
        }

        private void Form1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
        }
    }
}
