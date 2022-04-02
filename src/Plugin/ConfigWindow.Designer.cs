
namespace ArchiveCacheManager
{
    partial class ConfigWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConfigWindow));
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.deleteAllButton = new System.Windows.Forms.Button();
            this.refreshButton = new System.Windows.Forms.Button();
            this.openInExplorerButton = new System.Windows.Forms.Button();
            this.deleteSelectedButton = new System.Windows.Forms.Button();
            this.configureCacheButton = new System.Windows.Forms.Button();
            this.multiDiscSupportCheckBox = new System.Windows.Forms.CheckBox();
            this.useGameIdM3uFilenameCheckBox = new System.Windows.Forms.CheckBox();
            this.versionLabel = new System.Windows.Forms.Label();
            this.forumLink = new System.Windows.Forms.LinkLabel();
            this.sourceLink = new System.Windows.Forms.LinkLabel();
            this.pluginLink = new System.Windows.Forms.LinkLabel();
            this.fileExtensionPriorityGroupBox = new System.Windows.Forms.GroupBox();
            this.deletePriorityButton = new System.Windows.Forms.Button();
            this.extensionPriorityDataGridView = new System.Windows.Forms.DataGridView();
            this.Emulator = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Platform = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Priority = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.editPriorityButton = new System.Windows.Forms.Button();
            this.addPriorityButton = new System.Windows.Forms.Button();
            this.cacheDataGridView = new System.Windows.Forms.DataGridView();
            this.ArchivePath = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Keep = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Archive = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ArchivePlatform = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ArchiveSize = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cacheDetailsGroupBox = new System.Windows.Forms.GroupBox();
            this.cacheSummaryTextBox = new System.Windows.Forms.TextBox();
            this.okButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.additionalOptionsGroupBox = new System.Windows.Forms.GroupBox();
            this.updateCheckCheckBox = new System.Windows.Forms.CheckBox();
            this.smartExtractCheckBox = new System.Windows.Forms.CheckBox();
            this.fileExtensionPriorityGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.extensionPriorityDataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cacheDataGridView)).BeginInit();
            this.cacheDetailsGroupBox.SuspendLayout();
            this.additionalOptionsGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // deleteAllButton
            // 
            this.deleteAllButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.deleteAllButton.BackColor = System.Drawing.SystemColors.ControlLight;
            this.deleteAllButton.Image = global::ArchiveCacheManager.Resources.broom;
            this.deleteAllButton.Location = new System.Drawing.Point(629, 244);
            this.deleteAllButton.Name = "deleteAllButton";
            this.deleteAllButton.Size = new System.Drawing.Size(116, 23);
            this.deleteAllButton.TabIndex = 21;
            this.deleteAllButton.Text = "Delete All";
            this.deleteAllButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.deleteAllButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.toolTip.SetToolTip(this.deleteAllButton, "Delete all items from the cache. The cache folder is not removed.");
            this.deleteAllButton.UseVisualStyleBackColor = true;
            this.deleteAllButton.Click += new System.EventHandler(this.deleteAllButton_Click);
            // 
            // refreshButton
            // 
            this.refreshButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.refreshButton.Image = global::ArchiveCacheManager.Resources.arrow_circle_double;
            this.refreshButton.Location = new System.Drawing.Point(15, 244);
            this.refreshButton.Name = "refreshButton";
            this.refreshButton.Size = new System.Drawing.Size(75, 23);
            this.refreshButton.TabIndex = 19;
            this.refreshButton.Text = "Refresh";
            this.refreshButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.refreshButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.toolTip.SetToolTip(this.refreshButton, "Refresh the cache details.");
            this.refreshButton.UseVisualStyleBackColor = true;
            this.refreshButton.Click += new System.EventHandler(this.refreshButton_Click);
            // 
            // openInExplorerButton
            // 
            this.openInExplorerButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.openInExplorerButton.Image = global::ArchiveCacheManager.Resources.folder_horizontal_open;
            this.openInExplorerButton.Location = new System.Drawing.Point(589, 48);
            this.openInExplorerButton.Name = "openInExplorerButton";
            this.openInExplorerButton.Size = new System.Drawing.Size(156, 23);
            this.openInExplorerButton.TabIndex = 18;
            this.openInExplorerButton.Text = "Open In Explorer";
            this.openInExplorerButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.openInExplorerButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.toolTip.SetToolTip(this.openInExplorerButton, "Opens the confgured cache path in Windows Explorer.");
            this.openInExplorerButton.UseVisualStyleBackColor = true;
            this.openInExplorerButton.Click += new System.EventHandler(this.openInExplorerButton_Click);
            // 
            // deleteSelectedButton
            // 
            this.deleteSelectedButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.deleteSelectedButton.BackColor = System.Drawing.SystemColors.ControlLight;
            this.deleteSelectedButton.Image = global::ArchiveCacheManager.Resources.cross_script;
            this.deleteSelectedButton.Location = new System.Drawing.Point(508, 244);
            this.deleteSelectedButton.Name = "deleteSelectedButton";
            this.deleteSelectedButton.Size = new System.Drawing.Size(116, 23);
            this.deleteSelectedButton.TabIndex = 17;
            this.deleteSelectedButton.Text = "Delete";
            this.deleteSelectedButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.deleteSelectedButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.toolTip.SetToolTip(this.deleteSelectedButton, "Delete the selected items from the cache.");
            this.deleteSelectedButton.UseVisualStyleBackColor = true;
            this.deleteSelectedButton.Click += new System.EventHandler(this.deleteSelectedButton_Click);
            // 
            // configureCacheButton
            // 
            this.configureCacheButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.configureCacheButton.Image = global::ArchiveCacheManager.Resources.gear;
            this.configureCacheButton.Location = new System.Drawing.Point(589, 19);
            this.configureCacheButton.Name = "configureCacheButton";
            this.configureCacheButton.Size = new System.Drawing.Size(156, 23);
            this.configureCacheButton.TabIndex = 15;
            this.configureCacheButton.Text = "Configure Cache...";
            this.configureCacheButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.configureCacheButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.toolTip.SetToolTip(this.configureCacheButton, "Edit the cache configuration.");
            this.configureCacheButton.UseVisualStyleBackColor = true;
            this.configureCacheButton.Click += new System.EventHandler(this.configureCacheButton_Click);
            // 
            // multiDiscSupportCheckBox
            // 
            this.multiDiscSupportCheckBox.AutoSize = true;
            this.multiDiscSupportCheckBox.Location = new System.Drawing.Point(15, 25);
            this.multiDiscSupportCheckBox.Name = "multiDiscSupportCheckBox";
            this.multiDiscSupportCheckBox.Size = new System.Drawing.Size(110, 17);
            this.multiDiscSupportCheckBox.TabIndex = 0;
            this.multiDiscSupportCheckBox.Text = "Multi-disc Support";
            this.toolTip.SetToolTip(this.multiDiscSupportCheckBox, "Enable multi-disc support to extract all discs in a multi-disc game to the cache." +
        "\r\nIt will also generate and use an M3U file if supported by the emulator.");
            this.multiDiscSupportCheckBox.UseVisualStyleBackColor = true;
            this.multiDiscSupportCheckBox.CheckedChanged += new System.EventHandler(this.multiDiscSupportCheckBox_CheckedChanged);
            // 
            // useGameIdM3uFilenameCheckBox
            // 
            this.useGameIdM3uFilenameCheckBox.AutoSize = true;
            this.useGameIdM3uFilenameCheckBox.Location = new System.Drawing.Point(131, 25);
            this.useGameIdM3uFilenameCheckBox.Name = "useGameIdM3uFilenameCheckBox";
            this.useGameIdM3uFilenameCheckBox.Size = new System.Drawing.Size(176, 17);
            this.useGameIdM3uFilenameCheckBox.TabIndex = 0;
            this.useGameIdM3uFilenameCheckBox.Text = "Use Game ID As M3U Filename";
            this.toolTip.SetToolTip(this.useGameIdM3uFilenameCheckBox, "Use the game\'s ID (GUID) when creating an M3U file.\r\nIf unchecked, the M3U file w" +
        "ill be named \"<Title> <Version>.m3u\".");
            this.useGameIdM3uFilenameCheckBox.UseVisualStyleBackColor = true;
            // 
            // versionLabel
            // 
            this.versionLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.versionLabel.Location = new System.Drawing.Point(672, 594);
            this.versionLabel.Name = "versionLabel";
            this.versionLabel.Size = new System.Drawing.Size(100, 19);
            this.versionLabel.TabIndex = 8;
            this.versionLabel.Text = "v0.0.0";
            this.versionLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // forumLink
            // 
            this.forumLink.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.forumLink.AutoSize = true;
            this.forumLink.Location = new System.Drawing.Point(491, 597);
            this.forumLink.Name = "forumLink";
            this.forumLink.Size = new System.Drawing.Size(76, 13);
            this.forumLink.TabIndex = 10;
            this.forumLink.TabStop = true;
            this.forumLink.Text = "Forum Support";
            this.forumLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.forumLink_LinkClicked);
            // 
            // sourceLink
            // 
            this.sourceLink.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.sourceLink.AutoSize = true;
            this.sourceLink.Location = new System.Drawing.Point(573, 597);
            this.sourceLink.Name = "sourceLink";
            this.sourceLink.Size = new System.Drawing.Size(93, 13);
            this.sourceLink.TabIndex = 11;
            this.sourceLink.TabStop = true;
            this.sourceLink.Text = "GitHub Repository";
            this.sourceLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.sourceLink_LinkClicked);
            // 
            // pluginLink
            // 
            this.pluginLink.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.pluginLink.AutoSize = true;
            this.pluginLink.Location = new System.Drawing.Point(394, 597);
            this.pluginLink.Name = "pluginLink";
            this.pluginLink.Size = new System.Drawing.Size(91, 13);
            this.pluginLink.TabIndex = 10;
            this.pluginLink.TabStop = true;
            this.pluginLink.Text = "Plugin Homepage";
            this.pluginLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.pluginLink_LinkClicked);
            // 
            // fileExtensionPriorityGroupBox
            // 
            this.fileExtensionPriorityGroupBox.Controls.Add(this.deletePriorityButton);
            this.fileExtensionPriorityGroupBox.Controls.Add(this.extensionPriorityDataGridView);
            this.fileExtensionPriorityGroupBox.Controls.Add(this.editPriorityButton);
            this.fileExtensionPriorityGroupBox.Controls.Add(this.addPriorityButton);
            this.fileExtensionPriorityGroupBox.Location = new System.Drawing.Point(12, 300);
            this.fileExtensionPriorityGroupBox.Name = "fileExtensionPriorityGroupBox";
            this.fileExtensionPriorityGroupBox.Size = new System.Drawing.Size(760, 210);
            this.fileExtensionPriorityGroupBox.TabIndex = 12;
            this.fileExtensionPriorityGroupBox.TabStop = false;
            this.fileExtensionPriorityGroupBox.Text = "Filename Priority";
            // 
            // deletePriorityButton
            // 
            this.deletePriorityButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.deletePriorityButton.BackColor = System.Drawing.SystemColors.ControlLight;
            this.deletePriorityButton.Image = global::ArchiveCacheManager.Resources.cross_script;
            this.deletePriorityButton.Location = new System.Drawing.Point(670, 171);
            this.deletePriorityButton.Name = "deletePriorityButton";
            this.deletePriorityButton.Size = new System.Drawing.Size(75, 23);
            this.deletePriorityButton.TabIndex = 0;
            this.deletePriorityButton.Text = "Delete";
            this.deletePriorityButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.deletePriorityButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.deletePriorityButton.UseVisualStyleBackColor = true;
            this.deletePriorityButton.Click += new System.EventHandler(this.deletePriorityButton_Click);
            // 
            // extensionPriorityDataGridView
            // 
            this.extensionPriorityDataGridView.AllowUserToAddRows = false;
            this.extensionPriorityDataGridView.AllowUserToDeleteRows = false;
            this.extensionPriorityDataGridView.AllowUserToResizeRows = false;
            this.extensionPriorityDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.extensionPriorityDataGridView.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            this.extensionPriorityDataGridView.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.extensionPriorityDataGridView.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.extensionPriorityDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.extensionPriorityDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Emulator,
            this.Platform,
            this.Priority});
            this.extensionPriorityDataGridView.Location = new System.Drawing.Point(15, 19);
            this.extensionPriorityDataGridView.MultiSelect = false;
            this.extensionPriorityDataGridView.Name = "extensionPriorityDataGridView";
            this.extensionPriorityDataGridView.RowHeadersVisible = false;
            this.extensionPriorityDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.extensionPriorityDataGridView.Size = new System.Drawing.Size(730, 146);
            this.extensionPriorityDataGridView.StandardTab = true;
            this.extensionPriorityDataGridView.TabIndex = 13;
            this.extensionPriorityDataGridView.SelectionChanged += new System.EventHandler(this.extensionPriorityDataGridView_SelectionChanged);
            // 
            // Emulator
            // 
            this.Emulator.HeaderText = "Emulator";
            this.Emulator.Name = "Emulator";
            this.Emulator.ReadOnly = true;
            // 
            // Platform
            // 
            this.Platform.HeaderText = "Platform";
            this.Platform.Name = "Platform";
            this.Platform.ReadOnly = true;
            // 
            // Priority
            // 
            this.Priority.FillWeight = 200F;
            this.Priority.HeaderText = "Priority";
            this.Priority.Name = "Priority";
            this.Priority.ReadOnly = true;
            // 
            // editPriorityButton
            // 
            this.editPriorityButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.editPriorityButton.Image = global::ArchiveCacheManager.Resources.pencil;
            this.editPriorityButton.Location = new System.Drawing.Point(589, 171);
            this.editPriorityButton.Name = "editPriorityButton";
            this.editPriorityButton.Size = new System.Drawing.Size(75, 23);
            this.editPriorityButton.TabIndex = 0;
            this.editPriorityButton.Text = "Edit...";
            this.editPriorityButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.editPriorityButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.editPriorityButton.UseVisualStyleBackColor = true;
            this.editPriorityButton.Click += new System.EventHandler(this.editPriorityButton_Click);
            // 
            // addPriorityButton
            // 
            this.addPriorityButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.addPriorityButton.Image = global::ArchiveCacheManager.Resources.plus;
            this.addPriorityButton.Location = new System.Drawing.Point(508, 171);
            this.addPriorityButton.Name = "addPriorityButton";
            this.addPriorityButton.Size = new System.Drawing.Size(75, 23);
            this.addPriorityButton.TabIndex = 0;
            this.addPriorityButton.Text = "Add...";
            this.addPriorityButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.addPriorityButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.addPriorityButton.UseVisualStyleBackColor = true;
            this.addPriorityButton.Click += new System.EventHandler(this.addPriorityButton_Click);
            // 
            // cacheDataGridView
            // 
            this.cacheDataGridView.AllowUserToAddRows = false;
            this.cacheDataGridView.AllowUserToDeleteRows = false;
            this.cacheDataGridView.AllowUserToResizeRows = false;
            this.cacheDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.cacheDataGridView.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            this.cacheDataGridView.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.cacheDataGridView.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.cacheDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.cacheDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ArchivePath,
            this.Keep,
            this.Archive,
            this.ArchivePlatform,
            this.ArchiveSize});
            this.cacheDataGridView.Location = new System.Drawing.Point(15, 77);
            this.cacheDataGridView.Name = "cacheDataGridView";
            this.cacheDataGridView.RowHeadersVisible = false;
            this.cacheDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.cacheDataGridView.Size = new System.Drawing.Size(730, 161);
            this.cacheDataGridView.StandardTab = true;
            this.cacheDataGridView.TabIndex = 14;
            this.cacheDataGridView.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.cacheDataGridView_CellPainting);
            this.cacheDataGridView.CurrentCellDirtyStateChanged += new System.EventHandler(this.cacheDataGridView_CurrentCellDirtyStateChanged);
            // 
            // ArchivePath
            // 
            this.ArchivePath.HeaderText = "ArchivePath";
            this.ArchivePath.Name = "ArchivePath";
            this.ArchivePath.Visible = false;
            // 
            // Keep
            // 
            this.Keep.FillWeight = 12F;
            this.Keep.HeaderText = "Keep";
            this.Keep.Name = "Keep";
            this.Keep.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // Archive
            // 
            dataGridViewCellStyle1.Padding = new System.Windows.Forms.Padding(18, 0, 0, 0);
            this.Archive.DefaultCellStyle = dataGridViewCellStyle1;
            this.Archive.HeaderText = "Archive";
            this.Archive.Name = "Archive";
            this.Archive.ReadOnly = true;
            // 
            // ArchivePlatform
            // 
            this.ArchivePlatform.FillWeight = 60F;
            this.ArchivePlatform.HeaderText = "Platform";
            this.ArchivePlatform.Name = "ArchivePlatform";
            // 
            // ArchiveSize
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle2.Format = "N1";
            this.ArchiveSize.DefaultCellStyle = dataGridViewCellStyle2;
            this.ArchiveSize.FillWeight = 25F;
            this.ArchiveSize.HeaderText = "Size (MB)";
            this.ArchiveSize.Name = "ArchiveSize";
            this.ArchiveSize.ReadOnly = true;
            // 
            // cacheDetailsGroupBox
            // 
            this.cacheDetailsGroupBox.Controls.Add(this.deleteAllButton);
            this.cacheDetailsGroupBox.Controls.Add(this.cacheSummaryTextBox);
            this.cacheDetailsGroupBox.Controls.Add(this.refreshButton);
            this.cacheDetailsGroupBox.Controls.Add(this.openInExplorerButton);
            this.cacheDetailsGroupBox.Controls.Add(this.deleteSelectedButton);
            this.cacheDetailsGroupBox.Controls.Add(this.cacheDataGridView);
            this.cacheDetailsGroupBox.Controls.Add(this.configureCacheButton);
            this.cacheDetailsGroupBox.Location = new System.Drawing.Point(12, 12);
            this.cacheDetailsGroupBox.Name = "cacheDetailsGroupBox";
            this.cacheDetailsGroupBox.Size = new System.Drawing.Size(760, 282);
            this.cacheDetailsGroupBox.TabIndex = 17;
            this.cacheDetailsGroupBox.TabStop = false;
            this.cacheDetailsGroupBox.Text = "Cache Details";
            // 
            // cacheSummaryTextBox
            // 
            this.cacheSummaryTextBox.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.cacheSummaryTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.cacheSummaryTextBox.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cacheSummaryTextBox.Location = new System.Drawing.Point(15, 21);
            this.cacheSummaryTextBox.Multiline = true;
            this.cacheSummaryTextBox.Name = "cacheSummaryTextBox";
            this.cacheSummaryTextBox.ReadOnly = true;
            this.cacheSummaryTextBox.Size = new System.Drawing.Size(568, 50);
            this.cacheSummaryTextBox.TabIndex = 20;
            // 
            // okButton
            // 
            this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.okButton.Image = global::ArchiveCacheManager.Resources.tick;
            this.okButton.Location = new System.Drawing.Point(12, 592);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 0;
            this.okButton.Text = "OK";
            this.okButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.okButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Image = global::ArchiveCacheManager.Resources.cross_script;
            this.cancelButton.Location = new System.Drawing.Point(93, 592);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 1;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cancelButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // additionalOptionsGroupBox
            // 
            this.additionalOptionsGroupBox.Controls.Add(this.smartExtractCheckBox);
            this.additionalOptionsGroupBox.Controls.Add(this.useGameIdM3uFilenameCheckBox);
            this.additionalOptionsGroupBox.Controls.Add(this.updateCheckCheckBox);
            this.additionalOptionsGroupBox.Controls.Add(this.multiDiscSupportCheckBox);
            this.additionalOptionsGroupBox.Location = new System.Drawing.Point(12, 516);
            this.additionalOptionsGroupBox.Name = "additionalOptionsGroupBox";
            this.additionalOptionsGroupBox.Size = new System.Drawing.Size(760, 60);
            this.additionalOptionsGroupBox.TabIndex = 18;
            this.additionalOptionsGroupBox.TabStop = false;
            this.additionalOptionsGroupBox.Text = "Additional Options";
            // 
            // updateCheckCheckBox
            // 
            this.updateCheckCheckBox.AutoSize = true;
            this.updateCheckCheckBox.Location = new System.Drawing.Point(573, 25);
            this.updateCheckCheckBox.Name = "updateCheckCheckBox";
            this.updateCheckCheckBox.Size = new System.Drawing.Size(172, 17);
            this.updateCheckCheckBox.TabIndex = 0;
            this.updateCheckCheckBox.Text = "Check For Updates On Startup";
            this.updateCheckCheckBox.UseVisualStyleBackColor = true;
            this.updateCheckCheckBox.CheckedChanged += new System.EventHandler(this.multiDiscSupportCheckBox_CheckedChanged);
            // 
            // smartExtractCheckBox
            // 
            this.smartExtractCheckBox.AutoSize = true;
            this.smartExtractCheckBox.Location = new System.Drawing.Point(313, 25);
            this.smartExtractCheckBox.Name = "smartExtractCheckBox";
            this.smartExtractCheckBox.Size = new System.Drawing.Size(89, 17);
            this.smartExtractCheckBox.TabIndex = 1;
            this.smartExtractCheckBox.Text = "Smart Extract";
            this.toolTip.SetToolTip(this.smartExtractCheckBox, resources.GetString("smartExtractCheckBox.ToolTip"));
            this.smartExtractCheckBox.UseVisualStyleBackColor = true;
            // 
            // ConfigWindow
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(784, 627);
            this.Controls.Add(this.additionalOptionsGroupBox);
            this.Controls.Add(this.cacheDetailsGroupBox);
            this.Controls.Add(this.fileExtensionPriorityGroupBox);
            this.Controls.Add(this.sourceLink);
            this.Controls.Add(this.pluginLink);
            this.Controls.Add(this.forumLink);
            this.Controls.Add(this.versionLabel);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ConfigWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Archive Cache Manager";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ConfigWindow_FormClosed);
            this.fileExtensionPriorityGroupBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.extensionPriorityDataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cacheDataGridView)).EndInit();
            this.cacheDetailsGroupBox.ResumeLayout(false);
            this.cacheDetailsGroupBox.PerformLayout();
            this.additionalOptionsGroupBox.ResumeLayout(false);
            this.additionalOptionsGroupBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.Label versionLabel;
        private System.Windows.Forms.Button editPriorityButton;
        private System.Windows.Forms.Button deletePriorityButton;
        private System.Windows.Forms.Button addPriorityButton;
        private System.Windows.Forms.LinkLabel forumLink;
        private System.Windows.Forms.LinkLabel sourceLink;
        private System.Windows.Forms.LinkLabel pluginLink;
        private System.Windows.Forms.GroupBox fileExtensionPriorityGroupBox;
        private System.Windows.Forms.DataGridView extensionPriorityDataGridView;
        private System.Windows.Forms.DataGridView cacheDataGridView;
        private System.Windows.Forms.Button configureCacheButton;
        private System.Windows.Forms.GroupBox cacheDetailsGroupBox;
        private System.Windows.Forms.Button deleteSelectedButton;
        private System.Windows.Forms.Button openInExplorerButton;
        private System.Windows.Forms.Button refreshButton;
        private System.Windows.Forms.TextBox cacheSummaryTextBox;
        private System.Windows.Forms.Button deleteAllButton;
        private System.Windows.Forms.DataGridViewTextBoxColumn ArchivePath;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Keep;
        private System.Windows.Forms.DataGridViewTextBoxColumn Archive;
        private System.Windows.Forms.DataGridViewTextBoxColumn ArchivePlatform;
        private System.Windows.Forms.DataGridViewTextBoxColumn ArchiveSize;
        private System.Windows.Forms.DataGridViewTextBoxColumn Emulator;
        private System.Windows.Forms.DataGridViewTextBoxColumn Platform;
        private System.Windows.Forms.DataGridViewTextBoxColumn Priority;
        private System.Windows.Forms.GroupBox additionalOptionsGroupBox;
        private System.Windows.Forms.CheckBox multiDiscSupportCheckBox;
        private System.Windows.Forms.CheckBox useGameIdM3uFilenameCheckBox;
        private System.Windows.Forms.CheckBox updateCheckCheckBox;
        private System.Windows.Forms.CheckBox smartExtractCheckBox;
    }
}