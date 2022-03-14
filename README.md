# Archive Cache Manager
![Achive Cache Manager logo](images/logo-v2-title.png?raw=true "Achive Cache Manager")

A LaunchBox plugin which caches extracted ROM archives, letting you play games faster. Also allows launching individual files from archives, and loading preferred files from an archive.

## New in v2.11
* Multi-disc support and automatic M3U generation!
    * Extract and cache all discs in a multi-disc game
    * Generate and use M3U where supported by emulator / platform
* Custom filename priority for all emulators / platforms
* Minor bug fixes

## Description
When a compressed ROM (in zip, 7z, or rar format) is first extracted, it is stored in the archive cache. The next time it is played, the game is loaded directly from the cache, virtually eliminating wait time.

![Launch time comparison video](images/launch-video.gif?raw=true "Side-by-side video comparing launch time of game from zip vs. from cache")

As the cache approaches its maximum size, the least recently played games are deleted from the cache, making room for new games.


## Features
* Skip the extraction wait time for recently played games.
* Configurable cache size and location.
* Configurable minimum archive size (skip caching small archives).
* Option to keep select ROMs cached and ready to play.
* Select and play individual ROM files from an archive.
* Filename and extension priorities per emulator and platform (cue, bin, iso, etc).

### Example Use Cases
Why use Archive Cache Manager? Here's some example use cases.

* Zipped ROMs located on network server or cloud drive, where disk read or network transfer time is slow.
* ROM library maintained as accurately ripped/dumped collections, where specialised compression formats not an option.
* Extract and play archives from location other than `LaunchBox\ThirdParty\7-Zip\Temp`, such as high speed SSD.
* Playing ripped PS2 games with PCSX2 where the disc image is bin/cue format, avoiding the "CDVD plugin failed to open" error message.
* Your library contains GoodMerged sets, and you want a quick way to play individual ROMs.


## Installation
* Download the latest release from https://forums.launchbox-app.com/files/file/234-archive-cache-manager/ or https://github.com/fraganator/archive-cache-manager/releases
* Unblock the download if necessary (right-click file -> Properties -> Unblock)
* Extract this archive to your `LaunchBox\Plugins` folder, then run LaunchBox / BigBox.
* Within LaunchBox, ensure the desired emulator has the _"Extract ROM archives before running"_ option checked.


## Uninstallation
* Quit LaunchBox / BigBox, then navigate to the `LaunchBox\Plugins` folder and delete `ArchiveCacheManager`.
* Delete the archive cache folder (default is `LaunchBox\ArchiveCache`).


## Usage
Archive Cache Manager is designed to run transparent to the user. All that is required is the emulator has the _"Extract ROM archives before running"_ option checked. Extraction and cache management is carried out automatically when a game is launched.

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


## Configuration
Configuration can be accessed from the _Tools->Archive Cache Manager_ menu.

![Achive Cache Manager config screen](images/config-screen.png?raw=true "Achive Cache Manager config screen")

An overview of each of the configuration items is below.

### Cache Details
This section shows a summary of the cache including the _Cache Path_, _Cache Size_, and _Keep Size_. It also displays a list of the items currently in the cache.

#### Cached Items & Keep
Each item in the cache has a _Keep_ flag, which when set will prevent the item from being removed from the cache when the cache is full. This is useful for less frequently played games which you still want to load without waiting (very large games, party games, favourites, children's games, etc).

Items marked _Keep_ are not included in the cache size calculation. The total size of _Keep_ items is listed in the cache details summary.

#### Configure Cache
Clicking the `Configure Cache...` button opens the cache configuration window.

![Achive Cache Manager cache config screen](images/config-cache.png?raw=true "Achive Cache Manager cache config screen")

##### Configure Cache - Cache Path
This is the path of the cache on disk. The path can be absolute or relative, where relative paths are to the `LaunchBox` root folder.

Default: _ArchiveCache_

If the cache path is set to an invalid location (`LaunchBox` root for example), an error message will be displayed when clicking OK.

![Cache path error](images/config-cache-error.png?raw=true "Cache path error")

If the cache path is set to an existing path that already contains files or folders, a warning will be displayed when clicking OK. Click Yes to proceed, or No to go back and change the path.

![Cache path warning](images/config-cache-warning.png?raw=true "Cache path warning")

##### Configure Cache - Cache Size
This is the maximum cache size on disk in megabytes. The oldest played games will be deleted from the cache when it reaches this size.

Default: _20,000 MB (20 GB)_

##### Configure Cache - Minimum Archive Size
This is the minimum size in megabytes of an uncompressed archive before it will be added to the cache. Archives smaller than this won't be added to the cache.

Default: _100 MB_

#### Open In Explorer
Clicking the `Open In Explorer` button will open the configured cache path in Windows Explorer. This button is disabled if the cache path does not exist.

#### Refresh
Refreshes the cache summary and list of cached items from disk.

#### Delete
Clicking on the `Delete` button will remove the selected items from the cache (including items with the _Keep_ setting).

#### Purge Cache
Clicking on the `Delete All` button will delete everything from the cache (including items with the _Keep_ setting).

### Filename Priority
Files from an archive can be prioritized in cases where an emulator requires a certain filename or file type. This option defines the filename and/or extension priority for the specified emulator and platform combination. Use the Add / Edit / Delete buttons to manage priorities.

A wildcard (*) can be used to perform partial filename matches. Examples include:

* *Files ending with bin, then files ending in iso, then all other files:* `bin, iso`

* *Files named eboot.bin, then all other files:* `eboot.bin`

* *GoodMerged style - European 'good' ROM dumps, then USA 'good' ROM dumps, then other region 'good' ROM dumps, then all other files:* `*(*E*)*[!].*, *(*U*)*[!].*, *[!].*`

The priorities are a comma delimited list, where the highest priority is the first entry, the next highest priority is the second entry, and so on. If the filename extension is not found in the archive when a game is loaded, the default LaunchBox priority is used.

Note that the priority is applied to all archives, even if they are not cached.

If a specific emulator \ priority doesn't exist when launching a game, a special _`All` \ `All`_ entry is used to set the global file priority for all files in an archive. Use this for common file priorities, such as disc table-of-contents types (mds, gdi, cue). The _`All` \ `All`_ priority is only applied when specific emulator \ platform priority matches aren't found.

*Note that the _`All` \ `All`_ priority is the basis for the contents of the multi-disc M3U file generation. Be careful removing entries such as `cue`, unless a specific emulator \ platform is used to handle `cue` and similar file types.*

Default:
* _`All` \ `All` \ `mds, gdi, cue, eboot.bin`_
* _`PCSX2` \ `Sony Playstation 2` \ `bin, iso`_

### Additional Options

#### Multi-Disc Support
Check this option to enable multi-disc support. When enabled, the following actions occur when playing a multi-disc game:
* All disc archives from a multi-disc game will be extracted and added to the archive cache.
* M3U files will be generated, with the name based on LaunchBox's Game ID (this is LaunchBox's M3U naming convention).
* The M3U contents will list the absolute path to one cached file per disc, where the file is chosen based on the associated emulator \ platform file priority (see above), or the special _All \ All_ priority.
* If the emulator / platform supports M3U files, the generated M3U file will be used when launching the game.

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


## Acknowledgements
* Icons by Yusuke Kamiyamane. Licensed under a Creative Commons Attribution 3.0 License. https://p.yusukekamiyamane.com/
* INI File Parser library Copyright (c) 2008 Ricardo Amores Hernández. Licensed under the MIT license. https://github.com/rickyah/ini-parser
