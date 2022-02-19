using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unbroken.LaunchBox.Plugins;
using Unbroken.LaunchBox.Plugins.Data;

namespace ArchiveCacheManager
{
    static class PluginUtils
    {
        public static bool GetEmulatorPlatformAutoExtract(string emulatorId, string platformName)
        {
            var emulator = PluginHelper.DataManager.GetEmulatorById(emulatorId);
            var emulatorPlatform = Array.Find(emulator.GetAllEmulatorPlatforms(), p => p.Platform.Equals(platformName));

            // emulatorPlatform.AutoExtract will be null if the Emulator settings haven't been changed since updating to LaunchBox 12.8
            // So perform two checks to determine if AutoExtract is true, one at the emulator level, and one at the emulatorPlatform level
            return (emulator.AutoExtract && emulatorPlatform.AutoExtract == null) || (emulatorPlatform.AutoExtract == true);
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
    }
}
