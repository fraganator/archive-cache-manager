using System;
using System.IO;
using IniParser;
using IniParser.Model;

namespace ArchiveCacheManager
{
    public class GameInfo
    {
        public static string mArchivePath = "none";
        public static string mEmulator = "none";
        public static string mPlatform = "none";
        public static string mTitle = "none";

        private static readonly string section = "Game";

        static GameInfo()
        {
            LoadInfo();
        }

        public static string ArchivePath => mArchivePath;
        public static string Emulator => mEmulator;
        public static string Platform => mPlatform;
        public static string Title => mTitle;

        /// <summary>
        /// Loads some basic information about the game being launched.
        /// </summary>
        public static void LoadInfo()
        {
            if (File.Exists(PathUtils.GetGameInfoPath()))
            {
                try
                {
                    var parser = new FileIniDataParser();
                    IniData iniData = parser.ReadFile(PathUtils.GetGameInfoPath());

                    mArchivePath = iniData[section][nameof(ArchivePath)];
                    mEmulator = iniData[section][nameof(Emulator)];
                    mPlatform = iniData[section][nameof(Platform)];
                    mTitle = iniData[section][nameof(Title)];
                }
                catch (Exception e)
                {
                    Logger.Log(e.ToString(), Logger.LogLevel.Exception);
                }
            }
        }

        /// <summary>
        /// Saves basic information about the game being launched by LaunchBox.
        /// </summary>
        /// <param name="path">The original path to the rom.</param>
        /// <param name="emulator">The emulator used to play the rom.</param>
        /// <param name="platform">The platform of the game.</param>
        /// <param name="title">The game title.</param>
        public static void SaveInfo(string path, string emulator, string platform, string title)
        {
            var parser = new FileIniDataParser();
            IniData iniData = new IniData();

            mArchivePath = path;
            mEmulator = emulator;
            mPlatform = platform;
            mTitle = title;

            try
            {
                iniData[section][nameof(ArchivePath)] = mArchivePath;
                iniData[section][nameof(Emulator)] = mEmulator;
                iniData[section][nameof(Platform)] = mPlatform;
                iniData[section][nameof(Title)] = mTitle;

                parser.WriteFile(PathUtils.GetGameInfoPath(), iniData);
            }
            catch (Exception e)
            {
                Logger.Log(e.ToString(), Logger.LogLevel.Exception);
            }
        }
    }
}
