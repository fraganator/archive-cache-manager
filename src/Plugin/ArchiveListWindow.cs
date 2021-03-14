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

        public ArchiveListWindow(string archiveName, string[] fileList)
        {
            InitializeComponent();

            archiveNameLabel.Text = archiveName;

            fileListBox.Items.Clear();
            fileListBox.Items.AddRange(fileList);
            fileListBox.SelectedIndex = 0;
            SelectedFile = string.Empty;
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            SelectedFile = fileListBox.SelectedItem.ToString();
        }

        private void fileListBox_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            okButton.PerformClick();
        }
    }
}
