using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Unbroken.LaunchBox.Plugins;
using Unbroken.LaunchBox.Plugins.Data;
using System.Windows.Forms;
using System.Windows.Interop;

namespace ArchiveCacheManager
{
    class GameMenuItem : IGameMenuItemPlugin
    {
        public bool SupportsMultipleGames => false;
        public string Caption => PluginHelper.StateManager.IsBigBox ? "Select ROM In Archive" : "Select ROM In Archive...";
        public Image IconImage => Resources.icon16x16_play;
        public bool ShowInLaunchBox => true;
        public bool ShowInBigBox => true;

        public bool GetIsValidForGame(IGame selectedGame) => Zip.SupportedType(selectedGame.ApplicationPath)
                                                             && PluginUtils.GetEmulatorPlatformAutoExtract(selectedGame.EmulatorId, selectedGame.Platform);
        public bool GetIsValidForGames(IGame[] selectedGames) => false;

        public void OnSelected(IGame selectedGame)
        {
            // HACK
            // In case where game is launched, but launch failed or aborted, 7z isn't cleaned up. If this code then runs, it will
            // call the archive cache manager version of 7z, which will not return the correct results (file priority will be applied,
            // and the first file listing removed. Restore 7z here, just in case it wasn't cleaned up properly previously.
            GameLaunching.Restore7z();

            string[] fileList = new Zip().List(PathUtils.GetAbsolutePath(selectedGame.ApplicationPath));

            if (fileList.Count() == 0)
            {
                string errorMessage = string.Format("Error listing contents of {0}.\r\n\r\nCheck {1} for details.", Path.GetFileName(selectedGame.ApplicationPath), Path.GetFileName(PathUtils.GetLogFilePath()));

                if (PluginHelper.StateManager.IsBigBox)
                {
                    MessageBoxBigBox messageBox = new MessageBoxBigBox(errorMessage);
                    messageBox.ShowDialog();
                }
                else
                {
                    MessageBox.Show(errorMessage, "Archive Cache Manager", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                
                return;
            }

            var emulatorsTuple = PluginUtils.GetPlatformEmulators(selectedGame.Platform, selectedGame.EmulatorId);

            Form window;
            if (PluginHelper.StateManager.IsBigBox)
            {
                window = new ArchiveListWindowBigBox(Path.GetFileName(selectedGame.ApplicationPath), fileList, GameIndex.GetSelectedFile(selectedGame.Id));
            }
            else
            {
                window = new ArchiveListWindow(Path.GetFileName(selectedGame.ApplicationPath), fileList, emulatorsTuple.Select(emu => PluginUtils.GetEmulatorTitle(emu.Item1, emu.Item2)).ToArray(), GameIndex.GetSelectedFile(selectedGame.Id));
            }
            //NativeWindow parent = new NativeWindow();

            // Glue between the main app window (WPF) and this window (WinForms)
            //parent.AssignHandle(new WindowInteropHelper(System.Windows.Application.Current.MainWindow).Handle);
            window.ShowDialog();// parent);

            if (window.DialogResult == DialogResult.OK)
            {
                if (PluginHelper.StateManager.IsBigBox)
                {
                    GameIndex.SetSelectedFile(selectedGame.Id, (window as ArchiveListWindowBigBox).SelectedFile);
                    PluginHelper.BigBoxMainViewModel.PlayGame(selectedGame, null, PluginHelper.DataManager.GetEmulatorById(selectedGame.EmulatorId), null);
                }
                else
                {
                    int emulatorIndex = (window as ArchiveListWindow).EmulatorIndex;
                    // Use a specific command line for the IEmulatorPlatform. This covers the case where RetroArch has more than one core configured for the same platform.
                    string commandLine = emulatorsTuple[emulatorIndex].Item2.CommandLine;
                    // Use the game's custom command line if it exists and we're running with the game's emulator (index 0)
                    if (emulatorIndex == 0 && !string.IsNullOrEmpty(selectedGame.CommandLine))
                    {
                        commandLine = selectedGame.CommandLine;
                    }
                    GameIndex.SetSelectedFile(selectedGame.Id, (window as ArchiveListWindow).SelectedFile);
                    PluginHelper.LaunchBoxMainViewModel.PlayGame(selectedGame, null, emulatorsTuple[emulatorIndex].Item1, commandLine);
                }
            }
        }

        public void OnSelected(IGame[] selectedGames)
        {
            
        }
    }
}
