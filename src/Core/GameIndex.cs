using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using IniParser;
using IniParser.Model;

namespace ArchiveCacheManager
{
    public class GameIndex
    {
        private readonly string PlayRomInArchive = "PlayRomInArchive";

        private static IniData mGameIndex = null;

        static GameIndex()
        {
            Load();
        }

        public static void Load()
        {
            string gameIndexPath = PathUtils.GetPluginGameIndexPath();
            mGameIndex = null;

            if (File.Exists(gameIndexPath))
            {
                var parser = new FileIniDataParser();
                mGameIndex = new IniData();

                try
                {
                    mGameIndex = parser.ReadFile(gameIndexPath);
                }
                catch (Exception e)
                {
                    Logger.Log(string.Format("Error parsing game index file from {0}. Deleting invalid file.", gameIndexPath));
                    Logger.Log(e.ToString(), Logger.LogLevel.Exception);
                    File.Delete(gameIndexPath);
                    mGameIndex = null;
                }
            }
        }

        public static void Save()
        {
            string gameIndexPath = PathUtils.GetPluginGameIndexPath();

            if (mGameIndex != null)
            {
                var parser = new FileIniDataParser();

                try
                {
                    parser.WriteFile(gameIndexPath, mGameIndex);
                }
                catch (Exception e)
                {
                    Logger.Log(string.Format("Error saving game index file to {0}.", gameIndexPath));
                    Logger.Log(e.ToString(), Logger.LogLevel.Exception);
                }
            }
        }

        public static string GetPlayRomInArchive(string gameId)
        {
            string playRomInArchive = string.Empty;

            if (mGameIndex != null)
            {
                playRomInArchive = mGameIndex[gameId][nameof(PlayRomInArchive)];
            }

            return playRomInArchive;
        }

        public static void SetPlayRomInArchive(string gameId, string playRomInArchive)
        {
            if (mGameIndex == null)
            {
                mGameIndex = new IniData();
            }

            mGameIndex[gameId][nameof(PlayRomInArchive)] = playRomInArchive;

            Save();
        }
    }
}
