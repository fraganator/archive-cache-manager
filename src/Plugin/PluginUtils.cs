using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        public static bool GetEmulatorPlatformAutoExtract(string emulatorId, string platformName)
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
        /// Check if the launched game is a compressed archive based on the file extension.
        /// Extensions checked are zip, 7z, rar.
        /// </summary>
        /// <param name="applicationPath"></param>
        /// <returns></returns>
        public static bool IsApplicationPathCompressedArchive(string applicationPath)
        {
            string[] archiveExtensions = { "zip", "7z", "rar" };
            string extension = PathUtils.GetExtension(applicationPath);
            return archiveExtensions.Contains(extension);
        }

        /// <summary>
        /// Get info on a multi-disc game.
        /// </summary>
        /// <param name="game"></param>
        /// <param name="app"></param>
        /// <param name="totalDiscs">The total number of discs in a game. Determined by counting additional apps with Disc property set. Will be 0 if not a multi-dsc game.</param>
        /// <param name="selectedDisc">The selected disc, based on the additional app Disc property. Will be 1 if additional app is null, and 0 if not a multi-disc game.</param>
        /// <param name="discs">List of discs and associated info in disc order. Will be empty if not a multi-disc game.</param>
        /// <returns>True if multi-disc info populated, False otherwise.</returns>
        public static bool GetMultiDiscInfo(IGame game, IAdditionalApplication app, ref int totalDiscs, ref int selectedDisc, ref List<DiscInfo> discs)
        {
            if (!IsLaunchedGameMultiDisc(game, app))
            {
                totalDiscs = 0;
                selectedDisc = 0;
                discs.Clear();
                return false;
            }

            var additionalApps = game.GetAllAdditionalApplications();
            var discApps = Array.FindAll(additionalApps, a => a.Disc != null);
            foreach (var discApp in discApps)
            {
                DiscInfo discInfo = new DiscInfo();
                discInfo.ApplicationId = discApp.Id;
                discInfo.ArchivePath = PathUtils.GetAbsolutePath(discApp.ApplicationPath);
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
            return true;
        }
    }
}
