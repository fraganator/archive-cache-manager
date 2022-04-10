﻿
namespace ArchiveCacheManager
{
    partial class NewConfigWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NewConfigWindow));
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Cache Settings");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("Extraction Settings");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("Plugin Settings");
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.openInExplorerButton = new System.Windows.Forms.Button();
            this.configureCacheButton = new System.Windows.Forms.Button();
            this.deleteAllButton = new System.Windows.Forms.Button();
            this.refreshButton = new System.Windows.Forms.Button();
            this.deleteSelectedButton = new System.Windows.Forms.Button();
            this.smartExtractCheckBox = new System.Windows.Forms.CheckBox();
            this.multiDiscSupportCheckBox = new System.Windows.Forms.CheckBox();
            this.useGameIdM3uFilenameCheckBox = new System.Windows.Forms.CheckBox();
            this.versionLabel = new System.Windows.Forms.Label();
            this.forumLink = new System.Windows.Forms.LinkLabel();
            this.sourceLink = new System.Windows.Forms.LinkLabel();
            this.pluginLink = new System.Windows.Forms.LinkLabel();
            this.okButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.tabControl1 = new ArchiveCacheManager.StackPanel();
            this.tab1CacheSettings = new System.Windows.Forms.TabPage();
            this.label1 = new System.Windows.Forms.Label();
            this.cacheSummaryTextBox = new System.Windows.Forms.TextBox();
            this.cacheDataGridView = new System.Windows.Forms.DataGridView();
            this.ArchivePath = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Archive = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ArchivePlatform = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ArchiveSize = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Keep = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.tab2ExtractionSettings = new System.Windows.Forms.TabPage();
            this.label2 = new System.Windows.Forms.Label();
            this.deletePriorityButton = new System.Windows.Forms.Button();
            this.extensionPriorityDataGridView = new System.Windows.Forms.DataGridView();
            this.Emulator = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Platform = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Priority = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Action = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.MultiDisc = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.GameIdM3u = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.SmartExtract = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.editPriorityButton = new System.Windows.Forms.Button();
            this.addPriorityButton = new System.Windows.Forms.Button();
            this.tab3PluginSettings = new System.Windows.Forms.TabPage();
            this.updateCheckCheckBox = new System.Windows.Forms.CheckBox();
            this.tabControl1.SuspendLayout();
            this.tab1CacheSettings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cacheDataGridView)).BeginInit();
            this.tab2ExtractionSettings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.extensionPriorityDataGridView)).BeginInit();
            this.tab3PluginSettings.SuspendLayout();
            this.SuspendLayout();
            // 
            // openInExplorerButton
            // 
            this.openInExplorerButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.openInExplorerButton.Image = global::ArchiveCacheManager.Resources.folder_horizontal_open;
            this.openInExplorerButton.Location = new System.Drawing.Point(576, 96);
            this.openInExplorerButton.Name = "openInExplorerButton";
            this.openInExplorerButton.Size = new System.Drawing.Size(156, 28);
            this.openInExplorerButton.TabIndex = 18;
            this.openInExplorerButton.Text = "Open In Explorer";
            this.openInExplorerButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.openInExplorerButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.toolTip.SetToolTip(this.openInExplorerButton, "Opens the confgured cache path in Windows Explorer.");
            this.openInExplorerButton.UseVisualStyleBackColor = true;
            this.openInExplorerButton.Click += new System.EventHandler(this.openInExplorerButton_Click);
            // 
            // configureCacheButton
            // 
            this.configureCacheButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.configureCacheButton.Image = global::ArchiveCacheManager.Resources.gear;
            this.configureCacheButton.Location = new System.Drawing.Point(576, 62);
            this.configureCacheButton.Name = "configureCacheButton";
            this.configureCacheButton.Size = new System.Drawing.Size(156, 28);
            this.configureCacheButton.TabIndex = 15;
            this.configureCacheButton.Text = "Configure Cache...";
            this.configureCacheButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.configureCacheButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.toolTip.SetToolTip(this.configureCacheButton, "Edit the cache configuration.");
            this.configureCacheButton.UseVisualStyleBackColor = true;
            this.configureCacheButton.Click += new System.EventHandler(this.configureCacheButton_Click);
            // 
            // deleteAllButton
            // 
            this.deleteAllButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.deleteAllButton.BackColor = System.Drawing.SystemColors.ControlLight;
            this.deleteAllButton.Image = global::ArchiveCacheManager.Resources.broom;
            this.deleteAllButton.Location = new System.Drawing.Point(616, 488);
            this.deleteAllButton.Name = "deleteAllButton";
            this.deleteAllButton.Size = new System.Drawing.Size(116, 28);
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
            this.refreshButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.refreshButton.Image = global::ArchiveCacheManager.Resources.arrow_circle_double;
            this.refreshButton.Location = new System.Drawing.Point(6, 488);
            this.refreshButton.Name = "refreshButton";
            this.refreshButton.Size = new System.Drawing.Size(80, 28);
            this.refreshButton.TabIndex = 19;
            this.refreshButton.Text = "Refresh";
            this.refreshButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.refreshButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.toolTip.SetToolTip(this.refreshButton, "Refresh the cache details.");
            this.refreshButton.UseVisualStyleBackColor = true;
            this.refreshButton.Click += new System.EventHandler(this.refreshButton_Click);
            // 
            // deleteSelectedButton
            // 
            this.deleteSelectedButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.deleteSelectedButton.BackColor = System.Drawing.SystemColors.ControlLight;
            this.deleteSelectedButton.Image = global::ArchiveCacheManager.Resources.cross_script;
            this.deleteSelectedButton.Location = new System.Drawing.Point(494, 488);
            this.deleteSelectedButton.Name = "deleteSelectedButton";
            this.deleteSelectedButton.Size = new System.Drawing.Size(116, 28);
            this.deleteSelectedButton.TabIndex = 17;
            this.deleteSelectedButton.Text = "Delete";
            this.deleteSelectedButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.deleteSelectedButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.toolTip.SetToolTip(this.deleteSelectedButton, "Delete the selected items from the cache.");
            this.deleteSelectedButton.UseVisualStyleBackColor = true;
            this.deleteSelectedButton.Click += new System.EventHandler(this.deleteSelectedButton_Click);
            // 
            // smartExtractCheckBox
            // 
            this.smartExtractCheckBox.AutoSize = true;
            this.smartExtractCheckBox.Location = new System.Drawing.Point(318, 31);
            this.smartExtractCheckBox.Name = "smartExtractCheckBox";
            this.smartExtractCheckBox.Size = new System.Drawing.Size(89, 17);
            this.smartExtractCheckBox.TabIndex = 1;
            this.smartExtractCheckBox.Text = "Smart Extract";
            this.toolTip.SetToolTip(this.smartExtractCheckBox, resources.GetString("smartExtractCheckBox.ToolTip"));
            this.smartExtractCheckBox.UseVisualStyleBackColor = true;
            // 
            // multiDiscSupportCheckBox
            // 
            this.multiDiscSupportCheckBox.AutoSize = true;
            this.multiDiscSupportCheckBox.Location = new System.Drawing.Point(20, 31);
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
            this.useGameIdM3uFilenameCheckBox.Location = new System.Drawing.Point(136, 31);
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
            this.versionLabel.Location = new System.Drawing.Point(832, 580);
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
            this.forumLink.Location = new System.Drawing.Point(651, 583);
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
            this.sourceLink.Location = new System.Drawing.Point(733, 583);
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
            this.pluginLink.Location = new System.Drawing.Point(554, 583);
            this.pluginLink.Name = "pluginLink";
            this.pluginLink.Size = new System.Drawing.Size(91, 13);
            this.pluginLink.TabIndex = 10;
            this.pluginLink.TabStop = true;
            this.pluginLink.Text = "Plugin Homepage";
            this.pluginLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.pluginLink_LinkClicked);
            // 
            // okButton
            // 
            this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.okButton.Image = global::ArchiveCacheManager.Resources.tick;
            this.okButton.Location = new System.Drawing.Point(12, 578);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(80, 28);
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
            this.cancelButton.Location = new System.Drawing.Point(98, 578);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(80, 28);
            this.cancelButton.TabIndex = 1;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cancelButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // treeView1
            // 
            this.treeView1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.treeView1.FullRowSelect = true;
            this.treeView1.HideSelection = false;
            this.treeView1.ItemHeight = 28;
            this.treeView1.Location = new System.Drawing.Point(12, 12);
            this.treeView1.Name = "treeView1";
            treeNode1.Name = "CacheSettings";
            treeNode1.Text = "Cache Settings";
            treeNode2.Name = "ExtractionSettings";
            treeNode2.Text = "Extraction Settings";
            treeNode3.Name = "PluginSettings";
            treeNode3.Text = "Plugin Settings";
            this.treeView1.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2,
            treeNode3});
            this.treeView1.ShowLines = false;
            this.treeView1.Size = new System.Drawing.Size(166, 551);
            this.treeView1.TabIndex = 20;
            this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tab1CacheSettings);
            this.tabControl1.Controls.Add(this.tab2ExtractionSettings);
            this.tabControl1.Controls.Add(this.tab3PluginSettings);
            this.tabControl1.Location = new System.Drawing.Point(184, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(748, 550);
            this.tabControl1.TabIndex = 19;
            // 
            // tab1CacheSettings
            // 
            this.tab1CacheSettings.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tab1CacheSettings.Controls.Add(this.label1);
            this.tab1CacheSettings.Controls.Add(this.openInExplorerButton);
            this.tab1CacheSettings.Controls.Add(this.configureCacheButton);
            this.tab1CacheSettings.Controls.Add(this.deleteAllButton);
            this.tab1CacheSettings.Controls.Add(this.cacheSummaryTextBox);
            this.tab1CacheSettings.Controls.Add(this.refreshButton);
            this.tab1CacheSettings.Controls.Add(this.deleteSelectedButton);
            this.tab1CacheSettings.Controls.Add(this.cacheDataGridView);
            this.tab1CacheSettings.Location = new System.Drawing.Point(4, 22);
            this.tab1CacheSettings.Name = "tab1CacheSettings";
            this.tab1CacheSettings.Padding = new System.Windows.Forms.Padding(3);
            this.tab1CacheSettings.Size = new System.Drawing.Size(740, 524);
            this.tab1CacheSettings.TabIndex = 0;
            this.tab1CacheSettings.Text = "Cache Settings";
            this.tab1CacheSettings.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.label1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(6, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(726, 43);
            this.label1.TabIndex = 22;
            this.label1.Text = "Cache Settings";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cacheSummaryTextBox
            // 
            this.cacheSummaryTextBox.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.cacheSummaryTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.cacheSummaryTextBox.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cacheSummaryTextBox.Location = new System.Drawing.Point(9, 62);
            this.cacheSummaryTextBox.Multiline = true;
            this.cacheSummaryTextBox.Name = "cacheSummaryTextBox";
            this.cacheSummaryTextBox.ReadOnly = true;
            this.cacheSummaryTextBox.Size = new System.Drawing.Size(561, 62);
            this.cacheSummaryTextBox.TabIndex = 20;
            // 
            // cacheDataGridView
            // 
            this.cacheDataGridView.AllowUserToAddRows = false;
            this.cacheDataGridView.AllowUserToDeleteRows = false;
            this.cacheDataGridView.AllowUserToResizeRows = false;
            this.cacheDataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cacheDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.cacheDataGridView.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            this.cacheDataGridView.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.cacheDataGridView.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.cacheDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.cacheDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ArchivePath,
            this.Archive,
            this.ArchivePlatform,
            this.ArchiveSize,
            this.Keep});
            this.cacheDataGridView.Location = new System.Drawing.Point(6, 130);
            this.cacheDataGridView.Name = "cacheDataGridView";
            this.cacheDataGridView.RowHeadersVisible = false;
            this.cacheDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.cacheDataGridView.Size = new System.Drawing.Size(726, 352);
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
            // Archive
            // 
            dataGridViewCellStyle1.Padding = new System.Windows.Forms.Padding(24, 0, 0, 0);
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
            // Keep
            // 
            this.Keep.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.Keep.FillWeight = 12F;
            this.Keep.HeaderText = "Keep";
            this.Keep.Name = "Keep";
            this.Keep.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Keep.Width = 57;
            // 
            // tab2ExtractionSettings
            // 
            this.tab2ExtractionSettings.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tab2ExtractionSettings.Controls.Add(this.label2);
            this.tab2ExtractionSettings.Controls.Add(this.deletePriorityButton);
            this.tab2ExtractionSettings.Controls.Add(this.extensionPriorityDataGridView);
            this.tab2ExtractionSettings.Controls.Add(this.editPriorityButton);
            this.tab2ExtractionSettings.Controls.Add(this.addPriorityButton);
            this.tab2ExtractionSettings.Location = new System.Drawing.Point(4, 22);
            this.tab2ExtractionSettings.Name = "tab2ExtractionSettings";
            this.tab2ExtractionSettings.Padding = new System.Windows.Forms.Padding(3);
            this.tab2ExtractionSettings.Size = new System.Drawing.Size(740, 524);
            this.tab2ExtractionSettings.TabIndex = 1;
            this.tab2ExtractionSettings.Text = "Extraction Settings";
            this.tab2ExtractionSettings.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.BackColor = System.Drawing.SystemColors.ControlLight;
            this.label2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(6, 7);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(726, 43);
            this.label2.TabIndex = 23;
            this.label2.Text = "Extraction Settings";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // deletePriorityButton
            // 
            this.deletePriorityButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.deletePriorityButton.BackColor = System.Drawing.SystemColors.ControlLight;
            this.deletePriorityButton.Image = global::ArchiveCacheManager.Resources.cross_script;
            this.deletePriorityButton.Location = new System.Drawing.Point(652, 488);
            this.deletePriorityButton.Name = "deletePriorityButton";
            this.deletePriorityButton.Size = new System.Drawing.Size(80, 28);
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
            this.extensionPriorityDataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.extensionPriorityDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.extensionPriorityDataGridView.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            this.extensionPriorityDataGridView.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.extensionPriorityDataGridView.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.extensionPriorityDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.extensionPriorityDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Emulator,
            this.Platform,
            this.Priority,
            this.Action,
            this.MultiDisc,
            this.GameIdM3u,
            this.SmartExtract});
            this.extensionPriorityDataGridView.Location = new System.Drawing.Point(6, 62);
            this.extensionPriorityDataGridView.MultiSelect = false;
            this.extensionPriorityDataGridView.Name = "extensionPriorityDataGridView";
            this.extensionPriorityDataGridView.RowHeadersVisible = false;
            this.extensionPriorityDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.extensionPriorityDataGridView.Size = new System.Drawing.Size(726, 273);
            this.extensionPriorityDataGridView.StandardTab = true;
            this.extensionPriorityDataGridView.TabIndex = 13;
            this.extensionPriorityDataGridView.SelectionChanged += new System.EventHandler(this.extensionPriorityDataGridView_SelectionChanged);
            // 
            // Emulator
            // 
            this.Emulator.HeaderText = "Emulator";
            this.Emulator.MinimumWidth = 150;
            this.Emulator.Name = "Emulator";
            this.Emulator.ReadOnly = true;
            // 
            // Platform
            // 
            this.Platform.HeaderText = "Platform";
            this.Platform.MinimumWidth = 150;
            this.Platform.Name = "Platform";
            this.Platform.ReadOnly = true;
            // 
            // Priority
            // 
            this.Priority.HeaderText = "Priority";
            this.Priority.MinimumWidth = 150;
            this.Priority.Name = "Priority";
            // 
            // Action
            // 
            this.Action.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.Action.FillWeight = 50F;
            this.Action.HeaderText = "Action";
            this.Action.Items.AddRange(new object[] {
            "Extract",
            "Extract or Copy",
            "Copy Only"});
            this.Action.Name = "Action";
            this.Action.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Action.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Action.Width = 62;
            // 
            // MultiDisc
            // 
            this.MultiDisc.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.MultiDisc.FillWeight = 50F;
            this.MultiDisc.HeaderText = "Multi-Disc";
            this.MultiDisc.Name = "MultiDisc";
            this.MultiDisc.Width = 59;
            // 
            // GameIdM3u
            // 
            this.GameIdM3u.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.GameIdM3u.FillWeight = 50F;
            this.GameIdM3u.HeaderText = "M3U Name";
            this.GameIdM3u.Items.AddRange(new object[] {
            "Game ID",
            "Title + Version"});
            this.GameIdM3u.MinimumWidth = 100;
            this.GameIdM3u.Name = "GameIdM3u";
            this.GameIdM3u.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // SmartExtract
            // 
            this.SmartExtract.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.SmartExtract.FillWeight = 50F;
            this.SmartExtract.HeaderText = "Smart Extract";
            this.SmartExtract.Name = "SmartExtract";
            this.SmartExtract.Width = 76;
            // 
            // editPriorityButton
            // 
            this.editPriorityButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.editPriorityButton.Image = global::ArchiveCacheManager.Resources.pencil;
            this.editPriorityButton.Location = new System.Drawing.Point(566, 488);
            this.editPriorityButton.Name = "editPriorityButton";
            this.editPriorityButton.Size = new System.Drawing.Size(80, 28);
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
            this.addPriorityButton.Location = new System.Drawing.Point(480, 488);
            this.addPriorityButton.Name = "addPriorityButton";
            this.addPriorityButton.Size = new System.Drawing.Size(80, 28);
            this.addPriorityButton.TabIndex = 0;
            this.addPriorityButton.Text = "Add...";
            this.addPriorityButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.addPriorityButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.addPriorityButton.UseVisualStyleBackColor = true;
            this.addPriorityButton.Click += new System.EventHandler(this.addPriorityButton_Click);
            // 
            // tab3PluginSettings
            // 
            this.tab3PluginSettings.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tab3PluginSettings.Controls.Add(this.updateCheckCheckBox);
            this.tab3PluginSettings.Controls.Add(this.smartExtractCheckBox);
            this.tab3PluginSettings.Controls.Add(this.multiDiscSupportCheckBox);
            this.tab3PluginSettings.Controls.Add(this.useGameIdM3uFilenameCheckBox);
            this.tab3PluginSettings.Location = new System.Drawing.Point(4, 22);
            this.tab3PluginSettings.Name = "tab3PluginSettings";
            this.tab3PluginSettings.Padding = new System.Windows.Forms.Padding(3);
            this.tab3PluginSettings.Size = new System.Drawing.Size(740, 524);
            this.tab3PluginSettings.TabIndex = 2;
            this.tab3PluginSettings.Text = "Plugin Settings";
            this.tab3PluginSettings.UseVisualStyleBackColor = true;
            // 
            // updateCheckCheckBox
            // 
            this.updateCheckCheckBox.AutoSize = true;
            this.updateCheckCheckBox.Location = new System.Drawing.Point(235, 106);
            this.updateCheckCheckBox.Name = "updateCheckCheckBox";
            this.updateCheckCheckBox.Size = new System.Drawing.Size(172, 17);
            this.updateCheckCheckBox.TabIndex = 0;
            this.updateCheckCheckBox.Text = "Check For Updates On Startup";
            this.updateCheckCheckBox.UseVisualStyleBackColor = true;
            this.updateCheckCheckBox.CheckedChanged += new System.EventHandler(this.multiDiscSupportCheckBox_CheckedChanged);
            // 
            // NewConfigWindow
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(944, 613);
            this.Controls.Add(this.treeView1);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.sourceLink);
            this.Controls.Add(this.pluginLink);
            this.Controls.Add(this.forumLink);
            this.Controls.Add(this.versionLabel);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "NewConfigWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Archive Cache Manager";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ConfigWindow_FormClosed);
            this.tabControl1.ResumeLayout(false);
            this.tab1CacheSettings.ResumeLayout(false);
            this.tab1CacheSettings.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cacheDataGridView)).EndInit();
            this.tab2ExtractionSettings.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.extensionPriorityDataGridView)).EndInit();
            this.tab3PluginSettings.ResumeLayout(false);
            this.tab3PluginSettings.PerformLayout();
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
        private System.Windows.Forms.DataGridView extensionPriorityDataGridView;
        private System.Windows.Forms.DataGridView cacheDataGridView;
        private System.Windows.Forms.Button configureCacheButton;
        private System.Windows.Forms.Button deleteSelectedButton;
        private System.Windows.Forms.Button openInExplorerButton;
        private System.Windows.Forms.Button refreshButton;
        private System.Windows.Forms.TextBox cacheSummaryTextBox;
        private System.Windows.Forms.Button deleteAllButton;
        private System.Windows.Forms.CheckBox multiDiscSupportCheckBox;
        private System.Windows.Forms.CheckBox useGameIdM3uFilenameCheckBox;
        private System.Windows.Forms.CheckBox updateCheckCheckBox;
        private System.Windows.Forms.CheckBox smartExtractCheckBox;
        private StackPanel tabControl1;
        private System.Windows.Forms.TabPage tab1CacheSettings;
        private System.Windows.Forms.TabPage tab2ExtractionSettings;
        private System.Windows.Forms.TabPage tab3PluginSettings;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Emulator;
        private System.Windows.Forms.DataGridViewTextBoxColumn Platform;
        private System.Windows.Forms.DataGridViewTextBoxColumn Priority;
        private System.Windows.Forms.DataGridViewComboBoxColumn Action;
        private System.Windows.Forms.DataGridViewCheckBoxColumn MultiDisc;
        private System.Windows.Forms.DataGridViewComboBoxColumn GameIdM3u;
        private System.Windows.Forms.DataGridViewCheckBoxColumn SmartExtract;
        private System.Windows.Forms.DataGridViewTextBoxColumn ArchivePath;
        private System.Windows.Forms.DataGridViewTextBoxColumn Archive;
        private System.Windows.Forms.DataGridViewTextBoxColumn ArchivePlatform;
        private System.Windows.Forms.DataGridViewTextBoxColumn ArchiveSize;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Keep;
    }
}