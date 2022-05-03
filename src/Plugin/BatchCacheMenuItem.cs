using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Interop;
using Unbroken.LaunchBox.Plugins;
using Unbroken.LaunchBox.Plugins.Data;

namespace ArchiveCacheManager
{
    class BatchCacheMenuItem : IGameMenuItemPlugin
    {
        public bool SupportsMultipleGames => true;
        public string Caption => "Batch Cache Games...";
        public Image IconImage => Resources.icon16x16;
        public bool ShowInLaunchBox => true;
        public bool ShowInBigBox => false;

        public bool GetIsValidForGame(IGame selectedGame) => true;
        public bool GetIsValidForGames(IGame[] selectedGames) => true;

        public void OnSelected(IGame selectedGame)
        {
            OnSelected(new IGame[] { selectedGame });
        }

        public void OnSelected(IGame[] selectedGames)
        {
            BatchCacheWindow window = new BatchCacheWindow(selectedGames);
            NativeWindow parent = new NativeWindow();

            // Glue between the main app window (WPF) and this window (WinForms)
            parent.AssignHandle(new WindowInteropHelper(System.Windows.Application.Current.MainWindow).Handle);
            window.ShowDialog(parent);

            if (window.RefreshLaunchBox && !PluginHelper.StateManager.IsBigBox)
            {
                PluginHelper.LaunchBoxMainViewModel.RefreshData();
            }
        }
    }
}
