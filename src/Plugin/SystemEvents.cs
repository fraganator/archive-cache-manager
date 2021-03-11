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
                Logger.Log(string.Format("Archive Cache Manager plugin loaded ({0}).", CacheManager.Version));
                // Restore 7z in event Archive Cache Manager files are still in ThirdParty\7-Zip (caused by crash, etc)
                GameLaunching.Restore7z();
            }
            else if (eventType == "LaunchBoxShutdownBeginning" || eventType == "BigBoxShutdownBeginning")
            {
                Logger.Log(string.Format("LaunchBox / BigBox shutdown.", CacheManager.Version));
                // Restore 7z in event Archive Cache Manager files are still in ThirdParty\7-Zip (caused by crash, etc)
                GameLaunching.Restore7z();
            }
        }
    }
}
