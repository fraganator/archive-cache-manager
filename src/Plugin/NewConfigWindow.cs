using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Drawing;
using Unbroken.LaunchBox.Plugins;
using System.Threading.Tasks;

namespace ArchiveCacheManager
{
    public partial class NewConfigWindow : Form
    {
        public bool RefreshLaunchBox = false;

        private Dictionary<string, Bitmap> emulatorIcons;

        public NewConfigWindow()
        {
            InitializeComponent();

            UserInterface.SetDoubleBuffered(cacheDataGridView, true);
            UserInterface.SetDoubleBuffered(emulatorPlatformConfigDataGridView, true);
            UserInterface.ApplyTheme(this);

            emulatorIcons = new Dictionary<string, Bitmap>();
            foreach (var emulator in PluginHelper.DataManager.GetAllEmulators())
            {
                try
                {
                    Bitmap icon = ShellIcon.GetShellIcon(PathUtils.GetAbsolutePath(emulator.ApplicationPath), ShellIcon.IconSize.Small);
                    if (icon != null)
                    {
                        emulatorIcons.Add(emulator.Title, icon);
                    }
                }
                catch (Exception)
                {
                }
            }

            foreach (DataGridViewColumn column in cacheDataGridView.Columns)
            {
                UserInterface.SetColumnMinimumWidth(column);
            }
            foreach (DataGridViewColumn column in emulatorPlatformConfigDataGridView.Columns)
            {
                UserInterface.SetColumnMinimumWidth(column);
            }

            RefreshLaunchBox = false;

            Config.Load();

            versionLabel.Text = CacheManager.VersionString;

            emulatorPlatformConfigDataGridView.Rows.Clear();

            var actionItems = (emulatorPlatformConfigDataGridView.Columns["Action"] as DataGridViewComboBoxColumn).Items;
            var launchPathItems = (emulatorPlatformConfigDataGridView.Columns["LaunchPath"] as DataGridViewComboBoxColumn).Items;
            var m3uNameItems = (emulatorPlatformConfigDataGridView.Columns["M3uName"] as DataGridViewComboBoxColumn).Items;
            foreach (var config in Config.GetAllEmulatorPlatformConfig())
            {
                string[] priorityInfo = config.Key.Split(new string[] { @"\" }, StringSplitOptions.RemoveEmptyEntries);
                string priorityEmulator = priorityInfo[0].Trim();
                string priorityPlatform = priorityInfo[1].Trim();

                if (string.Equals(priorityEmulator, "All", StringComparison.InvariantCultureIgnoreCase) &&
                    string.Equals(priorityPlatform, "All", StringComparison.InvariantCultureIgnoreCase))
                {
                    emulatorPlatformConfigDataGridView.Rows.Insert(0, new object[] { priorityEmulator,
                                                                                priorityPlatform,
                                                                                config.Value.FilenamePriority,
                                                                                actionItems[(int)config.Value.Action],
                                                                                launchPathItems[(int)config.Value.LaunchPath],
                                                                                config.Value.MultiDisc,
                                                                                m3uNameItems[(int)config.Value.M3uName],
                                                                                config.Value.SmartExtract,
                                                                                config.Value.Chdman,
                                                                                config.Value.DolphinTool,
                                                                                config.Value.ExtractXiso });
                }
                else
                {
                    emulatorPlatformConfigDataGridView.Rows.Add(new object[] { priorityEmulator,
                                                                          priorityPlatform,
                                                                          config.Value.FilenamePriority,
                                                                          actionItems[(int)config.Value.Action],
                                                                          launchPathItems[(int)config.Value.LaunchPath],
                                                                          config.Value.MultiDisc,
                                                                          m3uNameItems[(int)config.Value.M3uName],
                                                                          config.Value.SmartExtract,
                                                                          config.Value.Chdman,
                                                                          config.Value.DolphinTool,
                                                                          config.Value.ExtractXiso });
                }
            }
            emulatorPlatformConfigDataGridView.ClearSelection();

            treeView1.SelectedNode = treeView1.Nodes[0];

            updateCheckCheckBox.Checked = (bool)Config.UpdateCheck;
            standaloneExtensions.Text = Config.StandaloneExtensions;
            metadataExtensions.Text = Config.MetadataExtensions;
            bypassPathCheckCheckBox.Checked = Config.BypassPathCheck;

            updateCacheInfo(true);
            updateEnabledState();
        }

        /// <summary>
        /// Update the enabled state of all controls based on the current configuration and selection.
        /// </summary>
        private void updateEnabledState()
        {
            string path = PathUtils.GetAbsolutePath(Config.CachePath);
            bool cachePathExists = Directory.Exists(path);

            openInExplorerButton.Enabled = cachePathExists;
            deleteAllButton.Enabled = (cacheDataGridView.Rows.Count > 0);
            deleteSelectedButton.Enabled = (cacheDataGridView.SelectedRows.Count > 0);

            if (emulatorPlatformConfigDataGridView.SelectedRows.Count == 1)
            {
                // Don't allow deletion of the default All / All file priority.
                if (string.Equals(emulatorPlatformConfigDataGridView.SelectedRows[0].Cells[0].Value.ToString(), "All", StringComparison.InvariantCultureIgnoreCase) &&
                    string.Equals(emulatorPlatformConfigDataGridView.SelectedRows[0].Cells[1].Value.ToString(), "All", StringComparison.InvariantCultureIgnoreCase))
                {
                    deletePriorityButton.Enabled = false;
                }
                else
                {
                    deletePriorityButton.Enabled = true;
                }
            }
            else
            {
                deletePriorityButton.Enabled = false;
            }
        }

        /// <summary>
        /// Updates the cache summary text and optionally the cached item table.
        /// </summary>
        /// <param name="updateDataGrid"></param>
        private void updateCacheInfo(bool updateDataGrid = false)
        {
            if (updateDataGrid)
            {
                updateCacheDataGrid();
            }

            double cacheSizeUsed = 0;
            double keepSize = 0;

            try
            {
                foreach (DataGridViewRow row in cacheDataGridView.Rows)
                {
                    double size = Convert.ToDouble(row.Cells["ArchiveSize"].Value);
                    if (!Convert.ToBoolean(row.Cells["Keep"].Value))
                    {
                        cacheSizeUsed += size;
                    }
                    else
                    {
                        keepSize += size;
                    }
                }
            }
            catch (Exception e)
            {
                Logger.Log(e.ToString(), Logger.LogLevel.Exception);
            }

            cacheSummaryTextBox.Text =      string.Format("Cache Path:  {0}", PathUtils.GetAbsolutePath(Config.CachePath));
            cacheSummaryTextBox.Text += string.Format("\r\nCache Size:  {0:n1} MB / {1:n1} MB ({2:n1}%)", cacheSizeUsed, Config.CacheSize, (cacheSizeUsed / Convert.ToDouble(Config.CacheSize)) * 100.0);
            cacheSummaryTextBox.Text += string.Format("\r\n Keep Size:  {0:n1} MB", keepSize);
            //cacheSummaryTextBox.Text += string.Format("\r\nArchives In Cache: {0}", cacheDataGridView.Rows.Count);
        }

        /// <summary>
        /// Updates the cache table with data from disk.
        /// </summary>
        private void updateCacheDataGrid()
        {
            cacheDataGridView.Rows.Clear();

            try
            {
                foreach (string directory in Directory.GetDirectories(PathUtils.GetAbsolutePath(Config.CachePath), "*", SearchOption.TopDirectoryOnly))
                {
                    GameInfo gameInfo = new GameInfo(Path.Combine(directory, PathUtils.GetGameInfoFileName()));

                    if (gameInfo.InfoLoaded)
                    {
                        cacheDataGridView.Rows.Add(new object[] { directory, Path.GetFileName(gameInfo.ArchivePath),
                                                   gameInfo.Platform, gameInfo.DecompressedSize / 1048576.0, gameInfo.KeepInCache });
                    }
                }
            }
            catch (Exception)
            {
            }

            if (cacheDataGridView.RowCount > 0)
            {
                cacheDataGridView.Rows[0].Selected = true;
            }
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            var config = Config.GetAllEmulatorPlatformConfigByRef();
            config.Clear();

            var actionItems = (emulatorPlatformConfigDataGridView.Columns["Action"] as DataGridViewComboBoxColumn).Items;
            var launchPathItems = (emulatorPlatformConfigDataGridView.Columns["LaunchPath"] as DataGridViewComboBoxColumn).Items;
            var m3uNameItems = (emulatorPlatformConfigDataGridView.Columns["M3uName"] as DataGridViewComboBoxColumn).Items;
            foreach (DataGridViewRow row in emulatorPlatformConfigDataGridView.Rows)
            {
                string key = Config.EmulatorPlatformKey(row.Cells[0].Value.ToString(), row.Cells[1].Value.ToString());
                config.Add(key, new Config.EmulatorPlatformConfig());
                config[key].FilenamePriority = row.Cells[2].Value == null ? string.Empty : row.Cells[2].Value.ToString();
                config[key].Action = (Config.Action)actionItems.IndexOf(row.Cells[3].Value);
                config[key].LaunchPath = (Config.LaunchPath)launchPathItems.IndexOf(row.Cells[4].Value);
                config[key].MultiDisc = Convert.ToBoolean(row.Cells[5].Value);
                config[key].M3uName = (Config.M3uName)m3uNameItems.IndexOf(row.Cells[6].Value);
                config[key].SmartExtract = Convert.ToBoolean(row.Cells[7].Value);
                config[key].Chdman = Convert.ToBoolean(row.Cells[8].Value);
                config[key].DolphinTool = Convert.ToBoolean(row.Cells[9].Value);
                config[key].ExtractXiso = Convert.ToBoolean(row.Cells[10].Value);
            }

            Config.UpdateCheck = updateCheckCheckBox.Checked;
            Config.StandaloneExtensions = standaloneExtensions.Text;
            Config.MetadataExtensions = metadataExtensions.Text;
            Config.BypassPathCheck = bypassPathCheckCheckBox.Checked;

            Config.Save();

            try
            {
                foreach (DataGridViewRow row in cacheDataGridView.Rows)
                {
                    GameInfo gameInfo = new GameInfo(Path.Combine(row.Cells["ArchivePath"].Value.ToString(), PathUtils.GetGameInfoFileName()));
                    bool keep = Convert.ToBoolean(row.Cells["Keep"].Value);
                    if (gameInfo.KeepInCache != keep)
                    {
                        gameInfo.KeepInCache = keep;
                        gameInfo.Save();
                    }
                }
            }
            catch (Exception exception)
            {
                Logger.Log(exception.ToString(), Logger.LogLevel.Exception);
            }

            this.Close();
        }

        private void addPriorityButton_Click(object sender, EventArgs e)
        {
            EmulatorPlatformSelectionWindow window = new EmulatorPlatformSelectionWindow();

            window.ShowDialog(this);

            if (window.DialogResult == DialogResult.OK)
            {
                foreach (DataGridViewRow row in emulatorPlatformConfigDataGridView.Rows)
                {
                    if (string.Equals(row.Cells["Emulator"].Value.ToString(), window.Emulator, StringComparison.InvariantCultureIgnoreCase) &&
                        string.Equals(row.Cells["Platform"].Value.ToString(), window.Platform, StringComparison.InvariantCultureIgnoreCase))
                    {
                        row.Selected = true;
                        return;
                    }
                }

                int index = emulatorPlatformConfigDataGridView.Rows.Add(new object[] { window.Emulator,
                                                                                       window.Platform,
                                                                                       string.Empty,
                                                                                       emulatorPlatformConfigDataGridView[3, 0].Value,
                                                                                       emulatorPlatformConfigDataGridView[4, 0].Value,
                                                                                       emulatorPlatformConfigDataGridView[5, 0].Value,
                                                                                       emulatorPlatformConfigDataGridView[6, 0].Value,
                                                                                       emulatorPlatformConfigDataGridView[7, 0].Value,
                                                                                       emulatorPlatformConfigDataGridView[8, 0].Value,
                                                                                       emulatorPlatformConfigDataGridView[9, 0].Value });
                emulatorPlatformConfigDataGridView.Rows[index].Selected = true;
            }
        }

        private void deletePriorityButton_Click(object sender, EventArgs e)
        {
            emulatorPlatformConfigDataGridView.Rows.Remove(emulatorPlatformConfigDataGridView.SelectedRows[0]);
            emulatorPlatformConfigDataGridView.ClearSelection();
        }

        private void extensionPriorityDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            updateEnabledState();
        }

        private void pluginLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            pluginLink.LinkVisited = true;
            PluginUtils.OpenURL("https://forums.launchbox-app.com/files/file/234-archive-cache-manager/");
        }

        private void forumLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            forumLink.LinkVisited = true;
            PluginUtils.OpenURL("https://forums.launchbox-app.com/topic/35010-archive-cache-manager/");
        }

        private void sourceLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            sourceLink.LinkVisited = true;
            PluginUtils.OpenURL("https://github.com/fraganator/archive-cache-manager");
        }

        private void configureCacheButton_Click(object sender, EventArgs e)
        {
            string cachePath = Config.CachePath;

            CacheConfigWindow window = new CacheConfigWindow();

            window.ShowDialog(this);

            if (window.DialogResult == DialogResult.OK)
            {
                updateCacheInfo(!PathUtils.ComparePaths(cachePath, Config.CachePath));
                updateEnabledState();
            }
        }

        private void openInExplorerButton_Click(object sender, EventArgs e)
        {
            string path = PathUtils.GetAbsolutePath(Config.CachePath);

            Process.Start("explorer.exe", path);
        }

        private void refreshButton_Click(object sender, EventArgs e)
        {
            updateCacheInfo(true);
            updateEnabledState();
        }

        private void deleteSelectedButton_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in cacheDataGridView.SelectedRows)
            {
                Logger.Log(string.Format("Manually deleting cached item \"{0}\".", row.Cells["ArchivePath"].Value));
                DiskUtils.DeleteDirectory(row.Cells["ArchivePath"].Value.ToString());
                cacheDataGridView.Rows.Remove(row);
            }

            updateCacheInfo();
            updateEnabledState();

            RefreshLaunchBox = true;
        }

        private void cacheDataGridView_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (cacheDataGridView.CurrentCell is DataGridViewCheckBoxCell)
            {
                cacheDataGridView.CommitEdit(DataGridViewDataErrorContexts.Commit);
                updateCacheInfo();
            }
        }

        private void deleteAllButton_Click(object sender, EventArgs e)
        {
            Logger.Log("Manually deleting entire cache.");

            CacheManager.ClearCacheSpace(long.MaxValue, true);

            updateCacheInfo(true);
            updateEnabledState();

            RefreshLaunchBox = true;
        }

        private void cacheDataGridView_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            if (e.ColumnIndex == cacheDataGridView.Columns["Archive"].Index)
            {
                Bitmap icon = UserInterface.GetMediaIcon(cacheDataGridView.Rows[e.RowIndex].Cells["ArchivePlatform"].Value.ToString());

                if (icon != null)
                {
                    UserInterface.DrawCellIcon(e, icon);
                }
            }
        }

        private void multiDiscSupportCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            updateEnabledState();
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            tabControl1.SelectTab(e.Node.Index);
        }

        private void emulatorPlatformConfigDataGridView_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dataGridView = sender as DataGridView;
            if (dataGridView.Cursor != Cursors.IBeam &&
                e.RowIndex >= 0 &&
                dataGridView.Columns[e.ColumnIndex] is DataGridViewTextBoxColumn &&
                !(dataGridView.Columns[e.ColumnIndex] as DataGridViewTextBoxColumn).ReadOnly)
            {
                dataGridView.Cursor = Cursors.IBeam;
            }
        }

        private void emulatorPlatformConfigDataGridView_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dataGridView = sender as DataGridView;
            if (dataGridView.Cursor != Cursors.Default)
            {
                dataGridView.Cursor = Cursors.Default;
            }
        }

        private void emulatorPlatformConfigDataGridView_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            if (e.ColumnIndex == emulatorPlatformConfigDataGridView.Columns["Emulator"].Index)
            {
                Bitmap icon = null;
                string emulator = emulatorPlatformConfigDataGridView.Rows[e.RowIndex].Cells["Emulator"].Value.ToString();

                if (string.Equals(emulator, "All", StringComparison.InvariantCultureIgnoreCase))
                {
                    icon = Resources.joystick;
                }
                else
                {
                    emulatorIcons.TryGetValue(emulator, out icon);
                }

                if (icon != null)
                {
                    UserInterface.DrawCellIcon(e, icon);
                }
            }
        }
    }
}
