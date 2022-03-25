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
        /// Plugin version.
        /// </summary>
        public static Version Version => Assembly.GetExecutingAssembly().GetName().Version;

        /// <summary>
        /// Plugin version string, in the form "vMajor.Minor".
        /// </summary>
        public static string VersionString
        {
            get
            {
                Version version = Version;

                return string.Format("v{0}.{1}", version.Major, version.Minor);
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
                Zip.ProgressDivisor = LaunchGameInfo.Game.TotalDiscs - LaunchGameInfo.GetDiscCountInCache();

                int discIndex = 0;
                foreach (var discInfo in LaunchGameInfo.Game.Discs)
                {
                    if (!LaunchGameInfo.GetArchiveInCache(discInfo.Disc))
                    {
                        Zip.ProgressOffset = (100 / Zip.ProgressDivisor) * discIndex;
                        AddArchiveToCache(discInfo.Disc);
                        discIndex++;
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

        public static void GenerateM3u()
        {
            if (LaunchGameInfo.Game.MultiDisc && Config.MultiDiscSupport)
            {
                Dictionary<int, string> filePaths = new Dictionary<int, string>();
                foreach (var discInfo in LaunchGameInfo.Game.Discs)
                {
                    filePaths.Add(discInfo.Disc, ListCacheArchive(discInfo.Disc).FirstOrDefault());
                }

                foreach (var discInfo in LaunchGameInfo.Game.Discs)
                {
                    List<string> multiDiscPaths = new List<string>();
                    foreach (var path in filePaths)
                    {
                        if (discInfo.Disc == path.Key)
                        {
                            multiDiscPaths.Insert(0, path.Value);
                        }
                        else
                        {
                            multiDiscPaths.Add(path.Value);
                        }
                    }

                    string m3uPathGameId = PathUtils.GetArchiveCacheM3uGameIdPath(LaunchGameInfo.GetArchiveCachePath(discInfo.Disc), LaunchGameInfo.Game.GameId);
                    string m3uPathGameTitle = PathUtils.GetArchiveCacheM3uGameTitlePath(LaunchGameInfo.GetArchiveCachePath(discInfo.Disc), LaunchGameInfo.Game.GameId, LaunchGameInfo.Game.Title, LaunchGameInfo.Game.Version, discInfo.Disc);
                    string m3uPath = Config.UseGameIdAsM3uFilename ? m3uPathGameId : m3uPathGameTitle;
                    try
                    {
                        DiskUtils.DeleteFile(m3uPathGameId);
                        DiskUtils.DeleteFile(m3uPathGameTitle);
                        File.WriteAllLines(m3uPath, multiDiscPaths);
                        DiskUtils.SetFileReadOnly(m3uPath);
                    }
                    catch (Exception e)
                    {
                        Logger.Log(string.Format("Failed to save m3u file \"{0}\".", m3uPath), Logger.LogLevel.Exception);
                        Logger.Log(e.ToString(), Logger.LogLevel.Exception);
                    }
                }
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
            string stdout = string.Empty;
            string stderr = string.Empty;
            int exitCode = 0;
            List<string> fileList = new List<string>();

            if (LaunchGameInfo.GetArchiveInCache())
            {
                if (LaunchGameInfo.Game.MultiDisc && Config.MultiDiscSupport && LaunchGameInfo.Game.EmulatorPlatformM3u)
                {
                    string m3uPath = Config.UseGameIdAsM3uFilename ? PathUtils.GetArchiveCacheM3uGameIdPath(LaunchGameInfo.GetArchiveCachePath(LaunchGameInfo.Game.SelectedDisc), LaunchGameInfo.Game.GameId)
                                                                   : PathUtils.GetArchiveCacheM3uGameTitlePath(LaunchGameInfo.GetArchiveCachePath(LaunchGameInfo.Game.SelectedDisc), LaunchGameInfo.Game.GameId, LaunchGameInfo.Game.Title, LaunchGameInfo.Game.Version, LaunchGameInfo.Game.SelectedDisc);
                    // This is a multi-disc game, and the emulator supports m3u files. Set the file list to the generated m3u file.
                    fileList.Add(m3uPath);
                }
                else
                {
                    fileList = ListCacheArchive();
                }
            }
            else
            {
                fileList = ListFileArchive(ref stderr, ref exitCode);
            }

            if (fileList.Count > 0)
            {
                stdout = string.Format("Path = {0}", string.Join("\r\nPath = ", fileList));
            }
            else
            {
                Logger.Log("File listing found 0 files.");
                exitCode = 1;
            }

            Console.Write(stdout);
            Console.Write(stderr);
            Environment.ExitCode = exitCode;
        }

        /// <summary>
        /// Lists all of the files in the archive.
        /// </summary>
        /// <returns>The file list for the archive in "Path = absolute\file\path.ext" format, with one entry per line.</returns>
        public static List<string> ListFileArchive(ref string stderr, ref int exitCode)
        {
            string stdout = string.Empty;
            string selectedFilePath = string.Empty;
            List<string> fileList = new List<string>();

            if (!LaunchGameInfo.Game.SelectedFile.Equals(string.Empty))
            {
                if (Zip.GetFileList(LaunchGameInfo.GetArchivePath(), LaunchGameInfo.Game.SelectedFile).Length > 0)
                {
                    fileList.Add(LaunchGameInfo.Game.SelectedFile);
                    Logger.Log(string.Format("Selected individual file from archive \"{0}\".", LaunchGameInfo.Game.SelectedFile));
                    return fileList;
                }
            }

            List<string> prioritySections = new List<string>();
            prioritySections.Add(string.Format(@"{0} \ {1}", LaunchGameInfo.Game.Emulator, LaunchGameInfo.Game.Platform));
            prioritySections.Add(@"All \ All");

            foreach (var prioritySection in prioritySections)
            {
                try
                {
                    string[] extensionPriority = Config.FilenamePriority[prioritySection].Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);

                    // Search the extensions in priority order
                    foreach (string extension in extensionPriority)
                    {
                        fileList = Zip.GetFileList(LaunchGameInfo.GetArchivePath(), string.Format("*{0}", extension.Trim())).ToList();

                        if (fileList.Count > 0)
                        {
                            Logger.Log(string.Format("Using filename priority \"{0}\".", extension.Trim()));
                            return fileList;
                        }
                    }
                }
                catch (KeyNotFoundException)
                {

                }
            }

            fileList = Zip.GetFileList(LaunchGameInfo.GetArchivePath()).ToList();

            return fileList;
        }

        /// <summary>
        /// Lists all of the files in the cached archive path.
        /// </summary>
        /// <returns>The file list for the cached archive in "Path = absolute\file\path.ext" format, with one entry per line.</returns>
        public static List<string> ListCacheArchive(int? disc = null)
        {
            List<string> fileList = new List<string>();
            string[] exclude = new string[] { PathUtils.GetArchiveCachePlaytimePath(LaunchGameInfo.GetArchiveCachePath(disc)),
                                              PathUtils.GetArchiveCacheGameInfoPath(LaunchGameInfo.GetArchiveCachePath(disc)),
                                              PathUtils.GetArchiveCacheExtractingFlagPath(LaunchGameInfo.GetArchiveCachePath(disc)),
                                              PathUtils.GetArchiveCacheM3uGameIdPath(LaunchGameInfo.GetArchiveCachePath(disc), LaunchGameInfo.Game.GameId),
                                              PathUtils.GetArchiveCacheM3uGameTitlePath(LaunchGameInfo.GetArchiveCachePath(disc), LaunchGameInfo.Game.GameId, LaunchGameInfo.Game.Title, LaunchGameInfo.Game.Version, disc) };

            if (!LaunchGameInfo.Game.SelectedFile.Equals(string.Empty) && disc == null)
            {
                if (Directory.GetFiles(LaunchGameInfo.GetArchiveCachePath(), LaunchGameInfo.Game.SelectedFile, SearchOption.AllDirectories).Length > 0)
                {
                    fileList.Add(Path.Combine(LaunchGameInfo.GetArchiveCachePath(), LaunchGameInfo.Game.SelectedFile));
                    Logger.Log(string.Format("Selected individual file from archive \"{0}\".", LaunchGameInfo.Game.SelectedFile));
                    return fileList;
                }
            }

            List<string> prioritySections = new List<string>();
            prioritySections.Add(string.Format(@"{0} \ {1}", LaunchGameInfo.Game.Emulator, LaunchGameInfo.Game.Platform));
            prioritySections.Add(@"All \ All");

            foreach (var prioritySection in prioritySections)
            {
                try
                {
                    string[] extensionPriority = Config.FilenamePriority[prioritySection].Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);

                    // Search the extensions in priority order
                    foreach (string extension in extensionPriority)
                    {
                        fileList = Directory.GetFiles(LaunchGameInfo.GetArchiveCachePath(disc), string.Format("*{0}", extension.Trim()), SearchOption.AllDirectories).ToList();

                        foreach (string ex in exclude)
                        {
                            fileList.Remove(ex);
                        }

                        if (fileList.Count > 0)
                        {
                            Logger.Log(string.Format("Using filename priority \"{0}\".", extension.Trim()));
                            // If we're listing a disc from a multi-disc game, only return a single file
                            if (disc != null)
                            {
                                fileList.RemoveRange(1, fileList.Count() - 1);
                            }
                            return fileList;
                        }
                    }
                }
                catch (KeyNotFoundException)
                {

                }
            }

            fileList = Directory.GetFiles(LaunchGameInfo.GetArchiveCachePath(disc), "*", SearchOption.AllDirectories).ToList();

            foreach (string ex in exclude)
            {
                fileList.Remove(ex);
            }

            if (fileList.Count > 0)
            {
                // If we're listing a disc from a multi-disc game, only return a single file
                if (disc != null)
                {
                    fileList.RemoveRange(1, fileList.Count() - 1);
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
                // Check if the sizeToDelete has wrapped (above sum overflowed and is now negative)
                if (sizeToDelete < 0)
                {
                    sizeToDelete = long.MaxValue;
                }

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
                GenerateM3u();
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
                    GenerateM3u();
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
