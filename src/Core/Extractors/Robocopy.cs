using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

namespace ArchiveCacheManager
{
    public class Robocopy : Extractor
    {
        private long? archiveSize;
        private string archiveSizePath;

        public Robocopy()
        {
            archiveSize = null;
            archiveSizePath = string.Empty;
        }

        public override bool Extract(string archivePath, string cachePath, string[] includeList = null, string[] excludeList = null)
        {
            // If the file is less than 50MB, the overhead of calling Robocopy isn't worth it. Instead just use File.Copy().
            if (GetSize(archivePath) > 52_428_800)
            {
                string args = string.Format("/c robocopy \"{0}\" \"{1}\" \"{2}\"", Path.GetDirectoryName(archivePath), cachePath, Path.GetFileName(archivePath));

                (string stdout, string stderr, int exitCode) = ProcessUtils.RunProcess("cmd.exe", args, true, ExtractionProgress, true);

                if (exitCode >= 8)
                {
                    Logger.Log(string.Format("Robocopy returned exit code {0} with error output:\r\n{1}", exitCode, stdout));
                    Environment.ExitCode = exitCode;
                }

                return exitCode < 8;
            }
            else
            {
                try
                {
                    File.Copy(archivePath, Path.Combine(cachePath, Path.GetFileName(archivePath)), true);
                    return true;
                }
                catch (Exception e)
                {
                    Logger.Log($"File copy error: {e.ToString()}");
                    Console.Out.WriteLine(e.Message);
                    Environment.ExitCode = 1;
                }
            }

            return false;
        }

        public override long GetSize(string archivePath, string fileInArchive = null)
        {
            if (!Equals(archivePath, archiveSizePath) || archiveSize == null)
            {
                archiveSizePath = archivePath;
                archiveSize = DiskUtils.GetFileSize(archivePath);
            }

            return (long)archiveSize;
        }

        public override string[] List(string archivePath)
        {
            return Path.GetFileName(archivePath).ToSingleArray();
        }

        public override string Name()
        {
            return "File Copy";
        }

        public override string GetExtractorPath()
        {
            return null;
        }
    }
}
