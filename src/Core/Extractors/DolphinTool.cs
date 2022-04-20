using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

namespace ArchiveCacheManager
{
    public class DolphinTool : Extractor
    {
        string executablePath = Path.Combine(PathUtils.GetExtractorRootPath(), "DolphinTool.exe");

        public DolphinTool()
        {

        }

        public static bool SupportedType(string archivePath)
        {
            return PathUtils.HasExtension(archivePath, new string[] { ".rvz", ".wia", ".gcz" });
        }

        public override bool Extract(string archivePath, string cachePath, string[] includeList = null, string[] excludeList = null)
        {
            string args = string.Format("convert -i \"{0}\" -o \"{1}\" -f iso", archivePath, Path.Combine(cachePath, Path.GetFileNameWithoutExtension(archivePath) + ".iso"));
            
            // chdman reports the progress status on stderr, not stdout
            (string stdout, string stderr, int exitCode) = ProcessUtils.RunProcess(executablePath, args);

            if (exitCode != 0)
            {
                Logger.Log(string.Format("DolphinTool returned exit code {0} with error output:\r\n{1}", exitCode, stderr));
                Environment.ExitCode = exitCode;
            }

            return exitCode == 0;
        }

        public override long GetSize(string archivePath, string fileInArchive = null)
        {
            return new FileInfo(archivePath).Length;
        }

        public override string[] List(string archivePath, string[] includeList = null, string[] excludeList = null, bool prefixWildcard = false)
        {
            return string.Format("{0}.iso", Path.GetFileNameWithoutExtension(archivePath)).ToSingleArray();
        }

        public override string Name()
        {
            return "DolphinTool";
        }

        public override string GetExtractorPath()
        {
            return executablePath;
        }
    }
}
