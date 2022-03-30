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
                Logger.Log(string.Format("Archive Cache Manager plugin initialized ({0}).", CacheManager.VersionString));
                // Restore 7z in event Archive Cache Manager files are still in ThirdParty\7-Zip (caused by crash, etc)
                GameLaunching.Restore7z();
                // Remove any invalid entries from the cache (from failed or aborted launches, or game.ini changes)
                CacheManager.VerifyCacheIntegrity();
            }
            // Only perform the actions below in LaunchBox
            else if (eventType == "LaunchBoxStartupCompleted")
            {
                // Restore any overridden settings if LaunchBox closed before they could be restored on normal game launch
                LaunchBoxDataBackup.RestoreAllSettingsDelay(1000);

                if (Config.UpdateCheck == true)
                {
                    Updater.CheckForUpdate(2000);
                }
                // UpdateCheck will be null if the option has never been set before. Prompt the user to enable or disable update checks.
                else if (Config.UpdateCheck == null)
                {
                    Updater.EnableUpdateCheckPrompt(2000);
                }
            }
            else if (eventType == "LaunchBoxShutdownBeginning" || eventType == "BigBoxShutdownBeginning")
            {
                Logger.Log("LaunchBox / BigBox shutdown.");
                // Restore 7z in event Archive Cache Manager files are still in ThirdParty\7-Zip (caused by crash, etc)
                GameLaunching.Restore7z();
                // Restore any overridden settings if LaunchBox closed before they could be restored on normal game launch
                LaunchBoxDataBackup.RestoreAllSettings();
            }
        }
    }
}
