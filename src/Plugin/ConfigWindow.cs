using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Drawing;
using Unbroken.LaunchBox.Plugins;

namespace ArchiveCacheManager
{
    public partial class ConfigWindow : Form
    {
        private bool refreshLaunchBox = false;

        public ConfigWindow()
        {
            InitializeComponent();

            refreshLaunchBox = false;

            Config.Load();

            versionLabel.Text = CacheManager.Version;

            extensionPriorityDataGridView.Rows.Clear();
            foreach (var priority in Config.FilenamePriority)
            {
                string[] priorityInfo = priority.Key.Split(new string[] { @"\" }, StringSplitOptions.RemoveEmptyEntries);
                string priorityEmulator = priorityInfo[0].Trim();
                string priorityPlatform = priorityInfo[1].Trim();

                if (string.Equals(priorityEmulator, "All", StringComparison.InvariantCultureIgnoreCase) &&
                    string.Equals(priorityPlatform, "All", StringComparison.InvariantCultureIgnoreCase))
                {
                    extensionPriorityDataGridView.Rows.Insert(0, new string[] { priorityEmulator, priorityPlatform, priority.Value });
                }
                else
                {
                    extensionPriorityDataGridView.Rows.Add(new string[] { priorityEmulator, priorityPlatform, priority.Value });
                }
            }
            extensionPriorityDataGridView.ClearSelection();

            multiDiscSupportCheckBox.Checked = Config.MultiDiscSupport;
            useGameIdM3uFilenameCheckBox.Checked = Config.UseGameIdAsM3uFilename;

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

            useGameIdM3uFilenameCheckBox.Enabled = multiDiscSupportCheckBox.Checked;

            if (extensionPriorityDataGridView.SelectedRows.Count == 1)
            {
                editPriorityButton.Enabled = true;

                // Don't allow deletion of the default All / All file priority.
                if (string.Equals(extensionPriorityDataGridView.SelectedRows[0].Cells[0].Value.ToString(), "All", StringComparison.InvariantCultureIgnoreCase) &&
                    string.Equals(extensionPriorityDataGridView.SelectedRows[0].Cells[1].Value.ToString(), "All", StringComparison.InvariantCultureIgnoreCase))
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
                editPriorityButton.Enabled = false;
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
                        cacheDataGridView.Rows.Add(new object[] { directory, gameInfo.KeepInCache, Path.GetFileName(gameInfo.ArchivePath),
                                                   gameInfo.Platform, gameInfo.DecompressedSize / 1048576.0 });
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        /// <summary>
        /// Opens a URL with the default web browser.
        /// </summary>
        /// <param name="url"></param>
        private void OpenURL(string url)
        {
            ProcessStartInfo ps = new ProcessStartInfo(url);
            ps.UseShellExecute = true;
            ps.Verb = "Open";
            Process.Start(ps);
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            Config.FilenamePriority.Clear();
            foreach (DataGridViewRow row in extensionPriorityDataGridView.Rows)
            {
                Config.FilenamePriority.Add(string.Format(@"{0} \ {1}", row.Cells[0].Value.ToString(), row.Cells[1].Value.ToString()), row.Cells[2].Value.ToString());
            }

            Config.MultiDiscSupport = multiDiscSupportCheckBox.Checked;
            Config.UseGameIdAsM3uFilename = useGameIdM3uFilenameCheckBox.Checked;

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
            PriorityEditWindow window = new PriorityEditWindow();

            window.ShowDialog(this);

            if (window.DialogResult == DialogResult.OK)
            {
                int index = extensionPriorityDataGridView.Rows.Add(new string[] { window.Emulator, window.Platform, window.PriorityList });
                extensionPriorityDataGridView.Rows[index].Selected = true;
            }
        }

        private void editPriorityButton_Click(object sender, EventArgs e)
        {
            DataGridViewCellCollection cells = extensionPriorityDataGridView.SelectedRows[0].Cells;

            PriorityEditWindow window = new PriorityEditWindow(cells[0].Value.ToString(), cells[1].Value.ToString(), cells[2].Value.ToString());

            window.ShowDialog(this);

            if (window.DialogResult == DialogResult.OK)
            {
                extensionPriorityDataGridView.SelectedRows[0].Cells[0].Value = window.Emulator;
                extensionPriorityDataGridView.SelectedRows[0].Cells[1].Value = window.Platform;
                extensionPriorityDataGridView.SelectedRows[0].Cells[2].Value = window.PriorityList;
            }
        }

        private void deletePriorityButton_Click(object sender, EventArgs e)
        {
            extensionPriorityDataGridView.Rows.Remove(extensionPriorityDataGridView.SelectedRows[0]);
            extensionPriorityDataGridView.ClearSelection();
        }

        private void extensionPriorityDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            updateEnabledState();
        }

        private void pluginLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            pluginLink.LinkVisited = true;
            OpenURL("https://forums.launchbox-app.com/files/file/234-archive-cache-manager/");
        }

        private void forumLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            forumLink.LinkVisited = true;
            OpenURL("https://forums.launchbox-app.com/topic/35010-archive-cache-manager/");
        }

        private void sourceLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            sourceLink.LinkVisited = true;
            OpenURL("https://github.com/fraganator/archive-cache-manager");
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

            refreshLaunchBox = true;
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

            refreshLaunchBox = true;
        }

        private void cacheDataGridView_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            if (e.ColumnIndex == cacheDataGridView.Columns["Archive"].Index)
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All);

                Bitmap mediaIcon = null;

                switch (cacheDataGridView.Rows[e.RowIndex].Cells["ArchivePlatform"].Value.ToString())
                {
                    case "Microsoft Xbox": mediaIcon = Resources.media_cd; break;
                    case "Microsoft Xbox 360": mediaIcon = Resources.media_cd; break;
                    case "Nintendo 64": mediaIcon = Resources.media_n64; break;
                    case "Nintendo GameCube": mediaIcon = Resources.media_gc; break;
                    case "Nintendo Wii": mediaIcon = Resources.media_cd; break;
                    case "Nintendo Wii U": mediaIcon = Resources.media_cd; break;
                    case "Sega 32X": mediaIcon = Resources.media_md; break;
                    case "Sega CD": mediaIcon = Resources.media_cd; break;
                    case "Sega Dreamcast": mediaIcon = Resources.media_cd; break;
                    case "Sega Genesis":mediaIcon = Resources.media_md; break;
                    case "Sega Saturn": mediaIcon = Resources.media_cd; break;
                    case "Sony Playstation": mediaIcon = Resources.media_ps1; break;
                    case "Sony Playstation 2":
                        if (Convert.ToDouble(cacheDataGridView.Rows[e.RowIndex].Cells["ArchiveSize"].Value) > 700.0)
                            mediaIcon = Resources.media_ps2;
                        else
                            mediaIcon = Resources.media_ps2_cd;
                        break;
                    case "Sony PSP": mediaIcon = Resources.media_psp; break;
                    default: mediaIcon = Resources.box_zipper; break;
                }

                if (mediaIcon != null)
                {
                    var w = mediaIcon.Width;
                    var h = mediaIcon.Height;
                    var x = e.CellBounds.Left + 1;// + (e.CellBounds.Width - w) / 2;
                    var y = e.CellBounds.Top + (e.CellBounds.Height - h) / 2;

                    e.Graphics.DrawImage(mediaIcon, new Rectangle(x, y, w, h));
                    e.Handled = true;
                }
            }
        }

        private void ConfigWindow_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (refreshLaunchBox  && !PluginHelper.StateManager.IsBigBox)
            {
                PluginHelper.LaunchBoxMainViewModel.RefreshData();
            }
        }

        private void multiDiscSupportCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            updateEnabledState();
        }
    }
}
