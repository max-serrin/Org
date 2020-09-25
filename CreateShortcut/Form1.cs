using IWshRuntimeLibrary;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Collections.Specialized;

namespace CreateShortcut
{
    public partial class Form1 : Form
    {
        WshShell shell = new WshShell();
        IWshShell_Class wsh = new IWshShell_Class();

        private List<Func<string, string, bool>> fileOperations;
        private Dictionary<Keys, Button> optionShortcuts = new Dictionary<Keys, Button>();
        private Button addOptionButton;
        private StringCollection options;
        private int optionHeight = 26;
        private int optionWidthOffset = 7;
        private string createShortcutButtonText = "Create";
        private int createShortcutButtonWidth = 75;
        private string editOptionButtonText = "...";
        private int editOptionButtonWidth = 50;
        private int textBoxWidthOffset = 175;

        private List<RadioButton> copyTypeRadioButtons;
        private int selectedCopyTypeIndex = 0;

        public Form1(string imagePath)
        {
            InitializeComponent();
            fileNameTextBox.Text = imagePath;
            fileOperations = new List<Func<string, string, bool>>
            {
                (fp, tp) => CreateShortcut(fp, tp),
                (fp, tp) => CopyFile(fp, tp),
                (fp, tp) => MoveFile(fp, tp),
            };
            InitializeOptions();
            InitializeCopyTypeRadioButtons();
        }

        private void InitializeOptions()
        {
            addOptionButton.Location = new Point(3, 3);
            addOptionButton.Name = "addOptionButton";
            addOptionButton.Size = new Size(22, 23);
            addOptionButton.TabIndex = 0;
            addOptionButton.Text = "+";
            addOptionButton.UseVisualStyleBackColor = true;
            addOptionButton.Click += new EventHandler(addOptionButton_Click);

            options = Properties.Settings.Default.Options ?? new StringCollection();

            foreach (var option in options)
            {
                var optionSplit = option.Split('|');
                if (optionSplit.Length > 1)
                {
                    optionsLayoutPanel.Controls.Add(CreateOption(optionSplit[0], optionSplit[1]));
                }
                else
                {
                    optionsLayoutPanel.Controls.Add(CreateOption(optionSplit[0]));
                }
            }
            optionsLayoutPanel.Controls.Add(addOptionButton);
        }

        private void InitializeCopyTypeRadioButtons()
        {
            copyTypeRadioButtons = new List<RadioButton>();

            var createShortcutRadioButton = new RadioButton();
            createShortcutRadioButton.Text = "Create Shortcut";
            createShortcutRadioButton.CheckedChanged += (o, e) => copyTypeRadio_CheckedChanged(createShortcutRadioButton);
            copyTypeRadioButtons.Add(createShortcutRadioButton);

            var copyFileRadioButton = new RadioButton();
            copyFileRadioButton.Text = "Copy File";
            copyFileRadioButton.CheckedChanged += (o, e) => copyTypeRadio_CheckedChanged(copyFileRadioButton);
            copyTypeRadioButtons.Add(copyFileRadioButton);

            var moveFileRadioButton = new RadioButton();
            moveFileRadioButton.Text = "Move File";
            moveFileRadioButton.CheckedChanged += (o, e) => copyTypeRadio_CheckedChanged(moveFileRadioButton);
            copyTypeRadioButtons.Add(moveFileRadioButton);

            copyTypeRadioButtons[selectedCopyTypeIndex].Checked = true;
            copyTypeFlowLayout.Controls.AddRange(copyTypeRadioButtons.ToArray());
        }

        private void addOptionButton_Click(object sender, EventArgs e)
        {
            optionsLayoutPanel.Controls.Remove(addOptionButton);
            optionsLayoutPanel.Controls.Add(CreateOption());
            optionsLayoutPanel.Controls.Add(addOptionButton);
        }

        private FlowLayoutPanel CreateOption(string text = "", string tag = "")
        {
            var option = new FlowLayoutPanel
            {
                Width = optionsLayoutPanel.Width - optionWidthOffset,
                Height = optionHeight
            };

            var optionButton = new Button
            {
                Text = string.IsNullOrEmpty(tag) ? createShortcutButtonText : tag,
                Width = createShortcutButtonWidth
            };
            option.Controls.Add(optionButton);

            var optionTextBox = new TextBox
            {
                Text = text,
                Width = optionsLayoutPanel.Width - textBoxWidthOffset,
                Tag = tag
            };
            option.Controls.Add(optionTextBox);

            var editOptionButton = new Button
            {
                Text = editOptionButtonText,
                Width = editOptionButtonWidth
            };
            option.Controls.Add(editOptionButton);

            var menu = new ContextMenuStrip();
            var changeHotkeyMenuItem = menu.Items.Add("Change Shortcut");
            var deleteOptionMenuItem = menu.Items.Add("Delete");
            changeHotkeyMenuItem.Click += (o, e) => {
                var setShortcutForm = new SetShortcut();
                Keys shortcut = 0;
                setShortcutForm.FormClosing += (oo, ee) => shortcut = setShortcutForm.Shortcut;
                var result = setShortcutForm.ShowDialog();
                if (result == DialogResult.OK)
                {
                    if (!string.IsNullOrEmpty(tag))
                    {
                        optionShortcuts.Remove((Keys)Enum.Parse(typeof(Keys), tag, true));
                    }
                    tag = shortcut.ToString();

                    if (shortcut == 0)
                    {
                        optionButton.Text = string.Empty;
                        optionTextBox.Tag = string.Empty;
                    }
                    else
                    {
                        optionButton.Text = shortcut.ToString();
                        optionTextBox.Tag = shortcut.ToString();
                        optionShortcuts.Add(shortcut, optionButton);
                    }
                    SaveOptions();
                }
            };
            deleteOptionMenuItem.Click += (o, e) => {
                optionsLayoutPanel.Controls.Remove(option);
                SaveOptions();
            };

            optionButton.Click += (o, e) =>
            {
                if (fileOperations[selectedCopyTypeIndex](fileNameTextBox.Text, optionTextBox.Text)) Close();
            };
            optionTextBox.TextChanged += (o, e) => SaveOptions();
            editOptionButton.Click += (o, e) =>
                menu.Show(option.PointToScreen(new Point(editOptionButton.Location.X, editOptionButton.Location.Y + editOptionButton.Height)));
            if (!string.IsNullOrEmpty(tag))
            {
                optionShortcuts.Add((Keys)Enum.Parse(typeof(Keys), tag, true), optionButton);
            }

            return option;
        }

        private bool CreateShortcut(string filePath, string targetPath)
        {
            if (string.IsNullOrEmpty(filePath) || string.IsNullOrEmpty(targetPath)) return false;
            if (!System.IO.File.Exists(filePath))
            {
                if (!Directory.Exists(filePath)) return false;

                return CreateDirectoryShortcut(filePath, targetPath);
            }

            try
            {
                var file = new FileInfo(filePath);
                CreateDirectoryIfNotExist(targetPath);

                // Create empty .lnk file
                var fileName = string.IsNullOrEmpty(targetFileNameTextBox.Text) ? file.Name : targetFileNameTextBox.Text;
                var shortcutName = fileName + ".lnk";
                string path = Path.Combine(targetPath, shortcutName);
                System.IO.File.WriteAllBytes(path, new byte[0]);

                // Create a ShellLinkObject that references the .lnk file
                Shell32.Shell shl = new Shell32.Shell();
                Shell32.Folder dir = shl.NameSpace(targetPath);
                Shell32.FolderItem itm = dir.Items().Item(shortcutName);
                Shell32.ShellLinkObject lnk = (Shell32.ShellLinkObject)itm.GetLink;

                // Set the .lnk file properties
                lnk.Path = file.FullName;
                lnk.Save(path);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }

            return true;
        }

        private bool CopyFile(string filePath, string targetPath)
        {
            if (string.IsNullOrEmpty(filePath) || string.IsNullOrEmpty(targetPath)) return false;
            if (!System.IO.File.Exists(filePath))
            {
                if (!Directory.Exists(filePath)) return false;

                return CopyDirectory(filePath, targetPath);
            }

            try
            {
                var file = new FileInfo(filePath);
                CreateDirectoryIfNotExist(targetPath);
                var fileName = string.IsNullOrEmpty(targetFileNameTextBox.Text) ? file.Name : targetFileNameTextBox.Text;

                string path = Path.Combine(targetPath, fileName);
                System.IO.File.Copy(file.FullName, path);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }

            return true;
        }

        private bool MoveFile(string filePath, string targetPath)
        {
            if (string.IsNullOrEmpty(filePath) || string.IsNullOrEmpty(targetPath)) return false;
            if (!System.IO.File.Exists(filePath))
            {
                if (!Directory.Exists(filePath)) return false;

                return MoveDirectory(filePath, targetPath);
            }

            try
            {
                var file = new FileInfo(filePath);
                CreateDirectoryIfNotExist(targetPath);
                var fileName = string.IsNullOrEmpty(targetFileNameTextBox.Text) ? file.Name : targetFileNameTextBox.Text;

                string path = Path.Combine(targetPath, fileName);
                System.IO.File.Move(file.FullName, path);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }

            return true;
        }

        private bool CreateDirectoryShortcut(string dirPath, string targetPath)
        {
            if (string.IsNullOrEmpty(dirPath) || string.IsNullOrEmpty(targetPath)) return false;
            if (!Directory.Exists(dirPath)) return false;

            try
            {
                var di = new DirectoryInfo(dirPath);
                CreateDirectoryIfNotExist(targetPath);

                // Create empty .lnk file
                var dirName = di.Name;
                var shortcutName = dirName + ".lnk";
                string path = Path.Combine(targetPath, shortcutName);
                System.IO.File.WriteAllBytes(path, new byte[0]);

                // Create a ShellLinkObject that references the .lnk file
                Shell32.Shell shl = new Shell32.Shell();
                Shell32.Folder dir = shl.NameSpace(targetPath);
                Shell32.FolderItem itm = dir.Items().Item(shortcutName);
                Shell32.ShellLinkObject lnk = (Shell32.ShellLinkObject)itm.GetLink;

                // Set the .lnk file properties
                lnk.Path = di.FullName;
                lnk.Save(path);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }

            return true;
        }

        private bool CopyDirectory(string dirPath, string targetPath)
        {
            if (string.IsNullOrEmpty(dirPath) || string.IsNullOrEmpty(targetPath)) return false;
            if (!Directory.Exists(dirPath)) return false;

            // TODO
            MessageBox.Show("Copy Directory not implemented.");
            return false;
        }

        private bool MoveDirectory(string dirPath, string targetPath)
        {
            if (string.IsNullOrEmpty(dirPath) || string.IsNullOrEmpty(targetPath)) return false;
            if (!Directory.Exists(dirPath)) return false;

            try
            {
                var dir = new DirectoryInfo(dirPath);
                CreateDirectoryIfNotExist(targetPath);
                var dirName = dir.Name;

                string path = Path.Combine(targetPath, dirName);
                Directory.Move(dir.FullName, path);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }

            return true;
        }

        private void CreateDirectoryIfNotExist(string path)
        {
            if (Directory.Exists(path)) return;

            Directory.CreateDirectory(path);
        }

        private void SaveOptions()
        {
            var options = new StringCollection();
            foreach (var control in optionsLayoutPanel.Controls)
            {
                if (control is FlowLayoutPanel)
                {
                    var _control = (FlowLayoutPanel)control;
                    var textBox = _control.Controls[1];
                    if (!string.IsNullOrEmpty(textBox.Text))
                    {
                        options.Add($"{textBox.Text}|{textBox.Tag}");
                    }
                }
            }

            Properties.Settings.Default.Options = options;
            Properties.Settings.Default.Save();
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (optionShortcuts.ContainsKey(keyData))
            {
                optionShortcuts[keyData].PerformClick();
                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData); ;
        }

        private void optionsLayoutPanel_Resize(object sender, EventArgs e)
        {
            var _sender = (FlowLayoutPanel)sender;
            foreach(var control in _sender.Controls)
            {
                if (control is FlowLayoutPanel)
                {
                    var _control = (FlowLayoutPanel)control;
                    _control.Width = _sender.Width - optionWidthOffset;
                    var textBox = _control.Controls[1];
                    textBox.Width = _sender.Width - textBoxWidthOffset;
                }
            }
        }

        private void copyTypeRadio_CheckedChanged(RadioButton radioButton)
        {
            if (!radioButton.Checked) return;

            selectedCopyTypeIndex = copyTypeRadioButtons.IndexOf(radioButton);
        }

        private void Form1_Click(object sender, EventArgs e)
        {
            Focus();
        }

        private void fileNameTextBox_TextChanged(object sender, EventArgs e)
        {
            if (System.IO.File.Exists(fileNameTextBox.Text))
            {
                var file = new FileInfo(fileNameTextBox.Text);
                targetFileNameTextBox.Text = $"【{file.Directory.Name}】 {file.Name}";
            }
        }
    }
}
