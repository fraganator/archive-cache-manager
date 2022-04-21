/* Archive Cache Manager - A LaunchBox plugin which extracts and caches ROM
 * archives, letting you play games faster.
 * 
 * Copyright (C) 2021  fraganator
 * 
 * This library is free software; you can redistribute it and/or
 * modify it under the terms of the GNU Lesser General Public
 * License as published by the Free Software Foundation; either
 * version 2.1 of the License, or (at your option) any later version.
 * 
 * This library is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
 * Lesser General Public License for more details.
 * 
 * You should have received a copy of the GNU Lesser General Public
 * License along with this library; if not, write to the Free Software
 * Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301
 * USA
 * 
 * Links:
 * Plugin Homepage - https://forums.launchbox-app.com/files/file/234-archive-cache-manager/
 * Forum Support - https://forums.launchbox-app.com/topic/35010-archive-cache-manager/
 * GitHub Repository - https://github.com/fraganator/archive-cache-manager
 * 
 * Contact:
 * GitHub - https://github.com/fraganator
 * LaunchBox Forum - https://forums.launchbox-app.com/profile/69812-fraganator/
 */

/* NOTES ON LAUNCHBOX AND 7-ZIP
 * 
 * When an emulator has the "Extract ROM archives before running option"
 * checked and a game is launched, 7z is called twice. The first call extracts
 * the archive with the command:
 * 
 *     7z.exe x <rom path> -o<output path> -y -aoa -bsp1
 * 
 * where <rom path> and <output path> are absolute, and <output path> is a temp
 * location of LaunchBox\ThirdParty\7-Zip\Temp
 * 
 * Once extraction completes and 7z returns, LaunchBox will call 7z a second
 * time to list the archive content with the command:
 * 
 *     7z.exe l <rom path> -slt
 * 
 * LaunchBox uses this file list to determine which rom path to supply to the
 * emulator. It also uses this list to determine which files to delete from the
 * temp location when the emulator exits.
 * 
 * LaunchBox also uses 7z to perform other functions, namely extracting
 * downloaded metadata.xml, and performing data backups.
 */

using System;
using System.Diagnostics;
using System.Linq;

namespace ArchiveCacheManager
{
    class Program
    {
        static void Main(string[] args)
        {
#if DEBUG
            Debugger.Launch();
#endif
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            Logger.Log("========");
            Logger.Log(string.Format("Archive Cache Manager started with arguments: {0}", string.Join(" ", args)));

            if (args.Count() >= 1)
            {
                // Global try/catch handler, in case there's an unhandled exception.
                try
                {
                    switch (args[0])
                    {
                        // Expected command: 7z.exe x <rom path> -o<output path> -y -aoa -bsp1
                        case "x":
                            Logger.Log("Running in extraction mode.");
                            ExtractArchive(args);
                            break;
                        // Expected command: 7z.exe l <rom path> -slt
                        case "l":
                            Logger.Log("Running in list mode.");
                            ListArchive(args);
                            break;
                        // Unknown command, pass straight to 7z
                        default:
                            Logger.Log("Unknown arguments, redirecting to 7-Zip.");
                            Zip.Call7z(args);
                            break;
                    }
                }
                catch (Exception e)
                {
                    Logger.Log(e.ToString(), Logger.LogLevel.Exception);
                    Environment.ExitCode = 1;
                }
            }

            Logger.Log($"Completed in {stopwatch.ElapsedMilliseconds}ms.");
        }

        /// <summary>
        /// Run the archive extraction. This will either extract the archive, or use the cached copy.
        /// </summary>
        /// <param name="args"></param>
        static void ExtractArchive(string[] args)
        {
            // Remove leading -o
            string outputPath = args[2].Remove(0, 2);

            // Check if the destination path ends in 7-Zip\Temp, which is assumed to be for a game
            // Any other path is likely to be either Metadata, or some other non-game archive
            if (outputPath.EndsWith(@"7-Zip\Temp", StringComparison.InvariantCultureIgnoreCase))
            {
                CacheManager.ExtractArchive(args);
            }
            else
            {
                Logger.Log("Unexpected output path, redirecting to 7-Zip.");
                Zip.Call7z(args);
            }
        }

        /// <summary>
        /// List the archive contents. File list may be cached files, or from archive.
        /// Will also apply file extension priority if configured.
        /// </summary>
        /// <param name="args"></param>
        static void ListArchive(string[] args)
        {
            CacheManager.ListArchive();
        }
    }
}
