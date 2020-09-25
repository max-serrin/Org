using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
//using System.Runtime.InteropServices;

namespace Reference
{
    public partial class Manager : Form
    {
        private readonly string[] figureFileNames = new string[] {"Figure_Clothed.txt", "Figure_Nude.txt", "Figure_Partial.txt"};
        private List<string> filePathList;

        public Manager()
        {
            InitializeComponent();

            filePathList = Properties.Settings.Default.filePathList.Split(';').ToList();
            var deletedFilePaths = new List<string>();

            foreach (var filePath in filePathList)
            {
                if (!File.Exists(filePath) && !Directory.Exists(filePath))
                {
                    deletedFilePaths.Add(filePath);
                    continue;
                }
                 
                var menuItem = new ToolStripMenuItem(filePath);
                tOpenSelected.DropDownItems.Insert(0, menuItem);
                menuItem.Name = filePath;
            }

            deletedFilePaths.ForEach(path => filePathList.Remove(path));

            SaveFilePathList();

            foreach (var drive in DriveInfo.GetDrives())
            {
                var node = tFolders.Nodes.Add(drive.Name);
                if (filePathList.Contains(node.FullPath))
                    node.Checked = true;
                try
                {
                    foreach (var di in new DirectoryInfo(drive.Name).GetDirectories())
                    {
                        var directoryNode = node.Nodes.Add(di.Name);
                        if (filePathList.Contains(directoryNode.FullPath))
                            directoryNode.Checked = true;
                        //HideCheckBox(tFolders, directoryNode);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error opening drive {drive.Name}: {ex.Message}");
                }
                //fileToolStripMenuItem.DropDownItems.Add(drive.Name, null, ChangeDrive);
            }
        }

        private void SaveFilePathList()
        {
            if (filePathList.Count() == 0)
                Properties.Settings.Default.filePathList = "";
            else
                Properties.Settings.Default.filePathList = filePathList.Aggregate((content, ss) => content.Length < 2 ? ss : $"{content};{ss}");
            Properties.Settings.Default.Save();
        }

        private void tFolders_AfterExpand(object sender, TreeViewEventArgs e)
        {
            Expand(e.Node);
        }

        private void tFolders_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (tFilePath.Text != e.Node.FullPath && tSave.ForeColor == Color.Red)
            {
                if (MessageBox.Show("File not saved. Save before closing?", "Unsaved work", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    SaveFile();
            }

            if (e.Button == MouseButtons.Left)
            {
                if (e.Node.Tag is null)
                {
                    tFile.Text = "";
                    tFilePath.Text = "";
                    tSave.Text = "";
                    tFile.Enabled = false;
                    tFilePath.Enabled = false;
                    tSave.Enabled = false;
                    tOpenFile.Enabled = false;
                    tSave.ForeColor = Color.Black;
                }
                else
                {
                    tFile.Text = File.ReadAllText(e.Node.FullPath);
                    tFilePath.Text = e.Node.FullPath;
                    tSave.Text = "No changes";
                    tFile.Enabled = true;
                    tFilePath.Enabled = true;
                    tSave.Enabled = true;
                    tOpenFile.Enabled = true;
                    tSave.ForeColor = Color.Black;
                }
            }
            else if (e.Button == MouseButtons.Right)
            {
                if (!(e.Node.Tag is null)) return;

                tFolders.SelectedNode = e.Node;

                var contextMenu = new ContextMenu(new MenuItem[] { new MenuItem("Create figure files here.", cRightClickMenu_CreateFigureFiles_OnClick) });
                contextMenu.Show(tFolders, tFolders.PointToClient(Cursor.Position));
            }
        }

        private void cRightClickMenu_CreateFigureFiles_OnClick(object sender, EventArgs e)
        {
            foreach(var fileName in figureFileNames)
                if (!File.Exists(tFolders.SelectedNode.FullPath + "\\" + fileName))
                    File.Create(tFolders.SelectedNode.FullPath + "\\" + fileName);
            RefreshNode(tFolders.SelectedNode);
        }

        private void RefreshNode(TreeNode node)
        {
            node.Nodes.Clear();
            Expand(node);
        }

        private void Expand(TreeNode selectedNode)
        {
            var nodesToDelete = new List<TreeNode>();
            foreach (TreeNode node in selectedNode.Nodes)
            {
                if (!(node.Tag is null)) continue;
                try
                {
                    var directoryInfo = new DirectoryInfo(node.FullPath);
                    foreach (var directory in directoryInfo.GetDirectories())
                    {
                        var directoryNode = node.Nodes.Add(directory.Name);
                        if (filePathList.Contains(directoryNode.FullPath))
                            directoryNode.Checked = true;
                        //HideCheckBox(tFolders, directoryNode);
                    }
                    foreach (var file in directoryInfo.GetFiles())
                    {
                        if (file.Name.StartsWith("Figure") && file.Extension == ".txt")
                        {
                            var fileNode = node.Nodes.Add(file.Name);
                            fileNode.Tag = "File";
                            if (filePathList.Contains(fileNode.FullPath))
                                fileNode.Checked = true;
                        }
                    }
                }
                catch (UnauthorizedAccessException ex)
                {
                    nodesToDelete.Add(node);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error opening folder {node.FullPath}: {ex.Message}");
                }
            }

            nodesToDelete.ForEach(node => selectedNode.Nodes.Remove(node));
        }

        private void tFile_TextChanged(object sender, EventArgs e)
        {
            tSave.Text = "Not saved";
            tSave.ForeColor = Color.Red;
        }

        private void tSave_Click(object sender, EventArgs e)
        {
            SaveFile();
        }

        private void tFile_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.S && e.Modifiers == Keys.Control && !(tFolders.SelectedNode.Tag is null))
                SaveFile();
        }

        private void SaveFile()
        {
            File.WriteAllText(tFilePath.Text, tFile.Text);
            tSave.Text = "Saved!";
            tSave.ForeColor = Color.Green;
        }

        private void tFile_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.All;
            else
                e.Effect = DragDropEffects.None;
        }

        private void tOpenFile_Click(object sender, EventArgs e)
        {
            var reference = new Reference(tFilePath.Text);
            reference.Show();
        }

        private void openAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var reference = new Reference(filePathList.ToArray());
            reference.Show();
        }

        public List<TreeNode> GetCheckedNodes(TreeNodeCollection nodes)
        {
            var treeNodes = new List<TreeNode>();

            foreach (TreeNode node in nodes)
            {
                if (node.Checked)
                    treeNodes.Add(node);

                if (node.Nodes.Count != 0)
                    treeNodes.AddRange(GetCheckedNodes(node.Nodes));
            }

            return treeNodes;
        }

        private void tFolders_AfterCheck(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Checked)
            {
                var menuItem = new ToolStripMenuItem(e.Node.FullPath);
                tOpenSelected.DropDownItems.Insert(0, menuItem);
                menuItem.Name = e.Node.FullPath;
                if (e.Node.Tag is null)
                    menuItem.Tag = "Directory";
                //menuItem.DropDownItems.Add("Remove");
                filePathList.Add(e.Node.FullPath);
                SaveFilePathList();
            }
            else
            {
                tOpenSelected.DropDownItems.RemoveByKey(e.Node.FullPath);
                filePathList.Remove(e.Node.FullPath);
                SaveFilePathList();
            }
        }

        private void tFile_DragDrop(object sender, DragEventArgs e)
        {
            string[] s = (string[])e.Data.GetData(DataFormats.FileDrop, false);
            string filePaths = s.Aggregate((content, ss) => content.Length < 2 ? ss : content + Environment.NewLine + ss);
            tFile.Text = tFile.Text.Length < 2 ? filePaths : tFile.Text + Environment.NewLine + filePaths;
        }
        
        //private const int TVIF_STATE = 0x8;
        //private const int TVIS_STATEIMAGEMASK = 0xF000;
        //private const int TV_FIRST = 0x1100;
        //private const int TVM_SETITEM = TV_FIRST + 63;

        //[StructLayout(LayoutKind.Sequential, Pack = 8, CharSet = CharSet.Auto)]
        //private struct TVITEM
        //{
        //    public int mask;
        //    public IntPtr hItem;
        //    public int state;
        //    public int stateMask;
        //    [MarshalAs(UnmanagedType.LPTStr)]
        //    public string lpszText;
        //    public int cchTextMax;
        //    public int iImage;
        //    public int iSelectedImage;
        //    public int cChildren;
        //    public IntPtr lParam;
        //}

        //[DllImport("user32.dll", CharSet = CharSet.Auto)]
        //private static extern IntPtr SendMessage(IntPtr hWnd, int Msg, IntPtr wParam,
        //                                 ref TVITEM lParam);

        ///// <summary>
        ///// Hides the checkbox for the specified node on a TreeView control.
        ///// </summary>
        //private void HideCheckBox(TreeView tvw, TreeNode node)
        //{
        //    TVITEM tvi = new TVITEM();
        //    tvi.hItem = node.Handle;
        //    tvi.mask = TVIF_STATE;
        //    tvi.stateMask = TVIS_STATEIMAGEMASK;
        //    tvi.state = 0;
        //    SendMessage(tvw.Handle, TVM_SETITEM, IntPtr.Zero, ref tvi);
        //}
    }
}
