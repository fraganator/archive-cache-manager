using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Unbroken.LaunchBox.Plugins;

namespace ArchiveCacheManager
{
    public partial class EmulatorPlatformSelectionWindow : Form
    {
        public string Emulator = "";
        public string Platform = "";

        public EmulatorPlatformSelectionWindow(string emulator = null, string platform = null)
        {
            InitializeComponent();

            emulatorComboBox.Items.Clear();
            platformComboBox.Items.Clear();

            if (emulator != null && platform != null)
            {
                emulatorComboBox.Items.Add(emulator);
                platformComboBox.Items.Add(platform);
                emulatorComboBox.SelectedIndex = 0;
                platformComboBox.SelectedIndex = 0;
            }
            else
            {
                emulatorComboBox.Items.AddRange(PluginHelper.DataManager.GetAllEmulators().Select(emu => emu.Title).ToArray());
                if (emulatorComboBox.Items.Count > 0)
                {
                    emulatorComboBox.SelectedIndex = 0;
                    populatePlatforms(emulatorComboBox.SelectedItem.ToString());
                }
            }

            updateEnabledState();

            UserInterface.ApplyTheme(this);
        }

        private void populatePlatforms(string emulatorName)
        {
            platformComboBox.Items.Clear();

            var platforms = PluginHelper.DataManager.GetAllEmulators().Single(emulator => emulator.Title == emulatorName).GetAllEmulatorPlatforms();
            platformComboBox.Items.AddRange(platforms.Select(platform => platform.Platform).ToArray());

            if (platformComboBox.Items.Count > 0)
            {
                platformComboBox.SelectedIndex = 0;
            }
        }

        private void updateEnabledState()
        {
            okButton.Enabled = (emulatorComboBox.Items.Count > 0 &&
                                emulatorComboBox.SelectedIndex != -1 &&
                                platformComboBox.Items.Count > 0 &&
                                platformComboBox.SelectedIndex != -1);

            emulatorComboBox.Enabled = (emulatorComboBox.Items.Count != 0);
            platformComboBox.Enabled = (platformComboBox.Items.Count != 0);
        }

        private void emulatorComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            populatePlatforms(emulatorComboBox.SelectedItem.ToString());
            
            updateEnabledState();
        }

        private void platformComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            updateEnabledState();
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            Emulator = emulatorComboBox.SelectedItem.ToString();
            Platform = platformComboBox.SelectedItem.ToString();
        }
    }
}
