using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Reference
{
    public partial class Settings : Form
    {
        public int maxWidth, maxHeight;
        public Boolean onTop;
        public int hh, mm, ss;
        public int timerBuffer;
        public Boolean copy, searchAll;

        private Form1 parentForm;

        public Settings(Form1 parentForm, int maxWidth, int maxHeight, Boolean onTop, int hh, int mm, int ss, int timerBuffer, Boolean copy, Boolean searchAll)
        {
            InitializeComponent();
            this.parentForm = parentForm;
            this.maxWidth = maxWidth; this.maxHeight = maxHeight;
            this.onTop = onTop;
            this.hh = hh; this.mm = mm; this.ss = ss; 
            this.timerBuffer = timerBuffer;
            this.copy = copy;
            this.searchAll = searchAll;
        }

        private void Settings_Load(object sender, EventArgs e)
        {
            tbMaxWidth.Text = maxWidth.ToString(); tbMaxHeight.Text = maxHeight.ToString();
            cbOnTop.Checked = onTop;
            tbTimer.Text = parentForm.CreateTimerString(hh, mm, ss, 2);
            if (hh == 0)
            {
                if (mm == 0)
                {
                    if (ss == 15)
                        tTimerTrackBar.Value = 0;
                    else if (ss == 30)
                        tTimerTrackBar.Value = 1;
                } else if (mm == 1)
                {
                    if (ss == 0)
                        tTimerTrackBar.Value = 2;
                    else if (ss == 30)
                        tTimerTrackBar.Value = 3;
                } else if (mm == 2)
                {
                    if (ss == 0)
                        tTimerTrackBar.Value = 4;
                    else if (ss == 30)
                        tTimerTrackBar.Value = 5;
                } else if (mm == 3)
                    tTimerTrackBar.Value = 6;
                else if (mm == 5)
                    tTimerTrackBar.Value = 7;
                else if (mm == 10)
                    tTimerTrackBar.Value = 8;
                else if (mm == 15)
                    tTimerTrackBar.Value = 9;
                else if (mm == 30)
                    tTimerTrackBar.Value = 10;
                else
                    tTimerTrackBar.Value = 10;
            } else if (hh == 1)
            {
                if (mm == 0)
                    tTimerTrackBar.Value = 11;
                else if (mm == 30)
                    tTimerTrackBar.Value = 12;
                else
                    tTimerTrackBar.Value = 11;
            } else
                tTimerTrackBar.Value = 12;
            tbTimerBuffer.Text = timerBuffer.ToString();
            cbCopy.Checked = copy;
            if (copy)
                cbCopy.Text = "Copy";
            else
                cbCopy.Text = "Cut";
            cbSearchAll.Checked = searchAll;
        }

        private void tbMaxWidth_TextChanged(object sender, EventArgs e)
        {
            int ret;
            if (int.TryParse(tbMaxWidth.Text, out ret))
                maxWidth = ret;
        }

        private void tbMaxHeight_TextChanged(object sender, EventArgs e)
        {
            int ret;
            if (int.TryParse(tbMaxHeight.Text, out ret))
                maxHeight = ret;
        }

        private void cbOnTop_CheckedChanged(object sender, EventArgs e)
        {
            onTop = cbOnTop.Checked;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            searchAll = cbSearchAll.Checked;
        }

        private void tbTimer_TextChanged(object sender, EventArgs e)
        {
            string[] parts = tbTimer.Text.Split(':');
            int hh_, mm_, ss_;
            if (!int.TryParse(parts[0], out hh_))
                return;
            if (!int.TryParse(parts[1], out mm_))
                return;
            if (!int.TryParse(parts[2], out ss_))
                return;
            hh = hh_; mm = mm_; ss = ss_;
        }

        private void tTimerTrackBar_Scroll(object sender, EventArgs e)
        {
            switch (tTimerTrackBar.Value)
            {
                case 0:
                    hh = 0;
                    mm = 0;
                    ss = 15;
                    break;
                case 1:
                    hh = 0;
                    mm = 0;
                    ss = 30;
                    break;
                case 2:
                    hh = 0;
                    mm = 1;
                    ss = 0;
                    break;
                case 3:
                    hh = 0;
                    mm = 1;
                    ss = 30;
                    break;
                case 4:
                    hh = 0;
                    mm = 2;
                    ss = 0;
                    break;
                case 5:
                    hh = 0;
                    mm = 2;
                    ss = 30;
                    break;
                case 6:
                    hh = 0;
                    mm = 3;
                    ss = 0;
                    break;
                case 7:
                    hh = 0;
                    mm = 5;
                    ss = 0;
                    break;
                case 8:
                    hh = 0;
                    mm = 10;
                    ss = 0;
                    break;
                case 9:
                    hh = 0;
                    mm = 15;
                    ss = 0;
                    break;
                case 10:
                    hh = 0;
                    mm = 30;
                    ss = 0;
                    break;
                case 11:
                    hh = 1;
                    mm = 0;
                    ss = 0;
                    break;
                case 12:
                    hh = 1;
                    mm = 30;
                    ss = 0;
                    break;
                default:
                    hh = 0;
                    mm = 1;
                    ss = 30;
                    break;
            }

            tbTimer.Text = parentForm.CreateTimerString(hh, mm, ss, 2);
        }

        private void tbTimerBuffer_TextChanged(object sender, EventArgs e)
        {
            int ret;
            if (int.TryParse(tbTimerBuffer.Text, out ret))
                timerBuffer = ret;
        }

        private void cbCopy_CheckedChanged(object sender, EventArgs e)
        {
            copy = cbCopy.Checked;
            if (copy)
                cbCopy.Text = "Copy";
            else
                cbCopy.Text = "Cut";
        }
    }
}
