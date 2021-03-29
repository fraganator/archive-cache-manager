using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Unbroken.LaunchBox.Plugins.Data;

namespace ArchiveCacheManager
{
    class GameBadge : IGameBadge
    {
        private int mIndex = 99;

        public string Name => "Archive Cached";
        public string UniqueId => "Archive Cached";
        public Image DefaultIcon => Resources.badge;
        public int Index
        {
            get => mIndex;
            set => mIndex = value;
        }

        public bool GetAppliesToGame(IGame game)
        {
            return File.Exists(PathUtils.GetArchiveCacheGameInfoPath(PathUtils.ArchiveCachePath(PathUtils.GetAbsolutePath(game.ApplicationPath))));
        }
    }
}
