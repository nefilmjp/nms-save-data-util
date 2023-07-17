using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAPICodePack.Dialogs;

namespace NMSSaveDataUtil.Classes
{
    internal class FolderDialog
    {
        public static string SelectSavePath(string currentDir)
        {
            string dir = currentDir == ""
                ? @$"{System.Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}\HelloGames\NMS"
                : currentDir;
            string res = Open("Select Save Folder", dir);
            return res;
        }

        public static string SelectBackupPath(string currentDir)
        {
            string dir = currentDir == ""
                ? System.Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)
                : currentDir;
            string res = Open("Select Backup Folder", dir);
            return res;
        }

        private static string Open(string title, string initialDir)
        {
            CommonOpenFileDialog d = new()
            {
                Title = title,
                IsFolderPicker = true,
                InitialDirectory = initialDir,
                DefaultDirectory = @"C:\",
            };

            CommonFileDialogResult res = d.ShowDialog();

            return (res == CommonFileDialogResult.Ok) ? d.FileName : "";
        }
    }
}
