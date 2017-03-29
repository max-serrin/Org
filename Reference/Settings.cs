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
        // Global Variables
        public int maxWidth, maxHeight;                 // Defined in parent form
        public Boolean onTop;
        public int hh, mm, ss;
        public int timerBuffer;
        public Boolean copy, searchAll;

        private Reference parentForm;                       // Parent form to run public functions

        /// <summary>
        /// Current settings are passed into to settigns formed, displayed and changed. 
        /// Setting variables are public and the returning application is expected to grab them before destroying the form.
        /// </summary>
        /// <param name="parentForm"></param>
        /// <param name="maxWidth"></param>
        /// <param name="maxHeight"></param>
        /// <param name="onTop"></param>
        /// <param name="hh"></param>
        /// <param name="mm"></param>
        /// <param name="ss"></param>
        /// <param name="timerBuffer"></param>
        /// <param name="copy"></param>
        /// <param name="searchAll"></param>
        public Settings(Reference parentForm, int maxWidth, int maxHeight, Boolean onTop, int hh, int mm, int ss, int timerBuffer, Boolean copy, Boolean searchAll)
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

        /// <summary>
        /// Sets visual form elements to specific values based on setting variables.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// Handle change of max width. Save parsed integers.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbMaxWidth_TextChanged(object sender, EventArgs e)
        {
            int ret;
            if (int.TryParse(tbMaxWidth.Text, out ret))
                maxWidth = ret;
        }

        /// <summary>
        /// Handle change of max height. Save parsed integers.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbMaxHeight_TextChanged(object sender, EventArgs e)
        {
            int ret;
            if (int.TryParse(tbMaxHeight.Text, out ret))
                maxHeight = ret;
        }

        /// <summary>
        /// Handle change of on top. Save boolean value of checked property.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbOnTop_CheckedChanged(object sender, EventArgs e)
        {
            onTop = cbOnTop.Checked;
        }

        /// <summary>
        /// Handle change of search option. Save boolean value of checked property.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            searchAll = cbSearchAll.Checked;
        }

        /// <summary>
        /// Check key press parameter, close form if Enter is received.
        /// </summary>
        /// <param name="e"></param>
        private void checkForEnter(KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.Close();
            }
        }

        /// <summary>
        /// Handles KeyDown events across form elements.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Settings_KeyDown(object sender, KeyEventArgs e)
        {
            checkForEnter(e);
        }

        /// <summary>
        /// Handles change of timer textbox.
        /// Attempts to parse a string of some format split twice by ':'.
        /// First parsed integer will be store as the hours, second as the minutes and third as the seconds.
        /// Failed attempts to parse information will result in old values being retained.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbTimer_TextChanged(object sender, EventArgs e)
        {
            string[] parts = tbTimer.Text.Split(':');
            int hh_ = hh, mm_ = mm, ss_ = ss;
            if (parts.Length == 3)
            {
                if (!int.TryParse(parts[0], out hh_))
                    return;
                if (!int.TryParse(parts[1], out mm_))
                    return;
                if (!int.TryParse(parts[2], out ss_))
                    return;
            }
            hh = hh_; mm = mm_; ss = ss_;
        }

        /// <summary>
        /// Handles changes made to the trackbar.
        /// Values 0-12 on the trackbar set the timer textbox to predetermined values.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tTimerTrackBar_Scroll(object sender, EventArgs e)
        {
            hh = 0; mm = 0; ss = 0;
            switch (tTimerTrackBar.Value)
            {
                case 0:
                    ss = 15;
                    break;
                case 1:
                    ss = 30;
                    break;
                case 2:
                    mm = 1;
                    break;
                case 3:
                    mm = 1;
                    ss = 30;
                    break;
                case 4:
                    mm = 2;
                    break;
                case 5:
                    mm = 2;
                    ss = 30;
                    break;
                case 6:
                    mm = 3;
                    break;
                case 7:
                    mm = 5;
                    break;
                case 8:
                    mm = 10;
                    break;
                case 9:
                    mm = 15;
                    break;
                case 10:
                    mm = 30;
                    break;
                case 11:
                    hh = 1;
                    break;
                case 12:
                    hh = 1;
                    mm = 30;
                    break;
                default:
                    mm = 1;
                    ss = 30;
                    break;
            }

            tbTimer.Text = parentForm.CreateTimerString(hh, mm, ss, 2);
        }

        /// <summary>
        /// Handle changes to the timer buffer text box. Save parsed integers.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbTimerBuffer_TextChanged(object sender, EventArgs e)
        {
            int ret;
            if (int.TryParse(tbTimerBuffer.Text, out ret))
                timerBuffer = ret;
        }

        /// <summary>
        /// Handle changes to the copy/cut check box. Save boolean value of checked property.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
