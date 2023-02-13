using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

namespace ArchiveCacheManager
{
    // this extractor is specificilly for nNASOS1.8 available at https://archive.org/details/nNASOS1.8
    public class NNASOS : Extractor
    {
        // Needed to place in LanchBox root ThirdParty/ folder as nNASOS uses a .dll itself that LaunchBox
        // mistakenly tries to register as a plugin, crashing LaunchBox on startup.
        string executablePath = Path.Combine(PathUtils.GetLaunchBoxNNASOSRootPath(), "nNASOS.exe");

        public NNASOS()
        {

        }

        public static bool SupportedType(string archivePath)
        {
            return PathUtils.HasExtension(archivePath, new string[] { ".7z", ".zip", ".iso.dec" });
        }

        public override bool Extract(string archivePath, string cachePath, string[] includeList = null, string[] excludeList = null)
        {
            string isoDecPath = string.Empty;
            string extension = Path.GetExtension(archivePath).ToLower();
            bool skipArchiveCheck = false;

            if (Directory.Exists(cachePath))
            {
                string[] files = Directory.GetFiles(cachePath, "*.iso.dec");
                if (files.Length > 0)
                {
                    // .iso.dec is already in the cache path, so no need to extract or copy anything
                    // from archive path
                    Logger.Log(string.Format("Skipping extract/copy from archive path as .iso.dec is already in cache path..."));
                    isoDecPath = files[0];
                    skipArchiveCheck = true;
                }
            }

            if (!skipArchiveCheck)
            {
                if (extension.Equals(".7z") || extension.Equals(".zip"))
                {
                    Extractor zip = new Zip();
                    if (zip.Extract(archivePath, cachePath, new string[] { "*.iso", "*.iso.dec" }, excludeList))
                    {
                        string[] files = Directory.GetFiles(cachePath, "*.iso");
                        if (files.Length > 0)
                        {
                            // no need to further process with nNASOS as already in .iso format
                            return true;
                        }
                        else
                        {
                            isoDecPath = Directory.GetFiles(cachePath, "*.iso.dec")[0];
                        }
                    }
                }
                else if (extension.Equals(".iso.dec"))
                {
                    Extractor robocopy = new Robocopy();
                    if (robocopy.Extract(archivePath, cachePath, includeList, excludeList))
                    {
                        isoDecPath = Directory.GetFiles(cachePath, "*.iso.dec")[0];
                    }
                }
                else if (extension.Equals(".iso"))
                {
                    Extractor robocopy = new Robocopy();
                    if (robocopy.Extract(archivePath, cachePath, includeList, excludeList))
                    {
                        // no need to extract as already in .iso format
                        return true;
                    }
                }
            }

            // Double-check isoDecPath is set, and not the original archive path, as it will be deleted.
            if (string.IsNullOrEmpty(isoDecPath) || string.Equals(isoDecPath, archivePath, StringComparison.InvariantCultureIgnoreCase))
            {
                Logger.Log($"nNASOS path check failed: {isoDecPath}");
                Environment.ExitCode = 1;
                return false;
            }

            /*
             * Usage: nNASOS <options> <file1> <file2> <fileN>
             * Possible options:
             * -v      Verify
             * -n      Do not pause at the end of processing
             * -d      Remove source file at successfull conversion
             * -p      Pause at error
             *
             * It is safe to delete the original .iso.dec since we're working with the
             * copied/extracted file in the cache.
             * Also, there is no option to specify an output path, but afaik, it simply extracts
             * to an .iso in the same folder as input file.
             */
            string args = string.Format("-d -n \"{0}\"", isoDecPath);
            
            (string stdout, string stderr, int exitCode) = ProcessUtils.RunProcess(executablePath, args);

            if (exitCode != 0)
            {
                // errors on stderr stream (at least according to the readme)
                Logger.Log(string.Format("nNASOS returned exit code {0} with error output:\r\n{1}", exitCode, stderr));
                Environment.ExitCode = exitCode;
            }

            return exitCode == 0;
        }

        public override long GetSize(string archivePath, string fileInArchive = null)
        {
            return DiskUtils.GetFileSize(archivePath);
        }

        public override string[] List(string archivePath)
        {
            return string.Format("{0}.iso.dec", Path.GetFileNameWithoutExtension(archivePath)).ToSingleArray();
        }

        public override string Name()
        {
            return "nNASOS";
        }

        public override string GetExtractorPath()
        {
            return executablePath;
        }
    }
}
