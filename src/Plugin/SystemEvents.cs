using System.Diagnostics;
using Unbroken.LaunchBox.Plugins;

namespace ArchiveCacheManager
{
    class SystemEvents : ISystemEventsPlugin
    {
        public void OnEventRaised(string eventType)
        {
            if (eventType == "PluginInitialized")
            {
                // Debugger.Launch();

                Logger.Init();
                Logger.Log("-------- PLUGIN INITIALIZED --------");
                Logger.Log(string.Format("Archive Cache Manager plugin initialized ({0}).", CacheManager.Version));
                // Restore 7z in event Archive Cache Manager files are still in ThirdParty\7-Zip (caused by crash, etc)
                GameLaunching.Restore7z();
                // Restore any overridden settings if LaunchBox crashed before they could be restored on normal game launch
                LaunchBoxDataBackup.RestoreAllSettings();
                // Remove any invalid entries from the cache (from failed or aborted launches, or game.ini changes)
                CacheManager.VerifyCacheIntegrity();
            }
            else if (eventType == "LaunchBoxShutdownBeginning" || eventType == "BigBoxShutdownBeginning")
            {
                Logger.Log("LaunchBox / BigBox shutdown.");
                // Restore 7z in event Archive Cache Manager files are still in ThirdParty\7-Zip (caused by crash, etc)
                GameLaunching.Restore7z();
            }
        }
    }
}
