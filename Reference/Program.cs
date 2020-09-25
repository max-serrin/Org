﻿using System;
using System.Windows.Forms;

namespace Reference
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Manager());
            Application.Run(new Reference(args));
        }
    }
}
