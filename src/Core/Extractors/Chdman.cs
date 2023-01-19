using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

namespace ArchiveCacheManager
{
    public class Chdman : Extractor
    {
        string executablePath = Path.Combine(PathUtils.GetExtractorRootPath(), "chdman.exe");

        public Chdman()
        {

        }

        public static bool SupportedType(string archivePath)
        {
            return PathUtils.HasExtension(archivePath, new string[] { ".chd" });
        }

        public override bool Extract(string archivePath, string cachePath, string[] includeList = null, string[] excludeList = null)
        {
            string args = string.Format("extractcd -i \"{0}\" -o \"{1}\" -f", archivePath, Path.Combine(cachePath, Path.GetFileNameWithoutExtension(archivePath) + ".cue"));
            
            // chdman reports the progress status on stderr, not stdout
            (string stdout, string stderr, int exitCode) = ProcessUtils.RunProcess(executablePath, args, false, null, true, ExtractionProgress);

            if (exitCode != 0)
            {
                Logger.Log(string.Format("chdman returned exit code {0} with error output:\r\n{1}", exitCode, stderr));
                Environment.ExitCode = exitCode;
            }

            return exitCode == 0;
        }

        public override long GetSize(string archivePath, string fileInArchive = null)
        {
            string args = string.Format("info -i \"{0}\"", archivePath);

            (string stdout, string stderr, int exitCode) = ProcessUtils.RunProcess(executablePath, args);

            if (exitCode != 0)
            {
                Logger.Log(string.Format("chdman returned exit code {0} with error output:\r\n{1}", exitCode, stderr));
                Environment.ExitCode = exitCode;
            }

            /*
            stdout will be in the format below:
            ------
            c:\LaunchBox\ThirdParty\chdman>chdman info -i "c:\Emulation\ROMs\Doom (USA).zip"
            chdman - MAME Compressed Hunks of Data (CHD) manager 0.242 (mame0242)
            Input file:   c:\Emulation\ROMs\Sony - Playstation\Doom (USA).chd
            File Version: 5
            Logical size: 302,660,928 bytes
            Hunk Size:    19,584 bytes
            Total Hunks:  15,455
            Unit Size:    2,448 bytes
            Total Units:  123,636
            Compression:  cdlz (CD LZMA), cdzl (CD Deflate), cdfl (CD FLAC)
            CHD size:     182,307,697 bytes
            Ratio:        60.2%
            SHA1:         1fe56d1e220712bdc2681bb55f2e90b457cc593b
            Data SHA1:    cdcd62e6d75a6e22a9f5172c5c9175def92dce84
            Metadata:     Tag='CHT2'  Index=0  Length=92 bytes
                          TRACK:1 TYPE:MODE2_RAW SUBTYPE:NONE FRAMES:35789 PREGAP:0 PG
            Metadata:     Tag='CHT2'  Index=1  Length=91 bytes
                          TRACK:2 TYPE:AUDIO SUBTYPE:NONE FRAMES:14344 PREGAP:150 PGTY
            Metadata:     Tag='CHT2'  Index=2  Length=90 bytes
                          TRACK:3 TYPE:AUDIO SUBTYPE:NONE FRAMES:8844 PREGAP:150 PGTYP
            Metadata:     Tag='CHT2'  Index=3  Length=91 bytes
                          TRACK:4 TYPE:AUDIO SUBTYPE:NONE FRAMES:17854 PREGAP:150 PGTY
            Metadata:     Tag='CHT2'  Index=4  Length=91 bytes
                          TRACK:5 TYPE:AUDIO SUBTYPE:NONE FRAMES:15611 PREGAP:150 PGTY
            Metadata:     Tag='CHT2'  Index=5  Length=90 bytes
                          TRACK:6 TYPE:AUDIO SUBTYPE:NONE FRAMES:9752 PREGAP:150 PGTYP
            Metadata:     Tag='CHT2'  Index=6  Length=90 bytes
                          TRACK:7 TYPE:AUDIO SUBTYPE:NONE FRAMES:4187 PREGAP:150 PGTYP
            Metadata:     Tag='CHT2'  Index=7  Length=91 bytes
                          TRACK:8 TYPE:AUDIO SUBTYPE:NONE FRAMES:17245 PREGAP:150 PGTY
            */

            long size = 0;

            if (exitCode == 0)
            {
                string[] stdoutArray = stdout.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                // We use the "logical size" as the uncompressed size, and while this isn't accurate, it's good enough.
                // Find the string beginning with "Logical size:", then take the substring after it which will be "xxx,xxx,xxx bytes"
                string sizeString = Array.Find(stdoutArray, a => a.StartsWith("Logical size:")).Substring(14);
                // Remove the trailing " bytes" from the string, leaving "xxx,xxx,xxx"
                sizeString = sizeString.Split(" ".ToSingleArray(), StringSplitOptions.RemoveEmptyEntries)[0];
                sizeString = sizeString.Replace(",", string.Empty);
                size = Convert.ToInt64(sizeString);
            }

            return size;
        }

        public override string[] List(string archivePath)
        {
            string[] fileList = new string[2];
            fileList[0] = Path.GetFileNameWithoutExtension(archivePath) + ".cue";
            fileList[1] = Path.GetFileNameWithoutExtension(archivePath) + ".bin";
            return fileList;
        }

        public override string Name()
        {
            return "chdman";
        }

        public override string GetExtractorPath()
        {
            return executablePath;
        }
    }
}
