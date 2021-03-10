using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

namespace ArchiveCacheManager
{
    public partial class ConfigWindow : Form
    {
        public ConfigWindow()
        {
            InitializeComponent();

            Config.LoadConfig();
            cachePath.Text = Config.CachePath;
            cacheSize.Value = Config.CacheSize;
            minArchiveSize.Value = Config.MinArchiveSize;
            versionLabel.Text = CacheManager.Version;

            extensionPriorityListView.Items.Clear();
            foreach (KeyValuePair<string, string> priority in Config.ExtensionPriority)
            {
                string[] priorityInfo = priority.Key.Split(new string[] { @"\" }, StringSplitOptions.RemoveEmptyEntries);

                extensionPriorityListView.Items.Add(new ListViewItem(new string[] { priorityInfo[0].Trim(), priorityInfo[1].Trim(), priority.Value }));
            }

            updateEnabledState();
        }

        private void updateEnabledState()
        {
            okButton.Enabled = (cachePath.Text != string.Empty);

            if (extensionPriorityListView.SelectedIndices.Count == 1)
            {
                editPriorityButton.Enabled = true;
                deletePriorityButton.Enabled = true;
            }
            else
            {
                editPriorityButton.Enabled = false;
                deletePriorityButton.Enabled = false;
            }
        }

        private void OpenURL(string url)
        {
            ProcessStartInfo ps = new ProcessStartInfo(url);
            ps.UseShellExecute = true;
            ps.Verb = "Open";
            Process.Start(ps);
        }

        private void cachePathBrowseButton_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            string browsePath = PathUtils.CachePath(cachePath.Text);

            dialog.SelectedPath = Directory.Exists(browsePath) ? browsePath : PathUtils.GetLaunchBoxRootPath();
            dialog.ShowNewFolderButton = true;

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                cachePath.Text = PathUtils.GetRelativePath(PathUtils.GetLaunchBoxRootPath(), dialog.SelectedPath);
            }

            updateEnabledState();
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            if (PathUtils.GetAbsolutePath(cachePath.Text).Equals(PathUtils.GetLaunchBoxRootPath(), StringComparison.InvariantCultureIgnoreCase))
            {
                MessageBox.Show(this, "ERROR! The cache path can not be set to the LaunchBox folder. Please change the cache path.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            if (!cachePath.Text.Equals(Config.CachePath, StringComparison.InvariantCultureIgnoreCase) &&
                DiskUtils.DirectorySize(new DirectoryInfo(cachePath.Text)) > 0)
            {
                DialogResult result = MessageBox.Show(this, "WARNING! The selected cache path already contains files. These files WILL be deleted when the cache is cleaned. Continue?", "Warning!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (result == DialogResult.No)
                {
                    return;
                }
            }

            Config.CachePath = cachePath.Text;
            Config.CacheSize = Convert.ToInt64(cacheSize.Value);
            Config.MinArchiveSize = Convert.ToInt64(minArchiveSize.Value);

            Config.ExtensionPriority.Clear();
            foreach (ListViewItem item in extensionPriorityListView.Items)
            {
                Config.ExtensionPriority.Add(string.Format(@"{0} \ {1}", item.SubItems[0].Text, item.SubItems[1].Text), item.SubItems[2].Text);
            }

            Config.SaveConfig();

            this.Close();
        }

        private void addPriorityButton_Click(object sender, EventArgs e)
        {
            PriorityEditWindow window = new PriorityEditWindow();

            window.ShowDialog(this);

            if (window.DialogResult == DialogResult.OK)
            {
                extensionPriorityListView.Items.Add(new ListViewItem(new string[] { window.Emulator, window.Platform, window.PriorityList }));
            }
        }

        private void editPriorityButton_Click(object sender, EventArgs e)
        {
            ListViewItem.ListViewSubItemCollection items = extensionPriorityListView.SelectedItems[0].SubItems;

            PriorityEditWindow window = new PriorityEditWindow(items[0].Text, items[1].Text, items[2].Text);

            window.ShowDialog(this);

            if (window.DialogResult == DialogResult.OK)
            {
                int selectedIndex = extensionPriorityListView.SelectedIndices[0];
                extensionPriorityListView.SelectedItems[0].Remove();
                extensionPriorityListView.Items.Insert(selectedIndex, new ListViewItem(new string[] { window.Emulator, window.Platform, window.PriorityList }));
            }
        }

        private void deletePriorityButton_Click(object sender, EventArgs e)
        {
            extensionPriorityListView.SelectedItems[0].Remove();
        }

        private void cachePath_TextChanged(object sender, EventArgs e)
        {
            updateEnabledState();
        }

        private void extensionPriorityListView_SelectedIndexChanged(object sender, EventArgs e)
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
    }
}
