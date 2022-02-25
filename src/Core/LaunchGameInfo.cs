using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArchiveCacheManager
{
    /// <summary>
    /// Singleton class containing current launched game information.
    /// </summary>
    public class LaunchGameInfo
    {
        private static GameInfo mGame;
        private static string mArchivePath;
        private static string mArchiveCachePath;
        private static Dictionary<int, string> mDiscArchivePaths;
        private static Dictionary<int, string> mDiscArchiveCachePaths;
        private static long mDecompressedSize;

        /// <summary>
        /// The game info from LaunchBox.
        /// </summary>
        public static GameInfo Game => mGame;
        /// <summary>
        /// The archive path from game info.
        /// </summary>
        public static string ArchivePath => mArchivePath;
        /// <summary>
        /// The derived archive path in the cache, in the form "cache\archive - MD5".
        /// </summary>
        public static string ArchiveCachePath => mArchiveCachePath;
        /// <summary>
        /// The decompressed archive size in bytes.
        /// </summary>
        public static long DecompressedSize => mDecompressedSize;

        static LaunchGameInfo()
        {
            mGame = new GameInfo(PathUtils.GetGameInfoPath());
            mArchivePath = mGame.ArchivePath;
            mArchiveCachePath = PathUtils.ArchiveCachePath(mGame.ArchivePath);
            Logger.Log(string.Format("Archive path set to \"{0}\".", mArchivePath));
            Logger.Log(string.Format("Archive cache path set to \"{0}\".", mArchiveCachePath));

            mDiscArchivePaths = new Dictionary<int, string>();
            mDiscArchiveCachePaths = new Dictionary<int, string>();
            foreach (var disc in mGame.Discs)
            {
                mDiscArchivePaths.Add(disc.Disc, disc.ArchivePath);
                mDiscArchiveCachePaths.Add(disc.Disc, PathUtils.ArchiveCachePath(disc.ArchivePath));
                Logger.Log(string.Format("Disc {0} archive path set to \"{1}\".", disc.Disc, mDiscArchivePaths[disc.Disc]));
                Logger.Log(string.Format("Disc {0} archive cache path set to \"{1}\".", disc.Disc, mDiscArchiveCachePaths[disc.Disc]));
            }
        }

        public static void UpdateDecompressedSize()
        {
            mDecompressedSize = Zip.GetDecompressedSize(mArchivePath);
            mGame.DecompressedSize = mDecompressedSize;
            Logger.Log(string.Format("Decompressed archive size is {0} bytes.", mDecompressedSize));
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
                    return mDiscArchivePaths[(int)disc];
                }
                catch (KeyNotFoundException e)
                {
                    Logger.Log(string.Format("Unknown disc number {0}, using ArchivePath instead.", (int)disc));
                }
            }

            return mArchivePath;
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
                    return mDiscArchiveCachePaths[(int)disc];
                }
                catch (KeyNotFoundException e)
                {
                    Logger.Log(string.Format("Unknown disc number {0}, using CachePath instead.", (int)disc));
                }
            }

            return mArchiveCachePath;
        }
    }
}
