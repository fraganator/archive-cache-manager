using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

namespace ArchiveCacheManager
{
    public class ExtractXiso : Extractor
    {
        string executablePath = Path.Combine(PathUtils.GetExtractorRootPath(), "extract-xiso.exe");

        public ExtractXiso()
        {

        }

        public static bool SupportedType(string archivePath)
        {
            return PathUtils.HasExtension(archivePath, new string[] { ".zip", ".iso" });
        }

        public override bool Extract(string archivePath, string cachePath, string[] includeList = null, string[] excludeList = null)
        {
            string isoPath = string.Empty;
            string extension = Path.GetExtension(archivePath).ToLower();

            if (extension.Equals(".zip"))
            {
                Extractor zip = new Zip();
                if (zip.Extract(archivePath, cachePath, "*.iso".ToSingleArray(), excludeList))
                {
                    isoPath = Directory.GetFiles(cachePath, "*.iso")[0];
                }
            }
            else if (extension.Equals(".iso"))
            {
                Extractor robocopy = new Robocopy();
                if (robocopy.Extract(archivePath, cachePath, includeList, excludeList))
                {
                    isoPath = Directory.GetFiles(cachePath, "*.iso")[0];
                }
            }

            // Double-check isoPath is set, and not the original archive path, as it will be deleted.
            if (string.IsNullOrEmpty(isoPath) || string.Equals(isoPath, archivePath, StringComparison.InvariantCultureIgnoreCase))
            {
                Logger.Log($"extract-xiso path check failed: {isoPath}");
                Environment.ExitCode = 1;
                return false;
            }

            // -d    Output directory
            // -D    Delete old iso after conversion
            // -r    Rewrite mode
            // Is safe to use -D option here to delete old iso, as we're working on copy in the cache
            string args = string.Format("-d \"{0}\" -D -r \"{1}\"", cachePath, isoPath);
            
            (string stdout, string stderr, int exitCode) = ProcessUtils.RunProcess(executablePath, args, false, null, false, ExtractionProgress);

            if (exitCode != 0)
            {
                Logger.Log($"extract-xiso returned exit code {exitCode} with error output:\r\n{stderr}");
                Environment.ExitCode = exitCode;
            }

            return exitCode == 0;
        }

        public override long GetSize(string archivePath, string fileInArchive = null)
        {
            string extension = Path.GetExtension(archivePath).ToLower();

            // If iso is still zipped, get its decompressed size. This will be (much) larger than the final xiso size.
            // This size will be used when calculating how much room to make in the cache, so no harm in over-estimating.
            // Actual converted xiso size will be checked and set in LaunchInfo.UpdateSizeFromCache() once extraction is complete.
            if (extension.Equals(".zip"))
            {
                Extractor zip = new Zip();
                return zip.GetSize(archivePath, fileInArchive);
            }
            
            string args = string.Format("-l \"{0}\"", archivePath);

            (string stdout, string stderr, int exitCode) = ProcessUtils.RunProcess(executablePath, args);

            /*
            stdout will be in the format below:
            ------
            c:\LaunchBox\ThirdParty\extract-xiso>extract-xiso -l "c:\Emulation\ROMs\JSRF - Jet Set Radio Future (USA).iso"
            extract-xiso v2.7.1 (01.11.14) for win32 - written by in <in@fishtank.com>

            listing JSRF - Jet Set Radio Future (USA).iso:

            \default.xbe (2281472 bytes)
            \Media\ (0 bytes)
            \Media\Cache\ (0 bytes)
            \Media\Cache\Cache00.tbl (3786 bytes)
            \Media\Cache\Cache01.tbl (5869 bytes)
            \Media\Cache\Cache02.tbl (72 bytes)
            \Media\Cache\Cache03.tbl (72 bytes)
            \Media\Cache\Cache04.tbl (72 bytes)
            \Media\Cache\Cache05.tbl (72 bytes)
            \Media\Cache\Cache06.tbl (72 bytes)
            \Media\Cache\Cache07.tbl (72 bytes)
            \Media\Cache\Cache08.tbl (72 bytes)
            [...]
            \Media\Z_ADX\MUSENJ\gjv009_01.adx (124092 bytes)
            \Media\Z_ADX\MUSENJ\gjv010_01.adx (117900 bytes)
            \Media\Z_ADX\MUSENJ\gjv011_01.adx (93078 bytes)

            3221 files in c:\Emulation\ROMs\JSRF - Jet Set Radio Future (USA).iso total 2498145117 bytes
            */

            long size = 0;

            if (exitCode == 0)
            {
                // Split on the archive path, which appears just before the byte size. Result should be two elements.
                string[] stdoutArray = stdout.Split(new string[] { archivePath }, StringSplitOptions.RemoveEmptyEntries);
                // Second element should be in form " total xxxxx bytes"
                string sizeString = stdoutArray[1];
                // Convert to array of 3 elements, where second element is the size.
                sizeString = sizeString.Split(" ".ToSingleArray(), StringSplitOptions.RemoveEmptyEntries)[1];
                size = Convert.ToInt64(sizeString);
            }
            else
            {
                Logger.Log($"extract-xiso returned exit code {exitCode} with error output:\r\n{stderr}");
                Environment.ExitCode = exitCode;
            }

            return size;
        }

        public override string[] List(string archivePath)
        {
            return string.Format("{0}.iso", Path.GetFileNameWithoutExtension(archivePath)).ToSingleArray();
        }

        public override string Name()
        {
            return "extract-xiso";
        }

        public override string GetExtractorPath()
        {
            return executablePath;
        }
    }
}
