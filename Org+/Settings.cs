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
    public partial class Settings : Form
    {
        Dictionary<string, string> folders;
        Dictionary<string, char> keys;

        public Settings(ref Dictionary<string, string> _folders, ref Dictionary<string, char> _keys)
        {
            InitializeComponent();

            folders = _folders;
            keys = _keys;

            leftClickKey.Text = keys["left_click"].ToString();
            rightClickKey.Text = keys["right_click"].ToString();
            middleClickKey.Text = keys["middle_click"].ToString();
            deleteKey.Text = keys["delete"].ToString();

            leftClickFolder.Text = folders["left_click"];
            rightClickFolder.Text = folders["right_click"];
            middleClickFolder.Text = folders["middle_click"];
            deleteFolder.Text = folders["delete"];
        }

        private void leftClickFolder_TextChanged(object sender, EventArgs e)
        {
            folders["left_click"] = leftClickFolder.Text;
        }

        private void rightClickFolder_TextChanged(object sender, EventArgs e)
        {
            folders["right_click"] = rightClickFolder.Text;
        }

        private void middleClickFolder_TextChanged(object sender, EventArgs e)
        {
            folders["middle_click"] = middleClickFolder.Text;
        }

        private void deleteFolder_TextChanged(object sender, EventArgs e)
        {
            folders["delete"] = deleteFolder.Text;
        }

        private void Settings_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }

        private void leftClickKey_MouseClick(object sender, MouseEventArgs e)
        {
            getKey("left_click");
            leftClickKey.Text = keys["left_click"].ToString();
        }

        private void rightClickKey_MouseClick(object sender, MouseEventArgs e)
        {
            getKey("right_click");
            rightClickKey.Text = keys["right_click"].ToString();
        }

        private void middleClickKey_MouseClick(object sender, MouseEventArgs e)
        {
            getKey("middle_click");
            middleClickKey.Text = keys["middle_click"].ToString();
        }

        private void deleteKey_MouseClick(object sender, MouseEventArgs e)
        {
            getKey("delete");
            deleteKey.Text = keys["delete"].ToString();
        }

        private void getKey(string s)
        {
            GetKey g = new GetKey();
            g.ShowDialog();
            keys[s] = g.Key;
            g.Close();
        }
    }
}
