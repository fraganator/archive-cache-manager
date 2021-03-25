using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text.RegularExpressions;
using System.Reflection;

namespace ArchiveCacheManager
{
    public class CacheManager
    {
        private static GameInfo launchGameInfo;

        static CacheManager()
        {
            launchGameInfo = new GameInfo(PathUtils.GetGameInfoPath());
        }

        /// <summary>
        /// Plugin version string, in the form "vX.Y.Z".
        /// </summary>
        public static string Version
        {
            get
            {
                Version version = Assembly.GetExecutingAssembly().GetName().Version;

                return string.Format("v{0}.{1}.{2}", version.Major, version.Minor, version.Build);
            }
        }

        /// <summary>
        /// Deletes everything from the cache path, but keeps the cache folder.
        /// </summary>
        public static void ClearCache()
        {
            DiskUtils.DeleteDirectory(Config.CachePath, true);
        }

        /// <summary>
        /// Attempts to create the cache path if it doesn't exist.
        /// </summary>
        /// <returns>True if cache created or already exists, False otherwise.</returns>
        public static bool CreateCache()
        {
            bool cacheExists = false;

            try
            {
                if (!Directory.Exists(PathUtils.CachePath()))
                {
                    Directory.CreateDirectory(PathUtils.CachePath());
                }

                cacheExists = true;
            }
            catch (Exception e)
            {
                Logger.Log(e.ToString(), Logger.LogLevel.Exception);
            }

            return cacheExists;
        }

        /// <summary>
        /// Check if the archive is already in the cache.
        /// </summary>
        /// <returns>True if the archive is cached, False otherwise.</returns>
        public static bool ArchiveInCache()
        {
            // Use the existence of the game.ini file, as it's only copied to the cache after a successful 7z extract command
            return File.Exists(PathUtils.GetArchiveCacheGameInfoPath(Archive.CachePath));
        }

        /// <summary>
        /// Extract the archive to the cache. In the event of a cache error, archive will be extracted to LaunchBox's temp lcation.
        /// </summary>
        public static void AddArchiveToCache()
        {
            string stdout = string.Empty;
            string stderr = string.Empty;
            int exitCode = 0;

            if (CreateCache())
            {
                // Clear anything already in the archive cache path (from bad extract, terminated process, or some other reason)
                DiskUtils.DeleteDirectory(Archive.CachePath, true);
                launchGameInfo.DecompressedSize = Archive.DecompressedSize;
                ClearCacheSpace(Archive.DecompressedSize);
                Logger.Log(string.Format("Extracting archive to \"{0}\".", Archive.CachePath));
                Zip.Extract(Archive.Path, Archive.CachePath, ref stdout, ref stderr, ref exitCode);
                if (exitCode == 0)
                {
                    launchGameInfo.Save(PathUtils.GetArchiveCacheGameInfoPath(Archive.CachePath));
                    //File.Copy(PathUtils.GetGameInfoPath(), PathUtils.GetArchiveCacheGameInfoPath(Archive.CachePath), true);
                    DiskUtils.SetDirectoryContentsReadOnly(Archive.CachePath);
                }
                else
                {
                    Logger.Log("Extraction error, removing output files from cache.");
                    DiskUtils.DeleteDirectory(Archive.CachePath);
                }
            }
            else
            {
                Logger.Log(@"Error creating or accessing cache location. Archive will be extracted to LaunchBox\ThirdParty\7-Zip\Temp.");
                Zip.Extract(Archive.Path, PathUtils.GetLaunchBox7zTempPath(), ref stdout, ref stderr, ref exitCode);
            }
        }

        /// <summary>
        /// Verify and fix cache entries. Invalid cache entries are removed, while missing game.ini keys are updated where possible.
        /// </summary>
        public static void VerifyCacheIntegrity()
        {
            if (Directory.Exists(PathUtils.CachePath()))
            {
                Logger.Log("Verifying cache integrity...");

                string[] dirs = Directory.GetDirectories(PathUtils.CachePath());

                foreach (string dir in dirs)
                {
                    GameInfo gameInfo = new GameInfo(Path.Combine(dir, PathUtils.GetGameInfoFileName()));
                    if (!gameInfo.InfoLoaded ||
                        string.IsNullOrEmpty(gameInfo.ArchivePath) ||
                        string.IsNullOrEmpty(gameInfo.Emulator) ||
                        string.IsNullOrEmpty(gameInfo.Platform) ||
                        string.IsNullOrEmpty(gameInfo.Title))
                    {
                        Logger.Log(string.Format("Error loading game.ini, deleting cached item \"{0}\".", dir));
                        DiskUtils.DeleteDirectory(dir);
                        continue;
                    }

                    if (gameInfo.DecompressedSize == 0)
                    {
                        gameInfo.DecompressedSize = DiskUtils.DirectorySize(new DirectoryInfo(dir));
                        gameInfo.Save();
                    }
                }

                Logger.Log("Verification complete.");
            }
        }

        /// <summary>
        /// Lists the content of the archive. If the archive is cached, returns the cached file list (including absolute paths).
        /// Also applies the configured file extension priority for the current emulator and platform.
        /// </summary>
        public static void ListArchive()
        {
            string stdout = string.Empty;
            string stderr = string.Empty;
            int exitCode = 0;

            if (launchGameInfo.PlayRomInArchive.Equals(string.Empty))
            {
                if (ArchiveInCache())
                {
                    stdout = ListCacheArchive();
                }
                else
                {
                    Zip.List(Archive.Path, ref stdout, ref stderr, ref exitCode);
                }
                stdout = ApplyExtensionPriority(stdout);
            }
            else
            {
                if (ArchiveInCache())
                {
                    stdout = "Path = " + Path.Combine(PathUtils.ArchiveCachePath(Archive.Path), launchGameInfo.PlayRomInArchive);
                }
                else
                {
                    stdout = "Path = " + launchGameInfo.PlayRomInArchive;
                }

                Logger.Log(string.Format("Loading individual file from archive \"{0}\".", launchGameInfo.PlayRomInArchive));
            }

            Console.Write(stdout);
            Console.Write(stderr);
            Environment.ExitCode = exitCode;
        }

        /// <summary>
        /// Lists all of the files in the cached archive path.
        /// </summary>
        /// <returns>The file list for the cached archive in "Path = absolute\file\path.ext" format, with one entry per line.</returns>
        public static string ListCacheArchive()
        {
            string fileList = string.Empty;
            string[] exclude = new string[] { PathUtils.GetArchiveCachePlaytimePath(Archive.CachePath), PathUtils.GetArchiveCacheGameInfoPath(Archive.CachePath) };

            foreach (string filePath in Directory.EnumerateFiles(Archive.CachePath, "*", SearchOption.AllDirectories))
            {
                if (!exclude.Contains(filePath))
                {
                    fileList = string.Format("{0}Path = {1}\r\n", fileList, filePath);
                }
            }

            return fileList;
        }

        /// <summary>
        /// Determine the amount of space used in the cache. Does not include items marked Keep.
        /// </summary>
        /// <returns></returns>
        public static long GetCacheSizeUsed()
        {
            long cacheSizeUsed = 0;

            try
            {
                foreach (string filePath in Directory.GetFiles(PathUtils.GetAbsolutePath(Config.CachePath), PathUtils.GetGameInfoFileName(), SearchOption.AllDirectories))
                {
                    GameInfo gameInfo = new GameInfo(filePath);

                    if (!gameInfo.KeepInCache)
                    {
                        cacheSizeUsed += gameInfo.DecompressedSize;
                    }
                }
            }
            catch (Exception)
            {
            }

            return cacheSizeUsed;
        }

        /// <summary>
        /// Clear the specified amount of space in the cache. If there is already enough room, no cached archives are removed.
        /// Otherwise cached items are removed in least recently played order until there is enough room. If sizeToFree exceeds
        /// the cache size, everything will be removed from the cache.
        /// </summary>
        /// <param name="sizeToFree">Number of bytes to free in the cache</param>
        public static void ClearCacheSpace(long sizeToFree, bool deleteKeep = false)
        {
            long availableCacheSpace = (Config.CacheSize * 1048576) - GetCacheSizeUsed();

            if (availableCacheSpace < sizeToFree)
            {
                // Subtract the available space to get the minimum number of bytes to delete from the cache.
                // Also handles case where available space is negative (configured cache size smaller than what's in the cache right now)
                long sizeToDelete = sizeToFree - availableCacheSpace;

                Logger.Log("Clearing space in cache.");
                long deletedSize = 0;

                // Get a list of directories in the cache, sorted by last played date\time
                string[] dirs = Directory.GetDirectories(PathUtils.CachePath());
                long[] lastUpdated = new long[dirs.Count()];
                for (int i = 0; i < lastUpdated.Count(); i++)
                {
                    lastUpdated[i] = GetArchiveCachePlaytime(dirs[i]);
                }
                Array.Sort(lastUpdated, dirs);

                // Progressively delete oldest cached directories until there is enough free space
                for (int i = 0; i < dirs.Count(); i++)
                {
                    GameInfo gameInfo = new GameInfo(Path.Combine(dirs[i], PathUtils.GetGameInfoFileName()));
                    if (!gameInfo.KeepInCache || deleteKeep)
                    {
                        Logger.Log(string.Format("Deleting cached item \"{0}\".", dirs[i]));
                        DiskUtils.DeleteDirectory(dirs[i]);
                        deletedSize += gameInfo.DecompressedSize;

                        if (deletedSize > sizeToDelete)
                        {
                            break;
                        }
                    }
                }

                Logger.Log(string.Format("Freed {0} bytes from cache.", deletedSize));
            }
        }

        /// <summary>
        /// Reads the last played time from the given archive cache.
        /// </summary>
        /// <param name="archiveCachePath">The path of the cached archive.</param>
        /// <returns>The last played time in ticks, or 0 if there was an error.</returns>
        static long GetArchiveCachePlaytime(string archiveCachePath)
        {
            long lastPlayTime = 0;

            try
            {
                StreamReader reader = new StreamReader(PathUtils.GetArchiveCachePlaytimePath(archiveCachePath));
                lastPlayTime = long.Parse(reader.ReadToEnd());
                reader.Close();
            }
            catch (Exception e)
            {
                Logger.Log("Error reading last played time from cache, using 0 default.");
                Logger.Log(e.ToString(), Logger.LogLevel.Exception);
                lastPlayTime = 0;
            }

            return lastPlayTime;
        }

        /// <summary>
        /// Updates the last played time of the given archive cache to now (in ticks)
        /// </summary>
        /// <param name="archiveCachePath">The path of the cached archive.</param>
        static void UpdateArchiveCachePlaytime(string archiveCachePath)
        {
            string lastPlayedPath = PathUtils.GetArchiveCachePlaytimePath(archiveCachePath);

            try
            {
                File.Delete(lastPlayedPath);
                StreamWriter writer = new StreamWriter(lastPlayedPath, false);
                writer.Write(DateTime.Now.Ticks.ToString());
                writer.Flush();
                writer.Close();
            }
            catch (Exception e)
            {
                Logger.Log("Error saving last played time in cache.");
                Logger.Log(e.ToString(), Logger.LogLevel.Exception);
            }
        }

        /// <summary>
        /// Checks for and extracts an archive to the archive cache if it is not already in there. Updates the last played time of the cached archive.
        /// </summary>
        public static void ExtractArchive()
        {
            if (ArchiveInCache())
            {
                Logger.Log("Archive found in cache, bypassing extraction.");
            }
            else
            {
                AddArchiveToCache();
            }
            UpdateArchiveCachePlaytime(Archive.CachePath);
        }

        /// <summary>
        /// DEPRECATED. Use ListCacheArchive() instead.
        /// Generates an absolute path list of files in the archive cache based on the file list from 7z.
        /// </summary>
        /// <param name="listStdout"></param>
        /// <returns></returns>
        static string MakeArchiveListAbsolute(string listStdout)
        {
            string absoluteStdout = listStdout;
            Match match = null;

            // Find the first "Path = " entry in the 7z list stdout, then skip to the next match (the first file within the archive)
            match = Regex.Match(listStdout, @"Path = .*", RegexOptions.IgnoreCase).NextMatch();

            if (match.Success)
            {
                absoluteStdout = string.Empty;
            }

            // Loop through all of the matches, building up a string of files with the absolute path
            while (match.Success)
            {
                // Remove the leading "Path = " portion of the result
                // Remove any white space from the result (namely a trailing \r)
                // LB only seems to check for "Path = X" strings in 7z list stdout, so we only need to build a string containing "Path = "
                absoluteStdout = string.Format("{0}\r\nPath = {1}", absoluteStdout, Path.Combine(Archive.CachePath, match.Value.Replace("Path = ", string.Empty).Trim()));
                match = match.NextMatch();
            }

            return absoluteStdout;
        }

        /// <summary>
        /// Applies the current file entension priority to the input file list by filtering out any files where the file extensions do not match the priority list.
        /// If there is no file priority list, or no files match, the file list will be unchanged.
        /// </summary>
        /// <param name="listStdout">Current file list.</param>
        /// <returns>Modified file list, or identical file list if no file priority applied.</returns>
        static string ApplyExtensionPriority(string listStdout)
        {
            string priorityStdout = listStdout;
            Match match = null;
            bool extensionFound = false;

            try
            {
                string[] extensionPriority = Config.ExtensionPriority[string.Format(@"{0} \ {1}", launchGameInfo.Emulator, launchGameInfo.Platform)].Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);

                // Search the extensions in priority order
                foreach (string extension in extensionPriority)
                {
                    // Find the first "Path = " entry in the 7z list stdout with the specified extension
                    match = Regex.Match(listStdout, string.Format(@"Path = .*\.{0}", extension.Trim()), RegexOptions.IgnoreCase);

                    if (match.Success)
                    {
                        priorityStdout = string.Empty;
                        extensionFound = true;

                        Logger.Log(string.Format("Applying extension priority \"{0}\".", extension));
                    }

                    // Loop through all of the matches, building up a string of files with the priority extension
                    while (match.Success)
                    {
                        priorityStdout = string.Format("{0}\r\n{1}", priorityStdout, match.Value.Trim());
                        match = match.NextMatch();
                    }

                    // Stop looking for extensions if we found one already
                    if (extensionFound)
                    {
                        break;
                    }
                }
            }
            catch (KeyNotFoundException)
            {

            }

            return priorityStdout;
        }
    }
}
