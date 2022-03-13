using System;
using System.Linq;
using System.IO;

namespace ArchiveCacheManager
{
    public class DiskUtils
    {
        /// <summary>
        /// Deletes the entire content of a directory, and optionally the path itself.
        /// All files will have the FileAttributes.Normal attribute applied, so read-only files will be deleted.
        /// </summary>
        /// <param name="path">The path to delete.</param>
        /// <param name="contentsOnly">Whether to delete the path itself, or only the contents.</param>
        public static void DeleteDirectory(string path, bool contentsOnly = false)
        {
            try
            {
                if (Directory.Exists(path))
                {
                    // Enumerate and delete all files in all subdirectories
                    foreach (string filePath in Directory.EnumerateFiles(path, "*", SearchOption.AllDirectories))
                    {
                        // Clear any read-only or other special file attributes.
                        File.SetAttributes(filePath, FileAttributes.Normal);
                        File.Delete(filePath);
                    }
                    // Enumerate and delete all subdirectories
                    foreach (string dirPath in Directory.EnumerateDirectories(path))
                    {
                        Directory.Delete(dirPath, true);
                    }

                    if (!contentsOnly)
                    {
                        Directory.Delete(path, true);
                    }
                }
            }
            catch (Exception e)
            {
                Logger.Log(e.ToString(), Logger.LogLevel.Exception);
            }
        }

        /// <summary>
        /// Deletes a file from disk. Will set the FileAttributes.Normal attribute, so read-only files will be deleted.
        /// </summary>
        /// <param name="filePath">The file to delete.</param>
        public static void DeleteFile(string filePath)
        {
            try
            {
                if (File.Exists(filePath))
                {
                    // Clear any read-only or other special file attributes.
                    File.SetAttributes(filePath, FileAttributes.Normal);
                    File.Delete(filePath);
                }
            }
            catch (Exception e)
            {
                Logger.Log(e.ToString(), Logger.LogLevel.Exception);
            }
        }

        /// <summary>
        /// Get the size on disk of the given directory. Recursively called to get the size of all subdirectories.
        /// </summary>
        /// <param name="directoryInfo">The directory to get the size of.</param>
        /// <returns>The size of the directory and all sub directories in bytes, 0 on error.</returns>
        public static long DirectorySize(DirectoryInfo directoryInfo)
        {
            long size = 0;

            try
            {
                if (directoryInfo.Exists)
                {
                    foreach (FileInfo fileInfo in directoryInfo.GetFiles())
                    {
                        size += fileInfo.Length;
                    }
                    foreach (DirectoryInfo dirInfo in directoryInfo.GetDirectories())
                    {
                        size += DirectorySize(dirInfo);
                    }
                }
            }
            catch (Exception e)
            {
                Logger.Log(e.ToString(), Logger.LogLevel.Exception);
            }

            return size;
        }

        /// <summary>
        /// Sets the read-only attribute on all files in the path, including subdirectories. Will NOT set read-only
        /// on files used internally by Archive Cache Manager.
        /// </summary>
        /// <param name="path">The root path of the files to set read-only.</param>
        public static void SetDirectoryContentsReadOnly(string path)
        {
            // These files will not be set read-only, as they are written by Archive Cache Manager.
            string[] managerFiles = { PathUtils.GetArchiveCachePlaytimePath(path),
                                      PathUtils.GetArchiveCacheGameInfoPath(path),
                                      PathUtils.GetArchiveCacheExtractingFlagPath(path) };

            try
            {
                // Set archive files as read-only, so LB doesn't delete them.
                foreach (string filePath in Directory.GetFiles(path, "*", SearchOption.AllDirectories))
                {
                    if (!managerFiles.Contains(filePath))
                    {
                        File.SetAttributes(filePath, File.GetAttributes(filePath) | FileAttributes.ReadOnly);
                    }
                }
            }
            catch (Exception e)
            {
                Logger.Log(e.ToString(), Logger.LogLevel.Exception);
            }
        }

        /// <summary>
        /// Sets the read-only attribute on the specified file.
        /// </summary>
        /// <param name="filePath">The path of the file to set read-only.</param>
        public static void SetFileReadOnly(string filePath)
        {
            try
            {
                File.SetAttributes(filePath, File.GetAttributes(filePath) | FileAttributes.ReadOnly);
            }
            catch (Exception e)
            {
                Logger.Log(e.ToString(), Logger.LogLevel.Exception);
            }
        }

        /// <summary>
        /// Create an empty file. If the path does not exist, it will be created.
        /// </summary>
        /// <param name="path"></param>
        public static bool CreateFile(string path)
        {
            StreamWriter writer = null;
            bool success = false;

            try
            {
                Directory.CreateDirectory(Path.GetDirectoryName(path));
                writer = new StreamWriter(path, true);
                success = true;
            }
            catch (Exception e)
            {
                Logger.Log(e.ToString(), Logger.LogLevel.Exception);
            }
            finally
            {
                writer.Close();
            }

            return success;
        }
    }

}
