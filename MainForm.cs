using NMSSaveDataUtil.Classes;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;
using System.Diagnostics;
using Microsoft.VisualBasic.FileIO;

namespace NMSSaveDataUtil
{
    public partial class MainForm : Form, INotifyPropertyChanged
    {
        private Settings settings = Settings.Load();
        private readonly MoveCamera? camera;

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

            saveFolderTextBox.DataBindings.Add(new Binding("Text", settings, "SaveFolder", true));
            backupFolderTextBox.DataBindings.Add(new Binding("Text", settings, "BackupFolder", true));

            deleteAutosaveCheckBox.DataBindings.Add(new Binding("Checked", settings, "DeleteAutosave", true));
            disableAutosaveCheckBox.DataBindings.Add(new Binding("Checked", settings, "DisableAutosave", true));
            enableCameraCheckBox.DataBindings.Add(new Binding("Checked", settings, "EnableCamera", true));

            cameraMoveSpeedNumericUpDown.DataBindings.Add(new Binding("Value", settings, "CameraMoveSpeed", true));
            cameraRotateDelayNumericUpDown.DataBindings.Add(new Binding("Value", settings, "CameraRotateDelay", true));
            cameraRotateSpeedNumericUpDown.DataBindings.Add(new Binding("Value", settings, "CameraRotateSpeed", true));
            cameraDurationNumericUpDown.DataBindings.Add(new Binding("Value", settings, "CameraDuration", true));

            if (settings.EnableCamera)
            {
                camera = new MoveCamera();
            }
            else
            {
                tabControl.TabPages.Remove(cameraTabPage);
            }
        }

        private void LockButton_CheckedChanged(object sender, EventArgs e)
        {
            if (settings.SaveFolder == "") return;
            if (lockButton.Checked)
            {
                lockButton.Text = "Unlock Save Files";
                lockButton.Image = Properties.Resources.Unlock;
                LockSaveFiles.Start(settings.SaveFolder);
            }
            else
            {
                lockButton.Text = "Lock Save Files";
                lockButton.Image = Properties.Resources.Lock;
                LockSaveFiles.Stop(settings.SaveFolder);
            }
        }

        private void BackupButton_Click(object sender, EventArgs e)
        {
            if (settings.SaveFolder == "" || settings.BackupFolder == "") return;

            saveDataGridView.CurrentCell = null; // Reflect current state
            string[] paths = ListFiles.GetSelectedSavePaths(saveDataGridView, settings.SaveFolder);

            if (paths.Length == 0) return;

            string date = DateTime.Now.ToString("yyyyMMddHHmmss");
            string includes = ListFiles.GetSaveIncludes(saveDataGridView);

            Archiver.Compress(@$"{settings.BackupFolder}\{date}_{includes}.7z", paths);
        }

        private void UpdateSaveGrid(System.Object source, System.IO.FileSystemEventArgs e)
        {
            ListFiles.InitSaveGrid(saveDataGridView, settings);
        }

        private void UpdateBackupGrid(System.Object source, System.IO.FileSystemEventArgs e)
        {
            ListFiles.InitBackupGrid(backupDataGridView, settings.BackupFolder);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
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
                LockSaveFiles.Stop(settings.SaveFolder);

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
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (settings.SaveFolder != "") LockSaveFiles.Stop(settings.SaveFolder);

            settings.SaveFolder = settings.SaveFolder;
            settings.BackupFolder = settings.BackupFolder;

            if (settings.SaveFolder != "")
            {
                // saveDataGridView.CurrentCell = null;
                settings.SaveFiles = ListFiles.GetSelectedFiles(saveDataGridView);
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

        private void MoveCameraButton_Click(object sender, EventArgs e)
        {
            short move = (short)cameraMoveSpeedNumericUpDown.Value;
            int delay = (int)cameraRotateDelayNumericUpDown.Value;
            short rotate = (short)cameraRotateSpeedNumericUpDown.Value;
            int duration = (int)cameraDurationNumericUpDown.Value;

            camera?.Start(move, delay, rotate, duration);
        }

        private void BackupDataGridView_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            if (e.ColumnIndex == 4)
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All);

                var w = Properties.Resources.Return.Width;
                var h = Properties.Resources.Return.Height;
                var x = e.CellBounds.Left + (e.CellBounds.Width - w) / 2;
                var y = e.CellBounds.Top + (e.CellBounds.Height - h) / 2;

                e.Graphics.DrawImage(Properties.Resources.Return, new Rectangle(x, y, w, h));
                e.Handled = true;
            }

            if (e.ColumnIndex == 5)
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

        private void BackupDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (settings.SaveFolder == "" || settings.BackupFolder == "") { return; }

            if (e.RowIndex < 0) { return; }

            DataGridView dgv = (DataGridView)sender;

            string filename = dgv.Rows[e.RowIndex].Cells[1].Value.ToString()!;
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
    }
}