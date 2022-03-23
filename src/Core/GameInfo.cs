using System;
using System.Collections.Generic;
using System.IO;
using IniParser;
using IniParser.Model;

namespace ArchiveCacheManager
{
    public class GameInfo
    {
        private bool mInfoLoaded = false;
        private string mGameInfoPath = string.Empty;

        private string mGameId = string.Empty;
        private string mArchivePath = string.Empty;
        private string mEmulator = string.Empty;
        private string mPlatform = string.Empty;
        private string mTitle = string.Empty;
        private string mVersion = string.Empty;
        private string mSelectedFile = string.Empty;
        private long mDecompressedSize = 0;
        private bool mKeepInCache = false;
        private bool mEmulatorPlatformM3u = false;
        private bool mMultiDisc = false;
        private int mTotalDiscs = 0;
        private int mSelectedDisc = 0;
        private List<DiscInfo> mDiscs = new List<DiscInfo>();

        private static readonly string gameSection = "Game";
        private static readonly string discSection = "Disc";

        /// <summary>
        /// True if GameInfo is loaded and valid, False otherwise.
        /// </summary>
        public bool InfoLoaded
        {
            get => mInfoLoaded;
        }
        public string GameId
        {
            get => mGameId;
            set => mGameId = value;
        }
        public string ArchivePath
        {
            get => mArchivePath;
            set => mArchivePath = value;
        }
        public string Emulator
        {
            get => mEmulator;
            set => mEmulator = value;
        }
        public string Platform
        {
            get => mPlatform;
            set => mPlatform = value;
        }
        public string Title
        {
            get => mTitle;
            set => mTitle = value;
        }
        public string Version
        {
            get => mVersion;
            set => mVersion = value;
        }
        public string SelectedFile
        {
            get => mSelectedFile;
            set => mSelectedFile = value;
        }
        public long DecompressedSize
        {
            get => mDecompressedSize;
            set => mDecompressedSize = value;
        }
        public bool KeepInCache
        {
            get => mKeepInCache;
            set => mKeepInCache = value;
        }
        public bool EmulatorPlatformM3u
        {
            get => mEmulatorPlatformM3u;
            set => mEmulatorPlatformM3u = value;
        }
        public bool MultiDisc
        {
            get => mMultiDisc;
            set => mMultiDisc = value;
        }
        public int TotalDiscs
        {
            get => mTotalDiscs;
            set => mTotalDiscs = value;
        }
        public int SelectedDisc
        {
            get => mSelectedDisc;
            set => mSelectedDisc = value;
        }
        public List<DiscInfo> Discs
        {
            get => mDiscs;
            set => mDiscs = value;
        }

        public GameInfo(string gameInfoPath)
        {
            mGameInfoPath = gameInfoPath;
            Load();
        }

        public GameInfo(GameInfo game)
        {
            mInfoLoaded = game.mInfoLoaded;
            mGameInfoPath = game.mGameInfoPath;
            mGameId = game.mGameId;
            mArchivePath = game.mArchivePath;
            mEmulator = game.mEmulator;
            mPlatform = game.mPlatform;
            mTitle = game.mTitle;
            mVersion = game.mVersion;
            mSelectedFile = game.mSelectedFile;
            mDecompressedSize = game.mDecompressedSize;
            mKeepInCache = game.mKeepInCache;
            mEmulatorPlatformM3u = game.mEmulatorPlatformM3u;
            mMultiDisc = game.mMultiDisc;
            mTotalDiscs = game.mTotalDiscs;
            mSelectedDisc = game.mSelectedDisc;
            mDiscs = new List<DiscInfo>(game.mDiscs);
        }

        /// <summary>
        /// Loads some basic information about the game being launched.
        /// </summary>
        /// <returns>True if loaded successfully, false otherwise.</returns>
        public bool Load()
        {
            mInfoLoaded = false;

            if (File.Exists(mGameInfoPath))
            {
                try
                {
                    var parser = new FileIniDataParser();
                    IniData iniData = parser.ReadFile(mGameInfoPath);

                    mGameId = iniData[gameSection][nameof(GameId)];
                    mArchivePath = iniData[gameSection][nameof(ArchivePath)];
                    mEmulator = iniData[gameSection][nameof(Emulator)];
                    mPlatform = iniData[gameSection][nameof(Platform)];
                    mTitle = iniData[gameSection][nameof(Title)];
                    mVersion = iniData[gameSection][nameof(Version)];
                    mSelectedFile = iniData[gameSection][nameof(SelectedFile)];
                    mDecompressedSize = Convert.ToInt64(iniData[gameSection][nameof(DecompressedSize)]);
                    mKeepInCache = Convert.ToBoolean(iniData[gameSection][nameof(KeepInCache)]);
                    mEmulatorPlatformM3u = Convert.ToBoolean(iniData[gameSection][nameof(EmulatorPlatformM3u)]);
                    mMultiDisc = Convert.ToBoolean(iniData[gameSection][nameof(MultiDisc)]);
                    mTotalDiscs = Convert.ToInt32(iniData[gameSection][nameof(TotalDiscs)]);
                    mSelectedDisc = Convert.ToInt32(iniData[gameSection][nameof(SelectedDisc)]);

                    foreach (SectionData sectionData in iniData.Sections)
                    {
                        if (sectionData.SectionName.StartsWith(discSection, StringComparison.InvariantCultureIgnoreCase))
                        {
                            DiscInfo discInfo = new DiscInfo();
                            discInfo.ApplicationId = sectionData.Keys[nameof(DiscInfo.ApplicationId)];
                            discInfo.ArchivePath = sectionData.Keys[nameof(DiscInfo.ArchivePath)];
                            discInfo.Version = sectionData.Keys[nameof(DiscInfo.Version)];
                            discInfo.Disc = Convert.ToInt32(sectionData.Keys[nameof(DiscInfo.Disc)]);
                            mDiscs.Add(discInfo);
                        }
                    }
                    mDiscs.Sort((a, b) => a.Disc - b.Disc);

                    // Check validity of what was loaded.
                    // All the items here are required for the GameInfo to be valid.
                    mInfoLoaded = !(/*string.IsNullOrEmpty(mGameId) ||*/
                                    string.IsNullOrEmpty(mArchivePath) ||
                                    string.IsNullOrEmpty(mEmulator) ||
                                    string.IsNullOrEmpty(mPlatform) ||
                                    string.IsNullOrEmpty(mTitle) ||
                                    mTotalDiscs != mDiscs.Count);
                }
                catch (Exception e)
                {
                    Logger.Log(e.ToString(), Logger.LogLevel.Exception);
                }
            }

            return mInfoLoaded;
        }

        /// <summary>
        /// Saves basic information about the game being launched by LaunchBox.
        /// </summary>
        /// <param name="savePath"></param>
        /// <returns>True if saved successfully, false otherwise.</returns>
        public bool Save(string savePath = null)
        {
            savePath = savePath ?? mGameInfoPath;
            var parser = new FileIniDataParser();
            IniData iniData = new IniData();

            try
            {
                iniData[gameSection][nameof(GameId)] = mGameId;
                iniData[gameSection][nameof(ArchivePath)] = mArchivePath;
                iniData[gameSection][nameof(Emulator)] = mEmulator;
                iniData[gameSection][nameof(Platform)] = mPlatform;
                iniData[gameSection][nameof(Title)] = mTitle;
                iniData[gameSection][nameof(Version)] = mVersion;
                iniData[gameSection][nameof(SelectedFile)] = mSelectedFile;
                iniData[gameSection][nameof(DecompressedSize)] = mDecompressedSize.ToString();
                iniData[gameSection][nameof(KeepInCache)] = mKeepInCache.ToString();
                iniData[gameSection][nameof(EmulatorPlatformM3u)] = mEmulatorPlatformM3u.ToString();
                iniData[gameSection][nameof(MultiDisc)] = mMultiDisc.ToString();
                if (mMultiDisc)
                {
                    iniData[gameSection][nameof(TotalDiscs)] = mTotalDiscs.ToString();
                    iniData[gameSection][nameof(SelectedDisc)] = mSelectedDisc.ToString();

                    foreach (DiscInfo discInfo in mDiscs)
                    {
                        string discNumberSection = string.Format("{0}{1}", discSection, discInfo.Disc);

                        iniData[discNumberSection][nameof(DiscInfo.ApplicationId)] = discInfo.ApplicationId;
                        iniData[discNumberSection][nameof(DiscInfo.ArchivePath)] = discInfo.ArchivePath;
                        iniData[discNumberSection][nameof(DiscInfo.Version)] = discInfo.Version;
                        iniData[discNumberSection][nameof(DiscInfo.Disc)] = discInfo.Disc.ToString();
                    }
                }

                parser.WriteFile(savePath, iniData);

                return true;
            }
            catch (Exception e)
            {
                Logger.Log(e.ToString(), Logger.LogLevel.Exception);
            }

            return false;
        }

        /// <summary>
        /// Merges a GameInfo object from the archive cache into this object. Will merge DecompressedSize and KeepInCache.
        /// </summary>
        /// <param name="cacheGameInfo"></param>
        public void MergeCacheInfo(GameInfo cacheGameInfo)
        {
            mDecompressedSize = cacheGameInfo.DecompressedSize;
            mKeepInCache = cacheGameInfo.KeepInCache;
        }
    }

    public class DiscInfo
    {
        private string mApplicationId = string.Empty;
        private string mArchivePath = string.Empty;
        private string mVersion = string.Empty;
        private int mDisc = 0;

        public string ApplicationId
        {
            get => mApplicationId;
            set => mApplicationId = value;
        }
        public string ArchivePath
        {
            get => mArchivePath;
            set => mArchivePath = value;
        }
        public string Version
        {
            get => mVersion;
            set => mVersion = value;
        }
        public int Disc
        {
            get => mDisc;
            set => mDisc = value;
        }

        public DiscInfo()
        {
        }

        public DiscInfo(DiscInfo disc)
        {
            mApplicationId = disc.mApplicationId;
            mArchivePath = disc.mArchivePath;
            mVersion = disc.mVersion;
            mDisc = disc.mDisc;
        }
    }
}
