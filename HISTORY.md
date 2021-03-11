# Archive Cache Manager change history

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