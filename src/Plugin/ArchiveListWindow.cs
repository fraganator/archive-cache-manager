using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ArchiveCacheManager
{
    public partial class ArchiveListWindow : Form
    {
        public string SelectedFile;
        public int EmulatorIndex;

        public ArchiveListWindow(string archiveName, string[] fileList, string[] emulatorList, string selection = "")
        {
            InitializeComponent();

            archiveNameLabel.Text = archiveName;

            emulatorComboBox.Items.Clear();
            if (emulatorList.Count() > 0)
            {
                emulatorComboBox.Items.AddRange(emulatorList);
                emulatorComboBox.SelectedIndex = 0;
                EmulatorIndex = emulatorComboBox.SelectedIndex;
                emulatorComboBox.Enabled = true;
            }
            else
            {
                emulatorComboBox.Enabled = false;
            }

            fileListBox.Items.Clear();
            fileListBox.Items.AddRange(fileList);
            if (selection != string.Empty)
            {
                fileListBox.SelectedItem = selection;
            }
            // Check that setting the selected item above actually worked. If not, set it to the first item.
            if (fileListBox.SelectedItems.Count == 0)
            {
                fileListBox.SelectedIndex = 0;
            }
            SelectedFile = string.Empty;
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            SelectedFile = fileListBox.SelectedItem.ToString();
            EmulatorIndex = emulatorComboBox.SelectedIndex;
        }

        private void fileListBox_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            okButton.PerformClick();
        }
    }
}
