using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Unbroken.LaunchBox.Plugins;
using Unbroken.LaunchBox.Plugins.Data;
using IniParser;
using IniParser.Model;
using System.Windows.Forms;

namespace ArchiveCacheManager
{
    /// <summary>
    /// Class for backing up and restoring LaunchBox data.
    /// </summary>
    static class LaunchBoxDataBackup
    {
        private static readonly string launchInfoSection = "LaunchInfo";
        private static readonly string valueKey = "Value";

        private static string mSettingsPath = PathUtils.GetRestoreSettingsFilenamePath();
        private static string mGameId = string.Empty;
        private static string mTitle = string.Empty;
        private static string mApplicationId = string.Empty;
        private static string mApplicationName = string.Empty;
        private static string mEmulator = string.Empty;
        private static string mEmulatorId = string.Empty;
        private static string mPlatform = string.Empty;
        private static Dictionary<SettingName, object> mSettings = new Dictionary<SettingName, object>();

        private static Task restoreSettingsTask = null;
        private static CancellationTokenSource restoreSettingsDelayToken = null;

        public static string GameId => mGameId;
        public static string Title => mTitle;
        public static string ApplicationId => mApplicationId;
        public static string ApplicationName => mApplicationName;
        public static string Emulator => mEmulator;
        public static string EmulatorId => mEmulatorId;
        public static string Platform => mPlatform;
        public static Dictionary<SettingName, object> Settings => mSettings;

        /// <summary>
        /// The fully qualified setting name, in format class_property.
        /// </summary>
        public enum SettingName
        {
            IEmulatorPlatform_M3uDiscLoadEnabled,
            IGame_ApplicationPath,
            IAdditionalApplication_ApplicationPath
        };

        /// <summary>
        /// Static constructor. Load any settings from disk.
        /// </summary>
        static LaunchBoxDataBackup()
        {
            Load();
        }

        /// <summary>
        /// Store launch game details required for saving and restoring LaunchBox settings.
        /// </summary>
        /// <param name="game"></param>
        /// <param name="app"></param>
        /// <param name="emulator"></param>
        public static void SetLaunchDetails(IGame game, IAdditionalApplication app, IEmulator emulator)
        {
            mGameId = game.Id;
            mTitle = game.Title;
            mApplicationId = app != null ? app.Id : null;
            mApplicationName = app != null ? app.Name : null;
            mEmulator = emulator.Title;
            mEmulatorId = emulator.Id;
            mPlatform = game.Platform;
        }

        /// <summary>
        /// Store the specified LaunchBox setting.
        /// </summary>
        /// <param name="setting"></param>
        /// <param name="value"></param>
        public static void BackupSetting(SettingName setting, object value)
        {
            mSettings.Add(setting, value);
        }

        /// <summary>
        /// Restore all of the stored LaunchBox settings. Once restored, will remove the stored settings file.
        /// </summary>
        private static void RestoreAllSettingsSync(int retries = 5)
        {
            foreach (var setting in mSettings)
            {
                switch (setting.Key)
                {
                    case SettingName.IEmulatorPlatform_M3uDiscLoadEnabled:
                        PluginUtils.SetEmulatorPlatformM3uDiscLoadEnabled(mEmulatorId, mPlatform, Convert.ToBoolean(setting.Value));
                        Logger.Log(string.Format("Restored IEmulatorPlatform.M3uDiscLoadEnabled for {0} \\ {1} to {2}.", mEmulator, mPlatform, Convert.ToBoolean(setting.Value)));
                        break;
                    case SettingName.IGame_ApplicationPath:
                        PluginHelper.DataManager.GetGameById(GameId).ApplicationPath = Convert.ToString(setting.Value);
                        Logger.Log(string.Format("Restored IGame.ApplicationPath for {0} ({1}) to {2}.", mTitle, mPlatform, Convert.ToString(setting.Value)));
                        break;
                    case SettingName.IAdditionalApplication_ApplicationPath:
                        PluginUtils.GetAdditionalApplicationById(mGameId, mApplicationId).ApplicationPath = Convert.ToString(setting.Value);
                        Logger.Log(string.Format("Restored IAdditionalApplication.ApplicationPath for {0} ({1} - {2}) to {3}.", mApplicationName, mTitle, mPlatform, Convert.ToString(setting.Value)));
                        break;
                    default: break;
                }
            }

            if (mSettings.Count > 0)
            {
                string message;
                PluginHelper.DataManager.Save();

                if (VerifySettingsRestored(out message))
                {
                    File.Delete(mSettingsPath);
                    Logger.Log("Temporary settings changes restored.");
                }
                else if (retries > 0)
                {
                    retries--;
                    Logger.Log(string.Format("Failed to restore temporary settings changes, retrying... ({0} retries left)", retries));
                    Task.Delay(500).Wait();
                    RestoreAllSettingsSync(retries);
                }
                else if (retries <= 0)
                {
                    UserInterface.ErrorDialog($"Archive Cache Manager:\r\nError restoring temporary settings changes.\r\n"
                                              + $"Please review the items below and manually restore them, or restore a Data Backup created by LaunchBox.\r\n\r\n{message}");
                    File.Delete(mSettingsPath);
                }
            }
        }

        /// <summary>
        /// Verify the stored settings match those in LaunchBox.
        /// </summary>
        /// <returns>True on successful verification, false on failure.</returns>
        private static bool VerifySettingsRestored(out string message)
        {
            List<SettingName> verifiedSettings = new List<SettingName>();
            bool boolValue;
            string stringValue;
            message = string.Empty;

            PluginHelper.DataManager.ReloadIfNeeded();

            foreach (var setting in mSettings)
            {
                switch (setting.Key)
                {
                    case SettingName.IEmulatorPlatform_M3uDiscLoadEnabled:
                        boolValue = PluginUtils.GetEmulatorPlatformM3uDiscLoadEnabled(mEmulatorId, mPlatform);
                        if (Convert.ToBoolean(setting.Value) != boolValue)
                        {
                            message += string.Format("Failed to verify IEmulatorPlatform.M3uDiscLoadEnabled for {0} \\ {1}.\r\nCurrent value = {2}\r\nCorrect value = {3}.\r\n\r\n", mEmulator, mEmulatorId, boolValue, Convert.ToBoolean(setting.Value));
                            Logger.Log(message);
                        }
                        else
                        {
                            verifiedSettings.Add(setting.Key);
                        }
                        break;
                    case SettingName.IGame_ApplicationPath:
                        stringValue = PluginHelper.DataManager.GetGameById(mGameId).ApplicationPath;
                        if (!string.Equals(Convert.ToString(setting.Value), stringValue))
                        {
                            message += string.Format("Failed to verify IGame.ApplicationPath for {0} ({1}).\r\nCurrent value = {2}\r\nCorrect value = {3}.\r\n\r\n", mTitle, mPlatform, stringValue, Convert.ToString(setting.Value));
                            Logger.Log(message);
                        }
                        else
                        {
                            verifiedSettings.Add(setting.Key);
                        }
                        break;
                    case SettingName.IAdditionalApplication_ApplicationPath:
                        stringValue = PluginUtils.GetAdditionalApplicationById(mGameId, mApplicationId).ApplicationPath;
                        if (!string.Equals(Convert.ToString(setting.Value), stringValue))
                        {
                            message += string.Format("Failed to verify IAdditionalApplication.ApplicationPath for {0} ({1} - {2}).\r\nCurrent value = {3}\r\nCorrect value = {4}.\r\n\r\n", mApplicationName, mTitle, mPlatform, stringValue, Convert.ToString(setting.Value));
                            Logger.Log(message);
                        }
                        else
                        {
                            verifiedSettings.Add(setting.Key);
                        }
                        break;
                    default: break;
                }
            }

            foreach (var setting in verifiedSettings)
            {
                mSettings.Remove(setting);
            }

            return (mSettings.Count == 0);
        }

        /// <summary>
        /// Restore any stored settings to LaunchBox after the specified delay.
        /// </summary>
        /// <param name="delay">Delay before restoring settings, in milliseconds.</param>
        private static async Task RestoreAllSettingsAsync(int delay = 0)
        {
            if (delay > 0)
            {
                try
                {
                    await Task.Delay(delay, restoreSettingsDelayToken.Token);
                }
                catch (TaskCanceledException)
                {

                }
            }

            RestoreAllSettingsSync();
        }

        private static bool IsRestoreTaskCompleted()
        {
            if (restoreSettingsTask != null && restoreSettingsDelayToken != null && !restoreSettingsTask.IsCompleted)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Restore any stored settings to LaunchBox. Waits for an asynchronous restore operation to complete, or begin a synchronous restore operation.
        /// </summary>
        public static void RestoreAllSettings()
        {
            if (!IsRestoreTaskCompleted())
            {
                // This doesn't cancel the restore operation, only the delay before the restoration
                restoreSettingsDelayToken.Cancel();
                restoreSettingsTask.Wait();
                restoreSettingsDelayToken = null;
                restoreSettingsTask = null;
            }
            else
            {
                RestoreAllSettingsSync();
            }
        }

        /// <summary>
        /// Restore the stored settings after the specified delay. The delay and restore operation run as an asynchronous task.
        /// </summary>
        /// <param name="delay"></param>
        public static void RestoreAllSettingsDelay(int delay)
        {
            restoreSettingsDelayToken = new CancellationTokenSource();
            restoreSettingsTask = Task.Run(() => RestoreAllSettingsAsync(delay));
        }

        /// <summary>
        /// Save the stored settings to disk.
        /// </summary>
        public static void Save()
        {
            var parser = new FileIniDataParser();
            IniData iniData = new IniData();

            iniData[launchInfoSection][nameof(GameId)] = GameId;
            iniData[launchInfoSection][nameof(Title)] = Title;
            iniData[launchInfoSection][nameof(ApplicationId)] = ApplicationId;
            iniData[launchInfoSection][nameof(ApplicationName)] = ApplicationName;
            iniData[launchInfoSection][nameof(EmulatorId)] = EmulatorId;
            iniData[launchInfoSection][nameof(Platform)] = Platform;

            foreach (var setting in mSettings)
            {
                iniData[setting.Key.ToString()][valueKey] = setting.Value.ToString();
            }

            try
            {
                parser.WriteFile(mSettingsPath, iniData);
            }
            catch (Exception e)
            {
                Logger.Log(string.Format("Error saving config file to {0}.", mSettingsPath));
                Logger.Log(e.ToString(), Logger.LogLevel.Exception);
            }
        }

        /// <summary>
        /// Load the stored settings from disk.
        /// </summary>
        public static void Load()
        {
            if (File.Exists(mSettingsPath))
            {
                try
                {
                    var parser = new FileIniDataParser();
                    IniData iniData = parser.ReadFile(mSettingsPath);

                    foreach (var section in iniData.Sections)
                    {
                        if (string.Equals(section.SectionName, launchInfoSection))
                        {
                            mGameId = iniData[launchInfoSection][nameof(GameId)];
                            mTitle = iniData[launchInfoSection][nameof(Title)];
                            mApplicationId = iniData[launchInfoSection][nameof(ApplicationId)];
                            mApplicationName = iniData[launchInfoSection][nameof(ApplicationName)];
                            mEmulatorId = iniData[launchInfoSection][nameof(EmulatorId)];
                            mPlatform = iniData[launchInfoSection][nameof(Platform)];
                        }
                        else
                        {
                            SettingName settingName;
                            if (Enum.TryParse(section.SectionName, out settingName))
                            {
                                mSettings.Add(settingName, section.Keys[valueKey]);
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    Logger.Log(e.ToString(), Logger.LogLevel.Exception);
                }
            }
        }
    }
}
