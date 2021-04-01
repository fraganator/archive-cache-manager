# Archive Cache Manager change history

## v2.0.7 (2021-03-30)
* Badge to indicate if game is in cache
    * Available under Badges -> Enable Archive Cached menu
* Remember previous selection made in "Select Rom In Archive..." right-click menu
    * Previous selection automatically applied when game started normally

## v2.0.6 (2021-03-26)
* Only remove items from cache path originally extracted by plugin
* Additional checks for invalid cache paths

## v2.0.5 (2021-03-25)
* Fix to ensure cache path valid if config file corrupt

## v2.0.4 (2021-03-24)
* New feature - 'Keep'
    * Keep your favourite games cached and ready to play
    * Games marked 'Keep' will not be removed from the cache, and do not contribute to the used cache size
* Configuration window updates
    * Cache info summary
    * View cached games, toggle the 'Keep' option
    * Manually remove games from the cache or clear it entirely
* Events and errors now logged to `LaunchBox\Plugins\ArchiveCacheManager\Logs`
* Minor bug fixes

## v2.0.3 (2021-03-17)
* Aborting game startup process (`Esc` on Startup Screen) now terminates extract operation
* Cleanup partially extracted archive from cache on 7z error, or previous startup process abort
* Fix archive list error when selecting individual ROM after a previous game launch failure

## v2.0.2 (2021-03-14)
* New feature - Select and play ROM file from archive
    * Right-click a game and click "Select ROM In Archive..."
    * Currently only supports LaunchBox

## v2.0.1 (2021-03-11)
* Support Startup Screen progress bar during initial extraction
* Minor bug fixes

## v2.0.0 (2021-03-10)
* Code now open source under LGPL
* Rewritten to use LaunchBox plugin API
* Added configuration window in Tools menu
* Support for LaunchBox & BigBox 10.x, 11.x

## v1.5 (2018-04-13)
* Add support for LaunchBox.Next

## v1.4 (2018-01-25)
* Add emulator + platform based file priority, for multi-system emulators

## v1.3 (2017-12-24)
* Fix LaunchBox overriding Archive Cache Manager during 7-Zip 16.04 update
* Include 7-Zip version 16.04 with installation

## v1.2 (2017-11-30)
* Support for loading files direct from cache, bypassing LaunchBox temp folder
    * This is now the default behaviour (no hardlinks/junctions)
* Configuration option to force using hardlinks/junctions
* Fix artwork not displaying for game titles containing an apostrophe
* Update to .NET Framework 4.7, inline with LaunchBox

## v1.1 (2017-01-25)
* Show clear logo on loading screen, text title if logo unavailable
* Display cover art in loading screen when region specific art not found
* Configuration option to enable/disable verbose logging
* Configuration option to force file copy from cache to LaunchBox temp folder
* Support non-NTFS volumes
* Support cache stored in network location

## v1.0 (2017-01-03)
* Initial release