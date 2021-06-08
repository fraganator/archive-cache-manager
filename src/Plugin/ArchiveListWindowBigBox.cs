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
using Unbroken.LaunchBox.Plugins.Data;

namespace ArchiveCacheManager
{
    public partial class ArchiveListWindowBigBox : Form //, IBigBoxThemeElementPlugin
    {
        public string SelectedFile;

        public ArchiveListWindowBigBox(string archiveName, string[] fileList, string selection = "")
        {
            InitializeComponent();

            if (LaunchBoxSettings.HideMouseCursor)
            {
                Cursor.Hide();
            }

            archiveNameLabel.Text = archiveName;

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

        private void FileListBox_DrawItem(object sender, DrawItemEventArgs e)
        {
            e.DrawBackground();
            Graphics g = e.Graphics;
            Brush brush = ((e.State & DrawItemState.Selected) == DrawItemState.Selected) ?
                            new SolidBrush(Color.FromArgb(0x5F, 0x33, 0x99, 0xFF)) : new SolidBrush(e.BackColor);
            g.FillRectangle(brush, e.Bounds);
            e.Graphics.DrawString("  " + fileListBox.Items[e.Index].ToString(), e.Font,
                        new SolidBrush(e.ForeColor), e.Bounds, StringFormat.GenericDefault);
            //e.DrawFocusRectangle();
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            SelectedFile = fileListBox.SelectedItem.ToString();
        }

        private void fileListBox_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            okButton.PerformClick();
        }




#if false
        public void OnSelectionChanged(FilterType filterType, string filterValue, IPlatform platform, IPlatformCategory category, IPlaylist playlist, IGame game)
        {
            
        }

        public bool OnEnter()
        {
            SelectedFile = fileListBox.SelectedItem.ToString();

            this.DialogResult = DialogResult.OK;
            this.Close();

            return true;
        }

        public bool OnEscape()
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();

            return true;
        }

        public bool OnUp(bool held)
        {
            if (fileListBox.SelectedIndex > 0)
            {
                fileListBox.SelectedIndex--;
            }

            return true;
        }

        public bool OnDown(bool held)
        {
            if (fileListBox.SelectedIndex < fileListBox.Items.Count - 1)
            {
                fileListBox.SelectedIndex++;
            }

            return true;
        }

        public bool OnLeft(bool held)
        {
            return true;
        }

        public bool OnRight(bool held)
        {
            return true;
        }

        public bool OnPageDown()
        {
            return true;
        }

        public bool OnPageUp()
        {
            return true;
        }
#endif
    }
}
