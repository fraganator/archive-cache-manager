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
        private static readonly bool defaultMultiDiscSupport = true;
        private static readonly string defaultFilenamePrioritySection = @"All \ All";
        // Priorities determined by launching zip game from LaunchBox, where zip contains common rom and disc file types.
        // As matches were found, those file types were removed from the zip and the process repeated.
        // LaunchBox's priority list isn't documented anywhere, so this is a best guess. A more exhaustive list might look like:
        // cue, gdi, toc, nrg, ccd, mds, cdr, iso, eboot.bin, bin, img, mdf, chd, pbp
        // where disc metadata / table-of-contents types take priority over disc data types.
        private static readonly string defaultFilenamePriority = @"mds, gdi, cue, eboot.bin";

        private static string mCachePath = defaultCachePath;
        private static long mCacheSize = defaultCacheSize;
        private static long mMinArchiveSize = defaultMinArchiveSize;
        private static Dictionary<string, string> mFilenamePriority;
        private static bool mMultiDiscSupport = defaultMultiDiscSupport;

        /// <summary>
        /// Static constructor which loads config from disk into memory.
        /// </summary>
        static Config()
        {
            SetDefaultConfig();
            Load();
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
        public static Dictionary<string, string> FilenamePriority
        {
            get => mFilenamePriority;
            set => mFilenamePriority = value;
        }

        /// <summary>
        /// Configured multi-disc support. Default is True.
        /// </summary>
        public static bool MultiDiscSupport
        {
            get => mMultiDiscSupport;
            set => mMultiDiscSupport = value;
        }

        /// <summary>
        /// Load the config into memory from the config file on disk. Will save new config file to disk if there was a error loading the config.
        /// </summary>
        public static void Load()
        {
            bool configMissing = false;

            if (File.Exists(PathUtils.GetPluginConfigPath()))
            {
                var parser = new FileIniDataParser();
                IniData iniData = new IniData();

                try
                {
                    iniData = parser.ReadFile(PathUtils.GetPluginConfigPath());

                    FilenamePriority.Clear();
                    foreach (SectionData section in iniData.Sections)
                    {
                        if (section.SectionName == configSection)
                        {
                            if (section.Keys.ContainsKey(nameof(CachePath)))
                            {
                                mCachePath = section.Keys[nameof(CachePath)];
                            }
                            // Older config file version used lower case first letter
                            else if (section.Keys.ContainsKey("cachePath"))
                            {
                                mCachePath = section.Keys["cachePath"];
                            }

                            if (section.Keys.ContainsKey(nameof(CacheSize)))
                            {
                                mCacheSize = Convert.ToInt64(section.Keys[nameof(CacheSize)]);
                            }
                            // Older config file version used lower case first letter
                            else if (section.Keys.ContainsKey("cacheSize"))
                            {
                                mCacheSize = Convert.ToInt64(section.Keys["cacheSize"]);
                            }


                            if (section.Keys.ContainsKey(nameof(MinArchiveSize)))
                            {
                                mMinArchiveSize = Convert.ToInt64(section.Keys[nameof(MinArchiveSize)]);
                            }
                            // Older config file version used lower case first letter
                            else if (section.Keys.ContainsKey("minArchiveSize"))
                            {
                                mMinArchiveSize = Convert.ToInt64(section.Keys["minArchiveSize"]);
                            }

                            if (section.Keys.ContainsKey(nameof(MultiDiscSupport)))
                            {
                                mMultiDiscSupport = Convert.ToBoolean(section.Keys[nameof(MultiDiscSupport)]);
                            }
                        }
                        else
                        {
                            if (section.Keys.ContainsKey(nameof(FilenamePriority)))
                            {
                                FilenamePriority.Add(section.SectionName, section.Keys[nameof(FilenamePriority)]);
                            }
                            else if (section.Keys.ContainsKey("ExtensionPriority"))
                            {
                                FilenamePriority.Add(section.SectionName, section.Keys["ExtensionPriority"]);
                            }
                            else if (section.Keys.ContainsKey("extensionPriority"))
                            {
                                FilenamePriority.Add(section.SectionName, section.Keys["extensionPriority"]);
                            }
                        }
                    }

                    // Check if the [All \ All] section exists.
                    if (!iniData.Sections.ContainsSection(defaultFilenamePrioritySection))
                    {
                        FilenamePriority.Add(defaultFilenamePrioritySection, defaultFilenamePriority);
                        configMissing |= true;
                    }
                }
                catch (Exception e)
                {
                    Logger.Log(string.Format("Error parsing config file from {0}. Using default config.", PathUtils.GetPluginConfigPath()));
                    Logger.Log(e.ToString(), Logger.LogLevel.Exception);
                    SetDefaultConfig();
                    configMissing |= true;
                }

                if (!PathUtils.IsPathSafe(mCachePath))
                {
                    Logger.Log(string.Format("Config CachePath can not be set to \"{0}\", using default ({1}).", mCachePath, defaultCachePath));
                    mCachePath = defaultCachePath;
                    configMissing |= true;
                }
                // CacheSize must be larger than 0
                if (mCacheSize <= 0)
                {
                    Logger.Log(string.Format("Config CacheSize can not be less than or equal 0, using default ({0:n0}).", defaultCacheSize));
                    mCacheSize = defaultCacheSize;
                    configMissing |= true;
                }
                // MinArchiveSize can be zero
                if (mMinArchiveSize < 0)
                {
                    Logger.Log(string.Format("Config MinArchiveSize can not be less than 0, using default ({0:n0}).", defaultMinArchiveSize));
                    mMinArchiveSize = defaultMinArchiveSize;
                    configMissing |= true;
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
                Save();
            }
        }

        /// <summary>
        /// Save current config to config file on disk.
        /// </summary>
        public static void Save()
        {
            var parser = new FileIniDataParser();
            IniData iniData = new IniData();

            iniData[configSection][nameof(CachePath)] = mCachePath;
            iniData[configSection][nameof(CacheSize)] = mCacheSize.ToString();
            iniData[configSection][nameof(MinArchiveSize)] = mMinArchiveSize.ToString();
            iniData[configSection][nameof(MultiDiscSupport)] = mMultiDiscSupport.ToString();

            foreach (KeyValuePair<string, string> priority in mFilenamePriority)
            {
                iniData[priority.Key][nameof(FilenamePriority)] = priority.Value;
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
            mFilenamePriority = new Dictionary<string, string>();
            mFilenamePriority.Add(defaultFilenamePrioritySection, defaultFilenamePriority);
            mFilenamePriority.Add(@"PCSX2 \ Sony Playstation 2", "bin, iso");
            mMultiDiscSupport = defaultMultiDiscSupport;
        }
    }
}
