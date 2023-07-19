namespace NMSSaveDataUtil
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            toolStripContainer = new ToolStripContainer();
            tabControl = new TabControl();
            backupTabPage = new TabPage();
            splitContainer = new SplitContainer();
            saveDataGridView = new DataGridView();
            saveSelectColumn = new DataGridViewCheckBoxColumn();
            saveFilenameColumn = new DataGridViewTextBoxColumn();
            saveDateColumn = new DataGridViewTextBoxColumn();
            saveLockColumn = new DataGridViewButtonColumn();
            backupDataGridView = new DataGridView();
            backupSelectColumn = new DataGridViewCheckBoxColumn();
            backupFilenameColumn = new DataGridViewTextBoxColumn();
            backupDateColumn = new DataGridViewTextBoxColumn();
            backupIncludedFilesColumn = new DataGridViewTextBoxColumn();
            backupRestoreColumn = new DataGridViewButtonColumn();
            backupDeleteColumn = new DataGridViewButtonColumn();
            settingsTabPage = new TabPage();
            enableCameraCheckBox = new CheckBox();
            disableAutosaveCheckBox = new CheckBox();
            deleteAutosaveCheckBox = new CheckBox();
            backupFolderTextBox = new TextBox();
            saveFolderTextBox = new TextBox();
            backupFolderButton = new Button();
            backupFolderLabel = new Label();
            saveFolderButton = new Button();
            saveFolderLabel = new Label();
            cameraTabPage = new TabPage();
            moveCameraButton = new Button();
            cameraMoveSpeedLabel = new Label();
            cameraMoveSpeedNumericUpDown = new NumericUpDown();
            label1 = new Label();
            cameraRotateDelayLabel = new Label();
            cameraRotateDelayNumericUpDown = new NumericUpDown();
            label2 = new Label();
            cameraRotateSpeedLabel = new Label();
            cameraRotateSpeedNumericUpDown = new NumericUpDown();
            label3 = new Label();
            cameraDurationLabel = new Label();
            cameraDurationNumericUpDown = new NumericUpDown();
            label4 = new Label();
            portalTabPage = new TabPage();
            portalGlyphsLabel = new Label();
            label5 = new Label();
            sendPortalAddressButton = new Button();
            portalAddressTextBox = new TextBox();
            toolStrip = new ToolStrip();
            lockButton = new ToolStripButton();
            backupButton = new ToolStripButton();
            saveFileSystemWatcher = new FileSystemWatcher();
            backupFileSystemWatcher = new FileSystemWatcher();
            toolStripContainer.ContentPanel.SuspendLayout();
            toolStripContainer.TopToolStripPanel.SuspendLayout();
            toolStripContainer.SuspendLayout();
            tabControl.SuspendLayout();
            backupTabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer).BeginInit();
            splitContainer.Panel1.SuspendLayout();
            splitContainer.Panel2.SuspendLayout();
            splitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)saveDataGridView).BeginInit();
            ((System.ComponentModel.ISupportInitialize)backupDataGridView).BeginInit();
            settingsTabPage.SuspendLayout();
            cameraTabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)cameraMoveSpeedNumericUpDown).BeginInit();
            ((System.ComponentModel.ISupportInitialize)cameraRotateDelayNumericUpDown).BeginInit();
            ((System.ComponentModel.ISupportInitialize)cameraRotateSpeedNumericUpDown).BeginInit();
            ((System.ComponentModel.ISupportInitialize)cameraDurationNumericUpDown).BeginInit();
            portalTabPage.SuspendLayout();
            toolStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)saveFileSystemWatcher).BeginInit();
            ((System.ComponentModel.ISupportInitialize)backupFileSystemWatcher).BeginInit();
            SuspendLayout();
            // 
            // toolStripContainer
            // 
            // 
            // toolStripContainer.ContentPanel
            // 
            toolStripContainer.ContentPanel.Controls.Add(tabControl);
            toolStripContainer.ContentPanel.Size = new Size(624, 416);
            toolStripContainer.Dock = DockStyle.Fill;
            toolStripContainer.Location = new Point(0, 0);
            toolStripContainer.Name = "toolStripContainer";
            toolStripContainer.Size = new Size(624, 441);
            toolStripContainer.TabIndex = 0;
            toolStripContainer.Text = "toolStripContainer1";
            // 
            // toolStripContainer.TopToolStripPanel
            // 
            toolStripContainer.TopToolStripPanel.Controls.Add(toolStrip);
            // 
            // tabControl
            // 
            tabControl.Controls.Add(backupTabPage);
            tabControl.Controls.Add(settingsTabPage);
            tabControl.Controls.Add(cameraTabPage);
            tabControl.Controls.Add(portalTabPage);
            tabControl.Dock = DockStyle.Fill;
            tabControl.Location = new Point(0, 0);
            tabControl.Name = "tabControl";
            tabControl.SelectedIndex = 0;
            tabControl.Size = new Size(624, 416);
            tabControl.TabIndex = 0;
            // 
            // backupTabPage
            // 
            backupTabPage.Controls.Add(splitContainer);
            backupTabPage.Location = new Point(4, 24);
            backupTabPage.Name = "backupTabPage";
            backupTabPage.Padding = new Padding(3);
            backupTabPage.Size = new Size(616, 388);
            backupTabPage.TabIndex = 0;
            backupTabPage.Text = "Backup";
            backupTabPage.UseVisualStyleBackColor = true;
            // 
            // splitContainer
            // 
            splitContainer.Dock = DockStyle.Fill;
            splitContainer.FixedPanel = FixedPanel.Panel1;
            splitContainer.IsSplitterFixed = true;
            splitContainer.Location = new Point(3, 3);
            splitContainer.Name = "splitContainer";
            // 
            // splitContainer.Panel1
            // 
            splitContainer.Panel1.Controls.Add(saveDataGridView);
            // 
            // splitContainer.Panel2
            // 
            splitContainer.Panel2.Controls.Add(backupDataGridView);
            splitContainer.Size = new Size(610, 382);
            splitContainer.SplitterDistance = 250;
            splitContainer.TabIndex = 0;
            // 
            // saveDataGridView
            // 
            saveDataGridView.AllowUserToAddRows = false;
            saveDataGridView.AllowUserToDeleteRows = false;
            saveDataGridView.AllowUserToResizeRows = false;
            saveDataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            saveDataGridView.Columns.AddRange(new DataGridViewColumn[] { saveSelectColumn, saveFilenameColumn, saveDateColumn, saveLockColumn });
            saveDataGridView.Dock = DockStyle.Fill;
            saveDataGridView.Location = new Point(0, 0);
            saveDataGridView.Name = "saveDataGridView";
            saveDataGridView.RowHeadersVisible = false;
            saveDataGridView.RowTemplate.Height = 25;
            saveDataGridView.ShowCellToolTips = false;
            saveDataGridView.Size = new Size(250, 382);
            saveDataGridView.TabIndex = 0;
            saveDataGridView.Paint += SaveDataGridView_Paint;
            // 
            // saveSelectColumn
            // 
            saveSelectColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;
            saveSelectColumn.FalseValue = "";
            saveSelectColumn.HeaderText = "";
            saveSelectColumn.Name = "saveSelectColumn";
            saveSelectColumn.ToolTipText = "Backup target";
            saveSelectColumn.Width = 5;
            // 
            // saveFilenameColumn
            // 
            saveFilenameColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            saveFilenameColumn.HeaderText = "Filename";
            saveFilenameColumn.Name = "saveFilenameColumn";
            saveFilenameColumn.ReadOnly = true;
            saveFilenameColumn.Width = 82;
            // 
            // saveDateColumn
            // 
            saveDateColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            saveDateColumn.HeaderText = "Date";
            saveDateColumn.Name = "saveDateColumn";
            saveDateColumn.ReadOnly = true;
            saveDateColumn.Width = 58;
            // 
            // saveLockColumn
            // 
            saveLockColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            saveLockColumn.HeaderText = "";
            saveLockColumn.Name = "saveLockColumn";
            saveLockColumn.Visible = false;
            // 
            // backupDataGridView
            // 
            backupDataGridView.AllowUserToAddRows = false;
            backupDataGridView.AllowUserToDeleteRows = false;
            backupDataGridView.AllowUserToResizeRows = false;
            backupDataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            backupDataGridView.Columns.AddRange(new DataGridViewColumn[] { backupSelectColumn, backupFilenameColumn, backupDateColumn, backupIncludedFilesColumn, backupRestoreColumn, backupDeleteColumn });
            backupDataGridView.Dock = DockStyle.Fill;
            backupDataGridView.Location = new Point(0, 0);
            backupDataGridView.Name = "backupDataGridView";
            backupDataGridView.RowHeadersVisible = false;
            backupDataGridView.RowTemplate.Height = 25;
            backupDataGridView.ShowCellToolTips = false;
            backupDataGridView.Size = new Size(356, 382);
            backupDataGridView.TabIndex = 0;
            backupDataGridView.CellContentClick += BackupDataGridView_CellContentClick;
            backupDataGridView.CellPainting += BackupDataGridView_CellPainting;
            // 
            // backupSelectColumn
            // 
            backupSelectColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;
            backupSelectColumn.HeaderText = "";
            backupSelectColumn.Name = "backupSelectColumn";
            backupSelectColumn.ToolTipText = "Select";
            backupSelectColumn.Visible = false;
            // 
            // backupFilenameColumn
            // 
            backupFilenameColumn.HeaderText = "Filename";
            backupFilenameColumn.Name = "backupFilenameColumn";
            backupFilenameColumn.Visible = false;
            // 
            // backupDateColumn
            // 
            backupDateColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            backupDateColumn.HeaderText = "Date";
            backupDateColumn.Name = "backupDateColumn";
            backupDateColumn.Width = 58;
            // 
            // backupIncludedFilesColumn
            // 
            backupIncludedFilesColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            backupIncludedFilesColumn.HeaderText = "Included";
            backupIncludedFilesColumn.Name = "backupIncludedFilesColumn";
            backupIncludedFilesColumn.Width = 78;
            // 
            // backupRestoreColumn
            // 
            backupRestoreColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            backupRestoreColumn.HeaderText = "Restore";
            backupRestoreColumn.Name = "backupRestoreColumn";
            backupRestoreColumn.Width = 58;
            // 
            // backupDeleteColumn
            // 
            backupDeleteColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            backupDeleteColumn.HeaderText = "Delete";
            backupDeleteColumn.Name = "backupDeleteColumn";
            backupDeleteColumn.Width = 50;
            // 
            // settingsTabPage
            // 
            settingsTabPage.Controls.Add(enableCameraCheckBox);
            settingsTabPage.Controls.Add(disableAutosaveCheckBox);
            settingsTabPage.Controls.Add(deleteAutosaveCheckBox);
            settingsTabPage.Controls.Add(backupFolderTextBox);
            settingsTabPage.Controls.Add(saveFolderTextBox);
            settingsTabPage.Controls.Add(backupFolderButton);
            settingsTabPage.Controls.Add(backupFolderLabel);
            settingsTabPage.Controls.Add(saveFolderButton);
            settingsTabPage.Controls.Add(saveFolderLabel);
            settingsTabPage.Location = new Point(4, 24);
            settingsTabPage.Name = "settingsTabPage";
            settingsTabPage.Padding = new Padding(12);
            settingsTabPage.Size = new Size(616, 388);
            settingsTabPage.TabIndex = 2;
            settingsTabPage.Text = "Settings";
            settingsTabPage.UseVisualStyleBackColor = true;
            // 
            // enableCameraCheckBox
            // 
            enableCameraCheckBox.AutoSize = true;
            enableCameraCheckBox.Location = new Point(15, 121);
            enableCameraCheckBox.Margin = new Padding(3, 12, 3, 3);
            enableCameraCheckBox.Name = "enableCameraCheckBox";
            enableCameraCheckBox.Size = new Size(264, 19);
            enableCameraCheckBox.TabIndex = 10;
            enableCameraCheckBox.Text = "Enable camera control (reboot is required)";
            enableCameraCheckBox.UseVisualStyleBackColor = true;
            // 
            // disableAutosaveCheckBox
            // 
            disableAutosaveCheckBox.AutoSize = true;
            disableAutosaveCheckBox.Location = new Point(15, 183);
            disableAutosaveCheckBox.Margin = new Padding(3, 9, 3, 3);
            disableAutosaveCheckBox.Name = "disableAutosaveCheckBox";
            disableAutosaveCheckBox.Size = new Size(119, 19);
            disableAutosaveCheckBox.TabIndex = 9;
            disableAutosaveCheckBox.Text = "Disable autosave";
            disableAutosaveCheckBox.UseVisualStyleBackColor = true;
            disableAutosaveCheckBox.Visible = false;
            // 
            // deleteAutosaveCheckBox
            // 
            deleteAutosaveCheckBox.AutoSize = true;
            deleteAutosaveCheckBox.Location = new Point(15, 152);
            deleteAutosaveCheckBox.Margin = new Padding(3, 9, 3, 3);
            deleteAutosaveCheckBox.Name = "deleteAutosaveCheckBox";
            deleteAutosaveCheckBox.Size = new Size(232, 19);
            deleteAutosaveCheckBox.TabIndex = 8;
            deleteAutosaveCheckBox.Text = "Delete autosave data when restoring";
            deleteAutosaveCheckBox.UseVisualStyleBackColor = true;
            deleteAutosaveCheckBox.Visible = false;
            // 
            // backupFolderTextBox
            // 
            backupFolderTextBox.Location = new Point(15, 83);
            backupFolderTextBox.Name = "backupFolderTextBox";
            backupFolderTextBox.ReadOnly = true;
            backupFolderTextBox.Size = new Size(320, 23);
            backupFolderTextBox.TabIndex = 7;
            // 
            // saveFolderTextBox
            // 
            saveFolderTextBox.Location = new Point(15, 30);
            saveFolderTextBox.Name = "saveFolderTextBox";
            saveFolderTextBox.ReadOnly = true;
            saveFolderTextBox.Size = new Size(320, 23);
            saveFolderTextBox.TabIndex = 6;
            // 
            // backupFolderButton
            // 
            backupFolderButton.Location = new Point(341, 83);
            backupFolderButton.Name = "backupFolderButton";
            backupFolderButton.Size = new Size(75, 23);
            backupFolderButton.TabIndex = 5;
            backupFolderButton.Text = "Change";
            backupFolderButton.UseVisualStyleBackColor = true;
            backupFolderButton.Click += BackupFolderButton_Click;
            // 
            // backupFolderLabel
            // 
            backupFolderLabel.AutoSize = true;
            backupFolderLabel.Location = new Point(15, 65);
            backupFolderLabel.Margin = new Padding(3, 9, 3, 0);
            backupFolderLabel.Name = "backupFolderLabel";
            backupFolderLabel.Size = new Size(83, 15);
            backupFolderLabel.TabIndex = 3;
            backupFolderLabel.Text = "Backup folder";
            // 
            // saveFolderButton
            // 
            saveFolderButton.Location = new Point(341, 30);
            saveFolderButton.Name = "saveFolderButton";
            saveFolderButton.Size = new Size(75, 23);
            saveFolderButton.TabIndex = 2;
            saveFolderButton.Text = "Change";
            saveFolderButton.UseVisualStyleBackColor = true;
            saveFolderButton.Click += SaveFolderButton_Click;
            // 
            // saveFolderLabel
            // 
            saveFolderLabel.AutoSize = true;
            saveFolderLabel.Location = new Point(15, 12);
            saveFolderLabel.Name = "saveFolderLabel";
            saveFolderLabel.Size = new Size(70, 15);
            saveFolderLabel.TabIndex = 0;
            saveFolderLabel.Text = "Save folder";
            // 
            // cameraTabPage
            // 
            cameraTabPage.Controls.Add(moveCameraButton);
            cameraTabPage.Controls.Add(cameraMoveSpeedLabel);
            cameraTabPage.Controls.Add(cameraMoveSpeedNumericUpDown);
            cameraTabPage.Controls.Add(label1);
            cameraTabPage.Controls.Add(cameraRotateDelayLabel);
            cameraTabPage.Controls.Add(cameraRotateDelayNumericUpDown);
            cameraTabPage.Controls.Add(label2);
            cameraTabPage.Controls.Add(cameraRotateSpeedLabel);
            cameraTabPage.Controls.Add(cameraRotateSpeedNumericUpDown);
            cameraTabPage.Controls.Add(label3);
            cameraTabPage.Controls.Add(cameraDurationLabel);
            cameraTabPage.Controls.Add(cameraDurationNumericUpDown);
            cameraTabPage.Controls.Add(label4);
            cameraTabPage.Location = new Point(4, 24);
            cameraTabPage.Name = "cameraTabPage";
            cameraTabPage.Padding = new Padding(12);
            cameraTabPage.Size = new Size(616, 388);
            cameraTabPage.TabIndex = 1;
            cameraTabPage.Text = "Camera";
            cameraTabPage.UseVisualStyleBackColor = true;
            // 
            // moveCameraButton
            // 
            moveCameraButton.Location = new Point(102, 140);
            moveCameraButton.Name = "moveCameraButton";
            moveCameraButton.Size = new Size(120, 46);
            moveCameraButton.TabIndex = 14;
            moveCameraButton.Text = "Start";
            moveCameraButton.UseVisualStyleBackColor = true;
            moveCameraButton.Click += MoveCameraButton_Click;
            // 
            // cameraMoveSpeedLabel
            // 
            cameraMoveSpeedLabel.AutoSize = true;
            cameraMoveSpeedLabel.Location = new Point(23, 17);
            cameraMoveSpeedLabel.Name = "cameraMoveSpeedLabel";
            cameraMoveSpeedLabel.Size = new Size(73, 15);
            cameraMoveSpeedLabel.TabIndex = 0;
            cameraMoveSpeedLabel.Text = "Move speed";
            // 
            // cameraMoveSpeedNumericUpDown
            // 
            cameraMoveSpeedNumericUpDown.Location = new Point(102, 15);
            cameraMoveSpeedNumericUpDown.Maximum = new decimal(new int[] { 32767, 0, 0, 0 });
            cameraMoveSpeedNumericUpDown.Minimum = new decimal(new int[] { 32767, 0, 0, int.MinValue });
            cameraMoveSpeedNumericUpDown.Name = "cameraMoveSpeedNumericUpDown";
            cameraMoveSpeedNumericUpDown.Size = new Size(120, 23);
            cameraMoveSpeedNumericUpDown.TabIndex = 5;
            cameraMoveSpeedNumericUpDown.TextAlign = HorizontalAlignment.Right;
            cameraMoveSpeedNumericUpDown.UseWaitCursor = true;
            cameraMoveSpeedNumericUpDown.Value = new decimal(new int[] { 32767, 0, 0, 0 });
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(228, 17);
            label1.Name = "label1";
            label1.Size = new Size(112, 15);
            label1.TabIndex = 10;
            label1.Text = "(-32767 to 32767)";
            // 
            // cameraRotateDelayLabel
            // 
            cameraRotateDelayLabel.AutoSize = true;
            cameraRotateDelayLabel.Location = new Point(19, 46);
            cameraRotateDelayLabel.Name = "cameraRotateDelayLabel";
            cameraRotateDelayLabel.Size = new Size(77, 15);
            cameraRotateDelayLabel.TabIndex = 8;
            cameraRotateDelayLabel.Text = "Rotate delay";
            // 
            // cameraRotateDelayNumericUpDown
            // 
            cameraRotateDelayNumericUpDown.Location = new Point(102, 44);
            cameraRotateDelayNumericUpDown.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            cameraRotateDelayNumericUpDown.Name = "cameraRotateDelayNumericUpDown";
            cameraRotateDelayNumericUpDown.Size = new Size(120, 23);
            cameraRotateDelayNumericUpDown.TabIndex = 9;
            cameraRotateDelayNumericUpDown.TextAlign = HorizontalAlignment.Right;
            cameraRotateDelayNumericUpDown.Value = new decimal(new int[] { 400, 0, 0, 0 });
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(228, 46);
            label2.Name = "label2";
            label2.Size = new Size(31, 15);
            label2.TabIndex = 11;
            label2.Text = "(ms)";
            // 
            // cameraRotateSpeedLabel
            // 
            cameraRotateSpeedLabel.AutoSize = true;
            cameraRotateSpeedLabel.Location = new Point(15, 75);
            cameraRotateSpeedLabel.Name = "cameraRotateSpeedLabel";
            cameraRotateSpeedLabel.Size = new Size(81, 15);
            cameraRotateSpeedLabel.TabIndex = 3;
            cameraRotateSpeedLabel.Text = "Rotate speed";
            // 
            // cameraRotateSpeedNumericUpDown
            // 
            cameraRotateSpeedNumericUpDown.Location = new Point(102, 73);
            cameraRotateSpeedNumericUpDown.Maximum = new decimal(new int[] { 32767, 0, 0, 0 });
            cameraRotateSpeedNumericUpDown.Minimum = new decimal(new int[] { 32767, 0, 0, int.MinValue });
            cameraRotateSpeedNumericUpDown.Name = "cameraRotateSpeedNumericUpDown";
            cameraRotateSpeedNumericUpDown.Size = new Size(120, 23);
            cameraRotateSpeedNumericUpDown.TabIndex = 4;
            cameraRotateSpeedNumericUpDown.TextAlign = HorizontalAlignment.Right;
            cameraRotateSpeedNumericUpDown.Value = new decimal(new int[] { 22000, 0, 0, int.MinValue });
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(228, 75);
            label3.Name = "label3";
            label3.Size = new Size(112, 15);
            label3.TabIndex = 12;
            label3.Text = "(-32767 to 32767)";
            // 
            // cameraDurationLabel
            // 
            cameraDurationLabel.AutoSize = true;
            cameraDurationLabel.Location = new Point(41, 104);
            cameraDurationLabel.Name = "cameraDurationLabel";
            cameraDurationLabel.Size = new Size(55, 15);
            cameraDurationLabel.TabIndex = 6;
            cameraDurationLabel.Text = "Duration";
            // 
            // cameraDurationNumericUpDown
            // 
            cameraDurationNumericUpDown.Location = new Point(102, 102);
            cameraDurationNumericUpDown.Maximum = new decimal(new int[] { 16777216, 0, 0, 0 });
            cameraDurationNumericUpDown.Name = "cameraDurationNumericUpDown";
            cameraDurationNumericUpDown.Size = new Size(120, 23);
            cameraDurationNumericUpDown.TabIndex = 7;
            cameraDurationNumericUpDown.TextAlign = HorizontalAlignment.Right;
            cameraDurationNumericUpDown.Value = new decimal(new int[] { 21000, 0, 0, 0 });
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(228, 104);
            label4.Name = "label4";
            label4.Size = new Size(31, 15);
            label4.TabIndex = 13;
            label4.Text = "(ms)";
            // 
            // portalTabPage
            // 
            portalTabPage.Controls.Add(portalGlyphsLabel);
            portalTabPage.Controls.Add(label5);
            portalTabPage.Controls.Add(sendPortalAddressButton);
            portalTabPage.Controls.Add(portalAddressTextBox);
            portalTabPage.Location = new Point(4, 24);
            portalTabPage.Name = "portalTabPage";
            portalTabPage.Padding = new Padding(12);
            portalTabPage.Size = new Size(616, 388);
            portalTabPage.TabIndex = 3;
            portalTabPage.Text = "Portal";
            portalTabPage.UseVisualStyleBackColor = true;
            // 
            // portalGlyphsLabel
            // 
            portalGlyphsLabel.AutoSize = true;
            portalGlyphsLabel.Location = new Point(15, 65);
            portalGlyphsLabel.Margin = new Padding(3, 9, 3, 0);
            portalGlyphsLabel.Name = "portalGlyphsLabel";
            portalGlyphsLabel.Size = new Size(0, 15);
            portalGlyphsLabel.TabIndex = 3;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(15, 12);
            label5.Name = "label5";
            label5.Size = new Size(170, 15);
            label5.TabIndex = 2;
            label5.Text = "Portal Address (12-digit Hex)";
            // 
            // sendPortalAddressButton
            // 
            sendPortalAddressButton.Enabled = false;
            sendPortalAddressButton.Location = new Point(181, 30);
            sendPortalAddressButton.Name = "sendPortalAddressButton";
            sendPortalAddressButton.Size = new Size(75, 23);
            sendPortalAddressButton.TabIndex = 1;
            sendPortalAddressButton.Text = "Send";
            sendPortalAddressButton.UseVisualStyleBackColor = true;
            sendPortalAddressButton.Click += SendPortalAddressButton_Click;
            // 
            // portalAddressTextBox
            // 
            portalAddressTextBox.CharacterCasing = CharacterCasing.Upper;
            portalAddressTextBox.ImeMode = ImeMode.Alpha;
            portalAddressTextBox.Location = new Point(15, 30);
            portalAddressTextBox.MaxLength = 12;
            portalAddressTextBox.Name = "portalAddressTextBox";
            portalAddressTextBox.Size = new Size(160, 23);
            portalAddressTextBox.TabIndex = 0;
            portalAddressTextBox.TextChanged += PortalAddressTextBox_TextChanged;
            // 
            // toolStrip
            // 
            toolStrip.Dock = DockStyle.None;
            toolStrip.GripStyle = ToolStripGripStyle.Hidden;
            toolStrip.Items.AddRange(new ToolStripItem[] { lockButton, backupButton });
            toolStrip.Location = new Point(3, 0);
            toolStrip.Name = "toolStrip";
            toolStrip.Size = new Size(228, 25);
            toolStrip.TabIndex = 0;
            // 
            // lockButton
            // 
            lockButton.CheckOnClick = true;
            lockButton.Image = Properties.Resources.Lock;
            lockButton.ImageTransparentColor = Color.Magenta;
            lockButton.Name = "lockButton";
            lockButton.Size = new Size(115, 22);
            lockButton.Text = "Lock Save Files";
            lockButton.ToolTipText = "Lock/unlock save data files";
            lockButton.CheckedChanged += LockButton_CheckedChanged;
            // 
            // backupButton
            // 
            backupButton.Image = Properties.Resources.ExportData;
            backupButton.ImageTransparentColor = Color.Magenta;
            backupButton.Name = "backupButton";
            backupButton.Size = new Size(110, 22);
            backupButton.Text = "Create Backup";
            backupButton.ToolTipText = "Backup save data";
            backupButton.Click += BackupButton_Click;
            // 
            // saveFileSystemWatcher
            // 
            saveFileSystemWatcher.EnableRaisingEvents = true;
            saveFileSystemWatcher.Filter = "*.hg";
            saveFileSystemWatcher.SynchronizingObject = this;
            // 
            // backupFileSystemWatcher
            // 
            backupFileSystemWatcher.EnableRaisingEvents = true;
            backupFileSystemWatcher.Filter = "*.7z";
            backupFileSystemWatcher.SynchronizingObject = this;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(624, 441);
            Controls.Add(toolStripContainer);
            Name = "MainForm";
            Text = "NMS Save Data Utility";
            FormClosing += MainForm_FormClosing;
            Load += MainForm_Load;
            toolStripContainer.ContentPanel.ResumeLayout(false);
            toolStripContainer.TopToolStripPanel.ResumeLayout(false);
            toolStripContainer.TopToolStripPanel.PerformLayout();
            toolStripContainer.ResumeLayout(false);
            toolStripContainer.PerformLayout();
            tabControl.ResumeLayout(false);
            backupTabPage.ResumeLayout(false);
            splitContainer.Panel1.ResumeLayout(false);
            splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer).EndInit();
            splitContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)saveDataGridView).EndInit();
            ((System.ComponentModel.ISupportInitialize)backupDataGridView).EndInit();
            settingsTabPage.ResumeLayout(false);
            settingsTabPage.PerformLayout();
            cameraTabPage.ResumeLayout(false);
            cameraTabPage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)cameraMoveSpeedNumericUpDown).EndInit();
            ((System.ComponentModel.ISupportInitialize)cameraRotateDelayNumericUpDown).EndInit();
            ((System.ComponentModel.ISupportInitialize)cameraRotateSpeedNumericUpDown).EndInit();
            ((System.ComponentModel.ISupportInitialize)cameraDurationNumericUpDown).EndInit();
            portalTabPage.ResumeLayout(false);
            portalTabPage.PerformLayout();
            toolStrip.ResumeLayout(false);
            toolStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)saveFileSystemWatcher).EndInit();
            ((System.ComponentModel.ISupportInitialize)backupFileSystemWatcher).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private ToolStripContainer toolStripContainer;
        private ToolStrip toolStrip;
        private ToolStripButton lockButton;
        private ToolStripButton backupButton;
        private Button saveFolderButton;
        private Button backupFolderButton;
        private TabControl tabControl;
        private TabPage backupTabPage;
        private TabPage settingsTabPage;
        private TextBox saveFolderTextBox;
        private TextBox cameraRotateSpeedTextBox;
        private Label saveFolderLabel;
        private Label backupFolderLabel;
        private TextBox backupFolderTextBox;
        private CheckBox deleteAutosaveCheckBox;
        private CheckBox disableAutosaveCheckBox;
        private TabPage cameraTabPage;
        private Button moveCameraButton;
        private Label label4;
        private Label label3;
        private Label label2;
        private Label label1;
        private Label cameraMoveSpeedLabel;
        private NumericUpDown cameraMoveSpeedNumericUpDown;
        private Label cameraRotateDelayLabel;
        private NumericUpDown cameraRotateDelayNumericUpDown;
        private Label cameraRotateSpeedLabel;
        private NumericUpDown cameraRotateSpeedNumericUpDown;
        private Label cameraDurationLabel;
        private NumericUpDown cameraDurationNumericUpDown;
        private SplitContainer splitContainer;
        private DataGridView saveDataGridView;
        private DataGridView backupDataGridView;
        private DataGridViewCheckBoxColumn backupSelectColumn;
        private DataGridViewTextBoxColumn backupFilenameColumn;
        private DataGridViewTextBoxColumn backupDateColumn;
        private DataGridViewTextBoxColumn backupIncludedFilesColumn;
        private DataGridViewButtonColumn backupRestoreColumn;
        private DataGridViewButtonColumn backupDeleteColumn;
        private CheckBox enableCameraCheckBox;
        private FileSystemWatcher saveFileSystemWatcher;
        private FileSystemWatcher backupFileSystemWatcher;
        private DataGridViewCheckBoxColumn saveSelectColumn;
        private DataGridViewTextBoxColumn saveFilenameColumn;
        private DataGridViewTextBoxColumn saveDateColumn;
        private DataGridViewButtonColumn saveLockColumn;
        private TabPage portalTabPage;
        private Label label5;
        private Button sendPortalAddressButton;
        private TextBox portalAddressTextBox;
        private Label portalGlyphsLabel;
    }
}