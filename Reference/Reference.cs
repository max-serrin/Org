using System;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using GetRandomImage;
using System.Collections.Specialized;
using MyExtensions;
using System.Linq;
using System.Collections.Generic;
using NLog;
using Shell32;
using System.ComponentModel;

namespace Reference
{
    public partial class Reference : Form
    {
        // Logging
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        private const string SHORTCUT_EXTENSION = ".lnk";

        // For dragging and drag drop
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        // Make edges resizable
        private const int
            HTLEFT = 10,
            HTRIGHT = 11,
            HTTOP = 12,
            HTTOPLEFT = 13,
            HTTOPRIGHT = 14,
            HTBOTTOM = 15,
            HTBOTTOMLEFT = 16,
            HTBOTTOMRIGHT = 17;

        const int _ = 10;

        private Rectangle TopRectangle => new Rectangle(0, 0, ClientSize.Width, _);
        private Rectangle LeftRectangle => new Rectangle(0, 0, _, ClientSize.Height);
        private Rectangle BottomRectangle => new Rectangle(0, ClientSize.Height - _, ClientSize.Width, _);
        private Rectangle RightRectangle => new Rectangle(ClientSize.Width - _, 0, _, ClientSize.Height);

        private Rectangle TopLeftRectangle => new Rectangle(0, 0, _, _);
        private Rectangle TopRightRectangle => new Rectangle(ClientSize.Width - _, 0, _, _);
        private Rectangle BottomLeftRectangle => new Rectangle(0, ClientSize.Height - _, _, _);
        private Rectangle BottomRightRectangle => new Rectangle(ClientSize.Width - _, ClientSize.Height - _, _, _);

        protected override void WndProc(ref Message message)
        {
            base.WndProc(ref message);

            if (message.Msg == 0x84) // WM_NCHITTEST
            {
                var cursor = pictureBox1.PointToClient(Cursor.Position);

                if (TopLeftRectangle.Contains(cursor)) message.Result = (IntPtr)HTTOPLEFT;
                else if (TopRightRectangle.Contains(cursor)) message.Result = (IntPtr)HTTOPRIGHT;
                else if (BottomLeftRectangle.Contains(cursor)) message.Result = (IntPtr)HTBOTTOMLEFT;
                else if (BottomRightRectangle.Contains(cursor)) message.Result = (IntPtr)HTBOTTOMRIGHT;

                else if (TopRectangle.Contains(cursor)) message.Result = (IntPtr)HTTOP;
                else if (LeftRectangle.Contains(cursor)) message.Result = (IntPtr)HTLEFT;
                else if (RightRectangle.Contains(cursor)) message.Result = (IntPtr)HTRIGHT;
                else if (BottomRectangle.Contains(cursor)) message.Result = (IntPtr)HTBOTTOM;
            }
        }

        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        // Global file info and bitmap (TODO these don't need to be global)
        FileInfo im;

        // Constants
        private const string TIMER_HEADER = "Time: ";        // Timer string constants
        private const string TIMER_DIVIDER = ":";
        private const string TIMER_PAUSED = " (Paused)";
        public const int MIN_WIDTH = 25;                    // Minimum width and height
        public const int MIN_HEIGHT = 25;

        // Global variables
        public string folderPath, fileListPath, fileList;   // Directory to retrieve images

        public int maxWidth, maxHeight;                     // Window cannot exceed these dimensions
        public int hh, mm, ss, maxhh, maxmm, maxss, timerPadding, timerBuffer;  // Timer variables, padding (H:M:S vs HH:MM:SS) and buffer (how many seconds to wait before reseting the timer)
        public Timer t;                                     // Timer to control countdown
        public bool copy;                                // Whether to copy or cut an image
        public bool onTop;                               // Whether the window will remain on top of other windows or not
        public bool searchAll;                           // Whther to search all directories or just the top directory

        private ContextMenu cm;                             // The right click menu and items
        private MenuItem timerMenuItem;
        private MenuItem nextMenuItem;
        private MenuItem lastMenuItem;
        private MenuItem refreshMenuItem;
        private MenuItem fileMenuItem;
        private MenuItem directoryMenuItem;
        private MenuItem settingsMenuItem;
        private MenuItem fullscreenMenuItem;
        private MenuItem exitMenuItem;

        private RandomImageList ril;                        // Random image class

        private bool dragging = false;

        public Reference()
        {
            ReferenceInit();
        }

        /// <summary>
        /// Initialization
        /// </summary>
        public Reference(FileInfo image)
        {
            ReferenceInit();

            // Images
            if (!ObjectExtensions.Exists(image)) return;

            pictureBox1.ImageLocation = image.FullName;
            folderPath = image.DirectoryName;
        }

        public Reference(string filePath)
        {
            ReferenceInit();

            if (string.IsNullOrEmpty(filePath)) return;

            if (!File.Exists(filePath)) return;

            var file = new FileInfo(filePath);
            if (file.Extension == ".txt")
            {
                folderPath = null;
                var fs = File.ReadLines(file.FullName).ToList();
                fs.RemoveAll(string.IsNullOrWhiteSpace);
                var fi = fs.Select(f => new FileInfo(f.Trim())).ToList();
                ril.Reload(new List<FileInfo>(fi));
                if (t.Enabled)
                    TimerToggle(this, null);
                ResetTimer();
                SetNextImage();
            }
            else if (ril.ext.Contains(file.Extension))
            {
                pictureBox1.ImageLocation = file.FullName;
                folderPath = file.DirectoryName;
            }
        }

        public Reference(string[] filePaths)
        {
            ReferenceInit();

            var ext = new string[] { ".jpg", ".jpeg", ".gif", ".png", ".bmp" };
            var extSearch = new string[] { "*.jpg", "*.jpeg", "*.gif", "*.png", "*.bmp" };

            if (filePaths is null || filePaths.Count() == 0) return;

            var fileList = new List<FileInfo>();

            folderPath = null;
            foreach (var filePath in filePaths)
            {
                if (!File.Exists(filePath) && !Directory.Exists(filePath)) continue;

                FileAttributes attr = File.GetAttributes(filePath);

                if ((attr & FileAttributes.Directory) == FileAttributes.Directory)
                {
                    var directoryInfo = new DirectoryInfo(filePath);
                    fileList.AddRange(extSearch.SelectMany(e => (directoryInfo.EnumerateFiles(e, SearchOption.AllDirectories))));
                }
                else
                {
                    var file = new FileInfo(filePath);

                    if (file.Extension == ".txt")
                    {
                        var fs = File.ReadLines(file.FullName).ToList();
                        fs.RemoveAll(string.IsNullOrWhiteSpace);
                        var fi = fs.Select(f => new FileInfo(f.Trim())).ToList();
                        fileList.AddRange(fi);
                    }
                    else if (ext.Contains(file.Extension))
                    {
                        fileList.Add(file);
                    }
                }
            }
            
            ril.Reload(new List<FileInfo>(fileList));
            if (t.Enabled)
                TimerToggle(this, null);
            ResetTimer();
            SetNextImage();
        }

        private void ReferenceInit()
        {
            var config = new NLog.Config.LoggingConfiguration();

            // Targets where to log to: File and Console
            var logfile = new NLog.Targets.FileTarget("logfile") { FileName = "log.txt" };
            var logconsole = new NLog.Targets.ConsoleTarget("logconsole");

            // Rules for mapping loggers to targets            
            config.AddRule(LogLevel.Info, LogLevel.Fatal, logconsole);
            config.AddRule(LogLevel.Debug, LogLevel.Fatal, logfile);

            // Apply config           
            LogManager.Configuration = config;

            Logger.Info("Start");

            InitializeComponent();

            FormBorderStyle = FormBorderStyle.None;
            DoubleBuffered = true;
            SetStyle(ControlStyles.ResizeRedraw, true);

            // Image
            im = null;
            pictureBox1.ImageLocation = "";
            folderPath = "";

            // Window
            maxWidth = Properties.Settings.Default.maxWidth;
            maxHeight = Properties.Settings.Default.maxHeight;
            onTop = Properties.Settings.Default.onTop;

            fileListPath = Properties.Settings.Default.fileListPath;
            fileList = Properties.Settings.Default.fileList;


            searchAll = Properties.Settings.Default.searchAll;
            ril = new RandomImageList(folderPath, searchAll)
            {
                ext = Properties.Settings.Default.ext?.Cast<string>().ToList(),
                tagWhitelist = Properties.Settings.Default.whitelist?.Cast<string>().ToList(),
                tagBlacklist = Properties.Settings.Default.blacklist?.Cast<string>().ToList(),
                useWhitelist = Properties.Settings.Default.useWhitelist,
                useBlacklist = Properties.Settings.Default.useBlacklist,
                fileNameFilter = Properties.Settings.Default.fileNameFilter
            };
            copy = Properties.Settings.Default.copy;

            // Timer
            maxhh = Properties.Settings.Default.maxhh;
            maxmm = Properties.Settings.Default.maxmm;
            maxss = Properties.Settings.Default.maxss;
            timerPadding = 2;
            timerBuffer = Properties.Settings.Default.timerBuffer;
            ResetTimer();

            t = new Timer
            {
                Interval = 1000
            };
            t.Tick += new EventHandler(TimerTick);

            // Context Menu
            cm = new ContextMenu();
            timerMenuItem = cm.MenuItems.Add(CreateTimerStringFull(hh, mm, ss, timerPadding));
            timerMenuItem.Click += new EventHandler(TimerToggle);

            nextMenuItem = cm.MenuItems.Add("Next Image");
            nextMenuItem.Click += new EventHandler(NextImage);

            lastMenuItem = cm.MenuItems.Add("Last Image");
            lastMenuItem.Click += new EventHandler(LastImage);

            refreshMenuItem = cm.MenuItems.Add("Refresh Image");
            refreshMenuItem.Click += new EventHandler(RefreshImage);

            fileMenuItem = cm.MenuItems.Add(im == null ? "" : im.Name);
            fileMenuItem.Click += new EventHandler(FileHandle);

            directoryMenuItem = cm.MenuItems.Add(im == null ? "" : im.DirectoryName);
            directoryMenuItem.Click += new EventHandler(DirectoryHandle);

            nextMenuItem.Visible = false;
            lastMenuItem.Visible = false;
            fileMenuItem.Visible = false;
            directoryMenuItem.Visible = false;

            settingsMenuItem = cm.MenuItems.Add("Settings");
            settingsMenuItem.Click += new EventHandler(OpenSettings);

            fullscreenMenuItem = cm.MenuItems.Add("Fullscreen");
            fullscreenMenuItem.Click += new EventHandler(ToggleFullscreen);

            cm.MenuItems.Add("-");

            exitMenuItem = cm.MenuItems.Add("Exit");
            exitMenuItem.Click += new EventHandler(FormExit_Click);

            ContextMenu = cm;
            AllowDrop = true;
        }

        /// <summary>
        /// Toggle the form's window state between fullscreen and normal.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToggleFullscreen(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Maximized)
            {
                WindowState = FormWindowState.Normal;
                fullscreenMenuItem.Text = "Fullscreen";
                ttPicturebox1.Active = true;
            } else
            {
                WindowState = FormWindowState.Maximized;
                fullscreenMenuItem.Text = "Exit Fullscreen";
                ttPicturebox1.Active = false;
            }
        }

        /// <summary>
        /// Go back an image in the image list.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LastImage(object sender, EventArgs e)
        {
            SetLastImage();
            ResetTimer();
        }

        /// <summary>
        /// Go forward and image in the image list.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NextImage(object sender, EventArgs e)
        {
            SetNextImage();
            ResetTimer();
        }

        /// <summary>
        /// Refresh the current image.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RefreshImage(object sender, EventArgs e)
        {
            RefreshCurrentImage();
            ResetTimer();
        }

        /// <summary>
        /// Open explorer.exe to the image's directory and focus on the image.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DirectoryHandle(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("explorer.exe", string.Format("/select,\"{0}\"", im.FullName));
        }

        /// <summary>
        /// Copy or cut the file. Cut will not work until the program is closed (TODO, don't know how to fix).
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FileHandle(object sender, EventArgs e)
        {
            StringCollection sc = new StringCollection
            {
                im.FullName
            };

            if (copy)
                Clipboard.SetFileDropList(sc);
            else
            {
                byte[] moveEffect = new byte[] { 2, 0, 0, 0 };
                MemoryStream dropEffect = new MemoryStream();
                dropEffect.Write(moveEffect, 0, moveEffect.Length);

                DataObject data = new DataObject();
                data.SetFileDropList(sc);
                data.SetData("Preferred DropEffect", dropEffect);

                Clipboard.Clear();
                Clipboard.SetDataObject(data, true);
            }
        }

        /// <summary>
        /// Starts or stops the timer. Will not function if there is no image loaded.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TimerToggle(object sender, EventArgs e)
        {
            if (pictureBox1.Image != null)
            {
                if (t.Enabled)
                    t.Stop();
                else
                    t.Start();
                timerMenuItem.Text = CreateTimerStringFull(hh, mm, ss, timerPadding);
            }
        }

        /// <summary>
        /// Creates a full timer string with prefix and suffix. Time: HH:MM:SS [(Paused)]
        /// </summary>
        /// <param name="hh"></param>
        /// <param name="mm"></param>
        /// <param name="ss"></param>
        /// <param name="padding"></param>
        /// <returns></returns>
        public string CreateTimerStringFull(int hh, int mm, int ss, int padding)
        {
            string ret = TIMER_HEADER + CreateTimerString(hh, mm, ss, padding);
                
            if (!t.Enabled) ret += TIMER_PAUSED;
            return ret;
        }

        /// <summary>
        /// Creates a timer string padding of 1 will create H:M:S, padding of 2 will create HH:MM:SS. A padding of 3 or more isn't used but will work.
        /// </summary>
        /// <param name="hh"></param>
        /// <param name="mm"></param>
        /// <param name="ss"></param>
        /// <param name="padding"></param>
        /// <returns></returns>
        public string CreateTimerString(int hh, int mm, int ss, int padding)
        {
            return hh.ToString().PadLeft(padding, '0') + TIMER_DIVIDER +
                mm.ToString().PadLeft(padding, '0') + TIMER_DIVIDER +
                ss.ToString().PadLeft(padding, '0');
        }

        /// <summary>
        /// Decreases the timer until -1 seconds.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TimerTick(object sender, EventArgs e)
        {
            if (--ss < 0)
            {
                if (--mm < 0)
                {
                    if (--hh < 0)
                        TimerEnd();
                    else mm = 59;
                }
                else ss = 59;
            }
            timerMenuItem.Text = CreateTimerStringFull(hh, mm, ss, timerPadding);
        }

        /// <summary>
        /// Clears the image between -1 and -timerBuffer seconds. 
        /// Resets the timer and advances to the next image once the timer hits -timerBuffer seconds.
        /// </summary>
        private void TimerEnd()
        {
            if (ss < -timerBuffer)
            {
                ResetTimer();
                SetNextImage();
            }
            else
                ClearScreen();
        }

        /// <summary>
        /// Removes the displayed image and reduces the window size to minimum.
        /// </summary>
        private void ClearScreen()
        {
            pictureBox1.Image = null;
        }

        /// <summary>
        /// Sets the timer back to maximum.
        /// </summary>
        private void ResetTimer()
        {
            hh = maxhh; mm = maxmm; ss = maxss;
            if  (timerMenuItem != null)
                timerMenuItem.Text = CreateTimerStringFull(hh, mm, ss, timerPadding);
        }

        /// <summary>
        /// Sets the picturebox to display the next image and resizes the window.
        /// </summary>
        private void SetNextImage()
        {
            im = ril?.getNext();
            SetPictureBox(im);
        }

        /// <summary>
        /// Sets the picturebox to display the last image and resizes the window.
        /// </summary>
        private void SetLastImage()
        {
            im = ril?.getPrevious();
            SetPictureBox(im);
        }

        private void RefreshCurrentImage()
        {
            SetPictureBox(im);
        }

        /// <summary>
        /// If a window drag occurs during fullscreen, the window will return to normal.
        /// Sets the fullscreen MenuItem's text when an outside window change event occurs.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_ResizeEnd(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Maximized)
                fullscreenMenuItem.Text = "Exit Fullscreen";
            else
            {
                fullscreenMenuItem.Text = "Fullscreen";

                if (!dragging)
                {
                    maxHeight = Height;
                    maxWidth = Width;

                    if (pictureBox1.Image != null)
                        SetFormSize(pictureBox1.Image.Size);
                }
            }

            dragging = false;
        }

        private void Reference_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
                SetLastImage();
            else if (e.KeyCode == Keys.Right)
                SetNextImage();
            else if (e.KeyCode == Keys.F5)
                RefreshCurrentImage();
        }

        /// <summary>
        /// Reloads the current directory.
        /// This is used to remove or add in directories within a directory when the option is changed in the settings.
        /// </summary>
        private void ReloadDirectory()
        {
            ril.Reload();
            SetNextImage();
        }

        /// <summary>
        /// Open the settings dialog and save and apply any settings once it closes.
        /// (TODO add a cancel... low priority)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void OpenSettings(object sender, EventArgs args)
        {
            try
            {
                Hide();
                Settings settings = new Settings(
                    this, 
                    maxWidth, 
                    maxHeight, 
                    onTop, 
                    maxhh, 
                    maxmm, 
                    maxss, 
                    timerBuffer, 
                    copy, 
                    searchAll, 
                    ril.ext, 
                    ril.tagWhitelist, 
                    ril.tagBlacklist, 
                    ril.useWhitelist, 
                    ril.useBlacklist, 
                    ril.fileNameFilter, 
                    fileListPath, 
                    fileList);

                settings.ShowDialog();

                if (settings.exitStatus)
                {
                    maxWidth = Math.Max(settings.maxWidth, MIN_WIDTH); maxHeight = Math.Max(settings.maxHeight, MIN_HEIGHT);
                    //SetFormSize();
                    onTop = settings.onTop;
                    TopMost = onTop;
                    maxhh = settings.hh; maxmm = settings.mm; maxss = settings.ss;
                    timerBuffer = settings.timerBuffer;
                    copy = settings.copy;
                    if (searchAll != settings.searchAll)
                    {
                        searchAll = settings.searchAll;
                        if (searchAll)
                            ril.so = SearchOption.AllDirectories;
                        else
                            ril.so = SearchOption.TopDirectoryOnly;
                        ReloadDirectory();
                    }
                    ril.ext = settings.ext;
                    ril.tagWhitelist = settings.whitelist;
                    ril.tagBlacklist = settings.blacklist;
                    ril.useWhitelist = settings.useWhitelist;
                    ril.useBlacklist = settings.useBlacklist;
                    ril.fileNameFilter = settings.fileNameFilter;
                    fileListPath = settings.fileListPath;
                    fileList = settings.fileList;

                    ResetTimer();

                    // Save properties
                    Properties.Settings.Default.maxWidth = maxWidth;
                    Properties.Settings.Default.maxHeight = maxHeight;
                    Properties.Settings.Default.onTop = onTop;
                    Properties.Settings.Default.maxhh = maxhh;
                    Properties.Settings.Default.maxmm = maxmm;
                    Properties.Settings.Default.maxss = maxss;
                    Properties.Settings.Default.timerBuffer = timerBuffer;
                    Properties.Settings.Default.copy = copy;
                    Properties.Settings.Default.searchAll = searchAll;
                    Properties.Settings.Default.ext = ril.ext.ToStringCollection();
                    Properties.Settings.Default.whitelist = ril.tagWhitelist.ToStringCollection();
                    Properties.Settings.Default.blacklist = ril.tagBlacklist.ToStringCollection();
                    Properties.Settings.Default.useWhitelist = ril.useWhitelist;
                    Properties.Settings.Default.useBlacklist = ril.useBlacklist;
                    Properties.Settings.Default.fileNameFilter = ril.fileNameFilter;
                    Properties.Settings.Default.fileListPath = fileListPath;
                    Properties.Settings.Default.fileList = fileList;
                    Properties.Settings.Default.Save();

                    if (fileList.Trim().Length > 2)
                    {
                        var fi = fileList.Split(Environment.NewLine.ToCharArray()).ToList();
                        fi.RemoveAll(string.IsNullOrWhiteSpace);
                        ril.Reload(fi.Select(f => {
                            var ff = new FileInfo(f);
                            if (ff.Extension == SHORTCUT_EXTENSION)
                            {
                                var target = GetShortcutTargetFile(f);
                                return new FileInfo(target);
                            }
                            else
                            {
                                return ff;
                            }
                        }).ToList());
                        SetNextImage();
                    }
                }

                Show();
            }
            catch (Exception e)
            {
                Logger.Error($"{pictureBox1.ImageLocation}|{e.Message}{Environment.NewLine}{e.StackTrace}");
            }
        }

        

        /// <summary>
        /// Closes the form when exit is chosen in the right click menu.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Handles mouse down event for dragging the window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                dragging = true;
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        /// <summary>
        /// Handles drag and drop when a file/directory first enters the window.
        /// (I believe I got this code on Stack Overflow)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PictureBox1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.All;
            else
                e.Effect = DragDropEffects.None;
        }

        /// <summary>
        /// Handles drag and drop when the file/directory is released inside the window.
        /// (I believe I got this code on Stack Overflow)
        /// If it is a directory, load it, get the first (random) image and display it.
        /// If it is a file (TODO doesn't check to make sure it's an image) display it and load the directory.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PictureBox1_DragDrop(object sender, DragEventArgs e)
        {
            string[] s = (string[])e.Data.GetData(DataFormats.FileDrop, false);
            FileAttributes attr = File.GetAttributes(s[0]);
            if ((attr & FileAttributes.Directory) == FileAttributes.Directory)
            {
                folderPath = s[0];
                ril.Reload(folderPath);
                SetNextImage();
            }
            else
            {
                im = new FileInfo(s[0]);
                if (im.Extension == ".txt")
                {
                    folderPath = null;
                    var fs = File.ReadLines(im.FullName).ToList();
                    fs.RemoveAll(string.IsNullOrWhiteSpace);
                    var fi = fs.Select(f => new FileInfo(f.Trim())).ToList();
                    ril.Reload(new List<FileInfo>(fi));
                    if (t.Enabled)
                        TimerToggle(this, null);
                    ResetTimer();
                    SetNextImage();
                }
                else
                {
                    folderPath = im.DirectoryName;
                    ril.Reload(folderPath);
                    if (t.Enabled)                          // Pause and reset the timer if it was enabled
                        TimerToggle(this, null);
                    ResetTimer();
                    SetNextImage();
                }
            }
        }

        /// <summary>
        /// Sets the picturebox image to the location of the given file. 
        /// Also enables the menu items that require a file or folder to be used.
        /// </summary>
        /// <param name="fi"></param>
        private void SetPictureBox(FileInfo fi)
        {
            try
            {
                if (fi != null)
                {
                    if (pictureBox1.Image != null)
                        pictureBox1.Image.Dispose();
                    var image = Image.FromFile(fi.FullName);
                    pictureBox1.Image = image;
                    SetFormSize(image.Size);

                    nextMenuItem.Visible = true;
                    lastMenuItem.Visible = true;
                    fileMenuItem.Visible = true;
                    directoryMenuItem.Visible = true;
                }

                if (fileMenuItem != null)
                    fileMenuItem.Text = fi?.Name ?? "";
                directoryMenuItem.Text = fi?.DirectoryName ?? "";
            }
            catch (Exception e)
            {
                Logger.Error($"{pictureBox1.ImageLocation}|{e.Message}{Environment.NewLine}{e.StackTrace}");
            }
        }

        /// <summary>
        /// Resizes the window to best fit the image without going over maximum height or width.
        /// </summary>
        private void SetFormSize(Size size)
        {
            if (size == null) return;

            try
            {
                double ratio;
                if (size.Width > maxWidth || size.Height > maxHeight)
                {
                    ratio = Math.Max((double)size.Width / maxWidth, (double)size.Height / maxHeight);
                    Width = (int)Math.Max(MIN_WIDTH, size.Width / ratio);
                    Height = (int)Math.Max(MIN_HEIGHT, size.Height / ratio);
                }
                else if (size.Width <= maxWidth && size.Height <= maxHeight)
                {
                    Width = size.Width;
                    Height = size.Height;
                }
            }
            catch (Exception e)
            {
                Logger.Error($"{pictureBox1.ImageLocation}|{e.Message}{Environment.NewLine}{e.StackTrace}");
            }
        }

        public void PictureBox1_Click(object sender, EventArgs e)
        {
            if (((MouseEventArgs)e).Button == MouseButtons.XButton2)
                SetNextImage();
            else if (((MouseEventArgs)e).Button == MouseButtons.XButton1)
                SetLastImage();
        }

        private void PictureBox1_MouseWheel(object sender, MouseEventArgs e)
        {
            if (e.Delta > 0)
                SetLastImage();
            else if (e.Delta < 0)
                SetNextImage();
        }

        public static string GetShortcutTargetFile(string shortcutFilename)
        {
            string pathOnly = System.IO.Path.GetDirectoryName(shortcutFilename);
            string filenameOnly = System.IO.Path.GetFileName(shortcutFilename);

            Shell shell = new Shell();
            Folder folder = shell.NameSpace(pathOnly);
            FolderItem folderItem = folder.ParseName(filenameOnly);
            if (folderItem != null)
            {
                Shell32.ShellLinkObject link = (Shell32.ShellLinkObject)folderItem.GetLink;
                return link.Path;
            }

            return string.Empty;
        }
    }

    public static class IEnumerableStringExtensions
    {
        public static StringCollection ToStringCollection(this IEnumerable<string> strings)
        {
            var stringCollection = new StringCollection();
            foreach (string s in strings)
                stringCollection.Add(s);
            return stringCollection;
        }
    }

    public class PictureBox : System.Windows.Forms.PictureBox
    {
        #region Members

        private System.Windows.Forms.PictureBox PicBox;
        private Panel OuterPanel;
        private Container components = null;
        private string m_sPicName = "";

        #endregion

        #region Constants

        private double ZOOMFACTOR = 1.25;   // = 25% smaller or larger
        private int MINMAX = 5;             // 5 times bigger or smaller than the ctrl

        #endregion

        protected override void WndProc(ref Message m)
        {
            const int WM_NCHITTEST = 0x0084;
            const int HTTRANSPARENT = (-1);

            if (m.Msg == WM_NCHITTEST)
            {
                m.Result = (IntPtr)HTTRANSPARENT;
            }
            else
            {
                base.WndProc(ref m);
            }
        }

        #region Designer generated code

        private void InitializeComponent()
        {
            this.PicBox = new System.Windows.Forms.PictureBox();
            this.OuterPanel = new System.Windows.Forms.Panel();
            this.OuterPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // PicBox
            // 
            //this.PicBox.Location = new System.Drawing.Point(0, 0);
            //this.PicBox.Name = "PicBox";
            //this.PicBox.Size = new System.Drawing.Size(150, 140);
            //this.PicBox.TabIndex = 3;
            //this.PicBox.TabStop = false;
            // 
            // OuterPanel
            // 
            this.OuterPanel.AutoScroll = true;
            this.OuterPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.OuterPanel.Controls.Add(this.PicBox);
            this.OuterPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.OuterPanel.Location = new System.Drawing.Point(0, 0);
            this.OuterPanel.Name = "OuterPanel";
            this.OuterPanel.Size = new System.Drawing.Size(210, 190);
            this.OuterPanel.TabIndex = 4;
            // 
            // PictureBox
            // 
            this.Controls.Add(this.OuterPanel);
            this.Name = "PictureBox";
            this.Size = new System.Drawing.Size(210, 190);
            this.OuterPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }
        #endregion

        #region Constructors

        public PictureBox()
        {
            InitializeComponent();
            InitCtrl(); // my special settings for the ctrl
        }

        #endregion

        #region Properties

        private Image _pictureImage;
        public Image PictureImage
        {
            get { return _pictureImage; }
            set
            {
                if (null != value)
                {
                    try
                    {
                        PicBox.Image = value;
                        _pictureImage = value;
                    }
                    catch (OutOfMemoryException ex)
                    {
                        RedCross();
                    }
                }
                else
                {
                    RedCross();
                }
            }
        }

        /// <summary>
        /// Property to select the picture which is displayed in the picturebox. If the 
        /// file doesn´t exist or we receive an exception, the picturebox displays 
        /// a red cross.
        /// </summary>
        /// <value>Complete filename of the picture, including path information</value>
        /// <remarks>Supported fileformat: *.gif, *.tif, *.jpg, *.bmp</remarks>
        /// 

        [Browsable(false)]
        public string Picture
        {
            get { return m_sPicName; }
            set
            {
                if (null != value)
                {
                    if (System.IO.File.Exists(value))
                    {
                        try
                        {
                            PicBox.Image = Image.FromFile(value);
                            m_sPicName = value;
                        }
                        catch (OutOfMemoryException ex)
                        {
                            RedCross();
                        }
                    }
                    else
                    {
                        RedCross();
                    }
                }
            }
        }

        /// <summary>
        /// Set the frametype of the picturbox
        /// </summary>
        [Browsable(false)]
        public BorderStyle Border
        {
            get { return OuterPanel.BorderStyle; }
            set { OuterPanel.BorderStyle = value; }
        }

        #endregion

        #region Other Methods

        /// <summary>
        /// Special settings for the picturebox ctrl
        /// </summary>
        private void InitCtrl()
        {
            PicBox.SizeMode = PictureBoxSizeMode.StretchImage;
            PicBox.Location = new Point(0, 0);
            OuterPanel.Dock = DockStyle.Fill;
            OuterPanel.Cursor = System.Windows.Forms.Cursors.NoMove2D;
            OuterPanel.AutoScroll = true;
            OuterPanel.MouseEnter += new EventHandler(PicBox_MouseEnter);
            PicBox.MouseEnter += new EventHandler(PicBox_MouseEnter);
            OuterPanel.MouseWheel += new MouseEventHandler(PicBox_MouseWheel);
        }

        /// <summary>
        /// Create a simple red cross as a bitmap and display it in the picturebox
        /// </summary>
        private void RedCross()
        {
            Bitmap bmp = new Bitmap(OuterPanel.Width, OuterPanel.Height, System.Drawing.Imaging.PixelFormat.Format16bppRgb555);
            Graphics gr;
            gr = Graphics.FromImage(bmp);
            Pen pencil = new Pen(Color.Red, 5);
            gr.DrawLine(pencil, 0, 0, OuterPanel.Width, OuterPanel.Height);
            gr.DrawLine(pencil, 0, OuterPanel.Height, OuterPanel.Width, 0);
            PicBox.Image = bmp;
            gr.Dispose();
        }

        #endregion

        #region Zooming Methods

        /// <summary>
        /// Make the PictureBox dimensions larger to effect the Zoom.
        /// </summary>
        /// <remarks>Maximum 5 times bigger</remarks>
        private void ZoomIn()
        {
            if ((PicBox.Width < (MINMAX * OuterPanel.Width)) &&
                (PicBox.Height < (MINMAX * OuterPanel.Height)))
            {
                PicBox.Width = Convert.ToInt32(PicBox.Width * ZOOMFACTOR);
                PicBox.Height = Convert.ToInt32(PicBox.Height * ZOOMFACTOR);
                PicBox.SizeMode = PictureBoxSizeMode.StretchImage;
            }
        }

        /// <summary>
        /// Make the PictureBox dimensions smaller to effect the Zoom.
        /// </summary>
        /// <remarks>Minimum 5 times smaller</remarks>
        private void ZoomOut()
        {
            if ((PicBox.Width > (OuterPanel.Width / MINMAX)) &&
                (PicBox.Height > (OuterPanel.Height / MINMAX)))
            {
                PicBox.SizeMode = PictureBoxSizeMode.StretchImage;
                PicBox.Width = Convert.ToInt32(PicBox.Width / ZOOMFACTOR);
                PicBox.Height = Convert.ToInt32(PicBox.Height / ZOOMFACTOR);
            }
        }

        #endregion

        #region Mouse events

        /// <summary>
        /// We use the mousewheel to zoom the picture in or out
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PicBox_MouseWheel(object sender, MouseEventArgs e)
        {
            if (e.Delta < 0)
            {
                ZoomIn();
            }
            else
            {
                ZoomOut();
            }
        }

        /// <summary>
        /// Make sure that the PicBox have the focus, otherwise it doesn´t receive 
        /// mousewheel events !.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PicBox_MouseEnter(object sender, EventArgs e)
        {
            if (PicBox.Focused == false)
            {
                PicBox.Focus();
            }
        }

        #endregion

        #region Disposing

        /// <summary>
        /// Die verwendeten Ressourcen bereinigen.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                    components.Dispose();
            }
            base.Dispose(disposing);
        }

        #endregion
    }
}
