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

namespace Org
{
    public partial class Rename : Form
    {
        Org parent;
        public Rename(Org _parent)
        {
            InitializeComponent();

            parent = _parent;
            renameTextBox.Text = parent.traverser.GetCurrent().Name;
        }

        private void Rename_Layout(object sender, LayoutEventArgs e)
        {
            renameTextBox.Width = Width - 40;
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void saveButton_Click(object sender, EventArgs e)
        {   // TODO check for illegal name, directory addition (e.g. "\new_dir\rename.jpg"), authorization to access folder/file, path length
            string movePath = parent.traverser.GetCurrent().DirectoryName + "\\" + renameTextBox.Text;
            File.Move(parent.traverser.GetCurrent().FullName, movePath);
            FileInfo movedFile = new FileInfo(movePath);
            parent.traverser.UpdateCurrent(movedFile);
            parent.pictureBox.ImageLocation = movePath;
        }
    }
}
