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

namespace ArchiveCacheManager
{
    /// <summary>
    /// Class for backing up and restoring LaunchBox data.
    /// </summary>
    static class LaunchBoxDataBackup
    {
        private static readonly string launchInfoSection = "LaunchInfo";
        private static readonly string valueKey = "Value";

        private static string mSettingsPath = PathUtils.GetPluginSettingsFilenamePath();
        private static string mGameId = string.Empty;
        private static string mApplicationId = string.Empty;
        private static string mEmulator = string.Empty;
        private static string mEmulatorId = string.Empty;
        private static string mPlatform = string.Empty;
        private static Dictionary<SettingName, object> mSettings = new Dictionary<SettingName, object>();

        private static Task restoreSettingsTask = null;
        private static CancellationTokenSource restoreSettingsDelayToken = null;

        public static string GameId => mGameId;
        public static string ApplicationId => mApplicationId;
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
            mApplicationId = app != null ? app.Id : null;
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
        private static void RestoreAllSettingsSync()
        {
            foreach (var setting in mSettings)
            {
                switch (setting.Key)
                {
                    case SettingName.IEmulatorPlatform_M3uDiscLoadEnabled:
                        PluginUtils.SetEmulatorPlatformM3uDiscLoadEnabled(mEmulatorId, mPlatform, Convert.ToBoolean(setting.Value));
                        Logger.Log(string.Format("Restored M3uDiscLoadEnabled to {0} for {1} \\ {2}.", Convert.ToBoolean(setting.Value), mEmulator, mPlatform));
                        break;
                    case SettingName.IGame_ApplicationPath: break;
                    case SettingName.IAdditionalApplication_ApplicationPath: break;
                    default: break;
                }
            }

            if (mSettings.Count > 0)
            {
                PluginHelper.DataManager.Save();

                if (VerifySettingsRestored())
                {
                    mSettings.Clear();
                    File.Delete(mSettingsPath);
                    Logger.Log("Temporary settings changes restored.");
                }
            }
        }

        /// <summary>
        /// Verify the stored settings match those in LaunchBox.
        /// </summary>
        /// <returns>True on successful verification, false on failure.</returns>
        private static bool VerifySettingsRestored()
        {
            bool verificationError = false;
            bool boolValue;
            string message;

            PluginHelper.DataManager.ReloadIfNeeded();

            foreach (var setting in mSettings)
            {
                switch (setting.Key)
                {
                    case SettingName.IEmulatorPlatform_M3uDiscLoadEnabled:
                        boolValue = PluginUtils.GetEmulatorPlatformM3uDiscLoadEnabled(mEmulatorId, mPlatform);
                        if (Convert.ToBoolean(setting.Value) != boolValue)
                        {
                            verificationError = true;
                            message = string.Format("Failed to verify M3uDiscLoadEnabled for {0} \\ {1}.\r\nValue is {2} and should be {3}.", mEmulator, mEmulatorId, boolValue, Convert.ToBoolean(setting.Value));
                            Logger.Log(message);
                            throw new ApplicationException(message);
                        }
                        break;
                    case SettingName.IGame_ApplicationPath: break;
                    case SettingName.IAdditionalApplication_ApplicationPath: break;
                    default: break;
                }
            }

            return !verificationError;
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
        public static void RestoreAllSettingsDelay(int delay = 0)
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
            iniData[launchInfoSection][nameof(ApplicationId)] = ApplicationId;
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
                            mApplicationId = iniData[launchInfoSection][nameof(ApplicationId)];
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
