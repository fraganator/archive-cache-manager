using System;
using System.Collections.Generic;
using System.IO;
using IniParser;
using IniParser.Model;

namespace ArchiveCacheManager
{
    public class Config
    {
        private static readonly string configSection = "Archive Cache Manager";
        private static readonly string defaultCachePath = "ArchiveCache";
        private static readonly long defaultCacheSize = 20000;
        private static readonly long defaultMinArchiveSize = 100;

        private static string mCachePath = defaultCachePath;
        private static long mCacheSize = defaultCacheSize;
        private static long mMinArchiveSize = defaultMinArchiveSize;
        private static Dictionary<string, string> mExtensionPriority;

        /// <summary>
        /// Static constructor which loads config from disk into memory.
        /// </summary>
        static Config()
        {
            SetDefaultConfig();
            LoadConfig();
        }

        /// <summary>
        /// Configured cache path, relative to LaunchBox folder or absolute. Default is ArchiveCache.
        /// </summary>
        public static string CachePath
        {
            get => mCachePath;
            set => mCachePath = value;
        }

        /// <summary>
        /// Configured cache size in megabytes. Default is 20000.
        /// </summary>
        public static long CacheSize
        {
            get => mCacheSize;
            set => mCacheSize = value;
        }

        /// <summary>
        /// Configured minimum archive size in megabytes. Default is 100.
        /// </summary>
        public static long MinArchiveSize
        {
            get => mMinArchiveSize;
            set => mMinArchiveSize = value;
        }

        /// <summary>
        /// Configured extension priorities. Default is "PCSX2 \ Sony Playstaion 2", "bin"
        /// </summary>
        public static Dictionary<string, string> ExtensionPriority
        {
            get => mExtensionPriority;
            set => mExtensionPriority = value;
        }

        /// <summary>
        /// Load the config into memory from the config file on disk. Will save new config file to disk if there was a error loading the config.
        /// </summary>
        public static void LoadConfig()
        {
            bool configMissing = false;

            if (File.Exists(PathUtils.GetPluginConfigPath()))
            {
                var parser = new FileIniDataParser();
                IniData iniData = new IniData();

                try
                {
                    iniData = parser.ReadFile(PathUtils.GetPluginConfigPath());
                }
                catch (Exception e)
                {
                    Logger.Log(string.Format("Error reading config file from {0}.", PathUtils.GetPluginConfigPath()));
                    Logger.Log(e.ToString(), Logger.LogLevel.Exception);
                }

                ExtensionPriority.Clear();
                foreach (SectionData section in iniData.Sections)
                {
                    if (section.SectionName == configSection)
                    {
                        if (section.Keys.ContainsKey(nameof(CachePath)))
                        {
                            mCachePath = iniData[configSection][nameof(CachePath)];
                        }
                        // Older config file version used lower case first letter
                        else if (section.Keys.ContainsKey("cachePath"))
                        {
                            mCachePath = iniData[configSection]["cachePath"];
                        }
                        // Double check cache path
                        if (string.IsNullOrEmpty(mCachePath.Trim()))
                        {
                            Logger.Log("Config CachePath not found, using default.");
                            mCachePath = defaultCachePath;
                            configMissing |= true;
                        }

                        if (section.Keys.ContainsKey(nameof(CacheSize)))
                        {
                            mCacheSize = Convert.ToInt64(iniData[configSection][nameof(CacheSize)]);
                        }
                        // Older config file version used lower case first letter
                        else if (section.Keys.ContainsKey("cacheSize"))
                        {
                            mCacheSize = Convert.ToInt64(iniData[configSection]["cacheSize"]);
                        }
                        // CacheSize must be larger than 0
                        if (mCacheSize <= 0)
                        {
                            Logger.Log("Config CacheSize not found, using default.");
                            mCacheSize = defaultCacheSize;
                            configMissing |= true;
                        }

                        if (section.Keys.ContainsKey(nameof(MinArchiveSize)))
                        {
                            mMinArchiveSize = Convert.ToInt64(iniData[configSection][nameof(MinArchiveSize)]);
                        }
                        // Older config file version used lower case first letter
                        else if (section.Keys.ContainsKey("minArchiveSize"))
                        {
                            mMinArchiveSize = Convert.ToInt64(iniData[configSection]["minArchiveSize"]);
                        }
                        // MinArchiveSize can be zero
                        if (mMinArchiveSize < 0)
                        {
                            Logger.Log("Config MinArchiveSize not found, using default.");
                            mMinArchiveSize = defaultMinArchiveSize;
                            configMissing |= true;
                        }
                    }
                    else
                    {
                        if (section.Keys.ContainsKey(nameof(ExtensionPriority)))
                        {
                            ExtensionPriority.Add(section.SectionName, iniData[section.SectionName][nameof(ExtensionPriority)]);
                        }
                        else if (section.Keys.ContainsKey("extensionPriority"))
                        {
                            ExtensionPriority.Add(section.SectionName, iniData[section.SectionName]["extensionPriority"]);
                        }
                    }
                }
                
            }
            else
            {
                Logger.Log("Config file does not exist, using default config.");
                SetDefaultConfig();
                configMissing |= true;
            }

            if (configMissing)
            {
                SaveConfig();
            }
        }

        /// <summary>
        /// Save current config to config file on disk.
        /// </summary>
        public static void SaveConfig()
        {
            var parser = new FileIniDataParser();
            IniData iniData = new IniData();

            iniData[configSection][nameof(CachePath)] = mCachePath;
            iniData[configSection][nameof(CacheSize)] = mCacheSize.ToString();
            iniData[configSection][nameof(MinArchiveSize)] = mMinArchiveSize.ToString();

            foreach (KeyValuePair<string, string> priority in mExtensionPriority)
            {
                iniData[priority.Key][nameof(ExtensionPriority)] = priority.Value;
            }

            try
            {
                parser.WriteFile(PathUtils.GetPluginConfigPath(), iniData);
            }
            catch (Exception e)
            {
                Logger.Log(string.Format("Error saving config file to {0}.", PathUtils.GetPluginConfigPath()));
                Logger.Log(e.ToString(), Logger.LogLevel.Exception);
            }
        }

        /// <summary>
        /// Initialise internal variables to defaults.
        /// </summary>
        private static void SetDefaultConfig()
        {
            mCachePath = defaultCachePath;
            mCacheSize = defaultCacheSize;
            mMinArchiveSize = defaultMinArchiveSize;
            mExtensionPriority = new Dictionary<string, string>();
            mExtensionPriority.Add(@"PCSX2 \ Sony Playstation 2", "bin, iso");
        }
    }
}
