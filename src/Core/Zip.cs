using System;
using System.Diagnostics;
using System.Linq;

namespace ArchiveCacheManager
{
    /// <summary>
    /// Handles all calls to 7-Zip.
    /// </summary>
    public class Zip
    {
        public static string Get7zVersion()
        {
            string stdout = null;
            string stderr = null;
            int exitCode = 0;
            string versionInfo = null;

            if (run7z("", ref stdout, ref stderr, ref exitCode))
            {
                versionInfo = stdout.Split("\r\n".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)[0];
            }

            return versionInfo;
        }

        /// <summary>
        /// Run the 7z extract command on the specified archive.
        /// </summary>
        /// <param name="archivePath"></param>
        /// <param name="stdout"></param>
        /// <param name="stderr"></param>
        /// <param name="exitCode"></param>
        public static void Extract(string archivePath, string cachePath, ref string stdout, ref string stderr, ref int exitCode)
        {
            // This command is a duplicate of that used by LaunchBox.
            // x = extract
            // {0} = archive path
            // -o{1} = output path
            // -y = answer yes to any queries
            // -aoa = 
            // -bsp1 = show progress
            string args = string.Format("x \"{0}\" \"-o{1}\" -y -aoa", archivePath, cachePath);

            run7z(args, ref stdout, ref stderr, ref exitCode);
        }

        /// <summary>
        /// Run the 7z list command on the specified archive.
        /// </summary>
        /// <param name="archivePath"></param>
        /// <param name="stdout"></param>
        /// <param name="stderr"></param>
        /// <param name="exitCode"></param>
        public static void List(string archivePath, ref string stdout, ref string stderr, ref int exitCode)
        {
            // This command is a duplicate of that used by LaunchBox.
            // l = list
            // {0} = archive path
            // -stl = technical listing
            string args = string.Format("l \"{0}\" -slt", archivePath);

            run7z(args, ref stdout, ref stderr, ref exitCode);
        }

        /// <summary>
        /// Get the decompressed size of the specified archive.
        /// </summary>
        /// <param name="archivePath"></param>
        /// <returns>The decompressed size of the archive in bytes.</returns>
        public static long GetDecompressedSize(string archivePath)
        {
            // Run List command
            // Parse stdout for all "Size = " entries
            // Extract sizes and sum
            // Return -1 on error
            string stdout = null;
            string stderr = null;
            int exitCode = 0;
            long size = 0;

            List(archivePath, ref stdout, ref stderr, ref exitCode);

            if (exitCode == 0)
            {
                // TODO: Replace logic with regex?
                string[] stdoutArray = stdout.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);

                foreach (string line in stdoutArray)
                {
                    if (line.StartsWith("Size ="))
                    {
                        size += Convert.ToInt64(line.Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)[2]);
                    }
                }
            }

            return size;
        }

        /// <summary>
        /// Run the desired 7z command.
        /// </summary>
        /// <param name="args"></param>
        public static void Call7z(string[] args)
        {
            string stdout = null;
            string stderr = null;
            int exitCode = 0;
            string[] quotedArgs = args;
            string argString;

            // Wrap any args containing spaces with double-quotes.
            for (int i = 0; i < quotedArgs.Count(); i++)
            {
                if (quotedArgs[i].Contains(" "))
                {
                    quotedArgs[i] = string.Format("\"{0}\"", quotedArgs[i]);
                }
            }

            argString = String.Join(" ", quotedArgs);

            run7z(argString, ref stdout, ref stderr, ref exitCode);

            // Print the results to console and set the error code for LaunchBox to deal with
            Console.Write(stdout);
            Console.Write(stderr);
            Environment.ExitCode = exitCode;
        }

        /// <summary>
        /// Run 7z with the specified arguments. Results are returned by ref.
        /// </summary>
        /// <param name="args"></param>
        /// <param name="stdout"></param>
        /// <param name="stderr"></param>
        /// <param name="exitCode"></param>
        static bool run7z(string args, ref string stdout, ref string stderr, ref int exitCode)
        {
            Process process = new Process();
            process.StartInfo.FileName = PathUtils.GetLaunchBox7zPath();
            process.StartInfo.Arguments = args;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardError = true;

            process.Start();

            // TODO
            // Mimic extraction progress. Capture asynchronous stdout and write to own stdout.
            
            stdout = process.StandardOutput.ReadToEnd();
            stderr = process.StandardError.ReadToEnd();
            process.WaitForExit();
            exitCode = process.ExitCode;

            return true;
        }
    }
}
