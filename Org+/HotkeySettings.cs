using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Org_
{
    public partial class HotkeySettings : Form
    {
        public Dictionary<Keys, string> hotkeys;

        public HotkeySettings()
        {
            InitializeComponent();
            hotkeys = new Dictionary<Keys, string>();
        }

        private void HotkeySettings_KeyPress(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Escape)
            {
                Close();
                return;
            }

            e.Handled = true;
            tbHotkey.Text = e.KeyData.ToString();
            tbHotkey.Tag = e.KeyData;
            if (hotkeys.ContainsKey(e.KeyData))
                tbPath.Text = hotkeys[e.KeyData];
        }

        private void bSave_Click(object sender, EventArgs e)
        {
            if (tbHotkey.Tag != null)
                hotkeys[(Keys)tbHotkey.Tag] = tbPath.Text;
        }
    }
}
