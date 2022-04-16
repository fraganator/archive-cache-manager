
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
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Cache Settings");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("Extraction Settings");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("Smart Extract Settings");
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("Plugin Settings");
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NewConfigWindow));
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.openInExplorerButton = new System.Windows.Forms.Button();
            this.configureCacheButton = new System.Windows.Forms.Button();
            this.deleteAllButton = new System.Windows.Forms.Button();
            this.refreshButton = new System.Windows.Forms.Button();
            this.deleteSelectedButton = new System.Windows.Forms.Button();
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
            this.cacheSummaryTextBox = new System.Windows.Forms.RichTextBox();
            this.cacheDataGridView = new System.Windows.Forms.DataGridView();
            this.ArchivePath = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Archive = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ArchivePlatform = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ArchiveSize = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Keep = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.tab2ExtractionSettings = new System.Windows.Forms.TabPage();
            this.extractionSettingsTipLabel = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.deletePriorityButton = new System.Windows.Forms.Button();
            this.emulatorPlatformConfigDataGridView = new System.Windows.Forms.DataGridView();
            this.Emulator = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Platform = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Priority = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Action = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.LaunchPath = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.MultiDisc = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.M3uName = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.SmartExtract = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Chdman = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.DolphinTool = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.addPriorityButton = new System.Windows.Forms.Button();
            this.tab3SmartExtractSettings = new System.Windows.Forms.TabPage();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.metadataExtensions = new System.Windows.Forms.TextBox();
            this.cachePathLabel = new System.Windows.Forms.Label();
            this.standaloneExtensions = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tab4PluginSettings = new System.Windows.Forms.TabPage();
            this.label3 = new System.Windows.Forms.Label();
            this.updateCheckCheckBox = new System.Windows.Forms.CheckBox();
            this.tabControl1.SuspendLayout();
            this.tab1CacheSettings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cacheDataGridView)).BeginInit();
            this.tab2ExtractionSettings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.emulatorPlatformConfigDataGridView)).BeginInit();
            this.tab3SmartExtractSettings.SuspendLayout();
            this.tab4PluginSettings.SuspendLayout();
            this.SuspendLayout();
            // 
            // openInExplorerButton
            // 
            this.openInExplorerButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.openInExplorerButton.Image = global::ArchiveCacheManager.Resources.folder_horizontal_open;
            this.openInExplorerButton.Location = new System.Drawing.Point(576, 92);
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
            this.configureCacheButton.Location = new System.Drawing.Point(576, 58);
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
            this.treeView1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.treeView1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.treeView1.FullRowSelect = true;
            this.treeView1.HideSelection = false;
            this.treeView1.ItemHeight = 32;
            this.treeView1.Location = new System.Drawing.Point(12, 12);
            this.treeView1.Name = "treeView1";
            treeNode1.Name = "CacheSettings";
            treeNode1.Text = "Cache Settings";
            treeNode2.Name = "ExtractionSettings";
            treeNode2.Text = "Extraction Settings";
            treeNode3.Name = "SmartExtractSettings";
            treeNode3.Text = "Smart Extract Settings";
            treeNode4.Name = "PluginSettings";
            treeNode4.Text = "Plugin Settings";
            this.treeView1.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2,
            treeNode3,
            treeNode4});
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
            this.tabControl1.Controls.Add(this.tab3SmartExtractSettings);
            this.tabControl1.Controls.Add(this.tab4PluginSettings);
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
            this.cacheSummaryTextBox.Cursor = System.Windows.Forms.Cursors.Default;
            this.cacheSummaryTextBox.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cacheSummaryTextBox.Location = new System.Drawing.Point(9, 62);
            this.cacheSummaryTextBox.Name = "cacheSummaryTextBox";
            this.cacheSummaryTextBox.ReadOnly = true;
            this.cacheSummaryTextBox.Size = new System.Drawing.Size(561, 62);
            this.cacheSummaryTextBox.TabIndex = 20;
            this.cacheSummaryTextBox.Text = "";
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
            this.cacheDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.cacheDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ArchivePath,
            this.Archive,
            this.ArchivePlatform,
            this.ArchiveSize,
            this.Keep});
            this.cacheDataGridView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
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
            this.ArchivePath.ReadOnly = true;
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
            this.ArchivePlatform.ReadOnly = true;
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
            this.tab2ExtractionSettings.Controls.Add(this.extractionSettingsTipLabel);
            this.tab2ExtractionSettings.Controls.Add(this.label2);
            this.tab2ExtractionSettings.Controls.Add(this.deletePriorityButton);
            this.tab2ExtractionSettings.Controls.Add(this.emulatorPlatformConfigDataGridView);
            this.tab2ExtractionSettings.Controls.Add(this.addPriorityButton);
            this.tab2ExtractionSettings.Location = new System.Drawing.Point(4, 22);
            this.tab2ExtractionSettings.Name = "tab2ExtractionSettings";
            this.tab2ExtractionSettings.Padding = new System.Windows.Forms.Padding(3);
            this.tab2ExtractionSettings.Size = new System.Drawing.Size(740, 524);
            this.tab2ExtractionSettings.TabIndex = 1;
            this.tab2ExtractionSettings.Text = "Extraction Settings";
            this.tab2ExtractionSettings.UseVisualStyleBackColor = true;
            // 
            // extractionSettingsTipLabel
            // 
            this.extractionSettingsTipLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.extractionSettingsTipLabel.AutoSize = true;
            this.extractionSettingsTipLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.extractionSettingsTipLabel.Location = new System.Drawing.Point(6, 496);
            this.extractionSettingsTipLabel.Name = "extractionSettingsTipLabel";
            this.extractionSettingsTipLabel.Size = new System.Drawing.Size(392, 13);
            this.extractionSettingsTipLabel.TabIndex = 24;
            this.extractionSettingsTipLabel.Text = "Tip: Hover the mouse cursor over a column header for a description of the setting" +
    ".";
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
            this.deletePriorityButton.Location = new System.Drawing.Point(616, 488);
            this.deletePriorityButton.Name = "deletePriorityButton";
            this.deletePriorityButton.Size = new System.Drawing.Size(116, 28);
            this.deletePriorityButton.TabIndex = 0;
            this.deletePriorityButton.Text = "Delete";
            this.deletePriorityButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.deletePriorityButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.deletePriorityButton.UseVisualStyleBackColor = true;
            this.deletePriorityButton.Click += new System.EventHandler(this.deletePriorityButton_Click);
            // 
            // emulatorPlatformConfigDataGridView
            // 
            this.emulatorPlatformConfigDataGridView.AllowUserToAddRows = false;
            this.emulatorPlatformConfigDataGridView.AllowUserToDeleteRows = false;
            this.emulatorPlatformConfigDataGridView.AllowUserToResizeRows = false;
            this.emulatorPlatformConfigDataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.emulatorPlatformConfigDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.emulatorPlatformConfigDataGridView.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            this.emulatorPlatformConfigDataGridView.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.emulatorPlatformConfigDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.emulatorPlatformConfigDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Emulator,
            this.Platform,
            this.Priority,
            this.Action,
            this.LaunchPath,
            this.MultiDisc,
            this.M3uName,
            this.SmartExtract,
            this.Chdman,
            this.DolphinTool});
            this.emulatorPlatformConfigDataGridView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.emulatorPlatformConfigDataGridView.Location = new System.Drawing.Point(6, 62);
            this.emulatorPlatformConfigDataGridView.MultiSelect = false;
            this.emulatorPlatformConfigDataGridView.Name = "emulatorPlatformConfigDataGridView";
            this.emulatorPlatformConfigDataGridView.RowHeadersVisible = false;
            this.emulatorPlatformConfigDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.emulatorPlatformConfigDataGridView.Size = new System.Drawing.Size(726, 420);
            this.emulatorPlatformConfigDataGridView.StandardTab = true;
            this.emulatorPlatformConfigDataGridView.TabIndex = 13;
            this.emulatorPlatformConfigDataGridView.CellMouseEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.emulatorPlatformConfigDataGridView_CellMouseEnter);
            this.emulatorPlatformConfigDataGridView.CellMouseLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.emulatorPlatformConfigDataGridView_CellMouseLeave);
            this.emulatorPlatformConfigDataGridView.SelectionChanged += new System.EventHandler(this.extensionPriorityDataGridView_SelectionChanged);
            // 
            // Emulator
            // 
            this.Emulator.HeaderText = "Emulator";
            this.Emulator.MinimumWidth = 150;
            this.Emulator.Name = "Emulator";
            this.Emulator.ReadOnly = true;
            this.Emulator.Width = 150;
            // 
            // Platform
            // 
            this.Platform.HeaderText = "Platform";
            this.Platform.MinimumWidth = 150;
            this.Platform.Name = "Platform";
            this.Platform.ReadOnly = true;
            this.Platform.Width = 150;
            // 
            // Priority
            // 
            this.Priority.HeaderText = "Priority";
            this.Priority.MinimumWidth = 150;
            this.Priority.Name = "Priority";
            this.Priority.ToolTipText = "Filename \\ extension priority within an archive.";
            this.Priority.Width = 150;
            // 
            // Action
            // 
            this.Action.FillWeight = 50F;
            this.Action.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Action.HeaderText = "Action";
            this.Action.Items.AddRange(new object[] {
            "Extract",
            "Copy",
            "Extract or Copy"});
            this.Action.Name = "Action";
            this.Action.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Action.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Action.ToolTipText = "Extract archive files to the cache, extract archives or copy non-archive files to" +
    " the cache, or just copy files to the cache (even if they\'re archives).";
            this.Action.Width = 62;
            // 
            // LaunchPath
            // 
            this.LaunchPath.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.LaunchPath.HeaderText = "Launch Path";
            this.LaunchPath.Items.AddRange(new object[] {
            "Default",
            "Title",
            "Platform",
            "Emulator"});
            this.LaunchPath.Name = "LaunchPath";
            this.LaunchPath.ToolTipText = "Launch games from a common path within the cache. Useful for RetroArch common set" +
    "tings.";
            this.LaunchPath.Width = 74;
            // 
            // MultiDisc
            // 
            this.MultiDisc.FillWeight = 50F;
            this.MultiDisc.HeaderText = "Multi-Disc";
            this.MultiDisc.Name = "MultiDisc";
            this.MultiDisc.ToolTipText = "Cache all discs in a multi-disc game. Generates and launches an M3U file if suppo" +
    "rted by the emulator.";
            this.MultiDisc.Width = 59;
            // 
            // M3uName
            // 
            this.M3uName.FillWeight = 50F;
            this.M3uName.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.M3uName.HeaderText = "M3U Name";
            this.M3uName.Items.AddRange(new object[] {
            "Game ID",
            "Title + Version"});
            this.M3uName.MinimumWidth = 100;
            this.M3uName.Name = "M3uName";
            this.M3uName.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.M3uName.ToolTipText = "Name of the M3U file to save. Game ID is LaunchBox\'s default.";
            // 
            // SmartExtract
            // 
            this.SmartExtract.FillWeight = 50F;
            this.SmartExtract.HeaderText = "Smart Extract";
            this.SmartExtract.Name = "SmartExtract";
            this.SmartExtract.ToolTipText = "Only extract a single ROM from an archive if certain conditions are met.";
            this.SmartExtract.Width = 76;
            // 
            // Chdman
            // 
            this.Chdman.HeaderText = "chdman";
            this.Chdman.Name = "Chdman";
            this.Chdman.ToolTipText = "Extract CHD files to CUE+BIN files. chdman.exe must be saved in the ArchiveCacheM" +
    "anager\\Extractors folder.";
            this.Chdman.Width = 51;
            // 
            // DolphinTool
            // 
            this.DolphinTool.HeaderText = "DolphinTool";
            this.DolphinTool.Name = "DolphinTool";
            this.DolphinTool.ToolTipText = "Extract RVZ, WIA, and GCZ files to ISO files. DolphinTool.exe must be saved in th" +
    "e ArchiveCacheManager\\Extractors folder.";
            this.DolphinTool.Width = 70;
            // 
            // addPriorityButton
            // 
            this.addPriorityButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.addPriorityButton.Image = global::ArchiveCacheManager.Resources.plus;
            this.addPriorityButton.Location = new System.Drawing.Point(494, 488);
            this.addPriorityButton.Name = "addPriorityButton";
            this.addPriorityButton.Size = new System.Drawing.Size(116, 28);
            this.addPriorityButton.TabIndex = 0;
            this.addPriorityButton.Text = "Add...";
            this.addPriorityButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.addPriorityButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.addPriorityButton.UseVisualStyleBackColor = true;
            this.addPriorityButton.Click += new System.EventHandler(this.addPriorityButton_Click);
            // 
            // tab3SmartExtractSettings
            // 
            this.tab3SmartExtractSettings.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tab3SmartExtractSettings.Controls.Add(this.label6);
            this.tab3SmartExtractSettings.Controls.Add(this.label5);
            this.tab3SmartExtractSettings.Controls.Add(this.metadataExtensions);
            this.tab3SmartExtractSettings.Controls.Add(this.cachePathLabel);
            this.tab3SmartExtractSettings.Controls.Add(this.standaloneExtensions);
            this.tab3SmartExtractSettings.Controls.Add(this.label4);
            this.tab3SmartExtractSettings.Location = new System.Drawing.Point(4, 22);
            this.tab3SmartExtractSettings.Name = "tab3SmartExtractSettings";
            this.tab3SmartExtractSettings.Padding = new System.Windows.Forms.Padding(3);
            this.tab3SmartExtractSettings.Size = new System.Drawing.Size(740, 524);
            this.tab3SmartExtractSettings.TabIndex = 3;
            this.tab3SmartExtractSettings.Text = "Smart Extract Settings";
            this.tab3SmartExtractSettings.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(7, 174);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(472, 65);
            this.label6.TabIndex = 29;
            this.label6.Text = resources.GetString("label6.Text");
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(7, 117);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(109, 13);
            this.label5.TabIndex = 28;
            this.label5.Text = "Metadata Extensions:";
            // 
            // metadataExtensions
            // 
            this.metadataExtensions.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.metadataExtensions.Location = new System.Drawing.Point(10, 133);
            this.metadataExtensions.MaxLength = 260;
            this.metadataExtensions.Name = "metadataExtensions";
            this.metadataExtensions.Size = new System.Drawing.Size(721, 20);
            this.metadataExtensions.TabIndex = 27;
            // 
            // cachePathLabel
            // 
            this.cachePathLabel.AutoSize = true;
            this.cachePathLabel.Location = new System.Drawing.Point(7, 60);
            this.cachePathLabel.Name = "cachePathLabel";
            this.cachePathLabel.Size = new System.Drawing.Size(149, 13);
            this.cachePathLabel.TabIndex = 26;
            this.cachePathLabel.Text = "Stand-alone ROM Extensions:";
            // 
            // standaloneExtensions
            // 
            this.standaloneExtensions.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.standaloneExtensions.Location = new System.Drawing.Point(10, 76);
            this.standaloneExtensions.MaxLength = 260;
            this.standaloneExtensions.Name = "standaloneExtensions";
            this.standaloneExtensions.Size = new System.Drawing.Size(721, 20);
            this.standaloneExtensions.TabIndex = 25;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.BackColor = System.Drawing.SystemColors.ControlLight;
            this.label4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(6, 7);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(724, 43);
            this.label4.TabIndex = 24;
            this.label4.Text = "Smart Extract Settings";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tab4PluginSettings
            // 
            this.tab4PluginSettings.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tab4PluginSettings.Controls.Add(this.label3);
            this.tab4PluginSettings.Controls.Add(this.updateCheckCheckBox);
            this.tab4PluginSettings.Location = new System.Drawing.Point(4, 22);
            this.tab4PluginSettings.Name = "tab4PluginSettings";
            this.tab4PluginSettings.Padding = new System.Windows.Forms.Padding(3);
            this.tab4PluginSettings.Size = new System.Drawing.Size(740, 524);
            this.tab4PluginSettings.TabIndex = 2;
            this.tab4PluginSettings.Text = "Plugin Settings";
            this.tab4PluginSettings.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.BackColor = System.Drawing.SystemColors.ControlLight;
            this.label3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(6, 7);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(726, 43);
            this.label3.TabIndex = 24;
            this.label3.Text = "Plugin Settings";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // updateCheckCheckBox
            // 
            this.updateCheckCheckBox.AutoSize = true;
            this.updateCheckCheckBox.Location = new System.Drawing.Point(7, 62);
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
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
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
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(640, 480);
            this.Name = "NewConfigWindow";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Archive Cache Manager";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ConfigWindow_FormClosed);
            this.tabControl1.ResumeLayout(false);
            this.tab1CacheSettings.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cacheDataGridView)).EndInit();
            this.tab2ExtractionSettings.ResumeLayout(false);
            this.tab2ExtractionSettings.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.emulatorPlatformConfigDataGridView)).EndInit();
            this.tab3SmartExtractSettings.ResumeLayout(false);
            this.tab3SmartExtractSettings.PerformLayout();
            this.tab4PluginSettings.ResumeLayout(false);
            this.tab4PluginSettings.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.Label versionLabel;
        private System.Windows.Forms.Button deletePriorityButton;
        private System.Windows.Forms.Button addPriorityButton;
        private System.Windows.Forms.LinkLabel forumLink;
        private System.Windows.Forms.LinkLabel sourceLink;
        private System.Windows.Forms.LinkLabel pluginLink;
        private System.Windows.Forms.DataGridView emulatorPlatformConfigDataGridView;
        private System.Windows.Forms.DataGridView cacheDataGridView;
        private System.Windows.Forms.Button configureCacheButton;
        private System.Windows.Forms.Button deleteSelectedButton;
        private System.Windows.Forms.Button openInExplorerButton;
        private System.Windows.Forms.Button refreshButton;
        private System.Windows.Forms.RichTextBox cacheSummaryTextBox;
        private System.Windows.Forms.Button deleteAllButton;
        private System.Windows.Forms.CheckBox updateCheckCheckBox;
        private StackPanel tabControl1;
        private System.Windows.Forms.TabPage tab1CacheSettings;
        private System.Windows.Forms.TabPage tab2ExtractionSettings;
        private System.Windows.Forms.TabPage tab4PluginSettings;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn ArchivePath;
        private System.Windows.Forms.DataGridViewTextBoxColumn Archive;
        private System.Windows.Forms.DataGridViewTextBoxColumn ArchivePlatform;
        private System.Windows.Forms.DataGridViewTextBoxColumn ArchiveSize;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Keep;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label extractionSettingsTipLabel;
        private System.Windows.Forms.DataGridViewTextBoxColumn Emulator;
        private System.Windows.Forms.DataGridViewTextBoxColumn Platform;
        private System.Windows.Forms.DataGridViewTextBoxColumn Priority;
        private System.Windows.Forms.DataGridViewComboBoxColumn Action;
        private System.Windows.Forms.DataGridViewComboBoxColumn LaunchPath;
        private System.Windows.Forms.DataGridViewCheckBoxColumn MultiDisc;
        private System.Windows.Forms.DataGridViewComboBoxColumn M3uName;
        private System.Windows.Forms.DataGridViewCheckBoxColumn SmartExtract;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Chdman;
        private System.Windows.Forms.DataGridViewCheckBoxColumn DolphinTool;
        private System.Windows.Forms.TabPage tab3SmartExtractSettings;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox standaloneExtensions;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox metadataExtensions;
        private System.Windows.Forms.Label cachePathLabel;
    }
}