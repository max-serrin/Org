using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace WindowsFormsApplication1
{
    public partial class Window : Form
    {
        private Game game = new Game();

        Point anchor = new Point(0, 0);
        Point anchor_ = new Point(1920, 1080);
        Size resolution;

        System.IO.Ports.SerialPort p;

        public Window()
        {
            InitializeComponent();

            resolution = new Size(anchor_);
            this.Size = resolution;
        }

        private void Window_FormClosing(object sender, FormClosingEventArgs e)
        {
            game.stopGame();
        }

        private void canvas_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = canvas.CreateGraphics();
            game.startGraphics(g);
        }

        // Console
        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAsAttribute(UnmanagedType.Bool)]
        static extern bool AllocConsole();

        private void Window_Load(object sender, EventArgs e)
        {
            AllocConsole();
        }
    }
}
