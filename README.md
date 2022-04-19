# Archive Cache Manager
![Achive Cache Manager logo](images/logo-v2-title.png?raw=true "Achive Cache Manager")

A LaunchBox plugin which caches extracted ROM archives, letting you play games faster. Also allows launching individual files from archives, and loading preferred file types from an archive.

## New in v2.12
* Option to copy non-archive files to cache
* Support for extracting additional formats
    * Option to extract chd to cue+bin using chdman
	* Option to extract rvz, wia, gcz to iso using DolphinTool
* Option to specify a launch folder for cached games (game platform, game title, or emulator title)
    * Useful for managing common RetroArch settings
* Smart Extract option to only extract required files from an archive
    * Useful for merged ROM sets
* Emulator selection when launching a file from "Select ROM In Archive" list
* Option to bypass LaunchBox's check the ROM file exists when launching a game
    * Allows launching cached game immediately - no waiting for slow disk spin-up or network latency
	* Allows playing cached games 'offline' if NAS or cloud storage unavailable
* Config window and UI overhaul
* Minor bug fixes

## Description
When a compressed ROM (zip, 7z, rar, gz, chd, rvz, etc.) is first extracted, it is stored in the archive cache. The next time it is played, the game is loaded directly from the cache, virtually eliminating wait time.

![Launch time comparison video](images/launch-video.gif?raw=true "Side-by-side video comparing launch time of game from zip vs. from cache")

As the cache size approaches the maximum size, the oldest played games are deleted from the cache, making room for new games.

## Features
* Skip the extraction wait time for recently played games.
* Configurable cache size and location.
* Configurable minimum archive size (don't cache small archives).
* Option to extract all discs in a multi-disc game, and generate M3U file.
* Option to copy ROM files to cache without extraction.
* Option to keep select ROMs cached and ready to play.
* Select and play individual ROM files from an archive.
* Filename and extension priorities per emulator and platform (cue, bin, iso, etc).
* Support for additional archive formats (chd, rvz, etc)
* Badge to indicate cached games

### Example Use Cases
Why use Archive Cache Manager? Here's some example use cases.

* Zipped ROMs located on network server or cloud drive, where disk read or network transfer time is slow.
* ROM library maintained as accurately ripped/dumped collections, where specialised compression formats not an option.
* Extract and play archives from location other than `LaunchBox\ThirdParty\7-Zip\Temp`, such as high speed SSD.
* Playing ripped PS2 games with PCSX2 where the disc image is bin/cue format, avoiding the "CDVD plugin failed to open" error message.
* Playing MSU versions of games, where need to launch the rom file instead of the cue file from the archive.
* Your library contains GoodMerged sets, and you want a quick way to play individual ROMs.

## Installation
* Download the latest release from https://forums.launchbox-app.com/files/file/234-archive-cache-manager/ or https://github.com/fraganator/archive-cache-manager/releases
* Unblock the download if necessary (right-click file -> Properties -> Unblock)
* Extract this archive to your `LaunchBox\Plugins` folder, then run LaunchBox / BigBox.
* Within LaunchBox, ensure the emulator or emulator platform has the _"Extract ROM archives before running"_ option checked.


## Uninstallation
* Quit LaunchBox / BigBox, then navigate to the `LaunchBox\Plugins` folder and delete `ArchiveCacheManager`.
* Delete the archive cache folder (default is `LaunchBox\ArchiveCache`).


## Usage

### Extracting and Caching
Archive Cache Manager runs transparently to the user. All that is required is the emulator or emulator platform has the _"Extract ROM archives before running"_ option checked. Extraction and cache management is carried out automatically when a game is launched.

Cache and extraction behaviour can be modified in plugin's configuration window.

### Multi-Disc Games
To use the multi-disc cache feature, check the _"Multi-disc Support"_ option in the Archive Cache Manager config window. The next time a multi-disc game is launched, all the discs from the game will be extracted to the cache and a corresponding M3U file generated.

If the emulator \ platform supports M3U files (as configured in LaunchBox), the generated M3U file will be used when launching the game. Otherwise a single disc will be launched, and swapping between cached discs can be done manually in the emulator.

### Selecting and Playing Individual ROMs From an Archive
To play an individual ROM from an archive containing multiple ROMs (different regions, hacks, or discs), right-click it and choose "Select ROM In Archive...".

A window will popup with a full listing of the archive contents. Select the desired ROM file, then click Play. That ROM will now launch with the configured emulator.

The next time the game is launched via the normal Play option, the previous ROM selection will be automatically applied. To select another ROM, use the same "Select ROM In Archive..." menu.

![ROM file selection window](images/select-file-window.png?raw=true "ROM file selection window")

The same menu is also available in BigBox, though currently only supports keyboard input.

![ROM file selection window](images/select-file-window-bigbox.png?raw=true "ROM file selection window")

### Keeping Games Cached
Games can be marked 'Keep' so they stay cached and ready to play. To keep a game cached, open the plugin configuration window from the _Tools->Archive Cache Manager_ menu. From there a list of games in the cache is shown. Check the Keep box next to the game, then click OK.

### Badge
The plugin includes a badge to indicate if a game is currently in the cache. It is available under the _Badges->Plugin Badges->Enable Archive Cached_ menu. There are additional Simple White and Neon style badges, which can be found in the `LaunchBox\Plugins\ArchiveCacheManager\Badges` folder. Copy your preferred icon to the `LaunchBox\Images\Badges` folder and rename it `Archive Cached.png`.

![Badge icon to indicate if a game is cached](images/badges.png?raw=true "Badge icon to indicate if a game is cached")


# Configuration
Configuration can be accessed from the _Tools->Archive Cache Manager_ menu. An overview of each of the configuration screens and options is below.

## Cache Settings
This page shows a summary of the cache storage and currently cache items, and provides options for cache configuration.

![Achive Cache Manager config screen](images/cache-settings.png?raw=true "Achive Cache Manager cache settings")

#### Configure Cache
Clicking the `Configure Cache...` button opens the cache configuration window.

![Achive Cache Manager cache config screen](images/config-cache.png?raw=true "Achive Cache Manager cache config screen")

##### _Configure Cache - Cache Path_
This is the path of the cache on disk. The path can be absolute or relative, where relative paths are to the `LaunchBox` root folder.

Default: _ArchiveCache_

If the cache path is set to an invalid location (`LaunchBox` root for example), an error message will be displayed when clicking OK.

![Cache path error](images/config-cache-error.png?raw=true "Cache path error")

If the cache path is set to an existing path that already contains files or folders, a warning will be displayed when clicking OK. Click Yes to proceed, or No to go back and change the path.

![Cache path warning](images/config-cache-warning.png?raw=true "Cache path warning")

##### _Configure Cache - Cache Size_
This is the maximum cache size on disk in megabytes. The oldest played games will be deleted from the cache when it reaches this size.

Default: _`20,000 MB (20 GB)`_

##### _Configure Cache - Minimum Archive Size_
This is the minimum size in megabytes of an uncompressed archive before it will be added to the cache. Archives smaller than this won't be added to the cache.

Default: _`100 MB`_

#### _Keep_
A _Keep_ flag can be applied to a cached item. When set, the item will be excluded from cache management and not be removed from the cache. This is useful for less frequently played games which need to load without waiting (very large games, party games, favourites, children's games, etc).

Default: _`Disabled`_

#### Open In Explorer
Clicking the `Open In Explorer` button will open the configured cache path in Windows Explorer. This button is disabled if the cache path does not exist.

#### Refresh
Refreshes the cache summary and list of cached items from disk.

#### Delete
Clicking on the `Delete` button will remove the selected items from the cache (including items with the _Keep_ setting).

#### Delete All
Clicking on the `Delete All` button will delete everything from the cache (including items with the _Keep_ setting).

## Extraction Settings
This page provides settings for archive extraction and launch behviour. Each row in the table applies to the specified emulator \ platform pair. If a game is launched which doesn't match an emulator \ platform, the settings in _All \ All_ will be used.

![Achive Cache Manager config screen](images/extraction-settings.png?raw=true "Achive Cache Manager extraction settings")

#### Emulator \ Platform
The specific emulator and platform for the settings to be applied to. Add a new emulator and platform row using the `Add...` button. Remove an emulator \ platform row using the `Delete` button.

When a new row is added its settings will be copied from the _All \ All_ entry, except for Priority which will be blank.

#### Priority
Files from an archive can be prioritized in the case where an emulator requires a certain filename or file type. This setting defines the filename or extension priority for the specified emulator and platform combination.

The priorities are a comma delimited list, where the highest priority is the first entry, the next highest priority is the second entry, and so on. If a match is not found in the archive when a game is loaded, the priority in _All \ All_ is used.

A wildcard (*) can be used to perform partial filename matches. Examples include:

* `bin, iso` - Files ending with bin, then files ending in iso, then all other files.
* `eboot.bin` - Files named eboot.bin, then all other files.
* `*(*E*)*[!].*, *(*U*)*[!].*, *[!].*` - GoodMerged style, prioritizing European 'good' ROM dumps, then USA 'good' ROM dumps, then other region 'good' ROM dumps, then all other files.

Note that the priority is applied to all archives, even if they are not cached.

The fallback _All \ All_ priority is the basis for the contents of the multi-disc M3U file generation. Be careful removing entries such as `cue`, unless a specific emulator \ platform is used to handle `cue` and similar file types.

Default:
* _`All \ All` | `mds, gdi, cue, eboot.bin`_
* _`PCSX2 \ Sony Playstation 2` | `bin, iso`_

#### Action
The action to take when processing a ROM file.

* `Extract` - Extract an archive to the cache. If a non-archive file is launched, Archive Cache Manager won't run.
* `Copy` - Copy the game file to the cache. Archive files will not be extracted, only copied.
* `Extract or Copy` - If the game file is an archive, extract it to the cache. Otherwise copy the game file to the cache.

Default: _`Extract`_

#### Launch Path
The path within the cache to launch the game from. Useful for managing common settings in RetroArch which are based on the game's folder.

* `Default` - The archive folder in the cache, which is in the form _\<Filename\> - \<MD5 Hash\>_
* `Title` - The game's title set in LaunchBox (e.g. _Final Fantasy VII_)
* `Platform` - The game's platform set in LaunchBox (e.g. _Sony Playstation_)
* `Emulator` - The emulator title set in LaunchBox (e.g. _RetroArch_)

Games will always be extracted \ copied to the _\<Filename\> - \<MD5 Hash\>_ location. If the Launch Path is set to something other than `Default`, the corresponding folder will be created (or cleared if it exists), and NTFS hardlinks will be created which point to the extracted \ copied files.

Default: _`Default`_

#### Multi-Disc
Check this option to enable multi-disc support. When enabled, the following actions occur when playing a multi-disc game:

* All discs from a multi-disc game will be extracted \ copied and added to the archive cache.
* M3U files will be generated, with the name based on the _M3U Name_ setting.
* The M3U contents will list the absolute path to one cached file per disc, where the file is chosen based on the emulator \ platform file priority, or the special _All \ All_ priority.
* If the emulator \ platform supports M3U files, the generated M3U file will be used when launching the game.

Default: _`Enabled`_

#### M3U Name
The name of the M3U file created when launching a multi-disc game, and _Multi-Disc_ is enabled. The M3U file name is used by some emulators to create a save file and match settings.

* `Game ID` - The unique ID for a game, generated by LaunchBox. This is the same M3U naming convention used by LaunchBox (e.g. _2828d969-4362-49d5-b080-c2b7cc6f7d59.m3u_)
* `Title + Version` - The game's title and version combined, with _(Disc N)_ removed from the version (e.g. _Final Fantasy VII (Europe).m3u_)

Default: _`Game ID`_

#### Smart Extract
Check this option to enable smart extraction, which will only extract a single file from an archive if a number of rules are met. See the _Smart Extract Settings_ section for details.

Default: _`Enabled`_

#### chdman
Check this option to extract a _chd_ file to _cue+bin_ files using chdman. The executable _chdman.exe_ must be copied to the folder `LaunchBox\Plugins\ArchiveCacheManager\Extractors`.

Default _`Disabled`_

#### DolphinTool
Check this option to extract _rvz_, _wia_, and _gcz_ files to _iso_ files using DolphinTool. The executable _DolphinTool.exe_ must be copied to the folder `LaunchBox\Plugins\ArchiveCacheManager\Extractors`.

Default _`Disabled`_

## Smart Extract Settings
When enabled, the Smart Extract option will check if it's possible to extract only a single file from an archive. For merged archives containing multiple ROM versions and hacks, this avoids the need to extract a potentially large number of files to play a single game.

![Achive Cache Manager config screen](images/smart-extract-settings.png?raw=true "Achive Cache Manager Smart Extract settings")

Smart Extract will extract and launch a single file from an archive if the following conditions are met:

* An individual file has been selected through the "Select ROM in Archive..." right-click menu.
* All of the file types in an archive are the same, excluding files with Metadata Extensions.
* All of the file types in an archive are Stand-alone ROMs, excluding files with Metadata Extensions.

#### Stand-alone ROM Extensions
A comma delimited list of file extensions which can run stand-alone (no dependencies on other files).

Default: _`gb, gbc, gba, agb, nes, fds, smc, sfc, n64, z64, v64, ndd, md, smd, gen, iso, chd, gg, gcm, 32x, bin`_

#### Metadata Extensions
A comma delimited list of file extensions which indicate ROM metadata, and aren't required to play a ROM file.

Default: _`nfo, txt, dat, xml, json`_

## Plugin Settings

![Achive Cache Manager config screen](images/plugin-settings.png?raw=true "Achive Cache Manager plugin settings")

#### Always Bypass LaunchBox Path Check
When enabled, bypasses LaunchBox's check that a game's application path (ROM file) exists before launch. If a game is cached and the source ROM storage is slow or unavailable, the game will still launch immediately.

The bypass check happens automatically if the extract action is Copy, or the file is not _zip_, _7z_, or _rar_.

Default: _`Disabled`_

#### Check For Updates On Startup
Enable this option to check for plugin updates when LaunchBox starts up. This is a simple version check of the latest release on github, and nothing is automatically downloaded or installed. If a new update is found a message box will appear shortly after LaunchBox is started, with the option to open the download page in a browser.

Default: _On first run, a message box will appear asking the user if they'd like to enable the update check._


## Building
The project is built with Visual Studio Community 2019 and .NET Framework 4.7.2.

The plugin references version 12.8 of `Unbroken.LaunchBox.Plugins` assembly, to handle certain newer features (per emulator/platform ROM extraction). If building for an older version of LaunchBox (pre 12.8), you can define the `LAUNCHBOX_PRE_12_8` conditional compilation symbol. The oldest minimum version of the `Unbroken.LaunchBox.Plugins` assembly supported is 1.0.0.0 included with LaunchBox 10.11, required for _Badge_ support.

### Dependencies
Dependencies are listed below, and can be installed using Visual Studio's Package Manager Console with the command shown.

Package               | Version | PM Command
----------------------|---------|--------------------------
System.Drawing.Common | 4.7.2   | `Install-Package System.Drawing.Common -Version 4.7.2 -ProjectName Plugin`
ini-parser            | latest  | `Install-Package ini-parser -ProjectName Core`
ini-parser            | latest  | `Install-Package ini-parser -ProjectName Plugin`
Octokit               | latest  | `Install-Package Octokit -ProjectName Plugin`


## Acknowledgements
* Icons by Yusuke Kamiyamane. Licensed under a Creative Commons Attribution 3.0 License. https://p.yusukekamiyamane.com/
* INI File Parser library Copyright (c) 2008 Ricardo Amores Hernández. Licensed under the MIT license. https://github.com/rickyah/ini-parser
* Octokit library Copyright (c) 2017 GitHub Inc. Licensed under the MIT license. https://github.com/octokit/octokit.net
* FlexibleMessageBox Copyright (c) 2014 Jörg Reichert. Licensed under the CPOL. https://www.codeproject.com/Articles/601900/FlexibleMessageBox
