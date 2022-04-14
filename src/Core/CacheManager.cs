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

                return string.Format("v{0}.{1} beta 3", version.Major, version.Minor);
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
            if (LaunchInfo.Game.MultiDisc && LaunchInfo.MultiDiscSupport && disc == null)
            {
                LaunchInfo.Extractor.ProgressDivisor = LaunchInfo.Game.TotalDiscs - LaunchInfo.GetDiscCountInCache();

                int discIndex = 0;
                foreach (var discInfo in LaunchInfo.Game.Discs)
                {
                    if (!LaunchInfo.GetArchiveInCache(discInfo.Disc))
                    {
                        LaunchInfo.Extractor.ProgressOffset = (100 / LaunchInfo.Extractor.ProgressDivisor) * discIndex;
                        AddArchiveToCache(discInfo.Disc);
                        discIndex++;
                    }
                }

                return;
            }

            if (CreateCache())
            {
                string singleFile = LaunchInfo.GetExtractSingleFile();
                if (!string.IsNullOrEmpty(singleFile))
                {
                    // Delete previous directory contents. Don't want a build up of old individually extracted files.
                    DiskUtils.DeleteDirectory(LaunchInfo.GetArchiveCachePath(disc), true);
                }

                DiskUtils.CreateFile(PathUtils.GetArchiveCacheExtractingFlagPath(LaunchInfo.GetArchiveCachePath(disc)));
                ClearCacheSpace(LaunchInfo.GetSize(disc));
                Logger.Log(string.Format("Extracting archive to \"{0}\".", LaunchInfo.GetArchiveCachePath(disc)));

                var result = LaunchInfo.Extractor.Extract(LaunchInfo.GetArchivePath(disc), LaunchInfo.GetArchiveCachePath(disc), singleFile.ToSingleArray());
                if (result)
                {
                    LaunchInfo.SaveToCache(disc);
                    DiskUtils.SetDirectoryContentsReadOnly(LaunchInfo.GetArchiveCachePath(disc));
                    File.Delete(PathUtils.GetArchiveCacheExtractingFlagPath(LaunchInfo.GetArchiveCachePath(disc)));
                }
                else
                {
                    Logger.Log("Extraction error, removing output files from cache.");
                    DiskUtils.DeleteDirectory(LaunchInfo.GetArchiveCachePath(disc));
                }
            }
            else
            {
                Logger.Log(@"Error creating or accessing cache location. Archive will be extracted to LaunchBox\ThirdParty\7-Zip\Temp.");
                LaunchInfo.Extractor.Extract(LaunchInfo.GetArchivePath(disc), PathUtils.GetLaunchBox7zTempPath(), LaunchInfo.GetExtractSingleFile().ToSingleArray());
            }
        }

        public static void GenerateM3u()
        {
            if (LaunchInfo.Game.MultiDisc && LaunchInfo.MultiDiscSupport)
            {
                Dictionary<int, string> filePaths = new Dictionary<int, string>();
                foreach (var discInfo in LaunchInfo.Game.Discs)
                {
                    // Delete any previously generated m3u file, so it doesn't get included in the subsequent archive listing
                    DiskUtils.DeleteFile(LaunchInfo.GetM3uName(discInfo.Disc));
                    filePaths.Add(discInfo.Disc, ListCacheArchive(LaunchInfo.GetArchiveCachePath(discInfo.Disc), discInfo.Disc).FirstOrDefault());
                }

                foreach (var discInfo in LaunchInfo.Game.Discs)
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

                    string m3uPath = LaunchInfo.GetM3uName(discInfo.Disc);
                    try
                    {
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
                            DiskUtils.DeleteDirectory(dir, false, true);
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

            if (LaunchInfo.GetArchiveInCache())
            {
                if (LaunchInfo.Game.MultiDisc && LaunchInfo.MultiDiscSupport && LaunchInfo.Game.EmulatorPlatformM3u)
                {
                    // This is a multi-disc game, and the emulator supports m3u files. Set the file list to the generated m3u file.
                    fileList.Add(LaunchInfo.GetM3uName(LaunchInfo.Game.SelectedDisc));
                }
                else
                {
                    fileList = ListCacheArchive(LaunchInfo.GetArchiveCacheLaunchPath());
                }
            }
            else
            {
                fileList = ListFileArchive();
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
        public static List<string> ListFileArchive()
        {
            string stdout = string.Empty;
            string selectedFilePath = string.Empty;
            List<string> fileList = new List<string>();

            if (!LaunchInfo.Game.SelectedFile.Equals(string.Empty))
            {
                if (LaunchInfo.Extractor.List(LaunchInfo.GetArchivePath(), LaunchInfo.Game.SelectedFile.ToSingleArray()).Length > 0)
                {
                    fileList.Add(LaunchInfo.Game.SelectedFile);
                    Logger.Log(string.Format("Selected individual file from archive \"{0}\".", LaunchInfo.Game.SelectedFile));
                    return fileList;
                }
            }

            List<string> prioritySections = new List<string>();
            prioritySections.Add(Config.EmulatorPlatformKey(LaunchInfo.Game.Emulator, LaunchInfo.Game.Platform));
            prioritySections.Add(Config.EmulatorPlatformKey("All", "All"));

            foreach (var prioritySection in prioritySections)
            {
                try
                {
                    string[] extensionPriority = Config.GetFilenamePriority(prioritySection).Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);

                    // Search the extensions in priority order
                    foreach (string extension in extensionPriority)
                    {
                        fileList = LaunchInfo.Extractor.List(LaunchInfo.GetArchivePath(), string.Format("{0}", extension.Trim()).ToSingleArray(), null, true).ToList();

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

            fileList = LaunchInfo.Extractor.List(LaunchInfo.GetArchivePath()).ToList();

            return fileList;
        }

        /// <summary>
        /// Lists the files in the cached archive path, excluding cache management files.
        /// </summary>
        /// <returns>The file list for the cached archive, with one entry per line. All paths are absolute.</returns>
        public static List<string> ListCacheArchive(string archiveCachePath, int? disc = null)
        {
            List<string> fileList = new List<string>();
            string[] managerFiles = PathUtils.GetManagerFiles(archiveCachePath);

            if (!string.IsNullOrEmpty(LaunchInfo.Game.SelectedFile) && !(LaunchInfo.Game.MultiDisc && LaunchInfo.MultiDiscSupport))
            {
                if (Directory.GetFiles(archiveCachePath, LaunchInfo.Game.SelectedFile, SearchOption.AllDirectories).Length > 0)
                {
                    fileList.Add(Path.Combine(archiveCachePath, LaunchInfo.Game.SelectedFile));
                    Logger.Log(string.Format("Selected individual file from archive \"{0}\".", LaunchInfo.Game.SelectedFile));
                    return fileList;
                }
            }

            List<string> prioritySections = new List<string>();
            prioritySections.Add(Config.EmulatorPlatformKey(LaunchInfo.Game.Emulator, LaunchInfo.Game.Platform));
            prioritySections.Add(Config.EmulatorPlatformKey("All", "All"));

            foreach (var prioritySection in prioritySections)
            {
                try
                {
                    string[] extensionPriority = Utils.SplitExtensions(Config.GetFilenamePriority(prioritySection));

                    // Search the extensions in priority order
                    foreach (string extension in extensionPriority)
                    {
                        fileList = Directory.GetFiles(archiveCachePath, string.Format("*{0}", extension), SearchOption.AllDirectories).ToList();

                        foreach (string ex in managerFiles)
                        {
                            fileList.Remove(ex);
                        }

                        if (fileList.Count > 0)
                        {
                            Logger.Log(string.Format("Using filename priority \"{0}\".", extension));
                            return fileList;
                        }
                    }
                }
                catch (KeyNotFoundException)
                {

                }
            }

            fileList = Directory.GetFiles(archiveCachePath, "*", SearchOption.AllDirectories).ToList();

            foreach (string ex in managerFiles)
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
                if (LaunchInfo.Game.MultiDisc && LaunchInfo.MultiDiscSupport)
                {
                    List<string> dirsToExclude = new List<string>();
                    foreach (var disc in LaunchInfo.Game.Discs)
                    {
                        dirsToExclude.Add(LaunchInfo.GetArchiveCachePath(disc.Disc));
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
            string lastPlayedPath = PathUtils.GetArchiveCachePlaytimePath(LaunchInfo.GetArchiveCachePath());

            try
            {
                File.Delete(lastPlayedPath);
                StreamWriter writer = new StreamWriter(lastPlayedPath, false);
                writer.Write(DateTime.Now.Ticks.ToString());
                writer.Flush();
                writer.Close();

                if (LaunchInfo.Game.MultiDisc && LaunchInfo.MultiDiscSupport)
                {
                    string dest = string.Empty;
                    foreach (var discInfo in LaunchInfo.Game.Discs)
                    {
                        dest = PathUtils.GetArchiveCachePlaytimePath(LaunchInfo.GetArchiveCachePath(discInfo.Disc));
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

        public static void LinkCacheToLaunchFolder()
        {
            string launchPath = LaunchInfo.GetArchiveCacheLaunchPath();
            if (LaunchInfo.LaunchPathConfig != Config.LaunchPath.Default)
            {
                Logger.Log(string.Format("Linking {0} to cached archive.", launchPath));
                try
                {
                    if (!Directory.Exists(launchPath))
                    {
                        Directory.CreateDirectory(launchPath);
                    }

                    // Delete the old directory contents
                    DiskUtils.DeleteDirectory(launchPath, true, true);

                    string[] managerFiles = PathUtils.GetManagerFiles(LaunchInfo.GetArchiveCachePath());

                    foreach (var file in Directory.GetFiles(LaunchInfo.GetArchiveCachePath()))
                    {
                        if (!managerFiles.Contains(file))
                        {
                            string relativeFilePath = PathUtils.GetRelativePath(LaunchInfo.GetArchiveCachePath(), file);
                            DiskUtils.HardLink(Path.Combine(launchPath, relativeFilePath), file);
                        }
                    }

                    File.WriteAllText(PathUtils.GetArchiveCacheLinkFlagPath(launchPath), LaunchInfo.GetArchiveCachePath());
                    File.WriteAllText(PathUtils.GetArchiveCacheLinkFlagPath(LaunchInfo.GetArchiveCachePath()), launchPath);
                }
                catch (Exception e)
                {
                    Logger.Log(e.ToString());
                }
            }
        }

        /// <summary>
        /// Checks for and extracts an archive to the archive cache if it is not already in there. Updates the last played time of the cached archive.
        /// </summary>
        public static void ExtractArchive(string[] args)
        {
            if (LaunchInfo.GetArchiveInCache())
            {
                Logger.Log("Archive found in cache, bypassing extraction.");
                LaunchInfo.SaveToCache();
                GenerateM3u();
                UpdateArchiveCachePlaytime();
                LinkCacheToLaunchFolder();
            }
            else
            {
                // Only now do we calculate the decompressed size, as can be very time consuming when large number of files in archive.
                // MinArchiveSize is megabytes, multiply to convert to bytes.
                // Always store multi-disc games in the cache, regardless of minimum size.
                // Always store games if LaunchPath is non-default, regardless of minimum size.
                if ((LaunchInfo.GetSize() > Config.MinArchiveSize * 1048576)
                    || (LaunchInfo.Game.MultiDisc && LaunchInfo.MultiDiscSupport)
                    || (LaunchInfo.LaunchPathConfig != Config.LaunchPath.Default))
                {
                    AddArchiveToCache();
                    GenerateM3u();
                    UpdateArchiveCachePlaytime();
                    LinkCacheToLaunchFolder();
                }
                else
                {
                    Logger.Log("Archive smaller than MinArchiveSize, bypassing extraction to cache.");
                    LaunchInfo.Extractor.Extract(LaunchInfo.GetArchivePath(), PathUtils.GetLaunchBox7zTempPath(), LaunchInfo.GetExtractSingleFile().ToSingleArray());
                }
            }
        }
    }
}
