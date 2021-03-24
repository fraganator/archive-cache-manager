using System;
using System.IO;
using IniParser;
using IniParser.Model;

namespace ArchiveCacheManager
{
    public class GameInfo
    {
        private bool mInfoLoaded = false;
        private string mGameInfoPath = string.Empty;
        private string mArchivePath = string.Empty;
        private string mEmulator = string.Empty;
        private string mPlatform = string.Empty;
        private string mTitle = string.Empty;
        private string mPlayRomInArchive = string.Empty;
        private long mDecompressedSize = 0;
        private bool mKeepInCache = false;

        private static readonly string section = "Game";

        public bool InfoLoaded
        {
            get => mInfoLoaded;
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
        public string PlayRomInArchive
        {
            get => mPlayRomInArchive;
            set => mPlayRomInArchive = value;
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

        public GameInfo(string gameInfoPath)
        {
            mGameInfoPath = gameInfoPath;
            mInfoLoaded = Load();
        }

        /// <summary>
        /// Loads some basic information about the game being launched.
        /// </summary>
        /// <returns>True if loaded successfully, false otherwise.</returns>
        public bool Load()
        {
            if (File.Exists(mGameInfoPath))
            {
                try
                {
                    var parser = new FileIniDataParser();
                    IniData iniData = parser.ReadFile(mGameInfoPath);

                    mArchivePath = iniData[section][nameof(ArchivePath)];
                    mEmulator = iniData[section][nameof(Emulator)];
                    mPlatform = iniData[section][nameof(Platform)];
                    mTitle = iniData[section][nameof(Title)];
                    mPlayRomInArchive = iniData[section][nameof(PlayRomInArchive)];
                    mDecompressedSize = Convert.ToInt64(iniData[section][nameof(DecompressedSize)]);
                    mKeepInCache = Convert.ToBoolean(iniData[section][nameof(KeepInCache)]);

                    return true;
                }
                catch (Exception e)
                {
                    Logger.Log(e.ToString(), Logger.LogLevel.Exception);
                }
            }

            return false;
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
                iniData[section][nameof(ArchivePath)] = mArchivePath;
                iniData[section][nameof(Emulator)] = mEmulator;
                iniData[section][nameof(Platform)] = mPlatform;
                iniData[section][nameof(Title)] = mTitle;
                iniData[section][nameof(PlayRomInArchive)] = mPlayRomInArchive;
                iniData[section][nameof(DecompressedSize)] = mDecompressedSize.ToString();
                iniData[section][nameof(KeepInCache)] = mKeepInCache.ToString();

                parser.WriteFile(savePath, iniData);

                return true;
            }
            catch (Exception e)
            {
                Logger.Log(e.ToString(), Logger.LogLevel.Exception);
            }

            return false;
        }
    }
}
