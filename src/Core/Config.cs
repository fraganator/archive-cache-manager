using System;
using System.Collections.Generic;
using System.IO;
using IniParser;
using IniParser.Model;

namespace ArchiveCacheManager
{
    public class Config
    {
        public enum Action
        {
            Extract,
            Copy,
            ExtractCopy
        };

        public enum LaunchPath
        {
            Default,
            Title,
            Platform,
            Emulator
        };

        public enum M3uName
        {
            GameId,
            TitleVersion
        };

        private static readonly string configSection = "Archive Cache Manager";
        private static readonly string defaultCachePath = "ArchiveCache";
        private static readonly long defaultCacheSize = 20000;
        private static readonly long defaultMinArchiveSize = 100;
        private static readonly bool defaultMultiDisc = true;
        private static readonly bool defaultUseGameIdAsM3uFilename = true;
        private static readonly bool defaultSmartExtract = true;
        private static readonly string defaultStandaloneExtensions = "gb, gbc, gba, agb, nes, fds, smc, sfc, n64, z64, v64, ndd, md, smd, gen, iso, chd, rvn, gg, gcm, 32x, bin";
        private static readonly string defaultMetadataExtensions = "nfo, txt, dat, xml, json";
        private static readonly bool? defaultUpdateCheck = null;
        private static readonly string defaultEmulatorPlatform = @"All \ All";
        // Priorities determined by launching zip game from LaunchBox, where zip contains common rom and disc file types.
        // As matches were found, those file types were removed from the zip and the process repeated.
        // LaunchBox's priority list isn't documented anywhere, so this is a best guess. A more exhaustive list might look like:
        // cue, gdi, toc, nrg, ccd, mds, cdr, iso, eboot.bin, bin, img, mdf, chd, pbp
        // where disc metadata / table-of-contents types take priority over disc data types.
        private static readonly string defaultFilenamePriority = @"mds, gdi, cue, eboot.bin";

        private static readonly LaunchPath defaultLaunchPath = LaunchPath.Default;
        private static readonly Action defaultAction = Action.Extract;
        private static readonly M3uName defaultM3uName = M3uName.GameId;
        private static readonly bool defaultChdman = false;
        private static readonly bool defaultDolphinTool = false;

        public class EmulatorPlatformConfig
        {
            public string FilenamePriority;
            public Action Action;
            public LaunchPath LaunchPath;
            public bool MultiDisc;
            public M3uName M3uName;
            public bool SmartExtract;
            public bool Chdman;
            public bool DolphinTool;

            public EmulatorPlatformConfig()
            {
                FilenamePriority = defaultFilenamePriority;
                Action = defaultAction;
                LaunchPath = defaultLaunchPath;
                MultiDisc = defaultMultiDisc;
                M3uName = defaultM3uName;
                SmartExtract = defaultSmartExtract;
                Chdman = defaultChdman;
                DolphinTool = defaultDolphinTool;
            }
        };

        private static string mCachePath = defaultCachePath;
        private static long mCacheSize = defaultCacheSize;
        private static long mMinArchiveSize = defaultMinArchiveSize;
        private static bool mMultiDiscSupport = defaultMultiDisc;
        private static bool mUseGameIdAsM3uFilename = defaultUseGameIdAsM3uFilename;
        private static bool? mUpdateCheck = defaultUpdateCheck;
        private static string mStandaloneExtensions = defaultStandaloneExtensions;
        private static string mMetadataExtensions = defaultMetadataExtensions;

        private static Dictionary<string, EmulatorPlatformConfig> mEmulatorPlatformConfig;

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

        public static bool? UpdateCheck
        {
            get => mUpdateCheck;
            set => mUpdateCheck = value;
        }

        public static string StandaloneExtensions
        {
            get => mStandaloneExtensions;
            set => mStandaloneExtensions = value;
        }

        public static string MetadataExtensions
        {
            get => mMetadataExtensions;
            set => mMetadataExtensions = value;
        }

        public static Dictionary<string, EmulatorPlatformConfig> GetAllEmulatorPlatformConfig()
        {
            return mEmulatorPlatformConfig;
        }

        public static ref Dictionary<string, EmulatorPlatformConfig> GetAllEmulatorPlatformConfigByRef()
        {
            return ref mEmulatorPlatformConfig;
        }

        public static EmulatorPlatformConfig GetEmulatorPlatformConfig(string key)
        {
            try
            {
                return mEmulatorPlatformConfig[key];
            }
            catch (KeyNotFoundException) { }

            try
            {
                return mEmulatorPlatformConfig[defaultEmulatorPlatform];
            }
            catch (KeyNotFoundException) { }

            return new EmulatorPlatformConfig();
        }

        public static string GetFilenamePriority(string key)
        {
            try
            {
                return mEmulatorPlatformConfig[key].FilenamePriority;
            }
            catch (KeyNotFoundException) { }

            try
            {
                return mEmulatorPlatformConfig[defaultEmulatorPlatform].FilenamePriority;
            }
            catch (KeyNotFoundException) { }

            return defaultFilenamePriority;
        }

        public static Action GetAction(string key)
        {
            try
            {
                return mEmulatorPlatformConfig[key].Action;
            }
            catch (KeyNotFoundException) { }

            try
            {
                return mEmulatorPlatformConfig[defaultEmulatorPlatform].Action;
            }
            catch (KeyNotFoundException) { }

            return defaultAction;
        }

        public static LaunchPath GetLaunchPath(string key)
        {
            try
            {
                return mEmulatorPlatformConfig[key].LaunchPath;
            }
            catch (KeyNotFoundException) { }

            try
            {
                return mEmulatorPlatformConfig[defaultEmulatorPlatform].LaunchPath;
            }
            catch (KeyNotFoundException) { }

            return defaultLaunchPath;
        }

        public static bool GetMultiDisc(string key)
        {
            try
            {
                return mEmulatorPlatformConfig[key].MultiDisc;
            }
            catch (KeyNotFoundException) { }

            try
            {
                return mEmulatorPlatformConfig[defaultEmulatorPlatform].MultiDisc;
            }
            catch (KeyNotFoundException) { }

            return defaultMultiDisc;
        }

        public static M3uName GetM3uName(string key)
        {
            try
            {
                return mEmulatorPlatformConfig[key].M3uName;
            }
            catch (KeyNotFoundException) { }

            try
            {
                return mEmulatorPlatformConfig[defaultEmulatorPlatform].M3uName;
            }
            catch (KeyNotFoundException) { }

            return defaultM3uName;
        }

        public static bool GetSmartExtract(string key)
        {
            try
            {
                return mEmulatorPlatformConfig[key].SmartExtract;
            }
            catch (KeyNotFoundException) { }

            try
            {
                return mEmulatorPlatformConfig[defaultEmulatorPlatform].SmartExtract;
            }
            catch (KeyNotFoundException) { }

            return defaultSmartExtract;
        }

        public static bool GetChdman(string key)
        {
            try
            {
                return mEmulatorPlatformConfig[key].Chdman;
            }
            catch (KeyNotFoundException) { }

            try
            {
                return mEmulatorPlatformConfig[defaultEmulatorPlatform].Chdman;
            }
            catch (KeyNotFoundException) { }

            return defaultChdman;
        }

        public static bool GetDolphinTool(string key)
        {
            try
            {
                return mEmulatorPlatformConfig[key].DolphinTool;
            }
            catch (KeyNotFoundException) { }

            try
            {
                return mEmulatorPlatformConfig[defaultEmulatorPlatform].DolphinTool;
            }
            catch (KeyNotFoundException) { }

            return defaultDolphinTool;
        }

        public static string EmulatorPlatformKey(string emulator, string platform) => string.Format(@"{0} \ {1}", emulator, platform);

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

                    mEmulatorPlatformConfig.Clear();
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

                            if (section.Keys.ContainsKey(nameof(UpdateCheck)))
                            {
                                mUpdateCheck = Convert.ToBoolean(section.Keys[nameof(UpdateCheck)]);
                            }
                            else
                            {
                                // Set this null to indicate the option has never been set.
                                mUpdateCheck = null;
                            }

                            if (section.Keys.ContainsKey(nameof(StandaloneExtensions)))
                            {
                                mStandaloneExtensions = section.Keys[nameof(StandaloneExtensions)];
                            }

                            if (section.Keys.ContainsKey(nameof(MetadataExtensions)))
                            {
                                mMetadataExtensions = section.Keys[nameof(MetadataExtensions)];
                            }


                            if (section.Keys.ContainsKey("MultiDiscSupport"))
                            {
                                mMultiDiscSupport = Convert.ToBoolean(section.Keys["MultiDiscSupport"]);
                            }

                            if (section.Keys.ContainsKey("UseGameIdAsM3uFilename"))
                            {
                                mUseGameIdAsM3uFilename = Convert.ToBoolean(section.Keys["UseGameIdAsM3uFilename"]);
                            }
                        }
                        else
                        {
                            // If this is the first time we've seen this section ("emulator \ platform" pair), create the EmulatorPlatformConfig object
                            if (!mEmulatorPlatformConfig.ContainsKey(section.SectionName))
                            {
                                mEmulatorPlatformConfig.Add(section.SectionName, new EmulatorPlatformConfig());
                            }

                            if (section.Keys.ContainsKey(nameof(EmulatorPlatformConfig.FilenamePriority)))
                            {
                                mEmulatorPlatformConfig[section.SectionName].FilenamePriority = section.Keys[nameof(EmulatorPlatformConfig.FilenamePriority)];
                            }
                            else if (section.Keys.ContainsKey("ExtensionPriority"))
                            {
                                mEmulatorPlatformConfig[section.SectionName].FilenamePriority = section.Keys["ExtensionPriority"];
                            }
                            else if (section.Keys.ContainsKey("extensionPriority"))
                            {
                                mEmulatorPlatformConfig[section.SectionName].FilenamePriority = section.Keys["extensionPriority"];
                            }

                            if (section.Keys.ContainsKey(nameof(EmulatorPlatformConfig.Action)))
                            {
                                Enum.TryParse(section.Keys[nameof(EmulatorPlatformConfig.Action)], out mEmulatorPlatformConfig[section.SectionName].Action);
                            }

                            if (section.Keys.ContainsKey(nameof(EmulatorPlatformConfig.LaunchPath)))
                            {
                                Enum.TryParse(section.Keys[nameof(EmulatorPlatformConfig.LaunchPath)], out mEmulatorPlatformConfig[section.SectionName].LaunchPath);
                            }

                            if (section.Keys.ContainsKey(nameof(EmulatorPlatformConfig.MultiDisc)))
                            {
                                mEmulatorPlatformConfig[section.SectionName].MultiDisc = Convert.ToBoolean(section.Keys[nameof(EmulatorPlatformConfig.MultiDisc)]);
                            }
                            else
                            {
                                mEmulatorPlatformConfig[section.SectionName].MultiDisc = mMultiDiscSupport;
                            }

                            if (section.Keys.ContainsKey(nameof(EmulatorPlatformConfig.M3uName)))
                            {
                                Enum.TryParse(section.Keys[nameof(EmulatorPlatformConfig.M3uName)], out mEmulatorPlatformConfig[section.SectionName].M3uName);
                            }
                            else
                            {
                                mEmulatorPlatformConfig[section.SectionName].M3uName = mUseGameIdAsM3uFilename ? M3uName.GameId : M3uName.TitleVersion;
                            }

                            if (section.Keys.ContainsKey(nameof(EmulatorPlatformConfig.SmartExtract)))
                            {
                                mEmulatorPlatformConfig[section.SectionName].SmartExtract = Convert.ToBoolean(section.Keys[nameof(EmulatorPlatformConfig.SmartExtract)]);
                            }

                            if (section.Keys.ContainsKey(nameof(EmulatorPlatformConfig.Chdman)))
                            {
                                mEmulatorPlatformConfig[section.SectionName].Chdman = Convert.ToBoolean(section.Keys[nameof(EmulatorPlatformConfig.Chdman)]);
                            }

                            if (section.Keys.ContainsKey(nameof(EmulatorPlatformConfig.DolphinTool)))
                            {
                                mEmulatorPlatformConfig[section.SectionName].DolphinTool = Convert.ToBoolean(section.Keys[nameof(EmulatorPlatformConfig.DolphinTool)]);
                            }
                        }
                    }

                    // Check if the [All \ All] section exists.
                    if (!iniData.Sections.ContainsSection(defaultEmulatorPlatform))
                    {
                        if (!mEmulatorPlatformConfig.ContainsKey(defaultEmulatorPlatform))
                        {
                            mEmulatorPlatformConfig.Add(defaultEmulatorPlatform, new EmulatorPlatformConfig());
                        }
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
            if (mUpdateCheck != null)
            {
                iniData[configSection][nameof(UpdateCheck)] = mUpdateCheck.ToString();
            }
            iniData[configSection][nameof(StandaloneExtensions)] = mStandaloneExtensions;
            iniData[configSection][nameof(MetadataExtensions)] = mMetadataExtensions;

            foreach (KeyValuePair<string, EmulatorPlatformConfig> priority in mEmulatorPlatformConfig)
            {
                iniData[priority.Key][nameof(EmulatorPlatformConfig.FilenamePriority)] = priority.Value.FilenamePriority;
                iniData[priority.Key][nameof(EmulatorPlatformConfig.Action)] = priority.Value.Action.ToString();
                iniData[priority.Key][nameof(EmulatorPlatformConfig.LaunchPath)] = priority.Value.LaunchPath.ToString();
                iniData[priority.Key][nameof(EmulatorPlatformConfig.MultiDisc)] = priority.Value.MultiDisc.ToString();
                iniData[priority.Key][nameof(EmulatorPlatformConfig.M3uName)] = priority.Value.M3uName.ToString();
                iniData[priority.Key][nameof(EmulatorPlatformConfig.SmartExtract)] = priority.Value.SmartExtract.ToString();
                iniData[priority.Key][nameof(EmulatorPlatformConfig.Chdman)] = priority.Value.Chdman.ToString();
                iniData[priority.Key][nameof(EmulatorPlatformConfig.DolphinTool)] = priority.Value.DolphinTool.ToString();
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
            mStandaloneExtensions = defaultStandaloneExtensions;
            mMetadataExtensions = defaultMetadataExtensions;

            mEmulatorPlatformConfig = new Dictionary<string, EmulatorPlatformConfig>();
            mEmulatorPlatformConfig.Add(defaultEmulatorPlatform, new EmulatorPlatformConfig());
            EmulatorPlatformConfig e = new EmulatorPlatformConfig();
            e.FilenamePriority = "bin, iso";
            mEmulatorPlatformConfig.Add(@"PCSX2 \ Sony Playstation 2", e);
        }
    }
}
