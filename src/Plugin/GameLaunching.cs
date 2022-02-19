using System;
using System.Collections.Generic;
using Unbroken.LaunchBox.Plugins;
using Unbroken.LaunchBox.Plugins.Data;
using System.IO;

namespace ArchiveCacheManager
{
    class GameLaunching : IGameLaunchingPlugin
    {
        private static bool cacheManagerActive = false;

        public void OnAfterGameLaunched(IGame game, IAdditionalApplication app, IEmulator emulator)
        {
            if (cacheManagerActive)
            {
                Logger.Log("Game started, cleaning up 7-Zip folder.");
                Restore7z();
            }
        }

        public void OnBeforeGameLaunching(IGame game, IAdditionalApplication app, IEmulator emulator)
        {
            if (PluginUtils.GetEmulatorPlatformAutoExtract(game.EmulatorId, game.Platform))
            {
                Logger.Log(string.Format("-------- {0} --------", game.Title.ToUpper()));
                Logger.Log(string.Format("Preparing cache for {0} ({1}) running with {2}.", game.Title, game.Platform, emulator.Title));

                cacheManagerActive = true;

                GameInfo gameInfo = new GameInfo(PathUtils.GetGameInfoPath());
                gameInfo.ArchivePath = (app != null && app.ApplicationPath != string.Empty) ? app.ApplicationPath : game.ApplicationPath;
                gameInfo.Emulator = emulator.Title;
                gameInfo.Platform = game.Platform;
                gameInfo.Title = game.Title;
                gameInfo.SelectedFile = GameIndex.GetSelectedFile(game.Id);
                gameInfo.Save();

                Replace7z();
            }
        }

        public void OnGameExited()
        {
            if (cacheManagerActive)
            {
                // The temp path will be empty most times, but in the case where files were extracted to temp (the cache folder
                // doesn't exist or the archive is too small) AND a file priority has been applied, non priority files won't be
                // removed from the temp folder (as LB doesn't know about them). Force clear this folder on game exit.
                DiskUtils.DeleteDirectory(PathUtils.GetLaunchBox7zTempPath(), true);

                if (!PluginHelper.StateManager.IsBigBox)
                {
                    PluginHelper.LaunchBoxMainViewModel.RefreshData();
                }

                cacheManagerActive = false;
            }
        }

        /// <summary>
        /// Replaces 7z.exe with Archive Cache Manager, renaming 7z.exe to 7-zip.exe.
        /// </summary>
        public static void Replace7z()
        {
            bool copyError = false;
            string pluginRootPath = PathUtils.GetPluginRootPath();
            string plugin7zRootPath = PathUtils.GetPlugin7zRootPath();
            string launchBox7zRootPath = PathUtils.GetLaunchBox7zRootPath();
            Dictionary<string, string> paths = new Dictionary<string, string>();

            paths.Add(Path.Combine(pluginRootPath, "ArchiveCacheManager.Core.dll"), Path.Combine(launchBox7zRootPath, "ArchiveCacheManager.Core.dll"));
            paths.Add(Path.Combine(pluginRootPath, "INIFileParser.dll"),            Path.Combine(launchBox7zRootPath, "INIFileParser.dll"));
            paths.Add(Path.Combine(pluginRootPath, "ArchiveCacheManager.exe"),      Path.Combine(launchBox7zRootPath, "7z.exe"));
            paths.Add(Path.Combine(plugin7zRootPath, "7z.exe.original"),            Path.Combine(launchBox7zRootPath, "7-zip.exe"));

            foreach (KeyValuePair<string, string> path in paths)
            {
                try
                {
                    File.Copy(path.Key, path.Value, true);
                }
                catch (Exception e)
                {
                    copyError = true;
                    Logger.Log(e.ToString(), Logger.LogLevel.Exception);
                    break;
                }
            }

            if (copyError)
            {
                Logger.Log("Error setting up Archive Cache Manager, cleaning up 7-Zip folder.");
                cacheManagerActive = false;
                Restore7z();
            }
        }

        /// <summary>
        /// Restores the original 7z.exe, and removes Archive Cache Manager from the ThirdParty\7-Zip folder.
        /// </summary>
        /// TODO: There's a potential race condition where a game may be extracting, and as the extraction
        /// completes and the replacement 7z.exe process exits, LB runs a data backup. The data backup runs
        /// under the replacement 7z.exe, but then this function runs and begins reverting files, including
        /// the renamed 7-zip.exe. 7z.exe will attempt to run 7-zip.exe, but throw an exception because it
        /// is missing.
        public static void Restore7z()
        {
            string plugin7zRootPath = PathUtils.GetPlugin7zRootPath();
            string launchBox7zRootPath = PathUtils.GetLaunchBox7zRootPath();

            string[] paths = new string[] { Path.Combine(launchBox7zRootPath, "ArchiveCacheManager.Core.dll"),
                                            Path.Combine(launchBox7zRootPath, "INIFileParser.dll"),
                                            Path.Combine(launchBox7zRootPath, "7-zip.exe"),
                                            PathUtils.GetGameInfoPath() };

            try
            {
                File.Copy(Path.Combine(plugin7zRootPath, "7z.exe.original"), Path.Combine(launchBox7zRootPath, "7z.exe"), true);
                File.Copy(Path.Combine(plugin7zRootPath, "7z.dll.original"), Path.Combine(launchBox7zRootPath, "7z.dll"), true);
            }
            catch (Exception e)
            {
                Logger.Log(e.ToString(), Logger.LogLevel.Exception);
            }

            foreach (string path in paths)
            {
                try
                {
                    if (File.Exists(path))
                    {
                        File.Delete(path);
                    }
                }
                catch (Exception e)
                {
                    Logger.Log(e.ToString(), Logger.LogLevel.Exception);
                }
            }
        }
    }
}
