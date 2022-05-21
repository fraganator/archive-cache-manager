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

            fileListGridView.Rows.Clear();
            for (int i = 0; i < fileList.Length; i++)
            {
                fileListGridView.Rows.Add(new object[] { fileList[i] });
                if (string.Equals(fileList[i], selection, StringComparison.InvariantCultureIgnoreCase))
                {
                    fileListGridView.Rows[i].Selected = true;
                    fileListGridView.CurrentCell = fileListGridView.Rows[i].Cells["File"];
                }
            }

            // Check that setting the selected item above actually worked. If not, set it to the first item.
            if (fileListGridView.SelectedRows.Count == 0)
            {
                fileListGridView.Rows[0].Selected = true;
                fileListGridView.CurrentCell = fileListGridView.Rows[0].Cells["File"];
            }
            SelectedFile = string.Empty;

            UserInterface.ApplyTheme(this);
            // fileListGridView.CellPainting += fileListGridView_CellPainting;
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            SelectedFile = fileListGridView.SelectedRows[0].Cells["File"].Value.ToString();
            EmulatorIndex = emulatorComboBox.SelectedIndex;
        }

        private void fileListBox_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            okButton.PerformClick();
        }

        /*
        private void fileListGridView_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            if (e.ColumnIndex == fileListGridView.Columns["File"].Index)
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All);

                Bitmap priorityIcon = null;
                Bitmap selectedIcon = null;

                if (e.RowIndex == priorityIndex)
                {
                    priorityIcon = Resources.star_blue;

                    var w = priorityIcon.Width;
                    var h = priorityIcon.Height;
                    var x = e.CellBounds.Left + 5;// + (e.CellBounds.Width - w) / 2;
                    var y = e.CellBounds.Top + (e.CellBounds.Height - h) / 2;

                    e.Graphics.DrawImage(priorityIcon, new Rectangle(x, y, w, h));
                    e.Handled = true;
                }
                
                if (e.RowIndex == selectedIndex)
                {
                    selectedIcon = Resources.star;

                    var w = selectedIcon.Width;
                    var h = selectedIcon.Height;
                    var x = e.CellBounds.Left + 15;// + (e.CellBounds.Width - w) / 2;
                    var y = e.CellBounds.Top + (e.CellBounds.Height - h) / 2;

                    e.Graphics.DrawImage(selectedIcon, new Rectangle(x, y, w, h));
                    e.Handled = true;
                }
            }
        }
        */
    }
}
