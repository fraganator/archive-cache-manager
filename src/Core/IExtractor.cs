namespace ArchiveCacheManager
{
    public interface IExtractor
    {
        /// <summary>
        /// Name of the extractor, used for informational purposes.
        /// </summary>
        /// <returns>Name of the extractor.</returns>
        string Name();

        /// <summary>
        /// Get the divisor used to calculate the extraction progress printed to the console. Primarily for multi-disc extraction.
        /// </summary>
        /// <returns></returns>
        int GetProgressDivisor();
        /// <summary>
        /// Set the divisor used to calculate the extraction progress printed to the console. Primarily for multi-disc extraction.
        /// </summary>
        /// <param name="divisor"></param>
        void SetProgressDivisor(int divisor);

        /// <summary>
        /// Get the offset used to calculate the extraction progress printed to the console. Primarily for multi-disc extraction.
        /// </summary>
        /// <returns></returns>
        int GetProgressOffset();
        /// <summary>
        /// Set the offset used to calculate the extraction progress printed to the console. Primarily for multi-disc extraction.
        /// </summary>
        /// <param name="offset"></param>
        void SetProgressOffset(int offset);

        /// <summary>
        /// Get the size of specified archive after extraction.
        /// </summary>
        /// <param name="archivePath"></param>
        /// /// <param name="fileInArchive"></param>
        /// <returns>The extracted size of the archive in bytes.</returns>
        long GetSize(string archivePath, string fileInArchive = null);

        /// <summary>
        /// Run the extract command on the specified archive. Console output should be redirected to this app's console so
        /// LaunchBox has access to the extraction progress.
        /// </summary>
        /// <param name="archivePath"></param>
        /// <returns>True on successful extraction, False otherwise.</returns>
        bool Extract(string archivePath, string cachePath, string[] includeList = null, string[] excludeList = null);

        /// <summary>
        /// Get a file list for the specified archive.
        /// </summary>
        /// <param name="archivePath">Archive to list.</param>
        /// <param name="includeList">Optional list of files or file extensions to include. The listing will only return these values if they are found.</param>
        /// <param name="excludeList">Optional list of files or file extensions to exclude. The listing will exclude these values if they are found.</param>
        /// <param name="prefixWildcard">Option to prefix all include and exclude lists with a "*" wildcard.</param>
        /// <returns>The list of files in an archive, filtered using include and exclude lists when specified.</returns>
        string[] List(string archivePath, string[] includeList = null, string[] excludeList = null, bool prefixWildcard = false);
    }
}
