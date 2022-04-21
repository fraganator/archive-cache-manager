using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace ArchiveCacheManager
{
    public abstract class Extractor
    {
        private static int mProgressDivisor = 1;
        private static int mProgressOffset = 0;
        // This regex handles 7z, robocopy, and chdman.
        // Other extractors may be different, so set ProgressRegex accordingly.
        private static string mProgressRegex = "(\\d+\\.?\\d*)%(.*)";

        public int ProgressDivisor
        {
            get => mProgressDivisor;
            set => mProgressDivisor = Math.Max(value, 1);
        }

        public int ProgressOffset
        {
            get => mProgressOffset;
            set => mProgressOffset = Math.Max(value, 0);
        }

        protected string ProgressRegex
        {
            get => mProgressRegex;
            set => mProgressRegex = value;
        }

        public static string ExtractionProgress(string stdout)
        {
            string progress = string.Empty;

            if (stdout != null)
            {
                try
                {
                    Match match = Regex.Match(stdout, mProgressRegex);
                    if (match.Success)
                    {
                        progress = string.Format("{0,3}% - extracting", (int)(double.Parse(match.Groups[1].Value) / mProgressDivisor) + mProgressOffset);
                    }
                }
                catch (Exception)
                {
                    progress = string.Empty;
                }
            }

            return progress;
        }

        /// <summary>
        /// Name of the extractor, used for informational purposes.
        /// </summary>
        /// <returns>Name of the extractor.</returns>
        public abstract string Name();

        /// <summary>
        /// Get the size of specified archive after extraction.
        /// </summary>
        /// <param name="archivePath"></param>
        /// /// <param name="fileInArchive"></param>
        /// <returns>The extracted size of the archive in bytes.</returns>
        public abstract long GetSize(string archivePath, string fileInArchive = null);

        /// <summary>
        /// Run the extract command on the specified archive. Console output should be redirected to this app's console so
        /// LaunchBox has access to the extraction progress.
        /// </summary>
        /// <param name="archivePath"></param>
        /// <returns>True on successful extraction, False otherwise.</returns>
        public abstract bool Extract(string archivePath, string cachePath, string[] includeList = null, string[] excludeList = null);

        /// <summary>
        /// Get a file list for the specified archive.
        /// </summary>
        /// <param name="archivePath">Archive to list.</param>
        /// <param name="includeList">Optional list of files or file extensions to include. The listing will only return these values if they are found.</param>
        /// <param name="excludeList">Optional list of files or file extensions to exclude. The listing will exclude these values if they are found.</param>
        /// <param name="prefixWildcard">Option to prefix all include and exclude lists with a "*" wildcard.</param>
        /// <returns>The list of files in an archive, filtered using include and exclude lists when specified.</returns>
        public abstract string[] List(string archivePath, string[] includeList = null, string[] excludeList = null, bool prefixWildcard = false);

        public abstract string GetExtractorPath();
    }
}