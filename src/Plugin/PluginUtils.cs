using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unbroken.LaunchBox.Plugins;
using Unbroken.LaunchBox.Plugins.Data;

namespace ArchiveCacheManager
{
    static class PluginUtils
    {
        /// <summary>
        /// Opens a URL with the default web browser.
        /// </summary>
        /// <param name="url"></param>
        public static void OpenURL(string url)
        {
            ProcessStartInfo ps = new ProcessStartInfo(url);
            ps.UseShellExecute = true;
            ps.Verb = "Open";
            Process.Start(ps);
        }

        public static string GetArchivePath(IGame game, IAdditionalApplication app)
        {
            return PathUtils.GetAbsolutePath((app != null && app.ApplicationPath != string.Empty) ? app.ApplicationPath : game.ApplicationPath);
        }

        public static bool GetEmulatorPlatformAutoExtract(string emulatorId, string platformName)
        {
            try
            {
                var emulator = PluginHelper.DataManager.GetEmulatorById(emulatorId);
                var emulatorPlatform = Array.Find(emulator.GetAllEmulatorPlatforms(), p => p.Platform.Equals(platformName));

#if LAUNCHBOX_PRE_12_8
            return emulator.AutoExtract;
#else
                // emulatorPlatform.AutoExtract will be null if the Emulator settings haven't been changed since updating to LaunchBox 12.8
                // So perform two checks to determine if AutoExtract is true, one at the emulator level, and one at the emulatorPlatform level
                return (emulator.AutoExtract && emulatorPlatform.AutoExtract == null) || (emulatorPlatform.AutoExtract == true);
#endif
            }
            catch (Exception)
            {
            }

            return false;
        }

        public static bool GetEmulatorPlatformM3uDiscLoadEnabled(string emulatorId, string platformName)
        {
            var emulator = PluginHelper.DataManager.GetEmulatorById(emulatorId);
            var emulatorPlatform = Array.Find(emulator.GetAllEmulatorPlatforms(), p => p.Platform.Equals(platformName));

            return emulatorPlatform.M3uDiscLoadEnabled;
        }

        public static void SetEmulatorPlatformM3uDiscLoadEnabled(string emulatorId, string platformName, bool enabled)
        {
            var emulator = PluginHelper.DataManager.GetEmulatorById(emulatorId);
            var emulatorPlatform = Array.Find(emulator.GetAllEmulatorPlatforms(), p => p.Platform.Equals(platformName));

            emulatorPlatform.M3uDiscLoadEnabled = enabled;
        }

        /// <summary>
        /// Checks if a game is a multi-disc game. Game must have additional apps with Disc property set, and game path must be one of the additional app paths.
        /// </summary>
        /// <param name="game"></param>
        /// <returns>True is game is multi-disc, False otherwise.</returns>
        public static bool IsGameMultiDisc(IGame game)
        {
            var additionalApps = game.GetAllAdditionalApplications();

            return Array.Exists(additionalApps, a => a.Disc != null && a.ApplicationPath == game.ApplicationPath);
        }

        /// <summary>
        /// Check if the selected additional app is a disc from a multi-disc game.
        /// </summary>
        /// <param name="app"></param>
        /// <returns>True if app is from multi-disc game, False otherwise.</returns>
        public static bool IsAdditionalAppMultiDisc(IAdditionalApplication app)
        {
            return IsGameMultiDisc(PluginHelper.DataManager.GetGameById(app.GameId)) && app.Disc != null;
        }

        /// <summary>
        /// Check if the launched game or additional application is a multi-disc game.
        /// </summary>
        /// <param name="game"></param>
        /// <param name="app"></param>
        /// <returns>True if launched game is multi-disc, False otherwise.</returns>
        public static bool IsLaunchedGameMultiDisc(IGame game, IAdditionalApplication app)
        {
            return (app != null && IsAdditionalAppMultiDisc(app)) || (app == null && IsGameMultiDisc(game));
        }

        /// <summary>
        /// Get info on a multi-disc game.
        /// 
        /// totalDiscs is the total number of discs in a game. Determined by counting additional apps with Disc property set. Will be 0 if not a multi-dsc game.
        /// selectedDisc is the selected disc, based on the additional app Disc property. Will be 1 if additional app is null, and 0 if not a multi-disc game.
        /// discs is a list of discs and associated info in disc order. Will be empty if not a multi-disc game.
        /// </summary>
        /// <param name="game"></param>
        /// <param name="app"></param>
        /// <returns>Tuple of (totalDiscs, selectedDisc, discs).</returns>
        public static (int, int, List<DiscInfo>) GetMultiDiscInfo(IGame game, IAdditionalApplication app)
        {
            int totalDiscs = 0;
            int selectedDisc = 0;
            List<DiscInfo> discs = new List<DiscInfo>();

            if (!IsLaunchedGameMultiDisc(game, app))
            {
                totalDiscs = 0;
                selectedDisc = 0;
                discs.Clear();
            }

            var additionalApps = game.GetAllAdditionalApplications();
            var discApps = Array.FindAll(additionalApps, a => a.Disc != null);
            foreach (var discApp in discApps)
            {
                DiscInfo discInfo = new DiscInfo();
                discInfo.ApplicationId = discApp.Id;
                discInfo.ArchivePath = PathUtils.GetAbsolutePath(discApp.ApplicationPath);
                discInfo.Version = discApp.Version;
                discInfo.Disc = (int)discApp.Disc;
                discs.Add(discInfo);
            }
            discs.Sort((a, b) => a.Disc - b.Disc);

            if (app != null)
            {
                selectedDisc = (int)app.Disc;
            }
            else
            {
                selectedDisc = (int)Array.Find(discApps, a => a.ApplicationPath == game.ApplicationPath).Disc;
            }

            totalDiscs = discs.Count;

            return (totalDiscs, selectedDisc, discs);
        }

        public static IAdditionalApplication GetAdditionalApplicationById(string gameId, string appId)
        {
            if (string.IsNullOrEmpty(appId))
            {
                return null;
            }

            var additionalApps = PluginHelper.DataManager.GetGameById(gameId).GetAllAdditionalApplications();
            return Array.Find(additionalApps, app => app.Id == appId);
        }

        /// <summary>
        /// Get a list of (emulator, emulator platform) tuples for a given platform.
        /// If the default emulator ID is specified, the resulting list will include the default emulator at index 0.
        /// </summary>
        /// <param name="platform"></param>
        /// <param name="defaultEmulatorId"></param>
        /// <returns>A list of (IEmulator, IEmulatorPlatform) tuples.</returns>
        public static List<(IEmulator, IEmulatorPlatform)> GetPlatformEmulators(string platform, string defaultEmulatorId = null)
        {
            List<(IEmulator, IEmulatorPlatform)> emulators = new List<(IEmulator, IEmulatorPlatform)>();

            foreach (var emulator in PluginHelper.DataManager.GetAllEmulators())
            {
                foreach (var emulatorPlatform in emulator.GetAllEmulatorPlatforms())
                {
                    if (string.Equals(emulatorPlatform.Platform, platform))
                    {
                        if (string.Equals(emulator.Id, defaultEmulatorId))
                        {
                            if (!emulators.Select(emu => emu.Item1.Id).Contains(defaultEmulatorId) || emulatorPlatform.IsDefault)
                            {
                                emulators.Insert(0, (emulator, emulatorPlatform));
                            }
                            else
                            {
                                emulators.Add((emulator, emulatorPlatform));
                            }
                        }
                        else
                        {
                            emulators.Add((emulator, emulatorPlatform));
                        }
                    }
                }
            }

            return emulators;
        }

        /// <summary>
        /// Get the emulator title. If the emulator is RetroArch, also gets the core in use
        /// </summary>
        /// <param name="emulator"></param>
        /// <param name="emulatorPlatform"></param>
        /// <returns>The emulator title, plus the core where applicable.</returns>
        public static string GetEmulatorTitle(IEmulator emulator, IEmulatorPlatform emulatorPlatform)
        {
            string title = emulator.Title;

            if (string.Equals(emulator.Title, "Retroarch", StringComparison.InvariantCultureIgnoreCase))
            {
                try
                {
                    string corePath = emulatorPlatform.CommandLine.Split(new[] { ' ' })[1].Trim(new[] { '"' });
                    string core = Path.GetFileNameWithoutExtension(Path.GetFileName(corePath));
                    title = string.Format("{0} ({1})", title, core);
                }
                catch (Exception)
                {
                }
            }

            return title;
        }
    }
}
