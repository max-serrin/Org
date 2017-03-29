using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Org
{
    public partial class Org : Form
    {
        private Images images;
        private Traverser<FileInfo> traverser;
        private Mode mode;

        private ContextMenu pictureBoxContextMenu;
        private ContextMenu treeViewContextMenu;
        private FullScreen fullScreen;

        private Delegate LeftClick;
        private Delegate RightClick;
        private Delegate MiddleClick;

        private Dictionary<Keys, string> hotkeys;
        private Regex scriptRegex;

        private enum Mode
        {
            Browse,
            Organize,
            Randomize
        };

        public Org(string directory)
        {
            InitializeComponent();

            hotkeys = new Dictionary<Keys, string>();
            foreach (Keys key in Enum.GetValues(typeof(Keys)))
            {
                if (!hotkeys.ContainsKey(key))
                    hotkeys.Add(key, "");
            }
            hotkeys[Keys.Left] = "previous";
            hotkeys[Keys.Right] = "next";
            hotkeys[Keys.A] = "move, %current_full%, %current_directory%\\best\\%current_name%";
            scriptRegex = new Regex(@"(?<="")\w[\w\s]*(?="")|\w+|""[\w\s]*""");

            pictureBoxContextMenu = new ContextMenu();
            pictureBoxContextMenu.MenuItems.Add("Randomize", pictureBoxContextMenu_Randomize_onClick);
            pictureBoxContextMenu.MenuItems.Add("Reference", pictureBoxContextMenu_Reference_onClick);
            pictureBox.ContextMenu = pictureBoxContextMenu;

            treeViewContextMenu = new ContextMenu();
            treeViewContextMenu.MenuItems.Add("Randomize", treeViewContextMenu_Randomize_onClick);
            folderBrowser.ContextMenu = treeViewContextMenu;
            fullScreen = new FullScreen(pictureBox, this);

            pictureBox.MouseWheel += PictureBox_MouseWheel;
            mode = Mode.Browse;
            browseToolStripMenuItem.Checked = true;
            images = new Images(directory);
            ListDirectory(folderBrowser);
        }

        private void hotkeyScriptParser(string script)
        {
            foreach (string line in Regex.Split(script, "\r\n"))
            {
                string[] scriptSplit = Regex.Split(line, ", ");
                if (scriptSplit.Length > 2)
                {
                    string scriptEdit = scriptSplit[1] + "|" + scriptSplit[2];
                    scriptEdit = Regex.Replace(scriptEdit, "%current_full%", traverser.GetCurrent().FullName);
                    scriptEdit = Regex.Replace(scriptEdit, "%current_name%", traverser.GetCurrent().Name);
                    scriptEdit = Regex.Replace(scriptEdit, "%current_directory%", traverser.GetCurrent().DirectoryName);
                    string[] scriptResplit = scriptEdit.Split('|');
                    scriptSplit[1] = scriptResplit[0];
                    scriptSplit[2] = scriptResplit[1];

                    string directoryPart = scriptSplit[2].Substring(0, scriptSplit[2].LastIndexOf('\\'));
                    if (!Directory.Exists(directoryPart))
                    {
                        Directory.CreateDirectory(directoryPart);
                    }
                }
                

                switch (scriptSplit[0])
                {
                    case "move":
                        File.Move(scriptSplit[1], scriptSplit[2]);
                        break;
                    case "next":
                        Next();
                        break;
                    case "previous":
                        Previous();
                        break;
                    default:
                        break;
                }
            }
        }

        private void pictureBoxContextMenu_Reference_onClick(object sender, EventArgs e)
        {
            new Reference.Reference(traverser.GetCurrent()).Show();
        }

        private void treeViewContextMenu_Randomize_onClick(object sender, EventArgs e)
        {
            randomizeToolStripMenuItem_Click(sender, e);
        }

        private void pictureBoxContextMenu_Randomize_onClick(object sender, EventArgs e)
        {
            randomizeToolStripMenuItem_Click(sender, e);
        }

        public void PictureBox_MouseWheel(object sender, MouseEventArgs e)
        {
            if (e.Delta > 0)
            {
                Next();
            }
            else if (e.Delta < 0)
            {
                Previous();
            }
        }

        private void ListDirectory(TreeView treeView)
        {
            treeView.Nodes.Clear();
            foreach (DriveInfo driveInfo in DriveInfo.GetDrives())
            {
                treeView.Nodes.Add(new TreeNode(driveInfo.Name));
            }
            foreach (TreeNode treeNode in treeView.Nodes)
            {
                CreateDirectoryNode(treeNode);
            }
        }

        private static void CreateDirectoryNode(TreeNode directoryNode)
        {
            directoryNode.Nodes.Clear();
            try
            {
                foreach (var directory in new DirectoryInfo(directoryNode.FullPath).GetDirectories())
                {
                    directoryNode.Nodes.Add(new TreeNode(directory.Name));
                }
            }
            catch
            {
                return;
            }
        }

        private void folderBrowser_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            images = new Images(e.Node.FullPath);

            if (e.Button.Equals(MouseButtons.Left))
            {
                LoadDirectory();
            }

        }

        private void LoadDirectory()
        {
            if (mode == Mode.Randomize)
            {
                traverser = (Traverser<FileInfo>)images.GetRandomizedTraverser();
            }
            else
            {
                traverser = (Traverser<FileInfo>)images.GetTraverser();
            }

            try
            {
                pictureBox.ImageLocation = traverser.GetCurrent().FullName;
            }
            catch (NullReferenceException ex)
            {
                pictureBox.ImageLocation = "";
            }
            toolStripStatusLabel1.Text = traverser.Count().ToString() + " items";
        }

        private void folderBrowser_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            foreach (TreeNode node in e.Node.Nodes)
            {
                CreateDirectoryNode(node);
            }
        }

        private void browseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UncheckMode();
            mode = Mode.Browse;
            browseToolStripMenuItem.Checked = true;
            LoadDirectory();
        }

        private void organizeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UncheckMode();
            mode = Mode.Organize;
            organizeToolStripMenuItem.Checked = true;
            LoadDirectory();
        }

        private void randomizeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UncheckMode();
            mode = Mode.Randomize;
            randomizeToolStripMenuItem.Checked = true;
            LoadDirectory();
        }

        private void UncheckMode()
        {
            foreach (ToolStripMenuItem item in modeToolStripMenuItem.DropDown.Items)
            {
                item.Checked = false;
            }
        }

        private void Org_KeyDown(object sender, KeyEventArgs e)
        {
            string script = hotkeys[e.KeyCode];
            if (script != "")
            {
                hotkeyScriptParser(script);
                e.Handled = true;
            }
            /*
            switch (e.KeyCode)
            {
                case Keys.Left:
                    Previous();
                    e.Handled = true;
                    break;
                case Keys.Right:
                    Next();
                    e.Handled = true;
                    break;
                default:
                    break;
            }
            */
        }

        private void Previous()
        {
            try
            {
                pictureBox.ImageLocation = traverser.MovePrevious().FullName;
            }
            catch (NullReferenceException ex)
            {
                pictureBox.ImageLocation = "";
            }
        }

        private void Next()
        {
            try
            {
                pictureBox.ImageLocation = traverser.MoveNext().FullName;
            }
            catch (NullReferenceException ex)
            {
                pictureBox.ImageLocation = "";
            }
        }

        private void folderBrowser_MouseClick(object sender, MouseEventArgs e)
        {
            CheckXButtons(e);

        }

        private void CheckXButtons(MouseEventArgs e)
        {
            if (e.Button.Equals(MouseButtons.XButton1))
            {
                Next();
            }
            else if (e.Button.Equals(MouseButtons.XButton2))
            {
                Previous();
            }
        }

        private void Org_MouseClick(object sender, MouseEventArgs e)
        {
            CheckXButtons(e);
        }

        public void pictureBox_MouseClick(object sender, MouseEventArgs e)
        {
            CheckXButtons(e);
        }

        private void pictureBox_MouseEnter(object sender, EventArgs e)
        {
            if (pictureBox.Focused == false)
            {
                pictureBox.Focus();
            }
        }

        private void pictureBox_DoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button.Equals(MouseButtons.Left))
            {
                fullScreen.UpdatePictureBox();
                fullScreen.Show();
            }
        }
    }
}
