using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Octokit;

namespace ArchiveCacheManager
{
    class Updater
    {
        public static async void CheckForUpdate(int delay = 0)
        {
            await Task.Delay(delay);

            try
            {
                var client = new GitHubClient(new ProductHeaderValue("archive-cache-manager"));
                var latestRelease = await client.Repository.Release.GetLatest("fraganator", "archive-cache-manager");
                string latestReleaseString = latestRelease.TagName.Replace("v", "");
                Version latestVersion = new Version(latestReleaseString);

                if (latestVersion > CacheManager.Version)
                {
                    Logger.Log(string.Format("Update check found new version: {0}", latestRelease.TagName));

                    if (string.IsNullOrEmpty(Config.SkipUpdate) || latestVersion > new Version(Config.SkipUpdate))
                    {
                        var result = FlexibleMessageBox.Show(string.Format("Version {0} of Archive Cache Manager is now available for download. New features include:\r\n\r\n{1}\r\n", latestReleaseString, latestRelease.Body),
                                                        "Update Check", MessageBoxButtons.YesNoCancel, Resources.icon32x32, MessageBoxDefaultButton.Button1,
                                                        "View Homepage", "Skip This Version", "Remind Me Later");
                        if (result == DialogResult.Yes)
                        {
                            PluginUtils.OpenURL("https://forums.launchbox-app.com/files/file/234-archive-cache-manager/");
                        }
                        else if (result == DialogResult.No)
                        {
                            Config.SkipUpdate = latestVersion.ToString();
                            Config.Save();
                        }
                    }
                }
                else
                {
                    Logger.Log("Update check found latest version installed.");
                }
            }
            catch (Exception e)
            {
                Logger.Log(string.Format("Update check failed: {0}", e.ToString()));
            }
        }

        public static async void EnableUpdateCheckPrompt(int delay = 0)
        {
            await Task.Delay(delay);

            var result = FlexibleMessageBox.Show("Archive Cache Manager can notify you when a new update is available.\r\nNothing will be automatically downloaded or installed.\r\n\r\nEnable update check on startup?",
                                         "Enable update check?", MessageBoxButtons.YesNo, Resources.icon32x32, MessageBoxDefaultButton.Button2);

            Config.UpdateCheck = (result == DialogResult.Yes);
            Config.Save();
        }
    }
}
