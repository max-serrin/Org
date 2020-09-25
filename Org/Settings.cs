using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MyExtensions;
using SettingsTypes = Org.Enumeration.SettingsTypes;
using Actions = Org.Enumeration.Actions;
using Sizes = Org.Enumeration.Sizes;
using Modes = Org.Enumeration.Modes;
using HotkeySettings = Org.Enumeration.HotkeySettings;

namespace Org
{
    public partial class Settings : Form
    {
        internal Org parent;
        private CheckBox checkbox;

        SettingsLayout layout;

        public Settings(Org _parent)
        {
            InitializeComponent();

            parent = _parent;

            settingsList.Items.Clear();
            foreach (SettingsTypes settingsType in Enum.GetValues(typeof(SettingsTypes)))
            {
                settingsList.Items.Add(settingsType);
            }
            settingsList.SelectedIndex = 0;
        }

        private void Settings_FormClosing(object sender, FormClosingEventArgs e)
        {
            Hide();
            e.Cancel = true;
        }

        private void SettingsList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (settingsList.SelectedItem.Equals(SettingsTypes.General))
            {
                SettingsLayoutPanelInitialize_GeneralSettings();
            }
            if (settingsList.SelectedItem.Equals(SettingsTypes.Hotkeys))
            {
                using (layout = new HotkeysSettingsLayout(parent, settingsLayoutPanel))
                {
                    layout.Show();
                }
            }
        }

        private abstract class SettingsLayout : IDisposable
        {
            internal Org parent;
            internal Panel panel;

            public SettingsLayout(Org _parent, Panel _panel)
            {
                parent = _parent;
                panel = _panel;
            }

            public abstract void Show();

            public void Dispose()
            {
            }
        }

        private class HotkeysSettingsLayout : SettingsLayout
        {
            private ComboBox hotkeys;
            private ComboBox currentMode;
            private TextBox textBox_Move;
            private NumericUpDown textBox_Skip;
            private TextBox textBox_Open;
            private Button exeDialogButton;
            private ComboBox textBox_Mode;

            public HotkeysSettingsLayout(Org _parent, Panel _panel) : base(_parent, _panel) 
            {
                panel.Controls.Clear();
            }

            public override void Show()
            {
                // Modifier
                panel.Controls.Add(new Label { Text = "Modifier:", TextAlign = ContentAlignment.MiddleRight, Width = 47 });
                CheckBox checkBox;
                panel.Controls.Add(checkBox = new CheckBox { Text = "Ctrl", Width = 47, Tag = Keys.Control });
                checkBox.CheckedChanged += CheckBox_CheckedChanged;
                panel.Controls.Add(checkBox = new CheckBox { Text = "Alt", Width = 47, Tag = Keys.Alt });
                checkBox.CheckedChanged += CheckBox_CheckedChanged;
                panel.Controls.Add(checkBox = new CheckBox { Text = "Shift", Width = 47, Tag = Keys.Shift });
                checkBox.CheckedChanged += CheckBox_CheckedChanged;
                ((FlowLayoutPanel)panel).SetFlowBreak(checkBox, true);

                // Mode
                panel.Controls.Add(new Label { Text = "Mode:", TextAlign = ContentAlignment.MiddleRight, Width = 47 });
                panel.Controls.Add(currentMode = new ComboBox() { Tag = Keys.M });
                foreach (Modes mode in Enum.GetValues(typeof(Modes)))
                {
                    currentMode.Items.Add(mode);
                }
                currentMode.SelectedIndex = 0;
                currentMode.SelectedIndexChanged += ComboBox_ModeChanged;
                ((FlowLayoutPanel)panel).SetFlowBreak(currentMode, true);

                // Key
                panel.Controls.Add(new Label { Text = "Key:", TextAlign = ContentAlignment.MiddleRight, Width = 47 });

                List<HotkeySettings> hotkeySettingsList = new List<HotkeySettings>();
                foreach (Keys key in DataExtensions.GetCommonKeysAndButtons())
                {
                    hotkeySettingsList.Add(new HotkeySettings(key));
                }

                hotkeys = new ComboBox();
                hotkeys.SelectedIndexChanged += LoadHotkeySettings;
                hotkeys.DataSource = hotkeySettingsList;
                hotkeys.DisplayMember = "Name";
                panel.Controls.Add(hotkeys);
                ((FlowLayoutPanel)panel).SetFlowBreak(hotkeys, true);

                // Radio selections
                HotkeySettings hotkeySettings = (HotkeySettings)hotkeys.SelectedItem;
                RadioButton radioButton;

                panel.Controls.Add(radioButton = new RadioButton { Text = "None", Tag = Actions.None });
                ((FlowLayoutPanel)panel).SetFlowBreak(radioButton, true);

                panel.Controls.Add(radioButton = new RadioButton { Text = "Move selected file to ", Tag = Actions.Move });
                panel.Controls.Add(textBox_Move = new TextBox { Text = "...", Tag = Actions.Move });
                ((FlowLayoutPanel)panel).SetFlowBreak(textBox_Move, true);

                panel.Controls.Add(new RadioButton { Text = "Copy", Tag = Actions.Copy, AutoSize = true, AutoEllipsis = false, Anchor = AnchorStyles.None });
                panel.Controls.Add(new RadioButton { Text = "Copy data", Tag = Actions.CopyData, AutoSize = true, AutoEllipsis = false, Anchor = AnchorStyles.None });
                panel.Controls.Add(radioButton = new RadioButton { Text = "Cut", Tag = Actions.Cut, AutoSize = true, AutoEllipsis = false, Anchor = AnchorStyles.None });
                ((FlowLayoutPanel)panel).SetFlowBreak(radioButton, true);

                panel.Controls.Add(new RadioButton { Text = "Delete", Tag = Actions.Delete, AutoSize = true, AutoEllipsis = false, Anchor = AnchorStyles.None });
                panel.Controls.Add(checkBox = new CheckBox { Text = "Permanent", Tag = Actions.Delete, AutoSize = true, AutoEllipsis = false, Anchor = AnchorStyles.None });
                ((FlowLayoutPanel)panel).SetFlowBreak(checkBox, true);

                panel.Controls.Add(new RadioButton { Text = "Select all", Tag = Actions.SelectAll, AutoSize = true, AutoEllipsis = false, Anchor = AnchorStyles.None });
                panel.Controls.Add(new RadioButton { Text = "All previous", Tag = Actions.SelectAllPrevious, AutoSize = true, AutoEllipsis = false, Anchor = AnchorStyles.None });
                panel.Controls.Add(new RadioButton { Text = "All following", Tag = Actions.SelectAllFollowing, AutoSize = true, AutoEllipsis = false, Anchor = AnchorStyles.None });
                panel.Controls.Add(new RadioButton { Text = "Fullscreen", Tag = Actions.FullScreen, AutoSize = true, AutoEllipsis = false, Anchor = AnchorStyles.None });
                panel.Controls.Add(new RadioButton { Text = "Next", Tag = Actions.NextFile, AutoSize = true, AutoEllipsis = false, Anchor = AnchorStyles.None });
                panel.Controls.Add(radioButton = new RadioButton { Text = "Previous", Tag = Actions.PreviousFile, AutoSize = true, AutoEllipsis = false, Anchor = AnchorStyles.None });
                ((FlowLayoutPanel)panel).SetFlowBreak(radioButton, true);

                panel.Controls.Add(new RadioButton { Text = "Skip", Tag = Actions.Skip, AutoSize = true, AutoEllipsis = false });
                panel.Controls.Add(textBox_Skip = new NumericUpDown { Value = 1, Tag = Actions.Skip });
                ((FlowLayoutPanel)panel).SetFlowBreak(textBox_Skip, true);

                panel.Controls.Add(new RadioButton { Text = "Up Directory", Tag = Actions.UpDirectory, AutoSize = true, AutoEllipsis = false });
                panel.Controls.Add(new RadioButton { Text = "Next Directory", Tag = Actions.NextDirectory, AutoSize = true, AutoEllipsis = false });
                panel.Controls.Add(new RadioButton { Text = "Previous Directory", Tag = Actions.PreviousDirectory, AutoSize = true, AutoEllipsis = false });
                panel.Controls.Add(radioButton = new RadioButton { Text = "Rename", Tag = Actions.Rename });
                ((FlowLayoutPanel)panel).SetFlowBreak(radioButton, true);

                panel.Controls.Add(new RadioButton { Text = "Open with", Tag = Actions.Open, AutoSize = true, AutoEllipsis = false });
                panel.Controls.Add(textBox_Open = new TextBox { Text = "...", Tag = Actions.Open });
                panel.Controls.Add(exeDialogButton = new Button { Text = "...", Tag = Actions.Open, AutoSize = true, AutoSizeMode = AutoSizeMode.GrowAndShrink, AutoEllipsis = false });
                ((FlowLayoutPanel)panel).SetFlowBreak(exeDialogButton, true);

                panel.Controls.Add(new RadioButton { Text = "Set mode", Tag = Actions.Mode, AutoSize = true, AutoEllipsis = false });
                panel.Controls.Add(textBox_Mode = new ComboBox() { Tag = Actions.Mode });
                foreach (Modes mode in Enum.GetValues(typeof(Modes)))
                {
                    textBox_Mode.Items.Add(mode);
                }
                ((FlowLayoutPanel)panel).SetFlowBreak(textBox_Mode, true);


                panel.Controls.Add(radioButton = new RadioButton { Text = "Undo", Tag = Actions.Undo });
                ((FlowLayoutPanel)panel).SetFlowBreak(radioButton, true);

                Button button;
                panel.Controls.Add(button = new Button { Text = "Save" });
                button.MouseClick += SaveHotkeySettings;

                panel.Controls.Add(new CheckBox { Text = "For All Modes", Tag = Keys.A, AutoSize = true, AutoEllipsis = false });

                panel.Layout += new LayoutEventHandler(SettingsLayoutPanel_Layout);

                // Load the initial hotkey
                LoadHotkeySettings(hotkeys, null);
            }

            private void ComboBox_ModeChanged(object sender, EventArgs e)
            {
                LoadHotkeySettings(hotkeys, null);
            }

            private void CheckBox_CheckedChanged(object sender, EventArgs e)
            {
                LoadHotkeySettings(hotkeys, null);
            }

            private void SaveHotkeySettings(object sender, MouseEventArgs e)
            {
                // Create the key data name for the hotkey
                Keys selectedKey = ((HotkeySettings)hotkeys.SelectedItem).Key |
                    ((panel.Controls.OfType<CheckBox>().Where(r => r.Tag.Equals(Keys.Control)).FirstOrDefault().Checked ? Keys.Control : Keys.None)) |
                    ((panel.Controls.OfType<CheckBox>().Where(r => r.Tag.Equals(Keys.Alt)).FirstOrDefault().Checked ? Keys.Alt : Keys.None)) |
                    ((panel.Controls.OfType<CheckBox>().Where(r => r.Tag.Equals(Keys.Shift)).FirstOrDefault().Checked ? Keys.Shift : Keys.None));

                // Create settings
                HotkeySettings newHotkeySettings = new HotkeySettings(selectedKey)
                {
                    Action = (Actions)panel.Controls.OfType<RadioButton>().Where(radioButton => radioButton.Checked == true)
                    .FirstOrDefault().Tag
                };

                // Arguments
                if (newHotkeySettings.Action.Equals(Actions.Move) || newHotkeySettings.Action.Equals(Actions.Open))
                    newHotkeySettings.Arguments.Add(panel.Controls.OfType<TextBox>().Where(textBox => textBox.Tag.Equals(newHotkeySettings.Action)).FirstOrDefault().Text);
                else if (newHotkeySettings.Action.Equals(Actions.Skip))
                    newHotkeySettings.Arguments.Add(panel.Controls.OfType<NumericUpDown>().Where(textBox => textBox.Tag.Equals(newHotkeySettings.Action)).FirstOrDefault().Value.ToString());
                else if (newHotkeySettings.Action.Equals(Actions.Mode))
                {
                    newHotkeySettings.Arguments.Add(panel.Controls.OfType<ComboBox>().Where(textBox => textBox.Tag.Equals(newHotkeySettings.Action)).FirstOrDefault().SelectedText);
                    newHotkeySettings.Tag = panel.Controls.OfType<ComboBox>().Where(textBox => textBox.Tag.Equals(newHotkeySettings.Action)).FirstOrDefault().SelectedItem;
                }
                else if (newHotkeySettings.Action.Equals(Actions.Delete))
                {
                    if (panel.Controls.OfType<CheckBox>().Where(checkBox => checkBox.Tag.Equals(Actions.Delete)).FirstOrDefault().Checked)
                        newHotkeySettings.Tag = Microsoft.VisualBasic.FileIO.RecycleOption.DeletePermanently;
                    else
                        newHotkeySettings.Tag = Microsoft.VisualBasic.FileIO.RecycleOption.SendToRecycleBin;
                }

                // Store the settings to the hotkey dictionary
                if (panel.Controls.OfType<CheckBox>().Where(checkBox => checkBox.Tag.Equals(Keys.A)).FirstOrDefault().Checked)
                    foreach (Modes mode in Enum.GetValues(typeof(Modes)))
                    {
                        parent.hotkeys[new Tuple<Keys, Modes>(selectedKey, mode)] = newHotkeySettings;
                    }
                else
                    parent.hotkeys[new Tuple<Keys, Modes>(selectedKey, (Modes)currentMode.SelectedItem)] = newHotkeySettings;
            }

            private void LoadHotkeySettings(object sender, EventArgs e)
            {
                Keys selectedKey = ((HotkeySettings)((ComboBox)sender).SelectedItem).Key;

                HotkeySettings selectedHotkeySettings = parent.hotkeys[new Tuple<Keys, Modes>(selectedKey |
                    (panel.Controls.OfType<CheckBox>().Where(r => r.Tag.Equals(Keys.Control)).FirstOrDefault().Checked ? Keys.Control : Keys.None) |
                    (panel.Controls.OfType<CheckBox>().Where(r => r.Tag.Equals(Keys.Alt)).FirstOrDefault().Checked ? Keys.Alt : Keys.None) |
                    (panel.Controls.OfType<CheckBox>().Where(r => r.Tag.Equals(Keys.Shift)).FirstOrDefault().Checked ? Keys.Shift : Keys.None), (Modes)currentMode.SelectedItem)];
                panel.Controls.OfType<RadioButton>().Where(radioButton => radioButton.Tag.Equals(selectedHotkeySettings.Action))
                    .FirstOrDefault().Checked = true;
                
                textBox_Move.Text = "...";
                textBox_Open.Text = "...";
                textBox_Skip.Value = 1;
                textBox_Mode.SelectedIndex = 0;
                if (selectedHotkeySettings.Arguments.Count > 0)
                {
                    if (selectedHotkeySettings.Action.Equals(Actions.Move) || selectedHotkeySettings.Action.Equals(Actions.Open))
                        panel.Controls.OfType<TextBox>().Where(textBox => textBox.Tag.Equals(selectedHotkeySettings.Action)).FirstOrDefault().Text = selectedHotkeySettings.Arguments[0];
                    else if (selectedHotkeySettings.Action.Equals(Actions.Skip))
                        panel.Controls.OfType<NumericUpDown>().Where(textBox => textBox.Tag.Equals(selectedHotkeySettings.Action)).FirstOrDefault().Value = int.Parse(selectedHotkeySettings.Arguments[0]);
                    else if (selectedHotkeySettings.Action.Equals(Actions.Mode))
                        panel.Controls.OfType<ComboBox>().Where(textBox => textBox.Tag.Equals(selectedHotkeySettings.Action)).FirstOrDefault().Text = selectedHotkeySettings.Arguments[0];
                }
                else if (selectedHotkeySettings.Action.Equals(Actions.Delete))
                    panel.Controls.OfType<CheckBox>().Where(checkBox => checkBox.Tag.Equals(Actions.Delete)).FirstOrDefault().Checked = selectedHotkeySettings.Tag.Equals(Microsoft.VisualBasic.FileIO.RecycleOption.DeletePermanently);
            }

            private void SettingsLayoutPanel_Layout(object sender, LayoutEventArgs e)
            {
                if (textBox_Move.Exists())
                    textBox_Move.Width = 100 + Math.Max(((FlowLayoutPanel)sender).Width - 300, 0);
                if (textBox_Open.Exists())
                    textBox_Open.Width = 100 + Math.Max(((FlowLayoutPanel)sender).Width - 300, 0);
            }

            new public void Dispose()
            {
                panel.Controls.Clear();
            }
        }

        private void SettingsLayoutPanelInitialize_GeneralSettings()
        {
            settingsLayoutPanel.Controls.Clear();
            settingsLayoutPanel.Controls.Add(checkbox = new CheckBox() { Text = "General"});
        }
    }
}
