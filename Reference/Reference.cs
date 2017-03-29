using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GetRandomImage;
using System.Collections.Specialized;
using MyExtensions;

namespace Reference
{
    public partial class Reference : Form
    {
        // For dragging and drag drop
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        // Global file info and bitmap (TODO these don't need to be global)
        FileInfo im;
        Bitmap imb;

        // Constants
        private const string TIMER_HEADER = "Time: ";        // Timer string constants
        private const string TIMER_DIVIDER = ":";
        private const string TIMER_PAUSED = " (Paused)";
        public const int MIN_WIDTH = 25;                    // Minimum width and height
        public const int MIN_HEIGHT = 25;

        // Global variables
        public string folderPath;                           // Directory to retrieve images

        public int maxWidth, maxHeight;                     // Window cannot exceed these dimensions
        public int hh, mm, ss, maxhh, maxmm, maxss, timerPadding, timerBuffer;  // Timer variables, padding (H:M:S vs HH:MM:SS) and buffer (how many seconds to wait before reseting the timer)
        public Timer t;                                     // Timer to control countdown
        public Boolean copy;                                // Whether to copy or cut an image
        public Boolean onTop;                               // Whether the window will remain on top of other windows or not
        public Boolean searchAll;                           // Whther to search all directories or just the top directory

        private ContextMenu cm;                             // The right click menu and items
        private MenuItem timerMenuItem;
        private MenuItem nextMenuItem;
        private MenuItem lastMenuItem;
        private MenuItem fileMenuItem;
        private MenuItem directoryMenuItem;
        private MenuItem settingsMenuItem;
        private MenuItem fullscreenMenuItem;
        private MenuItem exitMenuItem;

        private RandomImageList ril;                        // Random image class

        /// <summary>
        /// Initialization
        /// </summary>
        public Reference(FileInfo image = null)
        {
            InitializeComponent();

            this.FormBorderStyle = FormBorderStyle.None;
            this.DoubleBuffered = true;
            this.SetStyle(ControlStyles.ResizeRedraw, true);

            // Image
            im = null; imb = null;

            // Window
            maxWidth = Properties.Settings.Default.maxWidth;
            maxHeight = Properties.Settings.Default.maxHeight;
            onTop = Properties.Settings.Default.onTop;

            // Images
            if (ObjectExtensions.Exists(image))
            {
                pictureBox1.ImageLocation = image.FullName;
                folderPath = image.DirectoryName;
            }
            else
            {
                pictureBox1.ImageLocation = "";
                folderPath = "";
            }

            searchAll = Properties.Settings.Default.searchAll;
            ril = new RandomImageList(folderPath, searchAll);
            copy = Properties.Settings.Default.copy;

            // Timer
            maxhh = Properties.Settings.Default.maxhh;
            maxmm = Properties.Settings.Default.maxmm;
            maxss = Properties.Settings.Default.maxss;
            timerPadding = 2;
            timerBuffer = Properties.Settings.Default.timerBuffer;
            ResetTimer();
            
            t = new Timer(); t.Interval = 1000; t.Tick += new EventHandler(this.TimerTick);

            // Context Menu
            cm = new ContextMenu();
            timerMenuItem = cm.MenuItems.Add(CreateTimerStringFull(hh, mm, ss, timerPadding));
            timerMenuItem.Click += new EventHandler(this.TimerToggle);

            nextMenuItem = cm.MenuItems.Add("Next Image");
            nextMenuItem.Click += new EventHandler(this.NextImage);

            lastMenuItem = cm.MenuItems.Add("Last Image");
            lastMenuItem.Click += new EventHandler(this.LastImage);

            fileMenuItem = cm.MenuItems.Add(im == null ? "" : im.Name);
            fileMenuItem.Click += new EventHandler(this.FileHandle);

            directoryMenuItem = cm.MenuItems.Add(im == null ? "" : im.DirectoryName);
            directoryMenuItem.Click += new EventHandler(this.DirectoryHandle);

            nextMenuItem.Visible = false;
            lastMenuItem.Visible = false;
            fileMenuItem.Visible = false;
            directoryMenuItem.Visible = false;

            settingsMenuItem = cm.MenuItems.Add("Settings");
            settingsMenuItem.Click += new EventHandler(this.OpenSettings);

            fullscreenMenuItem = cm.MenuItems.Add("Fullscreen");
            fullscreenMenuItem.Click += new EventHandler(this.ToggleFullscreen);

            exitMenuItem = cm.MenuItems.Add("Exit");
            exitMenuItem.Click += new EventHandler(this.formExit_Click);

            pictureBox1.ContextMenu = cm;
            pictureBox1.AllowDrop = true;
        }

        /// <summary>
        /// Toggle the form's window state between fullscreen and normal.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToggleFullscreen(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Maximized)
            {
                this.WindowState = FormWindowState.Normal;
                fullscreenMenuItem.Text = "Fullscreen";
            } else
            {
                this.WindowState = FormWindowState.Maximized;
                fullscreenMenuItem.Text = "Exit Fullscreen";
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
            StringCollection sc = new StringCollection();
            sc.Add(im.FullName);
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
            if (pictureBox1.ImageLocation != "")
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
            String ret = TIMER_HEADER + CreateTimerString(hh, mm, ss, padding);
                
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
            return (hh.ToString()).PadLeft(padding, '0') + TIMER_DIVIDER +
                (mm.ToString()).PadLeft(padding, '0') + TIMER_DIVIDER +
                (ss.ToString()).PadLeft(padding, '0');
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
            pictureBox1.ImageLocation = "";
            this.Width = MIN_WIDTH; this.Height = MIN_HEIGHT;
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
            im = ril.getNext();
            SetPictureBox(im);
        }

        /// <summary>
        /// Sets the picturebox to display the last image and resizes the window.
        /// </summary>
        private void SetLastImage()
        {
            im = ril.getPrevious();
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
            if (this.WindowState == FormWindowState.Maximized)
                fullscreenMenuItem.Text = "Exit Fullscreen";
            else
                fullscreenMenuItem.Text = "Fullscreen";
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
        /// <param name="e"></param>
        private void OpenSettings(object sender, EventArgs e)
        {
            this.Hide();
            Settings settings = new Settings(this, maxWidth, maxHeight, onTop, maxhh, maxmm, maxss, timerBuffer, copy, searchAll);
            settings.ShowDialog();
            maxWidth = Math.Max(settings.maxWidth, MIN_WIDTH); maxHeight = Math.Max(settings.maxHeight, MIN_HEIGHT);
            SetFormSize();
            onTop = settings.onTop;
            this.TopMost = onTop;
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
            Properties.Settings.Default.Save();

            this.Show();
        }

        /// <summary>
        /// Closes the form when exit is chosen in the right click menu.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void formExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Handles mouse down event for dragging the window.
        /// (I believe I got this code on Stack Overflow)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PictureBox1_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
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
        private void PictureBox1_DragEnter(object sender, System.Windows.Forms.DragEventArgs e)
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
        private void PictureBox1_DragDrop(object sender, System.Windows.Forms.DragEventArgs e)
        {
            string[] s = (string[])e.Data.GetData(DataFormats.FileDrop, false);
            FileAttributes attr = File.GetAttributes(s[0]);
            if ((attr & FileAttributes.Directory) == FileAttributes.Directory)
            {
                folderPath = s[0];
                ril.Reload(folderPath);
                im = ril.getNext();
            }
            else
            {
                im = new FileInfo(s[0]);
                folderPath = im.DirectoryName;
                ril.Reload(folderPath);
                if (t.Enabled)                          // Pause and reset the timer if it was enabled
                    TimerToggle(this, null);
                ResetTimer();
            }
            if (im != null)
            {
                directoryMenuItem.Text = im.DirectoryName;
                SetPictureBox(im);
            }
        }

        /// <summary>
        /// Sets the picturebox image to the location of the given file. 
        /// Also enables the menu items that require a file or folder to be used.
        /// </summary>
        /// <param name="fi"></param>
        private void SetPictureBox(FileInfo fi)
        {
            pictureBox1.ImageLocation = fi.FullName;
            SetFormSize();
            if (fileMenuItem != null)
                fileMenuItem.Text = im == null ? "" : im.Name;
            nextMenuItem.Visible = true;
            lastMenuItem.Visible = true;
            fileMenuItem.Visible = true;
            directoryMenuItem.Visible = true;
        }

        /// <summary>
        /// Resizes the window to best fit the image without going over maximum height or width.
        /// </summary>
        private void SetFormSize()
        {
            if (pictureBox1.ImageLocation != "")
            {
                imb = new Bitmap(pictureBox1.ImageLocation);
                double ratio;
                if (imb.Width > maxWidth || imb.Height > maxHeight)
                {
                    ratio = Math.Max((double)imb.Width / maxWidth, (double)imb.Height / maxHeight);
                    this.Width = (int)Math.Max(MIN_WIDTH, imb.Width / ratio);
                    this.Height = (int)Math.Max(MIN_HEIGHT, imb.Height / ratio);
                }
                else if (imb.Width <= maxWidth && imb.Height <= maxHeight)
                {
                    this.Width = imb.Width;
                    this.Height = imb.Height;
                }
            }
        }
    }
}
