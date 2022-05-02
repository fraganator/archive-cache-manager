﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using Unbroken.LaunchBox.Plugins;
using Unbroken.LaunchBox.Plugins.Data;

namespace ArchiveCacheManager
{
    public partial class BatchCacheWindow : Form
    {
        enum StatusEnum
        {
            None,
            Running,
            Complete,
            Cancelled,
            Closing
        };

        private IGame[] mSelectedGames;
        private double requiredCacheSize = 0;
        private static int mStaticRowIndex = -1;
        private static DataGridView mStaticCacheStatusGridView = null;
        private static StatusEnum mStatus;
        private static ProgressBarFlat mStaticProgressBar = null;
        private static int mStaticCurrentGame = 0;
        private static int mStaticTotalGames = 0;

        public BatchCacheWindow(IGame[] selectedGames)
        {
            InitializeComponent();
            UserInterface.SetDoubleBuffered(cacheStatusGridView, true);
            UserInterface.ApplyTheme(this);

            mSelectedGames = selectedGames;
            mStaticCacheStatusGridView = cacheStatusGridView;
            mStaticProgressBar = progressBar;

            cacheButton.Enabled = false;
            cancelButton.Enabled = false;
            progressBar.Visible = false;
            mStatus = StatusEnum.None;

            PopulateTable();
        }

        private void PopulateTable()
        {
            for (int i = 0; i < mSelectedGames.Count(); i++)
            {
                if (PluginUtils.IsGameMultiDisc(mSelectedGames[i]))
                {
                    (_, _, List<DiscInfo> discs) = PluginUtils.GetMultiDiscInfo(mSelectedGames[i], null);
                    foreach (var disc in discs)
                    {
                        cacheStatusGridView.Rows.Add(new object[] { i.ToString(),
                                                                    mSelectedGames[i].Id,
                                                                    disc.ApplicationId,
                                                                    disc.ArchivePath,
                                                                    Path.GetFileName(disc.ArchivePath),
                                                                    mSelectedGames[i].Platform,
                                                                    "",
                                                                    "",
                                                                    "",
                                                                    "Checking..." });
                    }
                }
                else
                {
                    cacheStatusGridView.Rows.Add(new object[] { i.ToString(),
                                                                mSelectedGames[i].Id,
                                                                string.Empty,
                                                                mSelectedGames[i].ApplicationPath,
                                                                Path.GetFileName(mSelectedGames[i].ApplicationPath),
                                                                mSelectedGames[i].Platform,
                                                                "",
                                                                "",
                                                                "",
                                                                "Checking..." });
                }
            }
        }

        private async void PopulateTableArchiveSize()
        {
            Extractor zip = new Zip();
            Extractor chdman = new Chdman();
            Extractor dolphinTool = new DolphinTool();
            Extractor robocopy = new Robocopy();
            long archiveSize = 0;
            double archiveSizeMb = 0;
            requiredCacheSize = 0;
            string key;

            GameLaunching.Restore7z();

            progressBar.Maximum = cacheStatusGridView.RowCount - 1;
            progressBar.Value = 0;
            progressBar.Visible = true;

            try
            {
                for (int i = 0; i < cacheStatusGridView.RowCount; i++)
                {
                    Extractor extractor = null;
                    string path = cacheStatusGridView.Rows[i].Cells["ArchivePath"].Value.ToString();
                    int index = Convert.ToInt32(cacheStatusGridView.Rows[i].Cells["Index"].Value);
                    string gameInfoPath = PathUtils.GetArchiveCacheGameInfoPath(PathUtils.ArchiveCachePath(PathUtils.GetAbsolutePath(path)));

                    if (File.Exists(gameInfoPath))
                    {
                        archiveSize = new GameInfo(gameInfoPath).DecompressedSize;
                        archiveSizeMb = archiveSize / 1048576.0;
                        cacheStatusGridView.Rows[i].Cells["ArchiveSize"].Value = archiveSize;
                        cacheStatusGridView.Rows[i].Cells["ArchiveSizeMb"].Value = archiveSizeMb;
                        requiredCacheSize += archiveSizeMb;
                        cacheStatusGridView.Rows[i].Cells["CacheAction"].Value = "None";
                        cacheStatusGridView.Rows[i].Cells["CacheStatus"].Value = "Already cached.";
                        continue;
                    }

                    if (!File.Exists(path))
                    {
                        cacheStatusGridView.Rows[i].Cells["ArchiveSize"].Value = "";
                        cacheStatusGridView.Rows[i].Cells["ArchiveSizeMb"].Value = "";
                        cacheStatusGridView.Rows[i].Cells["CacheAction"].Value = "None";
                        cacheStatusGridView.Rows[i].Cells["CacheStatus"].Value = "File not found.";
                        continue;
                    }

                    if (!PluginUtils.GetEmulatorPlatformAutoExtract(mSelectedGames[index].EmulatorId, mSelectedGames[index].Platform))
                    {
                        cacheStatusGridView.Rows[i].Cells["ArchiveSize"].Value = "";
                        cacheStatusGridView.Rows[i].Cells["ArchiveSizeMb"].Value = "";
                        cacheStatusGridView.Rows[i].Cells["CacheAction"].Value = "None";
                        cacheStatusGridView.Rows[i].Cells["CacheStatus"].Value = "\"Extract ROM archives\" disabled.";
                        continue;
                    }

                    key = Config.EmulatorPlatformKey(PluginHelper.DataManager.GetEmulatorById(mSelectedGames[index].EmulatorId).Title, mSelectedGames[index].Platform);
                    Config.Action action = Config.GetAction(key);
                    bool extract = (action == Config.Action.Extract || action == Config.Action.ExtractCopy);
                    bool copy = (action == Config.Action.Copy || action == Config.Action.ExtractCopy);

                    if (extract && Zip.SupportedType(path))
                        extractor = zip;
                    else if (extract && Config.GetChdman(key) && Chdman.SupportedType(path))
                        extractor = chdman;
                    else if (extract && Config.GetDolphinTool(key) && DolphinTool.SupportedType(path))
                        extractor = dolphinTool;
                    else
                    {
                        extractor = robocopy;
                        extract = false;
                    }

                    if (extract || copy)
                    {
                        archiveSize = await Task.Run(() => extractor.GetSize(path));
                        archiveSizeMb = archiveSize / 1048576.0;
                        cacheStatusGridView.Rows[i].Cells["ArchiveSize"].Value = archiveSize;
                        cacheStatusGridView.Rows[i].Cells["ArchiveSizeMb"].Value = archiveSizeMb;
                        requiredCacheSize += archiveSizeMb;
                        cacheStatusGridView.Rows[i].Cells["CacheAction"].Value = extract ? "Extract" : "Copy";
                        cacheStatusGridView.Rows[i].Cells["CacheStatus"].Value = "Ready.";
                    }
                    else
                    {
                        cacheStatusGridView.Rows[i].Cells["ArchiveSize"].Value = "";
                        cacheStatusGridView.Rows[i].Cells["ArchiveSizeMb"].Value = "";
                        cacheStatusGridView.Rows[i].Cells["CacheAction"].Value = "None";
                        cacheStatusGridView.Rows[i].Cells["CacheStatus"].Value = "No rule to cache.";
                    }

                    progressBar.PerformStep();
                }

                cacheButton.Enabled = true;
                progressBar.Visible = false;
            }
            catch (Exception e)
            {
                if (mStatus != StatusEnum.Closing)
                {
                    FlexibleMessageBox.Show($"An exception occurred when querying the games to cache:\r\n{e.ToString()}", "Batch Cache Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void cacheButton_Click(object sender, EventArgs e)
        {
            if (requiredCacheSize > Config.CacheSize)
            {
                var result = FlexibleMessageBox.Show(string.Format("The configured cache size isn't large enough to fit all of the games.\r\n\r\nIncrease the cache size from {0:N0} MB to {1:N0} MB?", Config.CacheSize, Math.Ceiling(requiredCacheSize)),
                                                     "Increase cache size?", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (result == DialogResult.OK)
                {
                    Config.CacheSize = (long)Math.Ceiling(requiredCacheSize);
                    Config.Save();
                }
                else
                {
                    return;
                }
            }
            CacheGames();
        }

        public static string ExtractionProgress(string stdout)
        {
            string progressRegex = "(\\d+\\.?\\d*)%(.*)";

            if (stdout != null)
            {
                try
                {
                    Match match = Regex.Match(stdout, progressRegex);
                    if (match.Success)
                    {
                        double progress = double.Parse(match.Groups[1].Value);
                        mStaticProgressBar.Value = (int)(progress / mStaticTotalGames + mStaticCurrentGame / (double)mStaticTotalGames * 100.0);
                        mStaticCacheStatusGridView.Rows[mStaticRowIndex].Cells["CacheStatus"].Value = string.Format("Working... {0}%", (int)progress);
                    }
                }
                catch (Exception)
                {

                }
            }

            return string.Empty;
        }

        private async void CacheGames()
        {
            mStatus = StatusEnum.Running;

            cacheButton.Enabled = false;
            cancelButton.Enabled = true;
            progressBar.Maximum = 100;
            progressBar.Value = 0;
            progressBar.Visible = true;

            mStaticCurrentGame = 0;
            mStaticTotalGames = 0;
            for (int i = 0; i < cacheStatusGridView.Rows.Count; i++)
            {
                if (!string.Equals("None", cacheStatusGridView.Rows[i].Cells["CacheAction"].Value))
                {
                    mStaticTotalGames++;
                }
            }

            GameLaunching.Replace7z();

            for (int i = 0; i < cacheStatusGridView.Rows.Count; i++)
            {
                DataGridViewRow row = cacheStatusGridView.Rows[i];
                mStaticRowIndex = i;

                if (string.Equals("None", row.Cells["CacheAction"].Value))
                {
                    continue;
                }

                progressBar.Value = (int)(mStaticCurrentGame / (double)mStaticTotalGames * 100.0);
                string gameId = row.Cells["GameId"].Value.ToString();
                string appId = row.Cells["AppId"].Value.ToString();
                IGame game = PluginHelper.DataManager.GetGameById(gameId);
                IAdditionalApplication app = PluginUtils.GetAdditionalApplicationById(gameId, appId);
                IEmulator emulator = PluginHelper.DataManager.GetEmulatorById(game.EmulatorId);

                cacheStatusGridView.Rows[i].Cells["CacheStatus"].Value = "Working...";
                GameLaunching.SaveGameInfo(game, app, emulator);
                (string stdout, string stderr, int exitCode) = await Task.Run(() => ProcessUtils.RunProcess(PathUtils.GetLaunchBox7zPath(), $"c {cacheStatusGridView.Rows[i].Cells["ArchiveSize"].Value}", true, ExtractionProgress));
                mStaticCurrentGame++;

                if (mStatus == StatusEnum.Cancelled)
                {
                    cacheStatusGridView.Rows[i].Cells["CacheStatus"].Value = "Extraction cancelled.";
                    progressBar.Visible = false;
                    break;
                }
                else if (exitCode != 0)
                {
                    cacheStatusGridView.Rows[i].Cells["CacheStatus"].Value = "Extraction error.";
                    var result = FlexibleMessageBox.Show($"Error caching {cacheStatusGridView.Rows[i].Cells["Archive"].Value}:\r\n{stdout}", "Caching Error",
                                            MessageBoxButtons.OKCancel, SystemIcons.Error.ToBitmap(), MessageBoxDefaultButton.Button2,
                                            null, "Continue Caching", "Stop");
                    if (result == DialogResult.Cancel)
                    {
                        progressBar.Visible = false;
                        break;
                    }
                }
                else
                {
                    cacheStatusGridView.Rows[i].Cells["CacheStatus"].Value = "Complete.";
                }
            }

            GameLaunching.Restore7z();

            mStatus = StatusEnum.Complete;

            cacheButton.Enabled = true;
            cancelButton.Enabled = false;
            progressBar.Visible = false;
        }

        private void BatchCacheWindow_Shown(object sender, EventArgs e)
        {
            cacheStatusGridView.ClearSelection();

            Application.DoEvents();

            // Async call, as this can be time consuming
            PopulateTableArchiveSize();
        }

        private void BatchCacheWindow_FormClosed(object sender, FormClosedEventArgs e)
        {
            mStaticCacheStatusGridView = null;
            mSelectedGames = null;
            mStaticProgressBar = null;

            if (mStatus != StatusEnum.None && !PluginHelper.StateManager.IsBigBox)
            {
                PluginHelper.LaunchBoxMainViewModel.RefreshData();
            }
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            mStatus = StatusEnum.Cancelled;
            ProcessUtils.KillProcess();
        }

        private void BatchCacheWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (mStatus == StatusEnum.Running)
            {
                var result = FlexibleMessageBox.Show("Caching is still in progress!\r\n\r\nCancel caching?", "Cancel caching?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    mStatus = StatusEnum.Closing;
                    ProcessUtils.KillProcess();
                }
                else
                {
                    e.Cancel = true;
                }
            }
            else
            {
                mStatus = StatusEnum.Closing;
            }
            }

        private void cacheStatusGridView_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            if (e.ColumnIndex == cacheStatusGridView.Columns["Archive"].Index ||
                e.ColumnIndex == cacheStatusGridView.Columns["CacheStatus"].Index)
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All);

                Bitmap cellIcon = null;

                if (e.ColumnIndex == cacheStatusGridView.Columns["Archive"].Index)
                    cellIcon = UserInterface.GetMediaIcon(cacheStatusGridView.Rows[e.RowIndex].Cells["ArchivePlatform"].Value.ToString());
                else if (e.ColumnIndex == cacheStatusGridView.Columns["CacheStatus"].Index)
                {
                    string statusText = cacheStatusGridView.Rows[e.RowIndex].Cells["CacheStatus"].Value.ToString().ToLower();
                    if (statusText.Contains("complete") || statusText.Contains("already cached"))
                    {
                        cellIcon = Resources.tick;
                    }
                    else if (statusText.Contains("working"))
                    {
                        cellIcon = Resources.hourglass;
                    }
                    else if (statusText.Contains("error"))
                    {
                        cellIcon = Resources.exclamation_red;
                    }
                    else if (statusText.Contains("cancel"))
                    {
                        cellIcon = Resources.exclamation_white;
                    }
                    else if (statusText.Contains("file not found"))
                    {
                        cellIcon = Resources.exclamation;
                    }
                }

                if (cellIcon != null)
                {
                    var w = cellIcon.Width;
                    var h = cellIcon.Height;
                    var x = e.CellBounds.Left + 5;// + (e.CellBounds.Width - w) / 2;
                    var y = e.CellBounds.Top + (e.CellBounds.Height - h) / 2;

                    e.Graphics.DrawImage(cellIcon, new Rectangle(x, y, w, h));
                    e.Handled = true;
                }
            }
        }
    }
}