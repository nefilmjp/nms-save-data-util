﻿using System.Text.RegularExpressions;

namespace NMSSaveDataUtil.Classes
{
    internal class ListFiles
    {
        public static string[] GetChildren(string path)
        {
            string[] files = Directory.GetFiles(path);
            return files;
        }

        public static string[] GetSelectedFiles(DataGridView grid)
        {
            var rows = grid.Rows.Cast<DataGridViewRow>();
            string[] selectedFiles = rows
                .Where(row => row.Cells[0].Value != null && row.Cells[0].Value.ToString() != "")
                .Select(row => row.Cells[1].Value.ToString()!).ToArray();

            /*
            foreach (var row in rows)
            {
                System.Diagnostics.Debug.WriteLine(row.Cells[0].Value);
            }
            */

            return selectedFiles;
        }

        #region Save

        public static bool IsSave(string path)
        {
            //　accountdata.hg, save.hg, save_000.hg
            if (Regex.IsMatch(Path.GetFileName(path), @"^(save([0-9]+)?|accountdata)\.hg$")) return true;
            return false;
        }

        private static string[] FilterSave(string[] paths)
        {
            return Array.FindAll(paths, IsSave);
        }

        public static string[] GetSaveFiles(string path)
        {
            string[] files = GetChildren(path);
            return FilterSave(files);
        }

        public static void InitSaveGrid(DataGridView grid, Settings settings)
        {
            string path = settings.SaveFolder;
            string[] selectedFilename = settings.SaveFiles;

            grid.Rows.Clear();
            string[] files = GetSaveFiles(path);
            foreach ((string file, int idx) in files.Select((x, i) => (x, i)))
            {
                string filename = Path.GetFileName(file);

                grid.Rows.Add();
                grid.Rows[idx].Cells[1].Value = filename;
                grid.Rows[idx].Cells[2].Value = File.GetLastWriteTime(file);

                if (Array.IndexOf(selectedFilename, filename) >= 0)
                {
                    grid.Rows[idx].Cells[0].Value = true;
                }
            }
            grid.Sort(grid.Columns[1], System.ComponentModel.ListSortDirection.Ascending);
            grid.CurrentCell = null;
        }

        public static string GetSaveIncludes(DataGridView grid)
        {
            string[] selectedFiles = GetSelectedFiles(grid);

            var indexes = selectedFiles.ToList().Select(filename =>
            {
                if (filename == "accountdata.hg")
                {
                    return "a";
                }
                else if (filename == "save.hg")
                {
                    return "1";
                }
                else
                {
                    var res = Regex.Match(filename, "save([0-9]+).hg");
                    return res.Groups[1].Value;
                }
            });

            return String.Join(",", indexes);
        }

        public static string[] GetSelectedSavePaths(DataGridView grid, string dir)
        {
            string[] selectedFiles = GetSelectedFiles(grid);

            List<string> selectedSavefiles = new();

            foreach (string selectedFile in selectedFiles)
            {
                selectedSavefiles.Add($@"{dir}\{selectedFile}");
                selectedSavefiles.Add($@"{dir}\mf_{selectedFile}");
            }

            return selectedSavefiles.ToArray();
        }

        #endregion

        #region Backup

        public static bool IsBackup(string path)
        {
            // yyyyMMddHHmmss_a,1,2,3,4,5,6.7z
            if (Regex.IsMatch(Path.GetFileName(path), @"^[0-9]{14}_(a|[0-9]+)(,[0-9]+){0,}_?(.+)?\.7z$")) return true;
            return false;
        }

        private static string[] FilterBackup(string[] paths)
        {
            return Array.FindAll(paths, IsBackup);
        }

        public static string[] GetBackupFiles(string path)
        {
            string[] files = GetChildren(path);
            return FilterBackup(files);
        }

        public static void InitBackupGrid(DataGridView grid, string path)
        {
            grid.Rows.Clear();
            string[] files = GetBackupFiles(path);
            foreach ((string file, int idx) in files.Select((x, i) => (x, i)))
            {
                string filename = Path.GetFileName(file);
                Match res = Regex.Match(filename, @"^([0-9]{14})_((a|[0-9]+)(,[0-9]+){0,})_?(.+)?\.7z$");

                string included = res.Groups[2].Value;
                string savedDate = res.Groups[1].Value;
                string notes = res.Groups[5].Value;

                grid.Rows.Add();
                grid.Rows[idx].Cells["backupFilenameColumn"].Value = filename;
                grid.Rows[idx].Cells["backupDateColumn"].Value = DateTime.ParseExact(savedDate, "yyyyMMddHHmmss", null);
                grid.Rows[idx].Cells["backupIncludedFilesColumn"].Value = included;
                grid.Rows[idx].Cells["backupSavedDateColumn"].Value = savedDate;
                grid.Rows[idx].Cells["backupNotesColumn"].Value = notes;
            }
            grid.Sort(grid.Columns[2], System.ComponentModel.ListSortDirection.Descending);
            grid.CurrentCell = null;
        }

        #endregion
    }
}
