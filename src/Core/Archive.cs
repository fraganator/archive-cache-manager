using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArchiveCacheManager
{
    /// <summary>
    /// Singleton class containing current archive information.
    /// </summary>
    public class Archive
    {
        private static string mPath;
        private static string mCachePath;
        private static long mDecompressedSize;

        /// <summary>
        /// Init the archive information based on the path.
        /// </summary>
        /// <param name="archivePath"></param>
        public static void Init(string archivePath)
        {
            mPath = archivePath;
            mCachePath = PathUtils.ArchiveCachePath(archivePath);
            mDecompressedSize = Zip.GetDecompressedSize(archivePath);

            Logger.Log(string.Format("Archive path set to \"{0}\".", mPath));
            Logger.Log(string.Format("Archive cache path set to \"{0}\".", mCachePath));
            Logger.Log(string.Format("Decompressed archive size is {0} bytes.", mDecompressedSize));
        }

        /// <summary>
        /// The archive path supplied by LaunchBox.
        /// </summary>
        public static string Path => mPath;
        /// <summary>
        /// The derived archive path in the cache, in the form "cache\archive - MD5".
        /// </summary>
        public static string CachePath => mCachePath;
        /// <summary>
        /// The decompressed archive size in bytes.
        /// </summary>
        public static long DecompressedSize => mDecompressedSize;
    }
}
