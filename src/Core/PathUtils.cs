using System;
using System.Reflection;
using System.Text;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Cryptography;

namespace ArchiveCacheManager
{
    /// <summary>
    /// Helper class to manage the paths of files used by the plugin.
    /// </summary>
    public class PathUtils
    {
        /* There are multiple root paths to be handled:
         * <LaunchBox>\Core\LaunchBox.exe
         * <LaunchBox>\ThirdParty\7-Zip\7z.exe
         */
        public static readonly int MAX_PATH = 260;

        private static readonly string configFileName = @"config.ini";
        private static readonly string gameIndexFileName = @"game-index.ini";
        private static readonly string gameInfoFileName = @"game.ini";
        private static readonly string default7zFileName = @"7z.exe";
        private static readonly string alt7zFileName = @"7-zip.exe";
        private static readonly string relativePluginPath = @"Plugins\ArchiveCacheManager";
        private static readonly string relative7zPath = @"ThirdParty\7-Zip";
        private static readonly string relativeLogPath = Path.Combine(relativePluginPath, "Logs");
        private static readonly DateTime dateTimeNow = DateTime.Now;
        private static readonly string logFileName = string.Format("events-{0}-{1:00}-{2:00}.log", dateTimeNow.Year, dateTimeNow.Month, dateTimeNow.Day);

        private static string assemblyPath;
        private static string assemblyFileName;
        private static string assemblyDirectory;
        private static string launchBoxRootPath;

        [DllImport("shlwapi.dll", CharSet = CharSet.Unicode)]
        static extern bool PathRelativePathTo([Out] StringBuilder pszPath, [In] string pszFrom, [In] FileAttributes dwAttrFrom, [In] string pszTo, [In] FileAttributes dwAttrTo);

        static PathUtils()
        {
            assemblyPath = Assembly.GetEntryAssembly().Location;
            assemblyFileName = Path.GetFileName(assemblyPath);
            assemblyDirectory = Path.GetDirectoryName(assemblyPath);
            launchBoxRootPath = GetLaunchBoxRootPath();
        }

        /// <summary>
        /// Compares two paths if they are equal.
        /// </summary>
        /// <param name="path1"></param>
        /// <param name="path2"></param>
        /// <returns></returns>
        public static bool ComparePaths(string path1, string path2)
        {
            return string.Equals(Path.GetFullPath(path1).TrimEnd(Path.DirectorySeparatorChar),
                                 Path.GetFullPath(path2).TrimEnd(Path.DirectorySeparatorChar),
                                 StringComparison.InvariantCultureIgnoreCase);
        }

        /// <summary>
        /// Finds the relative path between pathFrom and pathTo. Leading .\ is removed.
        /// </summary>
        /// <param name="pathFrom"></param>
        /// <param name="pathTo"></param>
        /// <returns>The relative path, or pathTo if there is no common root path.</returns>
        public static string GetRelativePath(string pathFrom, string pathTo)
        {
            string resultPath = pathTo;

            // Paths must have common root to have relative path
            if (Path.GetPathRoot(pathFrom) == Path.GetPathRoot(pathTo))
            {
                StringBuilder relativePath = new StringBuilder(MAX_PATH);
                PathRelativePathTo(relativePath, pathFrom, FileAttributes.Directory, pathTo, FileAttributes.Normal);
                resultPath = relativePath.ToString();

                if (resultPath.StartsWith(@".\"))
                {
                    resultPath = resultPath.Remove(0, 2);
                }
            }

            return resultPath;
        }

        /// <summary>
        /// Returns the absolute path of the input path. Relative paths are to the root LaunchBox folder.
        /// </summary>
        /// <param name="path"></param>
        /// <returns>The absolute path of the input path.</returns>
        public static string GetAbsolutePath(string path)
        {
            if (!Path.IsPathRooted(path))
            {
                path = Path.GetFullPath(Path.Combine(PathUtils.GetLaunchBoxRootPath(), path));
            }

            return path;
        }

        /// <summary>
        /// Determine the LaunchBox root path based on whether this function was called by the plugin, or the Archive Cache Manager exe.
        /// </summary>
        /// <returns></returns>
        public static string GetLaunchBoxRootPath()
        {
            string path;

            // Called from <LaunchBox>\ThirdParty\7-Zip\7z.exe
            if (string.Equals(assemblyFileName, default7zFileName, StringComparison.InvariantCultureIgnoreCase))
            {
                // Call GetFullPath to resolve ..\.. in path
                path = Path.GetFullPath(Path.Combine(assemblyDirectory, @"..\.."));
            }
            // Called from <LaunchBox>\Core\LaunchBox.exe
            else
            {
                // Call GetFullPath to resolve .. in path
                path = Path.GetFullPath(Path.Combine(assemblyDirectory, @".."));
            }

            return path;
        }

        /// <summary>
        /// Absolute path to plugin config file.
        /// </summary>
        /// <returns>Absolute path to plugin config file.</returns>
        public static string GetPluginConfigPath() => Path.Combine(launchBoxRootPath, relativePluginPath, configFileName);

        /// <summary>
        /// Absolute path to game index info file.
        /// </summary>
        /// <returns>Absolute path to plugin game index info file.</returns>
        public static string GetPluginGameIndexPath() => Path.Combine(launchBoxRootPath, relativePluginPath, gameIndexFileName);

        /// <summary>
        /// Absolute path to 7z.exe.
        /// </summary>
        /// <returns>Absolute path to 7z.exe.</returns>
        public static string GetLaunchBox7zPath()
        {
            string path;

            // Called from <LaunchBox>\ThirdParty\7-Zip\7z.exe
            if (string.Equals(assemblyFileName, default7zFileName, StringComparison.InvariantCultureIgnoreCase))
            {
                path = Path.Combine(launchBoxRootPath, relative7zPath, alt7zFileName);
            }
            // Called from <LaunchBox>\Core\LaunchBox.exe
            else
            {
                path = Path.Combine(launchBoxRootPath, relative7zPath, default7zFileName);
            }

            return path;
        }

        /// <summary>
        /// Absolute path to 7-Zip folder.
        /// </summary>
        /// <returns>Absolute path to 7-Zip folder.</returns>
        public static string GetLaunchBox7zRootPath() => Path.Combine(launchBoxRootPath, relative7zPath);

        /// <summary>
        /// Absolute path to LaunchBox's temporary extraction location.
        /// </summary>
        /// <returns>Absolute path to LaunchBox's temporary extraction location.</returns>
        public static string GetLaunchBox7zTempPath() => Path.Combine(launchBoxRootPath, relative7zPath, "Temp");

        /// <summary>
        /// Absolute path to game info file.
        /// </summary>
        /// <returns>Absolute path to game info file.</returns>
        public static string GetGameInfoPath() => Path.Combine(launchBoxRootPath, relative7zPath, gameInfoFileName);

        /// <summary>
        /// Absolute path to plugin's log folder.
        /// </summary>
        /// <returns>Absolute path to plugin's log folder.</returns>
        public static string GetLogPath() => Path.Combine(launchBoxRootPath, relativeLogPath);

        /// <summary>
        /// Absolute path to plugin's log file.
        /// </summary>
        /// <returns>Absolute path to plugin's log file.</returns>
        public static string GetLogFilePath() => Path.Combine(launchBoxRootPath, relativeLogPath, logFileName);

        /// <summary>
        /// Absolute path to plugin root folder.
        /// </summary>
        /// <returns>Absolute path to plugin root folder.</returns>
        public static string GetPluginRootPath() => Path.Combine(launchBoxRootPath, relativePluginPath);

        /// <summary>
        /// Absolute path to the folder containing the plugin's copy of 7-Zip.
        /// </summary>
        /// <returns>Absolute path to the folder containing the plugin's copy of 7-Zip.</returns>
        public static string GetPlugin7zRootPath() => Path.Combine(launchBoxRootPath, relativePluginPath, "7-Zip");

        /// <summary>
        /// Absolute path to the last played file for the given archive cache path.
        /// </summary>
        /// <param name="archiveCachePath">Location of the cached archive.</param>
        /// <returns>Absolute path to the last played file.</returns>
        public static string GetArchiveCachePlaytimePath(string archiveCachePath) => Path.Combine(archiveCachePath, "lastplayed");

        /// <summary>
        /// Absolute path to the game info file for the given archive cache path.
        /// </summary>
        /// <param name="archiveCachePath">Location of the cached archive.</param>
        /// <returns>Absolute path to the game info file.</returns>
        public static string GetArchiveCacheGameInfoPath(string archiveCachePath) => Path.Combine(archiveCachePath, gameInfoFileName);

        /// <summary>
        /// Absolute path to the extracting flag file for the given archive cache path.
        /// </summary>
        /// <param name="archiveCachePath">Location of the cached archive.</param>
        /// <returns>Absolute path to the extracting flag file.</returns>
        public static string GetArchiveCacheExtractingFlagPath(string archiveCachePath) => Path.Combine(archiveCachePath, "extracting");

        /// <summary>
        /// Game info filename.
        /// </summary>
        /// <returns></returns>
        public static string GetGameInfoFileName() => gameInfoFileName;

        /// <summary>
        /// Calculates an MD5 hash of the given path.
        /// </summary>
        /// <param name="path">The path, including filename, to hash.</param>
        /// <returns>The MD5 hash.</returns>
        private static string PathHash(string path)
        {
            // Create hash of archive path
            byte[] hash;
            using (MD5 md5 = MD5.Create())
            {
                hash = md5.ComputeHash(Encoding.UTF8.GetBytes(path));
            }

            // Example: Doom (USA).zip hashes to 7309402b2dbee883f0f83e3e962dff24
            return BitConverter.ToString(hash).Replace("-", string.Empty);
        }

        /// <summary>
        /// Calculates an MD5 hash of the given path, and appends to the end of the filename in the path.
        /// Example: "Doom (USA).zip - 7309402b2dbee883f0f83e3e962dff24"
        /// </summary>
        /// <param name="path">The path, including filename, to hash.</param>
        /// <param name="hashLength">The length of the hash to append. Valid values are 1-32. Default is 32.</param>
        /// <returns>The filename with MD5 hash suffix.</returns>
        public static string FilenameWithHash(string path, int hashLength = 32)
        {
            if (hashLength > 32)
            {
                hashLength = 32;
            }
            else if (hashLength < 1)
            {
                hashLength = 1;
            }

            // Example: "Doom (USA).zip - 7309402b2dbee883f0f83e3e962dff24"
            return string.Format("{0} - {1}", Path.GetFileName(path), PathHash(path).Substring(0, hashLength));
        }

        /// <summary>
        /// Absolute path to the supplied cache path, or to the configured cache if cachePath omitted.
        /// </summary>
        /// <param name="cachePath">The path to the cache lcation.</param>
        /// <returns>Absolute path of the cache.</returns>
        public static string CachePath(string cachePath = null)
        {
            return GetAbsolutePath(cachePath ?? Config.CachePath);
        }

        /// <summary>
        /// Absolute path to the archive within the cache.
        /// </summary>
        /// <param name="archivePath">The path to the cache lcation.</param>
        /// <returns>Absolute path to the archive within the cache.</returns>
        public static string ArchiveCachePath(string archivePath)
        {
            return Path.Combine(CachePath(), FilenameWithHash(archivePath));
        }

        /// <summary>
        /// Check that the given path is safe to use as a cache location.
        /// Unsafe paths include empty or null, LaunchBox and its default subfolders, c:\, Windows, Program Files.
        /// </summary>
        /// <param name="path">The path to check.</param>
        /// <returns>True if the path is safe to use, false otherwise.</returns>
        public static bool IsPathSafe(string path)
        {
            try
            {
                if (string.IsNullOrEmpty(path.Trim()))
                {
                    return false;
                }

                path = GetAbsolutePath(path);

                string[] unsafePaths = new string[] {
                    launchBoxRootPath,
                    Path.Combine(launchBoxRootPath, "Backups"),
                    Path.Combine(launchBoxRootPath, "Core"),
                    Path.Combine(launchBoxRootPath, "Data"),
                    Path.Combine(launchBoxRootPath, "Images"),
                    Path.Combine(launchBoxRootPath, "LBThemes"),
                    Path.Combine(launchBoxRootPath, "Logs"),
                    Path.Combine(launchBoxRootPath, "Manuals"),
                    Path.Combine(launchBoxRootPath, "Metadata"),
                    Path.Combine(launchBoxRootPath, "Music"),
                    Path.Combine(launchBoxRootPath, "PauseThemes"),
                    Path.Combine(launchBoxRootPath, "Plugins"),
                    Path.Combine(launchBoxRootPath, "Sounds"),
                    Path.Combine(launchBoxRootPath, "StartupThemes"),
                    Path.Combine(launchBoxRootPath, "Themes"),
                    Path.Combine(launchBoxRootPath, "ThirdParty"),
                    Path.Combine(launchBoxRootPath, "Updates"),
                    Path.Combine(launchBoxRootPath, "Videos"),
                    @"C:\",
                    @"C:\Windows",
                    @"C:\Program Files",
                    @"C:\Program Files (x86)"
                };

                foreach (string unsafePath in unsafePaths)
                {
                    if (ComparePaths(path, unsafePath))
                    {
                        return false;
                    }
                }

                // Will throw an exception if the path is poorly formatted or permissions errors
                Directory.EnumerateFiles(path);
            }
            // Path may not exist, but is still valid
            catch (DirectoryNotFoundException)
            {
                return true;
            }
            catch (Exception e)
            {
                Logger.Log(e.ToString(), Logger.LogLevel.Exception);
                return false;
            }

            return true;
        }
    }
}
