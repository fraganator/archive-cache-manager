using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ArchiveCacheManager
{
    /// <summary>
    /// Singleton class containing current launched game information, including that game's cache information.
    /// </summary>
    public class LaunchInfo
    {
        private class CacheData
        {
            public string ArchivePath;
            public string ArchiveCachePath;
            public bool? ArchiveInCache;
            public long? Size;
            public bool? ExtractSingleFile;

            public Config.EmulatorPlatformConfig Config;
            public string ArchiveCacheLaunchPath;
            public string M3uName;

            public CacheData()
            {
                Config = new Config.EmulatorPlatformConfig();
            }
        };

        private static GameInfo mGame;
        private static CacheData mGameCacheData;
        private static Dictionary<int, CacheData> mMultiDiscCacheData;

        /// <summary>
        /// The game info from LaunchBox.
        /// </summary>
        public static GameInfo Game => mGame;
        public static Extractor Extractor = null;
        public static Config.LaunchPath LaunchPathConfig => mGameCacheData.Config.LaunchPath;
        public static bool MultiDiscSupport => mGameCacheData.Config.MultiDisc;

        static LaunchInfo()
        {
            mGame = new GameInfo(PathUtils.GetGameInfoPath());
            mGameCacheData = new CacheData();
            mGameCacheData.ArchivePath = mGame.ArchivePath;
            mGameCacheData.ArchiveCachePath = PathUtils.ArchiveCachePath(mGame.ArchivePath);
            mGameCacheData.Config = Config.GetEmulatorPlatformConfig(Config.EmulatorPlatformKey(mGame.Emulator, mGame.Platform));
            if (mGame.InfoLoaded)
            {
                Logger.Log(string.Format("Archive path set to \"{0}\".", mGameCacheData.ArchivePath));
                Logger.Log(string.Format("Archive cache path set to \"{0}\".", mGameCacheData.ArchiveCachePath));
            }
            mMultiDiscCacheData = new Dictionary<int, CacheData>();
            foreach (var disc in mGame.Discs)
            {
                mMultiDiscCacheData[disc.Disc] = new CacheData();
                mMultiDiscCacheData[disc.Disc].ArchivePath = disc.ArchivePath;
                mMultiDiscCacheData[disc.Disc].ArchiveCachePath = PathUtils.ArchiveCachePath(disc.ArchivePath);
                Logger.Log(string.Format("Disc {0} archive path set to \"{1}\".", disc.Disc, mMultiDiscCacheData[disc.Disc].ArchivePath));
                Logger.Log(string.Format("Disc {0} archive cache path set to \"{1}\".", disc.Disc, mMultiDiscCacheData[disc.Disc].ArchiveCachePath));
            }

            Extractor = GetExtractor(mGameCacheData.ArchivePath);
            Logger.Log(string.Format("Extractor set to \"{0}\".", Extractor.Name()));
        }

        private static Extractor GetExtractor(string archivePath)
        {
            bool extract = (mGameCacheData.Config.Action == Config.Action.Extract || mGameCacheData.Config.Action == Config.Action.ExtractCopy);
            bool copy = (mGameCacheData.Config.Action == Config.Action.Copy || mGameCacheData.Config.Action == Config.Action.ExtractCopy);

            if (extract && Zip.SupportedType(archivePath))
            {
                return new Zip();
            }
            else if (extract && mGameCacheData.Config.Chdman && Chdman.SupportedType(archivePath))
            {
                return new Chdman();
            }
            else if (extract && mGameCacheData.Config.DolphinTool && DolphinTool.SupportedType(archivePath))
            {
                return new DolphinTool();
            }
            else if (copy)
            {
                return new Robocopy();
            }

            // Default to Zip extractor
            return new Zip();
        }

        /// <summary>
        /// Get the size of the game archive when extracted. For multi-disc games, the size will be the total of all discs.
        /// </summary>
        /// <param name="disc"></param>
        /// <returns></returns>
        public static long GetSize(int? disc = null)
        {
            if (disc != null)
            {
                try
                {
                    if (mMultiDiscCacheData[(int)disc].Size == null)
                    {
                        mMultiDiscCacheData[(int)disc].Size = Extractor.GetSize(mMultiDiscCacheData[(int)disc].ArchivePath);
                        Logger.Log(string.Format("Disc {0} decompressed archive size is {1} bytes.", (int)disc, (long)mMultiDiscCacheData[(int)disc].Size));
                    }

                    return (long)mMultiDiscCacheData[(int)disc].Size;
                }
                catch (KeyNotFoundException)
                {
                    Logger.Log(string.Format("Unknown disc number {0}, using DecompressedSize instead.", (int)disc));
                }
            }

            if (mGame.MultiDisc && MultiDiscSupport && disc == null)
            {
                long multiDiscDecompressedSize = 0;

                foreach (var discCacheData in mMultiDiscCacheData)
                {
                    multiDiscDecompressedSize += GetSize(discCacheData.Key);
                }

                return multiDiscDecompressedSize;
            }

            if (mGameCacheData.Size == null)
            {
                mGameCacheData.Size = Extractor.GetSize(mGameCacheData.ArchivePath, GetExtractSingleFile());
                mGame.DecompressedSize = (long)mGameCacheData.Size;
                Logger.Log(string.Format("Decompressed archive size is {0} bytes.", (long)mGameCacheData.Size));
            }

            return (long)mGameCacheData.Size;
        }

        /// <summary>
        /// Get the individual file to be extracted from the archive, if supported.
        /// Will only return a result if SmartExtract is enabled, and the game was launched with a file selected.
        /// </summary>
        /// <returns>Name of file to extract from archive, or null if not applicable.</returns>
        public static string GetExtractSingleFile()
        {
            if (mGameCacheData.ExtractSingleFile == null)
            {
                mGameCacheData.ExtractSingleFile = false;

                if (mGameCacheData.Config.SmartExtract && !string.IsNullOrEmpty(mGame.SelectedFile))
                {
                    List<string> standaloneList = Utils.SplitExtensions(Config.StandaloneExtensions).ToList();
                    List<string> metadataList = Utils.SplitExtensions(Config.MetadataExtensions).ToList();
                    List<string> excludeList = new List<string>(metadataList);

                    string extension = Path.GetExtension(mGame.SelectedFile).TrimStart(new char[] { '.' }).ToLower();
                    if (standaloneList.Contains(extension))
                    {
                        excludeList.AddRange(standaloneList);
                    }
                    else
                    {
                        excludeList.Add(extension);
                    }

                    if (Extractor.List(mGame.ArchivePath, null, excludeList.ToArray(), true).Count() == 0)
                    {
                        mGameCacheData.ExtractSingleFile = true;
                        Logger.Log(string.Format("Smart Extraction enabled for file \"{0}\".", mGame.SelectedFile));
                    }
                }
            }

            return (bool)mGameCacheData.ExtractSingleFile ? mGame.SelectedFile : null;
        }

        /// <summary>
        /// Get the source archive path of the game. If disc is specified, the archive path will be to that disc.
        /// </summary>
        /// <param name="disc"></param>
        /// <returns></returns>
        public static string GetArchivePath(int? disc = null)
        {
            if (disc != null)
            {
                try
                {
                    return mMultiDiscCacheData[(int)disc].ArchivePath;
                }
                catch (KeyNotFoundException)
                {
                    Logger.Log(string.Format("Unknown disc number {0}, using ArchivePath instead.", (int)disc));
                }
            }

            return mGameCacheData.ArchivePath;
        }

        /// <summary>
        /// Get the cache path of the game. If disc is specified, the archive path will be to that disc.
        /// </summary>
        /// <param name="disc"></param>
        /// <returns></returns>
        public static string GetArchiveCachePath(int? disc = null)
        {
            if (disc != null)
            {
                try
                {
                    return mMultiDiscCacheData[(int)disc].ArchiveCachePath;
                }
                catch (KeyNotFoundException)
                {
                    Logger.Log(string.Format("Unknown disc number {0}, using ArchiveCachePath instead.", (int)disc));
                }
            }

            return mGameCacheData.ArchiveCachePath;
        }

        private static string GetLaunchPath(int? disc = null)
        {
            string launchPath;

            switch (mGameCacheData.Config.LaunchPath)
            {
                case Config.LaunchPath.Title:
                    launchPath = Path.Combine(PathUtils.CachePath(), PathUtils.GetValidPath(mGame.Title, "Title"));
                    break;
                case Config.LaunchPath.Platform:
                    launchPath = Path.Combine(PathUtils.CachePath(), PathUtils.GetValidPath(mGame.Platform, "Platform"));
                    break;
                case Config.LaunchPath.Emulator:
                    launchPath = Path.Combine(PathUtils.CachePath(), PathUtils.GetValidPath(mGame.Emulator, "Emulator"));
                    break;
                case Config.LaunchPath.Default:
                default:
                    launchPath = GetArchiveCachePath(disc);
                    break;
            }

            return launchPath;
        }

        /// <summary>
        /// Get the cache path of the game. If disc is specified, the archive path will be to that disc.
        /// </summary>
        /// <param name="disc"></param>
        /// <returns></returns>
        public static string GetArchiveCacheLaunchPath(int? disc = null)
        {
            if (disc != null)
            {
                if (mMultiDiscCacheData[(int)disc].ArchiveCacheLaunchPath == null)
                {
                    mMultiDiscCacheData[(int)disc].ArchiveCacheLaunchPath = GetLaunchPath(disc);
                }

                return mMultiDiscCacheData[(int)disc].ArchiveCacheLaunchPath;
            }

            if (mGameCacheData.ArchiveCacheLaunchPath == null)
            {
                mGameCacheData.ArchiveCacheLaunchPath = GetLaunchPath();
            }

            return mGameCacheData.ArchiveCacheLaunchPath;
        }

        private static string GetM3u(int? disc = null)
        {
            string m3uName;

            switch (mGameCacheData.Config.M3uName)
            {
                case Config.M3uName.GameId:
                default:
                    m3uName = PathUtils.GetArchiveCacheM3uGameIdPath(GetArchiveCachePath(disc), mGame.GameId);
                    break;
                case Config.M3uName.TitleVersion:
                    m3uName = PathUtils.GetArchiveCacheM3uGameTitlePath(GetArchiveCachePath(disc), mGame.GameId, mGame.Title, mGame.Version, disc);
                    break;
            }

            return m3uName;
        }

        public static string GetM3uName(int? disc = null)
        {
            if (disc != null)
            {
                if (mMultiDiscCacheData[(int)disc].M3uName == null)
                {
                    mMultiDiscCacheData[(int)disc].M3uName = GetM3u(disc);
                }

                return mMultiDiscCacheData[(int)disc].M3uName;
            }

            if (mGameCacheData.M3uName == null)
            {
                mGameCacheData.M3uName = GetM3u();
            }

            return mGameCacheData.M3uName;
        }

        /// <summary>
        /// Check if the game is cached. Will check if all discs of a multi-disc game are cached.
        /// </summary>
        /// <param name="disc">When specified, checks if a particular disc of a game is cached.</param>
        /// <returns>True if the game is cached, and False otherwise.</returns>
        public static bool GetArchiveInCache(int? disc = null)
        {
            if (disc != null)
            {
                try
                {
                    if (mMultiDiscCacheData[(int)disc].ArchiveInCache == null)
                    {
                        mMultiDiscCacheData[(int)disc].ArchiveInCache = File.Exists(PathUtils.GetArchiveCacheGameInfoPath(mMultiDiscCacheData[(int)disc].ArchiveCachePath));
                    }

                    return (bool)mMultiDiscCacheData[(int)disc].ArchiveInCache;
                }
                catch (KeyNotFoundException)
                {
                    Logger.Log(string.Format("Unknown disc number {0}, using ArchiveInCache instead.", (int)disc));
                }
            }

            if (mGame.MultiDisc && MultiDiscSupport && disc == null)
            {
                // Set true if there are multiple discs, false otherwise. When false, subsequent boolean operations will be false.
                bool multiDiscArchiveInCache = mMultiDiscCacheData.Count > 0;

                foreach (var discCacheData in mMultiDiscCacheData)
                {
                    multiDiscArchiveInCache &= GetArchiveInCache(discCacheData.Key);
                }

                return multiDiscArchiveInCache;
            }

            if (mGameCacheData.ArchiveInCache == null)
            {
                mGameCacheData.ArchiveInCache = File.Exists(PathUtils.GetArchiveCacheGameInfoPath(mGameCacheData.ArchiveCachePath));
            }

            if (mGameCacheData.Config.SmartExtract && !string.IsNullOrEmpty(mGame.SelectedFile))
            {
                mGameCacheData.ArchiveInCache &= File.Exists(Path.Combine(mGameCacheData.ArchiveCachePath, mGame.SelectedFile));
            }

            return (bool)mGameCacheData.ArchiveInCache;
        }

        /// <summary>
        /// Get the number of discs already cached.
        /// </summary>
        /// <returns>The number of game discs already in the cache. Will be 0 or 1 for non multi-disc games.</returns>
        public static int GetDiscCountInCache()
        {
            if (mGame.MultiDisc)
            {
                int discCount = 0;

                foreach (var discCacheData in mMultiDiscCacheData)
                {
                    discCount += GetArchiveInCache(discCacheData.Key) ? 1 : 0;
                }

                return discCount;
            }

            return GetArchiveInCache() ? 1 : 0;
        }

        public static void SaveToCache(int? disc = null)
        {
            if (mGame.MultiDisc && MultiDiscSupport && disc == null)
            {
                foreach (var discInfo in mGame.Discs)
                {
                    SaveToCache(discInfo.Disc);
                }

                return;
            }

            string archiveCacheGameInfoPath = PathUtils.GetArchiveCacheGameInfoPath(GetArchiveCachePath(disc));
            GameInfo savedGameInfo = new GameInfo(mGame);
            GameInfo cachedGameInfo = new GameInfo(archiveCacheGameInfoPath);

            if (cachedGameInfo.InfoLoaded)
            {
                savedGameInfo.MergeCacheInfo(cachedGameInfo);
            }
            else
            {
                savedGameInfo.DecompressedSize = GetSize(disc);
            }

            if (disc != null)
            {
                savedGameInfo.ArchivePath = mGame.Discs.Find(d => d.Disc == (int)disc).ArchivePath;
                savedGameInfo.SelectedDisc = (int)disc;
            }

            savedGameInfo.Save(archiveCacheGameInfoPath);
        }
    }
}
