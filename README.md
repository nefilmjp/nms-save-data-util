# No Man's Sky Save Data Utility

The desktop application for Windows to backup/restore/lock/unlock save files.

![image](https://github.com/nefilmjp/nms-save-data-util/assets/136662366/5434ff64-c67c-4404-a543-c16dc68ebb04)

- Currently unstable
- For Windows only
  - Requires [.NET Desktop Runtime 6.0](https://dotnet.microsoft.com/download/dotnet/6.0)
- Installation: Unzip and place files
- Uninstallation: Delete files
- Reset: Empty lock targets, exit the app and delete `Settings.xml`
  - The read-only flag can be changed from the Explorer context menu
- The lock/unlock feature is for ignoring auto-save
- Backups are compressed with 7-Zip
- `accountdata.hg` contains common data for the account
- The rules for file names of game data are as follows:
  - `save.hg` : Slot1/Autosave
  - `save2.hg` : Slot1/Manual Save
  - `save3.hg` : Slot2/Autosave
  - `save4.hg` : Slot2/Manual Save
<!--
- Requires [7-Zip Extra](https://7-zip.org/download.html) ( `7za.exe` to be placed in the same location as the executable)
-->
