using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ArchiveCacheManager
{
    public static class ProcessUtils
    {
        static Process mProcess;

        public static void KillProcess()
        {
            try
            {
                mProcess.Kill();
            }
            catch (Exception)
            {
            }
        }

        public static (string, string, int) RunProcess(string executable, string args, bool redirectOutput = false, Func<string, string> processOutput = null, bool redirectError = false, Func<string, string> processError = null)
        {
            string stdout;
            string stderr;
            int exitCode;
            mProcess = new Process();
            mProcess.StartInfo.FileName = executable;
            mProcess.StartInfo.Arguments = args;
            mProcess.StartInfo.UseShellExecute = false;
            mProcess.StartInfo.CreateNoWindow = true;
            mProcess.StartInfo.RedirectStandardOutput = true;
            mProcess.StartInfo.RedirectStandardError = true;
            StringBuilder asyncError = new StringBuilder();
            StringBuilder asyncOutput = new StringBuilder();
            string processedStdout = string.Empty;
            string processedStderr = string.Empty;
            mProcess.OutputDataReceived += new DataReceivedEventHandler((sender, e) =>
            {
                if (redirectOutput)
                {
                    processedStdout = (processOutput != null) ? processOutput(e.Data) : e.Data;
                    Console.Out.WriteLine(processedStdout);
                }
                asyncOutput.Append("\r\n" + e.Data);
            });
            mProcess.ErrorDataReceived += new DataReceivedEventHandler((sender, e) =>
            {
                if (redirectError)
                {
                    if (processError != null)
                    {
                        processedStderr = processError(e.Data);
                        Console.Out.WriteLine(processedStderr);
                    }
                    else
                    {
                        Console.Error.WriteLine(e.Data);
                    }
                }
                asyncError.Append("\r\n" + e.Data);
            });

            try
            {
                mProcess.Start();
                mProcess.BeginErrorReadLine();
                mProcess.BeginOutputReadLine();

                // LB allows terminating the extraction process by pressing Esc on the loading screen. If this process is killed,
                // the child process (the real 7z in this case) will NOT be terminated. Add 7z as a tracked child process, which
                // will be automatically killed if this process is also killed.
                ChildProcessTracker.AddProcess(mProcess);

                mProcess.WaitForExit();
                exitCode = mProcess.ExitCode;
            }
            catch (Exception e)
            {
                exitCode = -1;
                Logger.Log(e.ToString(), Logger.LogLevel.Exception);
            }

            stdout = asyncOutput.ToString();
            stderr = asyncError.ToString();

            return (stdout, stderr, exitCode);
        }
    }

    /// <summary>
    /// Allows processes to be automatically killed if this parent process unexpectedly quits.
    /// This feature requires Windows 8 or greater. On Windows 7, nothing is done.</summary>
    /// <remarks>StackOverflow post:
    /// https://stackoverflow.com/a/37034966
    /// References:
    /// https://stackoverflow.com/a/4657392/386091
    /// https://stackoverflow.com/a/9164742/386091 </remarks>
    public static class ChildProcessTracker
    {
        /// <summary>
        /// Add the process to be tracked. If our current process is killed, the child processes
        /// that we are tracking will be automatically killed, too. If the child process terminates
        /// first, that's fine, too.</summary>
        /// <param name="process"></param>
        public static void AddProcess(Process process)
        {
            if (s_jobHandle != IntPtr.Zero)
            {
                bool success = AssignProcessToJobObject(s_jobHandle, process.Handle);
                if (!success && !process.HasExited)
                    throw new Win32Exception();
            }
        }

        static ChildProcessTracker()
        {
            // This feature requires Windows 8 or later. To support Windows 7 requires
            //  registry settings to be added if you are using Visual Studio plus an
            //  app.manifest change.
            //  https://stackoverflow.com/a/4232259/386091
            //  https://stackoverflow.com/a/9507862/386091
            if (Environment.OSVersion.Version < new Version(6, 2))
                return;

            // The job name is optional (and can be null) but it helps with diagnostics.
            //  If it's not null, it has to be unique. Use SysInternals' Handle command-line
            //  utility: handle -a ChildProcessTracker
            string jobName = "ChildProcessTracker" + Process.GetCurrentProcess().Id;
            s_jobHandle = CreateJobObject(IntPtr.Zero, jobName);

            var info = new JOBOBJECT_BASIC_LIMIT_INFORMATION();

            // This is the key flag. When our process is killed, Windows will automatically
            //  close the job handle, and when that happens, we want the child processes to
            //  be killed, too.
            info.LimitFlags = JOBOBJECTLIMIT.JOB_OBJECT_LIMIT_KILL_ON_JOB_CLOSE;

            var extendedInfo = new JOBOBJECT_EXTENDED_LIMIT_INFORMATION();
            extendedInfo.BasicLimitInformation = info;

            int length = Marshal.SizeOf(typeof(JOBOBJECT_EXTENDED_LIMIT_INFORMATION));
            IntPtr extendedInfoPtr = Marshal.AllocHGlobal(length);
            try
            {
                Marshal.StructureToPtr(extendedInfo, extendedInfoPtr, false);

                if (!SetInformationJobObject(s_jobHandle, JobObjectInfoType.ExtendedLimitInformation,
                    extendedInfoPtr, (uint)length))
                {
                    throw new Win32Exception();
                }
            }
            finally
            {
                Marshal.FreeHGlobal(extendedInfoPtr);
            }
        }

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
        static extern IntPtr CreateJobObject(IntPtr lpJobAttributes, string name);

        [DllImport("kernel32.dll")]
        static extern bool SetInformationJobObject(IntPtr job, JobObjectInfoType infoType,
            IntPtr lpJobObjectInfo, uint cbJobObjectInfoLength);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool AssignProcessToJobObject(IntPtr job, IntPtr process);

        // Windows will automatically close any open job handles when our process terminates.
        //  This can be verified by using SysInternals' Handle utility. When the job handle
        //  is closed, the child processes will be killed.
        private static readonly IntPtr s_jobHandle;
    }

    public enum JobObjectInfoType
    {
        AssociateCompletionPortInformation = 7,
        BasicLimitInformation = 2,
        BasicUIRestrictions = 4,
        EndOfJobTimeInformation = 6,
        ExtendedLimitInformation = 9,
        SecurityLimitInformation = 5,
        GroupInformation = 11
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct JOBOBJECT_BASIC_LIMIT_INFORMATION
    {
        public Int64 PerProcessUserTimeLimit;
        public Int64 PerJobUserTimeLimit;
        public JOBOBJECTLIMIT LimitFlags;
        public UIntPtr MinimumWorkingSetSize;
        public UIntPtr MaximumWorkingSetSize;
        public UInt32 ActiveProcessLimit;
        public Int64 Affinity;
        public UInt32 PriorityClass;
        public UInt32 SchedulingClass;
    }

    [Flags]
    public enum JOBOBJECTLIMIT : uint
    {
        JOB_OBJECT_LIMIT_KILL_ON_JOB_CLOSE = 0x2000
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct IO_COUNTERS
    {
        public UInt64 ReadOperationCount;
        public UInt64 WriteOperationCount;
        public UInt64 OtherOperationCount;
        public UInt64 ReadTransferCount;
        public UInt64 WriteTransferCount;
        public UInt64 OtherTransferCount;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct JOBOBJECT_EXTENDED_LIMIT_INFORMATION
    {
        public JOBOBJECT_BASIC_LIMIT_INFORMATION BasicLimitInformation;
        public IO_COUNTERS IoInfo;
        public UIntPtr ProcessMemoryLimit;
        public UIntPtr JobMemoryLimit;
        public UIntPtr PeakProcessMemoryUsed;
        public UIntPtr PeakJobMemoryUsed;
    }
}
