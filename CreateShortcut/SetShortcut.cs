using System.Windows.Forms;

namespace CreateShortcut
{
    public partial class SetShortcut : Form
    {
        public Keys Shortcut { get; set; }

        public SetShortcut()
        {
            InitializeComponent();
        }

        private void SetShortcut_KeyDown(object sender, KeyEventArgs e)
        {
            shortcutTextBox.Text = e.KeyData.ToString();
            Shortcut = e.KeyData;
        }

        private void okButton_KeyDown(object sender, KeyEventArgs e)
        {
            SetShortcut_KeyDown(sender, e);
        }

        private void cancelButton_Click(object sender, System.EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void okButton_Click(object sender, System.EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
