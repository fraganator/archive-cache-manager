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
        /// Extract the archive to the cache. In the event of a cache error, archive will be extracted to LaunchBox's temp lcation.
        /// </summary>
        public static void AddArchiveToCache(int? disc = null)
        {
            if (LaunchGameInfo.Game.MultiDisc && Config.MultiDiscSupport && disc == null)
            {
                foreach (var discInfo in LaunchGameInfo.Game.Discs)
                {
                    if (!LaunchGameInfo.GetArchiveInCache(discInfo.Disc))
                    {
                        AddArchiveToCache(discInfo.Disc);
                    }
                }

                return;
            }

            string stdout = string.Empty;
            string stderr = string.Empty;
            int exitCode = 0;

            if (CreateCache())
            {
                DiskUtils.CreateFile(PathUtils.GetArchiveCacheExtractingFlagPath(LaunchGameInfo.GetArchiveCachePath(disc)));
                ClearCacheSpace(LaunchGameInfo.GetDecompressedSize(disc));
                Logger.Log(string.Format("Extracting archive to \"{0}\".", LaunchGameInfo.GetArchiveCachePath(disc)));
                Zip.Extract(LaunchGameInfo.GetArchivePath(disc), LaunchGameInfo.GetArchiveCachePath(disc), ref stdout, ref stderr, ref exitCode);
                if (exitCode == 0)
                {
                    LaunchGameInfo.SaveToCache(disc);
                    DiskUtils.SetDirectoryContentsReadOnly(LaunchGameInfo.GetArchiveCachePath(disc));
                    File.Delete(PathUtils.GetArchiveCacheExtractingFlagPath(LaunchGameInfo.GetArchiveCachePath(disc)));
                }
                else
                {
                    Logger.Log("Extraction error, removing output files from cache.");
                    DiskUtils.DeleteDirectory(LaunchGameInfo.GetArchiveCachePath(disc));
                }
            }
            else
            {
                Logger.Log(@"Error creating or accessing cache location. Archive will be extracted to LaunchBox\ThirdParty\7-Zip\Temp.");
                Zip.Extract(LaunchGameInfo.GetArchivePath(disc), PathUtils.GetLaunchBox7zTempPath(), ref stdout, ref stderr, ref exitCode);
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
                    else
                    {
                        GameInfo gameInfo = new GameInfo(Path.Combine(dir, PathUtils.GetGameInfoFileName()));
                        if (!gameInfo.InfoLoaded)
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

            if (LaunchGameInfo.GetArchiveInCache())
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

            if (!LaunchGameInfo.Game.SelectedFile.Equals(string.Empty))
            {
                if (Zip.GetFileList(LaunchGameInfo.GetArchivePath(), LaunchGameInfo.Game.SelectedFile).Length > 0)
                {
                    fileList = "Path = " + LaunchGameInfo.Game.SelectedFile;
                    Logger.Log(string.Format("Selected individual file from archive \"{0}\".", LaunchGameInfo.Game.SelectedFile));
                }
            }

            if (fileList == string.Empty)
            {
                try
                {
                    string[] extensionPriority = Config.FilenamePriority[string.Format(@"{0} \ {1}", LaunchGameInfo.Game.Emulator, LaunchGameInfo.Game.Platform)].Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);

                    // Search the extensions in priority order
                    foreach (string extension in extensionPriority)
                    {
                        List<string> filePaths = Zip.GetFileList(LaunchGameInfo.GetArchivePath(), string.Format("*{0}", extension.Trim())).ToList();

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
                List<string> filePaths = Zip.GetFileList(LaunchGameInfo.GetArchivePath()).ToList();

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
            string[] exclude = new string[] { PathUtils.GetArchiveCachePlaytimePath(LaunchGameInfo.GetArchiveCachePath()),
                                              PathUtils.GetArchiveCacheGameInfoPath(LaunchGameInfo.GetArchiveCachePath()),
                                              PathUtils.GetArchiveCacheExtractingFlagPath(LaunchGameInfo.GetArchiveCachePath()) };
            string selectedFilePath = string.Empty;

            if (!LaunchGameInfo.Game.SelectedFile.Equals(string.Empty))
            {
                if (Directory.GetFiles(LaunchGameInfo.GetArchiveCachePath(), LaunchGameInfo.Game.SelectedFile, SearchOption.AllDirectories).Length > 0)
                {
                    fileList = "Path = " + Path.Combine(LaunchGameInfo.GetArchiveCachePath(), LaunchGameInfo.Game.SelectedFile);
                    Logger.Log(string.Format("Selected individual file from archive \"{0}\".", LaunchGameInfo.Game.SelectedFile));
                }
            }

            if (fileList == string.Empty)
            {
                try
                {
                    string[] extensionPriority = Config.FilenamePriority[string.Format(@"{0} \ {1}", LaunchGameInfo.Game.Emulator, LaunchGameInfo.Game.Platform)].Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);

                    // Search the extensions in priority order
                    foreach (string extension in extensionPriority)
                    {
                        List<string> filePaths = Directory.GetFiles(LaunchGameInfo.GetArchiveCachePath(), string.Format("*{0}", extension.Trim()), SearchOption.AllDirectories).ToList();

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
                List<string> filePaths = Directory.GetFiles(LaunchGameInfo.GetArchiveCachePath(), "*", SearchOption.AllDirectories).ToList();

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
                // Ignore any of the archive cache paths for the same multi-disc game
                if (LaunchGameInfo.Game.MultiDisc && Config.MultiDiscSupport)
                {
                    List<string> dirsToExclude = new List<string>();
                    foreach (var disc in LaunchGameInfo.Game.Discs)
                    {
                        dirsToExclude.Add(LaunchGameInfo.GetArchiveCachePath(disc.Disc));
                    }
                    dirs = dirs.Except(dirsToExclude).ToArray();
                }
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
        public static void UpdateArchiveCachePlaytime()
        {
            string lastPlayedPath = PathUtils.GetArchiveCachePlaytimePath(LaunchGameInfo.GetArchiveCachePath());

            try
            {
                File.Delete(lastPlayedPath);
                StreamWriter writer = new StreamWriter(lastPlayedPath, false);
                writer.Write(DateTime.Now.Ticks.ToString());
                writer.Flush();
                writer.Close();

                if (LaunchGameInfo.Game.MultiDisc && Config.MultiDiscSupport)
                {
                    string dest = string.Empty;
                    foreach (var discInfo in LaunchGameInfo.Game.Discs)
                    {
                        dest = PathUtils.GetArchiveCachePlaytimePath(LaunchGameInfo.GetArchiveCachePath(discInfo.Disc));
                        if (!string.Equals(lastPlayedPath, dest, StringComparison.InvariantCultureIgnoreCase))
                        {
                            File.Copy(lastPlayedPath, dest, true);
                        }
                    }

                    return;
                }
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
        public static void ExtractArchive(string[] args)
        {
            if (LaunchGameInfo.GetArchiveInCache())
            {
                Logger.Log("Archive found in cache, bypassing extraction.");
                LaunchGameInfo.SaveToCache();
                UpdateArchiveCachePlaytime();
            }
            else
            {
                // Only now do we calculate the decompressed size, as can be very time consuming when large number of files in archive.
                // MinArchiveSize is megabytes, multiply to convert to bytes.
                // Always store multi-disc games in the cache, regardless of minimum size.
                if ((LaunchGameInfo.GetDecompressedSize() > Config.MinArchiveSize * 1048576) || (LaunchGameInfo.Game.MultiDisc && Config.MultiDiscSupport))
                {
                    AddArchiveToCache();
                    UpdateArchiveCachePlaytime();
                }
                else
                {
                    Logger.Log("Archive smaller than MinArchiveSize, bypassing extraction to cache.");
                    Zip.Call7z(args);
                }
            }
        }
    }
}
