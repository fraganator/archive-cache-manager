using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.IO;
using Unbroken.LaunchBox.Plugins;

namespace ArchiveCacheManager
{
    class LaunchBoxSettings
    {
        public static bool HideMouseCursor = false;

        // The values below are LB/BB defaults. That said, they don't directly map to
        // either DirectInput / XInput values, or standard key codes
        public static int ControllerUpButton = 0;
        public static int ControllerDownButton = 0;
        public static int ControllerSelectButton = 1;
        public static int ControllerBackButton = 2;
        public static int ControllerPlayButton = 3;
        public static int KeyboardUp = 24;
        public static int KeyboardDown = 26;
        public static int KeyboardSelect = 6;
        public static int KeyboardBack = 13;
        public static int KeyboardPlay = 59;

        static LaunchBoxSettings()
        {
            Load();
        }

        public static void Load()
        {
            string dataPath = Path.Combine(PathUtils.GetLaunchBoxRootPath(), "Data");

            if (PluginHelper.StateManager.IsBigBox)
            {
                LoadBigBox(Path.Combine(dataPath, "BigBoxSettings.xml"));
            }
            else
            {
                LoadLaunchBox(Path.Combine(dataPath, "Settings.xml"));
            }
        }

        private static void LoadLaunchBox(string settingsPath)
        {
            XmlDocument settings = new XmlDocument();
            settings.Load(settingsPath);
            XmlNode node;
            string xpathPrefix = "/LaunchBox/Settings/";

            node = settings.SelectSingleNode(xpathPrefix + "ControllerSelectButton");
            ControllerSelectButton = (node != null ? Convert.ToInt32(node.InnerText) : ControllerSelectButton);

            // Use the context menu button as the back / cancel button
            node = settings.SelectSingleNode(xpathPrefix + "ControllerContextMenuButton");
            ControllerBackButton = (node != null ? Convert.ToInt32(node.InnerText) : ControllerBackButton);

            node = settings.SelectSingleNode(xpathPrefix + "ControllerPlayButton");
            ControllerPlayButton = (node != null ? Convert.ToInt32(node.InnerText) : ControllerPlayButton);
        }

        private static void LoadBigBox(string settingsPath)
        {
            XmlDocument settings = new XmlDocument();
            settings.Load(settingsPath);
            XmlNode node;
            string xpathPrefix = "/LaunchBox/BigBoxSettings/";

            node = settings.SelectSingleNode(xpathPrefix + "HideMouseCursor");
            HideMouseCursor = (node != null ? Convert.ToBoolean(node.InnerText) : HideMouseCursor);

            node = settings.SelectSingleNode(xpathPrefix + "ControllerSelectButton");
            ControllerSelectButton = (node != null ? Convert.ToInt32(node.InnerText) : ControllerSelectButton);

            node = settings.SelectSingleNode(xpathPrefix + "ControllerBackButton");
            ControllerBackButton = (node != null ? Convert.ToInt32(node.InnerText) : ControllerBackButton);

            node = settings.SelectSingleNode(xpathPrefix + "ControllerPlayButton");
            ControllerPlayButton = (node != null ? Convert.ToInt32(node.InnerText) : ControllerPlayButton);

            node = settings.SelectSingleNode(xpathPrefix + "KeyboardUp");
            KeyboardUp = (node != null ? Convert.ToInt32(node.InnerText) : KeyboardUp);

            node = settings.SelectSingleNode(xpathPrefix + "KeyboardDown");
            KeyboardDown = (node != null ? Convert.ToInt32(node.InnerText) : KeyboardDown);

            node = settings.SelectSingleNode(xpathPrefix + "KeyboardSelect");
            KeyboardSelect = (node != null ? Convert.ToInt32(node.InnerText) : KeyboardSelect);

            node = settings.SelectSingleNode(xpathPrefix + "KeyboardBack");
            KeyboardBack = (node != null ? Convert.ToInt32(node.InnerText) : KeyboardBack);

            node = settings.SelectSingleNode(xpathPrefix + "KeyboardPlay");
            KeyboardPlay = (node != null ? Convert.ToInt32(node.InnerText) : KeyboardPlay);
        }
    }
}
