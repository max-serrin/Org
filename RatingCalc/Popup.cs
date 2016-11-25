using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RatingCalc
{
    public partial class Popup : Form
    {
        string returnVal;
        string format;

        public string GetValue(double rating, string format)
        {
            this.format = format;
            tbRating.Text = rating.ToString(format);
            this.ShowDialog();
            return returnVal;
        }

        public Popup()
        {
            InitializeComponent();
            this.Focus();
        }

        private void Popup_KeyDown(object sender, KeyEventArgs e)
        {
            SetReturnVal(getKeyValue(e));
        }

        private void SetReturnVal(int val)
        {
            if (tbRating.Text.Length == 1)
                tbRating.Text += ".00";
            else if (tbRating.Text.Length == 3)
                tbRating.Text += "0";
            switch (val)
            {
                case 0:
                    returnVal = "redo";
                    break;
                case 1:
                    returnVal = "\\explicit\\" + tbRating.Text;
                    break;
                case 2:
                    returnVal = "\\questionable\\" + tbRating.Text;
                    break;
                case 3:
                    returnVal = "\\safe\\" + tbRating.Text;
                    break;
            }

            this.Close();
        }

        private int getKeyValue(KeyEventArgs e)
        {
            int returnVal = 0;

            switch (e.KeyCode)
            {
                case Keys.NumPad1:
                    returnVal = 1;
                    break;
                case Keys.NumPad2:
                    returnVal = 2;
                    break;
                case Keys.NumPad3:
                    returnVal = 3;
                    break;
                case Keys.D1:
                    returnVal = 1;
                    break;
                case Keys.D2:
                    returnVal = 2;
                    break;
                case Keys.D3:
                    returnVal = 3;
                    break;
            }

            return returnVal;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SetReturnVal(1);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SetReturnVal(2);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SetReturnVal(3);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            SetReturnVal(0);
        }

        private void button1_KeyDown(object sender, KeyEventArgs e)
        {
            SetReturnVal(getKeyValue(e));
        }
    }
}
