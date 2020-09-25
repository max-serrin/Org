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

namespace YAOrg
{
    public partial class YAOrg : Form
    {
        List<FileInfo> fileInfos = new List<FileInfo>();
        List<string> extensions = new List<string> { "*.jpg", "*.jpeg", "*.gif", "*.png", "*.bmp" };
        SearchOption so = SearchOption.AllDirectories;

        public YAOrg()
        {
            InitializeComponent();
        }

        private void InitializeFileInfos()
        {
            if (fileInfos.Count <= 0) return;

            lbFileList.Items.Clear();
            lbFileList.Items.AddRange(fileInfos.Select(fileInfo => fileInfo.FullName).ToArray());
            lbFileList.SelectedIndex = 0;
        }

        private void YAOrg_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.All;
            else
                e.Effect = DragDropEffects.None;
        }

        private void YAOrg_DragDrop(object sender, DragEventArgs e)
        {
            string[] s = (string[])e.Data.GetData(DataFormats.FileDrop, false);
            if (s.Length <= 0) { return; }

            fileInfos = new List<FileInfo>();
            foreach (var item in s)
            {
                FileAttributes attr = File.GetAttributes(item);
                if ((attr & FileAttributes.Directory) == FileAttributes.Directory)
                {
                    var di = new DirectoryInfo(item);
                    fileInfos.AddRange(extensions.SelectMany(e => di.EnumerateFiles(e, so)));
                } else
                {
                    var fileInfo = new FileInfo(item);
                    fileInfos.Add(fileInfo);
                }
            }

            InitializeFileInfos();
        }

        private void lbFileList_SelectedIndexChanged(object sender, EventArgs e)
        {
            tbCurrentFile.Text = (string)lbFileList.SelectedItem;
        }
    }
}
