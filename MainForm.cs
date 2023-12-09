using NMSSaveDataUtil.Classes;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;
using System.Diagnostics;
using Microsoft.VisualBasic.FileIO;
using System.Text.RegularExpressions;
using System.Drawing.Text;
using System.Runtime.InteropServices;

namespace NMSSaveDataUtil
{
    public partial class MainForm : Form, INotifyPropertyChanged
    {
        private Settings settings = Settings.Load();
        private readonly MoveCamera? camera;
        private readonly Portal? portal;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler? PropertyChanged;

        #endregion

        public MainForm()
        {
            InitializeComponent();

            PrivateFontCollection pfc = new();
            LocalFont.AddFontFromResource(pfc, Properties.Resources.NMS_Glyphs_Mono_fix);
            Font glyphFont = new(pfc.Families[0], 32);
            portalGlyphsLabel.Font = glyphFont;

            saveFolderTextBox.DataBindings.Add(new Binding("Text", settings, "SaveFolder", true));
            backupFolderTextBox.DataBindings.Add(new Binding("Text", settings, "BackupFolder", true));

            enableCameraCheckBox.DataBindings.Add(new Binding("Checked", settings, "EnableCamera", true));

            cameraMoveSpeedNumericUpDown.DataBindings.Add(new Binding("Value", settings, "CameraMoveSpeed", true));
            cameraRotateDelayNumericUpDown.DataBindings.Add(new Binding("Value", settings, "CameraRotateDelay", true));
            cameraRotateSpeedNumericUpDown.DataBindings.Add(new Binding("Value", settings, "CameraRotateSpeed", true));
            cameraDurationNumericUpDown.DataBindings.Add(new Binding("Value", settings, "CameraDuration", true));

            // enablePortalCheckBox.DataBindings.Add(new Binding("Checked", settings, "EnablePortal", true));

            if (settings.EnableCamera)
            {
                camera = new MoveCamera();
            }
            else
            {
                tabControl.TabPages.Remove(cameraTabPage);
            }

            if (settings.EnablePortal)
            {
                portal = new Portal();
            }
            else
            {
                tabControl.TabPages.Remove(portalTabPage);
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            Cache.CreateTempDir();

            if (settings.WinSize.Width == 0 || settings.WinSize.Height == 0)
            {
                // Set default values if needed
            }
            else
            {
                WindowState = settings.WinState;

                if (WindowState == FormWindowState.Minimized) WindowState = FormWindowState.Normal;

                Location = settings.WinLocation;
                Size = settings.WinSize;
            }

            if (settings.SaveFolder == "" || settings.BackupFolder == "")
            {
                MessageBox.Show(
                    "Please set up folders in the settings tab.",
                    "Warning",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
            }

            saveFileSystemWatcher.Changed += new FileSystemEventHandler(UpdateSaveGrid);
            saveFileSystemWatcher.Created += new FileSystemEventHandler(UpdateSaveGrid);
            saveFileSystemWatcher.Deleted += new FileSystemEventHandler(UpdateSaveGrid);
            saveFileSystemWatcher.Renamed += new RenamedEventHandler(UpdateSaveGrid);

            backupFileSystemWatcher.Changed += new FileSystemEventHandler(UpdateBackupGrid);
            backupFileSystemWatcher.Created += new FileSystemEventHandler(UpdateBackupGrid);
            backupFileSystemWatcher.Deleted += new FileSystemEventHandler(UpdateBackupGrid);
            backupFileSystemWatcher.Renamed += new RenamedEventHandler(UpdateBackupGrid);

            // Init DataGridView
            if (settings.SaveFolder != "")
            {
                LockSaveFiles.Stop(settings);

                ListFiles.InitSaveGrid(saveDataGridView, settings);
                saveFileSystemWatcher.Path = settings.SaveFolder;
                saveFileSystemWatcher.EnableRaisingEvents = true;
            }

            if (settings.BackupFolder != "")
            {
                ListFiles.InitBackupGrid(backupDataGridView, settings.BackupFolder);
                backupFileSystemWatcher.Path = settings.BackupFolder;
                backupFileSystemWatcher.EnableRaisingEvents = true;
            }

            LockSaveFiles.Init(settings);
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Cache.DeleteTempDir();

            if (settings.SaveFolder != "") LockSaveFiles.Stop(settings);

            settings.SaveFolder = settings.SaveFolder;
            settings.BackupFolder = settings.BackupFolder;

            if (settings.SaveFolder != "")
            {
                // saveDataGridView.CurrentCell = null;
                settings.BackupTargets = ListFiles.GetSelectedSaveFiles(saveDataGridView);
            }

            settings.WinState = WindowState;
            if (WindowState == FormWindowState.Normal)
            {
                settings.WinLocation = Location;
                settings.WinSize = Size;
            }
            else
            {
                settings.WinLocation = RestoreBounds.Location;
                settings.WinSize = RestoreBounds.Size;
            }

            Settings.Save(settings);
        }

        private void LockButton_CheckedChanged(object sender, EventArgs e)
        {
            if (settings.SaveFolder == "") return;
            if (lockButton.Checked)
            {
                lockButton.Text = "Unlock";
                lockButton.Image = Properties.Resources.Unlock;
                LockSaveFiles.Start(settings);
            }
            else
            {
                lockButton.Text = "Lock";
                lockButton.Image = Properties.Resources.Lock;
                LockSaveFiles.Stop(settings);
            }
            saveDataGridView.Refresh();
        }

        private void BackupButton_Click(object sender, EventArgs e)
        {
            if (settings.SaveFolder == "" || settings.BackupFolder == "") return;

            saveDataGridView.CurrentCell = null; // Reflect current state
            string[] sourcePaths = ListFiles.GetSelectedSavePaths(saveDataGridView, settings.SaveFolder);

            if (sourcePaths.Length == 0) return;

            string[] cachePaths = Cache.Create(sourcePaths);

            string date = DateTime.Now.ToString("yyyyMMddHHmmss");
            string includes = ListFiles.GetSaveIncludes(saveDataGridView);

            Archiver.Compress(@$"{settings.BackupFolder}\{date}_{includes}.7z", cachePaths);
        }

        private void MoveCameraButton_Click(object sender, EventArgs e)
        {
            short move = (short)cameraMoveSpeedNumericUpDown.Value;
            int delay = (int)cameraRotateDelayNumericUpDown.Value;
            short rotate = (short)cameraRotateSpeedNumericUpDown.Value;
            int duration = (int)cameraDurationNumericUpDown.Value;

            camera?.Start(move, delay, rotate, duration);
        }

        private void UpdateSaveGrid(System.Object source, System.IO.FileSystemEventArgs e)
        {
            ListFiles.InitSaveGrid(saveDataGridView, settings);
        }

        private void SaveDataGridView_Paint(object sender, PaintEventArgs e)
        {
            Debug.WriteLine("SaveDataGridView_Paint");

            if (saveDataGridView.ColumnHeadersHeight + saveDataGridView.Rows.GetRowsHeight(DataGridViewElementStates.Visible) + 2 > splitContainer.Height)
            {
                splitContainer.SplitterDistance = saveDataGridView.Columns.GetColumnsWidth(DataGridViewElementStates.Visible) + 3 + 18;
            }
            else
            {
                splitContainer.SplitterDistance = saveDataGridView.Columns.GetColumnsWidth(DataGridViewElementStates.Visible) + 3;
            }
        }

        private void SaveDataGridView_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0) { return; }

            DataGridView dgv = (DataGridView)sender;

            if (e.ColumnIndex == 1)
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All);

                var w = Properties.Resources.Return.Width;
                var h = Properties.Resources.Return.Height;
                var x = e.CellBounds.Left + (e.CellBounds.Width - w) / 2;
                var y = e.CellBounds.Top + (e.CellBounds.Height - h) / 2;

                switch (settings.GetFileSetting(dgv.Rows[e.RowIndex].Cells["saveFilenameColumn"].Value.ToString()!))
                {
                    case "thru":
                        // e.Graphics.DrawImage(Properties.Resources.HiddenFile, new Rectangle(x, y, w, h));
                        break;
                    case "lock":
                        e.Graphics.DrawImage(Properties.Resources.MapPublic, new Rectangle(x, y, w, h));
                        break;
                    case "ctrl":
                        if (lockButton.Checked == true)
                        {
                            e.Graphics.DrawImage(Properties.Resources.Lock, new Rectangle(x, y, w, h));
                        }
                        else
                        {
                            e.Graphics.DrawImage(Properties.Resources.Unlock, new Rectangle(x, y, w, h));
                        }
                        break;
                    default:
                        break;
                }
                e.Handled = true;
            }
        }

        private void SaveDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) { return; }

            DataGridView dgv = (DataGridView)sender;

            // Update checkbox
            if (e.ColumnIndex == 0)
            {
                saveDataGridView.CommitEdit(DataGridViewDataErrorContexts.Commit);
                settings.BackupTargets = ListFiles.GetSelectedSaveFiles(saveDataGridView);
            }

            if (e.ColumnIndex == 1)
            {
                Rectangle rect = dgv.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false);
                saveContextMenuStrip.Show(dgv, new System.Drawing.Point(rect.X + 32, rect.Y));
            }
        }

        private void UpdateBackupGrid(System.Object source, System.IO.FileSystemEventArgs e)
        {
            ListFiles.InitBackupGrid(backupDataGridView, settings.BackupFolder);
        }

        private void BackupDataGridView_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            if (e.ColumnIndex == 1)
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All);

                var w = Properties.Resources.Return.Width;
                var h = Properties.Resources.Return.Height;
                var x = e.CellBounds.Left + (e.CellBounds.Width - w) / 2;
                var y = e.CellBounds.Top + (e.CellBounds.Height - h) / 2;

                e.Graphics.DrawImage(Properties.Resources.Return, new Rectangle(x, y, w, h));
                e.Handled = true;
            }

            if (e.ColumnIndex == 7)
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All);

                var w = Properties.Resources.Delete.Width;
                var h = Properties.Resources.Delete.Height;
                var x = e.CellBounds.Left + (e.CellBounds.Width - w) / 2;
                var y = e.CellBounds.Top + (e.CellBounds.Height - h) / 2;

                e.Graphics.DrawImage(Properties.Resources.Delete, new Rectangle(x, y, w, h));
                e.Handled = true;
            }
        }

        private void BackupDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (settings.SaveFolder == "" || settings.BackupFolder == "") { return; }

            if (e.RowIndex < 0) { return; }

            DataGridView dgv = (DataGridView)sender;

            string filename = dgv.Rows[e.RowIndex].Cells["backupFilenameColumn"].Value.ToString()!;
            string filepath = @$"{settings.BackupFolder}\{filename}";

            // Restore
            if (dgv.Columns[e.ColumnIndex].Name == "backupRestoreColumn")
            {
                Archiver.Decompress(filepath, settings.SaveFolder);
            }

            // Delete
            if (dgv.Columns[e.ColumnIndex].Name == "backupDeleteColumn")
            {
                // File.Delete(filepath);
                FileSystem.DeleteFile(filepath, UIOption.OnlyErrorDialogs, RecycleOption.SendToRecycleBin);
            }
        }

        private void BackupDataGridView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) { return; }

            DataGridView dgv = (DataGridView)sender;

            string oldFilename = dgv.Rows[e.RowIndex].Cells["backupFilenameColumn"].Value.ToString()!;
            string date = dgv.Rows[e.RowIndex].Cells["backupSavedDateColumn"].Value.ToString()!;
            string included = dgv.Rows[e.RowIndex].Cells["backupIncludedFilesColumn"].Value.ToString()!;
            string notes = dgv.Rows[e.RowIndex].Cells["backupNotesColumn"].Value is null
                ? ""
                : dgv.Rows[e.RowIndex].Cells["backupNotesColumn"].Value.ToString()!;

            char[] invalidChars = System.IO.Path.GetInvalidFileNameChars();
            string invalidCharsString = new(invalidChars);
            string removePattern = string.Format("[{0}]", Regex.Escape(invalidCharsString));

            string validNotes = Regex.Replace(notes, removePattern, "");
            dgv.Rows[e.RowIndex].Cells["backupNotesColumn"].Value = validNotes;

            string newFilename = notes == "" ? $"{date}_{included}.7z" : $"{date}_{included}_{validNotes}.7z";

            File.Move(@$"{settings.BackupFolder}\{oldFilename}", @$"{settings.BackupFolder}\{newFilename}");
        }

        private void SaveFolderButton_Click(object sender, EventArgs e)
        {
            string res = FolderDialog.SelectSavePath(settings.SaveFolder);
            if (res != "")
            {
                saveFileSystemWatcher.EnableRaisingEvents = false;
                settings.SaveFolder = res;
                ListFiles.InitSaveGrid(saveDataGridView, settings);
                saveFileSystemWatcher.Path = settings.SaveFolder;
                saveFileSystemWatcher.EnableRaisingEvents = true;
            }
        }

        private void BackupFolderButton_Click(object sender, EventArgs e)
        {
            string res = FolderDialog.SelectBackupPath(settings.BackupFolder);
            if (res != "")
            {
                saveFileSystemWatcher.EnableRaisingEvents = false;
                settings.BackupFolder = res;
                ListFiles.InitBackupGrid(backupDataGridView, settings.BackupFolder);
                saveFileSystemWatcher.Path = settings.BackupFolder;
                saveFileSystemWatcher.EnableRaisingEvents = true;
            }
        }

        private void PortalAddressTextBox_TextChanged(object sender, EventArgs e)
        {
            string address = portalAddressTextBox.Text;

            portalGlyphsLabel.Text = address;
            if (address.Length == 12 && Regex.IsMatch(address, "[0-9A-F]{12}"))
            {
                sendPortalAddressButton.Enabled = true;
            }
            else
            {
                sendPortalAddressButton.Enabled = false;
            }
        }

        private void SendPortalAddressButton_Click(object sender, EventArgs e)
        {
            string address = portalAddressTextBox.Text;
            portal?.SendAddress(address);
        }

        private void ThruToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataGridViewCell cell = saveDataGridView.SelectedCells[0];
            string filename = saveDataGridView.Rows[cell.RowIndex].Cells["saveFilenameColumn"].Value.ToString()!;
            settings.SetFileSetting(filename, "thru");
            LockSaveFiles.Unlockfile(@$"{settings.SaveFolder}\{filename}");
            LockSaveFiles.Unlockfile(@$"{settings.SaveFolder}\mf_{filename}");
            saveDataGridView.CurrentCell = null;
        }

        private void CtrlToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataGridViewCell cell = saveDataGridView.SelectedCells[0];
            string filename = saveDataGridView.Rows[cell.RowIndex].Cells["saveFilenameColumn"].Value.ToString()!;
            settings.SetFileSetting(filename, "ctrl");
            if (lockButton.Checked)
            {
                LockSaveFiles.Lockfile(@$"{settings.SaveFolder}\{filename}");
                LockSaveFiles.Lockfile(@$"{settings.SaveFolder}\mf_{filename}");

            }
            else
            {
                LockSaveFiles.Unlockfile(@$"{settings.SaveFolder}\{filename}");
                LockSaveFiles.Unlockfile(@$"{settings.SaveFolder}\mf_{filename}");
            }
            saveDataGridView.CurrentCell = null;
        }

        private void LockToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataGridViewCell cell = saveDataGridView.SelectedCells[0];
            string filename = saveDataGridView.Rows[cell.RowIndex].Cells["saveFilenameColumn"].Value.ToString()!;
            settings.SetFileSetting(filename, "lock");
            LockSaveFiles.Lockfile(@$"{settings.SaveFolder}\{filename}");
            LockSaveFiles.Lockfile(@$"{settings.SaveFolder}\mf_{filename}");
            saveDataGridView.CurrentCell = null;
        }
    }
}
