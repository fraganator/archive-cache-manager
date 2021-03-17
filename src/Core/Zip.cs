using System;
using System.Collections.Generic;
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
            string stdout = string.Empty;
            string stderr = string.Empty;
            int exitCode = 0;
            string versionInfo = string.Empty;

            if (run7z("", ref stdout, ref stderr, ref exitCode))
            {
                versionInfo = stdout.Split("\r\n".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)[0];
            }

            return versionInfo;
        }

        /// <summary>
        /// Run the 7z extract command on the specified archive. Console output from 7z will be redirected to this app's console so
        /// LaunchBox has access to the extraction progress.
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
            // -aoa = overwrite all existing files
            // -bsp1 = redirect progress to stdout
            string args = string.Format("x \"{0}\" \"-o{1}\" -y -aoa -bsp1", archivePath, cachePath);

            run7z(args, ref stdout, ref stderr, ref exitCode, true);
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
            // -slt = technical listing
            string args = string.Format("l \"{0}\" -slt", archivePath);

            run7z(args, ref stdout, ref stderr, ref exitCode);
        }

        /// <summary>
        /// Get an undecorated file list for the specified archive.
        /// </summary>
        /// <param name="archivePath"></param>
        /// <returns>A simple file list of archive contents.</returns>
        public static string[] GetFileList(string archivePath)
        {
            // Run List command
            // Parse stdout for all "Path = " entries
            // Return -1 on error
            string stdout = string.Empty;
            string stderr = string.Empty;
            int exitCode = 0;
            List<string> fileList = new List<string>();
            bool foundFirstPath = false;

            List(archivePath, ref stdout, ref stderr, ref exitCode);

            if (exitCode == 0)
            {
                string[] stdoutArray = stdout.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);

                foreach (string line in stdoutArray)
                {
                    if (line.StartsWith("Path ="))
                    {
                        if (foundFirstPath)
                        {
                            // Format of line is "Path = Filename.xyz"
                            // Split on '=' and get second element
                            fileList.Add(line.Split("=".ToCharArray())[1].Trim());
                        }
                        else
                        {
                            // The first "Path = " entry from 7z is the archive name itself, so skip adding it to the list
                            foundFirstPath = true;
                        }
                    }
                }
            }
            else
            {
                Logger.Log(string.Format("Error listing archive {0}. 7-Zip returned exit code {1} with error output:\r\n{2}", archivePath, exitCode, stderr));
            }

            return fileList.ToArray();
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
            string stdout = string.Empty;
            string stderr = string.Empty;
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
            string stdout = string.Empty;
            string stderr = string.Empty;
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
        static bool run7z(string args, ref string stdout, ref string stderr, ref int exitCode, bool redirectOutput = false, bool redirectError = false)
        {
            Process process = new Process();
            process.StartInfo.FileName = PathUtils.GetLaunchBox7zPath();
            process.StartInfo.Arguments = args;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardError = true;
            string asyncError = "";
            string asyncOutput = "";
            process.OutputDataReceived += new DataReceivedEventHandler((sender, e) =>
            {
                if (redirectOutput)
                {
                    Console.Out.WriteLine(e.Data);
                }
                asyncOutput = string.Format("{0}\r\n{1}", asyncOutput, e.Data);
            });
            process.ErrorDataReceived += new DataReceivedEventHandler((sender, e) =>
            {
                if (redirectError)
                {
                    Console.Error.WriteLine(e.Data);
                }
                asyncError = string.Format("{0}\r\n{1}", asyncError, e.Data);
            });

            process.Start();
            process.BeginErrorReadLine();
            process.BeginOutputReadLine();

            // LB allows terminating the extraction process by pressing Esc on the loading screen. If this process is killed,
            // the child process (the real 7z in this case) will NOT be terminated. Add 7z as a tracked child process, which
            // will be automatically killed if this process is also killed.
            ChildProcessTracker.AddProcess(process);

            process.WaitForExit();
            exitCode = process.ExitCode;
            stdout = asyncOutput;
            stderr = asyncError;

            return true;
        }
    }
}
