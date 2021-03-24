using System;
using System.IO;

namespace ArchiveCacheManager
{
    /// <summary>
    /// Helper class to log plugin status and errors.
    /// </summary>
    public class Logger
    {
        public enum LogLevel
        {
            None = 0,
            Exception,
            Debug,
            Info
        };

        /// <summary>
        /// Initialise the log file. Will delete the previous log file if it exists.
        /// </summary>
        public static void Init()
        {
            try
            {
                string logPath = PathUtils.GetLogPath();

                if (!Directory.Exists(logPath))
                {
                    Directory.CreateDirectory(logPath);
                }
                else
                {
                    string[] logs = Directory.GetFiles(PathUtils.GetLogPath(), "*.log");

                    Array.Sort(logs);

                    for (int i = 0; i < logs.Length - 10; i++)
                    {
                        File.Delete(logs[i]);
                    }
                }

                string oldLogFilePath = Path.Combine(PathUtils.GetPluginRootPath(), "events.log");

                if (File.Exists(oldLogFilePath))
                {
                    File.Delete(oldLogFilePath);
                }
            }
            catch (IOException)
            {
            }
        }

        /// <summary>
        /// Write a message to the log file. All entries will be timestamped.
        /// </summary>
        /// <param name="message">The message to log.</param>
        /// <param name="logLevel">The severity of the log entry. Default is LogLevel.Info.</param>
        public static void Log(string message, LogLevel logLevel = LogLevel.Info)
        {
            StreamWriter writer = null;

            try
            {
                writer = new StreamWriter(PathUtils.GetLogFilePath(), true);
                writer.Write(string.Format("{0} - {1}\r\n", GetDateTime(), message));
            }
            catch (IOException)
            {

            }
            finally
            {
                if (writer != null)
                {
                    writer.Close();
                }
            }
        }

        /// <summary>
        /// Returns the current date and time in YYYY-MM-DD HH:MM:SS format.
        /// </summary>
        /// <returns>The current date and time in YYYY-MM-DD HH:MM:SS format.</returns>
        private static string GetDateTime()
        {
            DateTime dt = DateTime.Now;
            return string.Format("{0:0000}-{1:00}-{2:00} {3:00}:{4:00}:{5:00}", dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute, dt.Second);
        }
    }
}
