using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RandomTest
{
    public partial class Form1 : Form
    {
        Random rng;

        public Form1()
        {
            InitializeComponent();
            rng = new Random();
            int[] r = new int[10];
            int it = 100;
            for (int i = 0; i < 10; i++ )
            {
                r[i] = 0;
            }



            for (int i = 1; i <= it; i++)
            {
                r[rng.Next(0, 10)]++;
            }

            textBox1.Text = ((double)r[0] / (it / 100) ).ToString();
            textBox2.Text = ((double)r[1] / (it / 100)).ToString();
            textBox3.Text = ((double)r[2] / (it / 100)).ToString();
            textBox4.Text = ((double)r[3] / (it / 100)).ToString();
            textBox5.Text = ((double)r[4] / (it / 100)).ToString();
            textBox6.Text = ((double)r[5] / (it / 100)).ToString();
            textBox7.Text = ((double)r[6] / (it / 100)).ToString();
            textBox8.Text = ((double)r[7] / (it / 100)).ToString();
            textBox9.Text = ((double)r[8] / (it / 100)).ToString();
            textBox10.Text = ((double)r[9] / (it / 100)).ToString();
        }
    }
}
