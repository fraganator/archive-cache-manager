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

                    var result = MessageBox.Show(string.Format("Version {0} of Archive Cache Manager is now available for download.\r\n\r\nView release page now?", latestReleaseString),
                                                    "Archive Cache Manager Update Check", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                    if (result == DialogResult.Yes)
                    {
                        PluginUtils.OpenURL("https://forums.launchbox-app.com/files/file/234-archive-cache-manager/");
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

            var result = MessageBox.Show("Archive Cache Manager can notify you when a new update is available. Nothing is automatically downloaded or installed.\r\n\r\nEnable update check on startup?",
                                         "Enable update check?", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            Config.UpdateCheck = (result == DialogResult.Yes);
            Config.Save();
        }
    }
}
