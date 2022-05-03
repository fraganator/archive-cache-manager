using System.Drawing;
using Unbroken.LaunchBox.Plugins;
using System.Windows.Forms;
using System.Windows.Interop;

namespace ArchiveCacheManager
{
    public class SystemMenuItem : ISystemMenuItemPlugin
    {
        public string Caption => "Archive Cache Manager...";
        public Image IconImage => Resources.icon16x16;
        public bool ShowInLaunchBox => true;
        public bool ShowInBigBox => false;
        public bool AllowInBigBoxWhenLocked => false;

        public void OnSelected()
        {
            //ConfigWindow window = new ConfigWindow();
            NewConfigWindow window = new NewConfigWindow();
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
