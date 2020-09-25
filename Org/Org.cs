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
using MyExtensions;

using Modes = Org.Enumeration.Modes;
using Actions = Org.Enumeration.Actions;
using HotkeysSettings = Org.Enumeration.HotkeySettings;
using UndoActions = Org.Enumeration.UndoActions;
using System.Diagnostics;
using System.Collections.Specialized;

namespace Org
{
    public partial class Org : Form
    {
        private Images images;
        internal Traverser<FileInfo> traverser;
        private Modes mode;

        private ContextMenu pictureBoxContextMenu;
        private ContextMenu treeViewContextMenu;
        private TreeNode favorites;
        private FullScreen fullScreen;
        private Settings settings;

        internal Dictionary<Tuple<Keys, Modes>, HotkeysSettings> hotkeys;
        private Stack<UndoActions> undoHistory;
        
        public Org(string directory)
        {
            // Initialization
            InitializeComponent();
            KeyPreview = true;

            // Hotkeys Setup
            hotkeys = new Dictionary<Tuple<Keys, Modes>, HotkeysSettings>();
            
            foreach (Keys key in DataExtensions.GetCommonKeysAndButtons())
            {
                foreach (Modes mode in Enum.GetValues(typeof(Modes)))
                {
                    hotkeys.Add(new Tuple<Keys, Modes>(key, mode), new HotkeysSettings(key));
                    hotkeys.Add(new Tuple<Keys, Modes>(key | Keys.Control, mode), new HotkeysSettings(key | Keys.Control));
                    hotkeys.Add(new Tuple<Keys, Modes>(key | Keys.Control | Keys.Shift, mode), new HotkeysSettings(key | Keys.Control | Keys.Shift));
                    hotkeys.Add(new Tuple<Keys, Modes>(key | Keys.Control | Keys.Alt, mode), new HotkeysSettings(key | Keys.Control | Keys.Alt));
                    hotkeys.Add(new Tuple<Keys, Modes>(key | Keys.Control | Keys.Shift | Keys.Alt, mode), new HotkeysSettings(key | Keys.Control | Keys.Shift | Keys.Alt));
                    hotkeys.Add(new Tuple<Keys, Modes>(key | Keys.Shift, mode), new HotkeysSettings(key | Keys.Shift));
                    hotkeys.Add(new Tuple<Keys, Modes>(key | Keys.Shift | Keys.Alt, mode), new HotkeysSettings(key | Keys.Shift | Keys.Alt));
                    hotkeys.Add(new Tuple<Keys, Modes>(key | Keys.Alt, mode), new HotkeysSettings(key | Keys.Alt));
                }
            }
            undoHistory = new Stack<UndoActions>();

            pictureBox.MouseWheel += PictureBox_MouseWheel;

            // PictureBox Context Menu Setup
            pictureBoxContextMenu = new ContextMenu();
            pictureBoxContextMenu.MenuItems.Add("Randomize", PictureBoxContextMenu_Randomize_onClick);
            pictureBoxContextMenu.MenuItems.Add("Reference", PictureBoxContextMenu_Reference_onClick);
            pictureBox.ContextMenu = pictureBoxContextMenu;

            // TreeView Context Menu Setup
            treeViewContextMenu = new ContextMenu();
            treeViewContextMenu.MenuItems.Add("Add to Favorites", TreeViewContextMenu_AddToFavorites_onClick);
            treeViewContextMenu.MenuItems.Add("Remove from Favorites", TreeViewContextMenu_RemoveFromFavorites_onClick);
            treeViewContextMenu.MenuItems.Add("Randomize", TreeViewContextMenu_Randomize_onClick);
            folderBrowser.ContextMenu = treeViewContextMenu;

            //Dialogs
            fullScreen = new FullScreen(pictureBox, this);
            settings = new Settings(this);

            // Mode Setup
            mode = Modes.Browse;
            browseToolStripMenuItem.Checked = true;

            browseToolStripMenuItem.Tag = Modes.Browse;
            organizeToolStripMenuItem.Tag = Modes.Organize;
            randomizeToolStripMenuItem.Tag = Modes.Randomize;

            // Load Images
            images = new Images(directory);

            // TreeView Setup
            folderBrowser.Nodes.Clear();
            favorites = new TreeNode("Favorites")
            {
                Tag = ""
            };
            folderBrowser.Nodes.Add(favorites);
            ListDirectory(folderBrowser);
        }

        internal void UpdateHotkeyDictionary()
        {

        }

        private void TreeViewContextMenu_RemoveFromFavorites_onClick(object sender, EventArgs e)
        {
            if (folderBrowser.SelectedNode.Tag.Exists())
            {   // Selected node is part of the favorites node
                if (folderBrowser.SelectedNode.Parent.Exists())
                {   // Selected node is not the favorite node itself
                    favorites.Nodes.Remove(folderBrowser.SelectedNode);
                }
            }
            else
            {
                // Figure out which favorite node matches the selected node and remove it
                foreach (TreeNode node in favorites.Nodes)
                {
                    if (node.Tag.Equals(folderBrowser.SelectedNode.FullPath))
                    {
                        favorites.Nodes.Remove(node);
                        break;
                    }
                }
            }
        }

        private void TreeViewContextMenu_AddToFavorites_onClick(object sender, EventArgs e)
        {
            TreeNode favorite = new TreeNode(folderBrowser.SelectedNode.Text)
            {
                Tag = folderBrowser.SelectedNode.FullPath
            };
            favorites.Nodes.Add(favorite);
        }

        private void HotkeyScriptParser(string script)
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

        private void PictureBoxContextMenu_Reference_onClick(object sender, EventArgs e)
        {
            new Reference.Reference(traverser.GetCurrent()).Show();
        }

        private void TreeViewContextMenu_Randomize_onClick(object sender, EventArgs e)
        {
            // Set randomize mode
            SetMode(Modes.Randomize);
            // Simluate a left click on the node
            FolderBrowser_MouseClick(sender, new MouseEventArgs(MouseButtons.Left, 1, 0, 0, 0));
        }

        private void PictureBoxContextMenu_Randomize_onClick(object sender, EventArgs e)
        {
            RandomizeToolStripMenuItem_Click(sender, e);
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

        private void FolderBrowser_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            // Set the selected node (right clicks don't do this automatically)
            ((TreeView)sender).SelectedNode = e.Node;
            
            // If we left clicked, load the directory
            if (e.Button.Equals(MouseButtons.Left))
            {
                if (e.Node.Tag.Exists())
                {   // Exists in the favorite node
                    if (e.Node.Parent.Exists())
                    {   // Is not the favorite node itself
                        images = new Images((string)e.Node.Tag);
                    }
                }
                else
                {   // Is a directory
                    images = new Images(e.Node.FullPath);
                }
                LoadDirectory();
            }

        }

        private void LoadDirectory()
        {
            if (mode == Modes.Randomize)
            {
                traverser = (Traverser<FileInfo>)images.GetRandomizedTraverser();
            }
            else
            {
                traverser = (Traverser<FileInfo>)images.GetTraverser();
            }
            pictureBox.ImageLocation = traverser?.GetCurrent()?.FullName ?? "";
            statusLabel_Index.Text = traverser.Count.ToString() + " items";
        }

        private void FolderBrowser_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            foreach (TreeNode node in e.Node.Nodes)
            {
                CreateDirectoryNode(node);
            }
        }

        private void BrowseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetMode(Modes.Browse);
            LoadDirectory();
        }

        private void OrganizeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetMode(Modes.Organize);
            LoadDirectory();
        }

        private void RandomizeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetMode(Modes.Randomize);
            LoadDirectory();
        }

        private void SetMode(Modes _mode)
        {
            UncheckMode();
            mode = _mode;
            Controls.OfType<ToolStripMenuItem>().Where(menuItem => menuItem.Tag.Equals(_mode)).FirstOrDefault().Checked = true;
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
            if (hotkeys.TryGetValue(new Tuple<Keys, Modes>(e.KeyData, mode), out HotkeysSettings hotkeySettings))
            {
                //MessageBox.Show(hotkeySettings.Action.ToString());
                switch (hotkeySettings.Action)
                {
                    case Actions.Move:
                        {
                            // Parse destination directory string
                            DirectoryInfo destination = null;
                            string directoryFullName = hotkeySettings.Arguments[0];
                            List<string> arguments = directoryFullName.Split('\\').ToList();
                            if (arguments[0].Equals(".") || arguments[0].Equals(".."))
                            {   // Process a local path
                                if (arguments[0].Equals("."))
                                {
                                    destination = traverser.GetCurrent().Directory;
                                }
                                else if (arguments[0].Equals(".."))
                                {
                                    destination = traverser.GetCurrent().Directory.Parent;
                                }

                                arguments.RemoveAt(0);
                                string secondHalf = "";
                                foreach (string argument in arguments)
                                {   // Walk backward for every \..\
                                    if (argument.Equals(".."))
                                    {
                                        destination = destination.Parent;
                                    }
                                    else
                                    {   // TODO test backslash position
                                        secondHalf += "\\" + argument;
                                    }
                                }
                                // J:\dtm\6.Organize\Folder 1\
                                // ..\..\1.Real\1.Best\tass\images\

                                directoryFullName = destination.FullName + secondHalf;
                            }

                            // Attempt to get the directory info or create the directory
                            if (Directory.Exists(directoryFullName))
                                destination = new DirectoryInfo(directoryFullName);
                            else
                                destination = Directory.CreateDirectory(directoryFullName);
                            if (destination.Exists())
                            {
                                UndoActions undoAction = new UndoActions();
                                undoAction.Arguments.Add(traverser.Index.ToString());                                   // Current index
                                undoAction.Arguments.Add(traverser.GetCurrent().FullName);                              // From
                                undoAction.Arguments.Add(destination.FullName + "\\" + traverser.GetCurrent().Name);    // To

                                // Move the file
                                File.Move(traverser.GetCurrent().FullName, destination.FullName + "\\" + traverser.GetCurrent().Name);

                                // Tell traverser to remove the file and get the new item that has taken its place
                                pictureBox.ImageLocation = traverser?.RemoveCurrent()?.FullName ?? "";

                                // Add the action to the undo history
                                undoHistory.Push(undoAction);
                            }
                            break;
                        }
                    case Actions.Copy:
                        {
                            Clipboard.Clear();
                            Clipboard.SetFileDropList(new StringCollection() { pictureBox.ImageLocation });
                            break;
                        }
                    case Actions.CopyData:
                        {
                            // TODO
                            break;
                        }
                    case Actions.Cut:
                        {
                            byte[] moveEffect = new byte[] { 2, 0, 0, 0 };
                        MemoryStream dropEffect = new MemoryStream();
                        dropEffect.Write(moveEffect, 0, moveEffect.Length);

                        DataObject data = new DataObject();
                        data.SetFileDropList(new StringCollection() { pictureBox.ImageLocation });
                        data.SetData("Preferred DropEffect", dropEffect);

                        Clipboard.Clear();
                        Clipboard.SetDataObject(data, true);

                        // TODO Implement pasting... ugh
                        // TODO Remove the file, (how to know it's left? monitor the file until it moves?)
                        break;
                        }
                    case Actions.Undo:
                        {
                            // TODO (create undo stack and undo reactions)
                            if (undoHistory.Count > 0)
                            {

                            }
                            break;
                        }
                    case Actions.SelectAll:
                        {
                            // TODO (includes adding functionality to Move/Copy/Cut/Delete)
                            break;
                        }
                    case Actions.SelectAllPrevious:
                        {
                            // TODO
                            break;
                        }
                    case Actions.SelectAllFollowing:
                        {
                            // TODO
                            break;
                        }
                    case Actions.Delete:
                        {
                            if (((Microsoft.VisualBasic.FileIO.RecycleOption)hotkeySettings.Tag).Equals(Microsoft.VisualBasic.FileIO.RecycleOption.SendToRecycleBin))
                            {
                                UndoActions undoAction = new UndoActions();
                                undoAction.Arguments.Add(traverser.Index.ToString());
                                undoAction.Arguments.Add(traverser.GetCurrent().FullName);
                                undoAction.Arguments.Add(""); // TODO get recycled path...
                                //undoHistory.Push(undoAction);
                            }
                            Microsoft.VisualBasic.FileIO.FileSystem.DeleteFile(pictureBox.ImageLocation, 
                                Microsoft.VisualBasic.FileIO.UIOption.OnlyErrorDialogs, 
                                (Microsoft.VisualBasic.FileIO.RecycleOption)hotkeySettings.Tag);
                            pictureBox.ImageLocation = traverser?.RemoveCurrent()?.Name ?? "";
                            break;
                        }
                    case Actions.FullScreen:
                        {
                            if (fullScreen.Visible)
                            {
                                fullScreen.UpdatePictureBox();
                                fullScreen.Show();
                            }
                            else
                            {
                                fullScreen.Hide();
                            }
                            break;
                        }
                    case Actions.NextFile:
                        {
                            Next();
                            break;
                        }
                    case Actions.PreviousFile:
                        {
                            Previous();
                            break;
                        }
                    case Actions.Skip:
                        {
                            for (int i = 0; i < int.Parse(hotkeySettings.Arguments[0]); i++)
                                Next();
                            break;
                        }
                    case Actions.UpDirectory:
                        {
                            // Can't be root or favorites folder
                            if (folderBrowser.SelectedNode.Parent.Exists() && !folderBrowser.SelectedNode.Parent.Tag.Exists())
                                folderBrowser.SelectedNode = folderBrowser.SelectedNode.Parent;
                            break;
                        }
                    case Actions.PreviousDirectory:
                        {
                            folderBrowser.SelectedNode = folderBrowser.SelectedNode.PrevNode;
                            break;
                        }
                    case Actions.NextDirectory:
                        {
                            folderBrowser.SelectedNode = folderBrowser.SelectedNode.NextNode;
                            break;
                        }
                    case Actions.Rename:
                        {
                            UndoActions undoAction = new UndoActions();
                            undoAction.Arguments.Add(traverser.Index.ToString());
                            undoAction.Arguments.Add(traverser.GetCurrent().FullName);

                            Rename rename = new Rename(this);
                            rename.ShowDialog();    // Don't allow interaction with parent until this dialog closes (prevent changing selection while editing)

                            undoAction.Arguments.Add(""); // TODO add renamed file (this could have moved to another folder potentially, unless I want to prevent that)
                            //undoHistory.Push(undoAction);
                            break;
                        }
                    case Actions.Open:
                        {
                            Process myProcess = new Process();
                            myProcess.StartInfo.FileName = hotkeySettings.Arguments[0];
                            myProcess.StartInfo.Arguments = pictureBox.ImageLocation;
                            myProcess.Start();
                            break;
                        }
                    case Actions.Mode:
                        {
                            SetMode((Modes)hotkeySettings.Tag);
                            break;
                        }
                    default:
                        {
                            break;
                        }
                }
            }
        }

        private void Previous()
        {
            pictureBox.ImageLocation = traverser?.MovePrevious()?.FullName ?? "";
            UpdateStatusStrip();
        }

        private void Next()
        {
            pictureBox.ImageLocation = traverser?.MoveNext()?.FullName ?? "";
            UpdateStatusStrip();
        }

        private void UpdateStatusStrip()
        {
            statusLabel_Index.Text = traverser?.Index.ToString() ?? "0" + "/" + traverser?.Count ?? "0";
        }

        private void FolderBrowser_MouseClick(object sender, MouseEventArgs e)
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

        public void PictureBox_MouseClick(object sender, MouseEventArgs e)
        {
            CheckXButtons(e);
        }

        private void PictureBox_MouseEnter(object sender, EventArgs e)
        {
            if (!pictureBox.Focused && !settings.Visible)
            {
                pictureBox.Focus();
            }
        }

        private void PictureBox_DoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button.Equals(MouseButtons.Left))
            {
                fullScreen.UpdatePictureBox();
                fullScreen.Show();
            }
        }

        private void SettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            settings.Show();
        }
    }
}
