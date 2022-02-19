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

                return string.Format("v{0}.{1}.{2} beta", version.Major, version.Minor, version.Build);
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
                DiskUtils.CreateFile(PathUtils.GetArchiveCacheExtractingFlagPath(Archive.CachePath));
                launchGameInfo.DecompressedSize = Archive.DecompressedSize;
                ClearCacheSpace(Archive.DecompressedSize);
                Logger.Log(string.Format("Extracting archive to \"{0}\".", Archive.CachePath));
                Zip.Extract(Archive.Path, Archive.CachePath, ref stdout, ref stderr, ref exitCode);
                if (exitCode == 0)
                {
                    launchGameInfo.Save(PathUtils.GetArchiveCacheGameInfoPath(Archive.CachePath));
                    DiskUtils.SetDirectoryContentsReadOnly(Archive.CachePath);
                    File.Delete(PathUtils.GetArchiveCacheExtractingFlagPath(Archive.CachePath));
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
                Logger.Log(string.Format("Verifying cache integrity... ({0})", PathUtils.CachePath()));

                string[] dirs = Directory.GetDirectories(PathUtils.CachePath());

                foreach (string dir in dirs)
                {
                    string extractingFlagPath = PathUtils.GetArchiveCacheExtractingFlagPath(dir);
                    string gameInfoPath = Path.Combine(dir, PathUtils.GetGameInfoFileName());

                    if (File.Exists(extractingFlagPath))
                    {
                        Logger.Log(string.Format("Found partially extracted archive, deleting cached item \"{0}\".", dir));
                        DiskUtils.DeleteDirectory(dir);
                        continue;
                    }
                    else if (File.Exists(gameInfoPath))
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
            string stderr = string.Empty;
            int exitCode = 0;
            string fileList = string.Empty;

            if (ArchiveInCache())
            {
                fileList = ListCacheArchive();
            }
            else
            {
                fileList = ListFileArchive(ref stderr, ref exitCode);
            }

            Console.Write(fileList);
            Console.Write(stderr);
            Environment.ExitCode = exitCode;
        }

        /// <summary>
        /// Lists all of the files in the archive.
        /// </summary>
        /// <returns>The file list for the archive in "Path = absolute\file\path.ext" format, with one entry per line.</returns>
        public static string ListFileArchive(ref string stderr, ref int exitCode)
        {
            string stdout = string.Empty;
            string selectedFilePath = string.Empty;
            string fileList = string.Empty;

            if (!launchGameInfo.SelectedFile.Equals(string.Empty))
            {
                if (Zip.GetFileList(Archive.Path, launchGameInfo.SelectedFile).Length > 0)
                {
                    fileList = "Path = " + launchGameInfo.SelectedFile;
                    Logger.Log(string.Format("Selected individual file from archive \"{0}\".", launchGameInfo.SelectedFile));
                }
            }

            if (fileList == string.Empty)
            {
                try
                {
                    string[] extensionPriority = Config.FilenamePriority[string.Format(@"{0} \ {1}", launchGameInfo.Emulator, launchGameInfo.Platform)].Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);

                    // Search the extensions in priority order
                    foreach (string extension in extensionPriority)
                    {
                        List<string> filePaths = Zip.GetFileList(Archive.Path, string.Format("*{0}", extension.Trim())).ToList();

                        if (filePaths.Count > 0)
                        {
                            fileList = string.Join("\r\nPath = ", filePaths);
                            fileList = string.Format("Path = {0}\r\n", fileList);
                            Logger.Log(string.Format("Using filename priority \"{0}\".", extension.Trim()));
                            break;
                        }
                    }
                }
                catch (KeyNotFoundException)
                {

                }
            }

            if (fileList == string.Empty)
            {
                List<string> filePaths = Zip.GetFileList(Archive.Path).ToList();

                if (filePaths.Count > 0)
                {
                    fileList = string.Join("\r\nPath = ", filePaths);
                    fileList = string.Format("Path = {0}\r\n", fileList);
                }
            }

            return fileList;

            /*
            Zip.ListVerbose(Archive.Path, ref stdout, ref stderr, ref exitCode);
            selectedFilePath = "Path = " + launchGameInfo.SelectedFile;

            if (!launchGameInfo.SelectedFile.Equals(string.Empty) && stdout.Contains(selectedFilePath))
            {
                fileList = selectedFilePath;
                Logger.Log(string.Format("Selected individual file from archive \"{0}\".", launchGameInfo.SelectedFile));
            }
            else
            {
                fileList = ApplyExtensionPriority(stdout);
            }
            
            return fileList;*/
        }

        /// <summary>
        /// Lists all of the files in the cached archive path.
        /// </summary>
        /// <returns>The file list for the cached archive in "Path = absolute\file\path.ext" format, with one entry per line.</returns>
        public static string ListCacheArchive()
        {
            string fileList = string.Empty;
            string[] exclude = new string[] { PathUtils.GetArchiveCachePlaytimePath(Archive.CachePath),
                                              PathUtils.GetArchiveCacheGameInfoPath(Archive.CachePath),
                                              PathUtils.GetArchiveCacheExtractingFlagPath(Archive.CachePath) };
            string selectedFilePath = string.Empty;

            if (!launchGameInfo.SelectedFile.Equals(string.Empty))
            {
                if (Directory.GetFiles(Archive.CachePath, launchGameInfo.SelectedFile, SearchOption.AllDirectories).Length > 0)
                {
                    fileList = "Path = " + Path.Combine(PathUtils.ArchiveCachePath(Archive.Path), launchGameInfo.SelectedFile);
                    Logger.Log(string.Format("Selected individual file from archive \"{0}\".", launchGameInfo.SelectedFile));
                }
            }

            if (fileList == string.Empty)
            {
                try
                {
                    string[] extensionPriority = Config.FilenamePriority[string.Format(@"{0} \ {1}", launchGameInfo.Emulator, launchGameInfo.Platform)].Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);

                    // Search the extensions in priority order
                    foreach (string extension in extensionPriority)
                    {
                        List<string> filePaths = Directory.GetFiles(Archive.CachePath, string.Format("*{0}", extension.Trim()), SearchOption.AllDirectories).ToList();

                        foreach (string ex in exclude)
                        {
                            filePaths.Remove(ex);
                        }

                        if (filePaths.Count > 0)
                        {
                            fileList = string.Join("\r\nPath = ", filePaths);
                            fileList = string.Format("Path = {0}\r\n", fileList);
                            Logger.Log(string.Format("Using filename priority \"{0}\".", extension.Trim()));
                            break;
                        }
                    }
                }
                catch (KeyNotFoundException)
                {

                }
            }

            if (fileList == string.Empty)
            {
                List<string> filePaths = Directory.GetFiles(Archive.CachePath, "*", SearchOption.AllDirectories).ToList();

                foreach (string ex in exclude)
                {
                    filePaths.Remove(ex);
                }

                if (filePaths.Count > 0)
                {
                    fileList = string.Join("\r\nPath = ", filePaths);
                    fileList = string.Format("Path = {0}\r\n", fileList);
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
                    string gameInfoPath = Path.Combine(dirs[i], PathUtils.GetGameInfoFileName());

                    if (File.Exists(gameInfoPath))
                    {
                        GameInfo gameInfo = new GameInfo(gameInfoPath);
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
            string playTimePath = PathUtils.GetArchiveCachePlaytimePath(archiveCachePath);

            if (File.Exists(playTimePath))
            {
                try
                {
                    StreamReader reader = new StreamReader(playTimePath);
                    lastPlayTime = long.Parse(reader.ReadToEnd());
                    reader.Close();
                }
                catch (Exception e)
                {
                    Logger.Log("Error reading last played time from cache, using 0 default.");
                    Logger.Log(e.ToString(), Logger.LogLevel.Exception);
                    lastPlayTime = 0;
                }
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
    }
}
