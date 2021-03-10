
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConfigWindow));
            this.cachePath = new System.Windows.Forms.TextBox();
            this.cachePathLabel = new System.Windows.Forms.Label();
            this.cachePathBrowseButton = new System.Windows.Forms.Button();
            this.cacheSize = new System.Windows.Forms.NumericUpDown();
            this.cacheSizeLabel = new System.Windows.Forms.Label();
            this.cacheSizeUnitLabel = new System.Windows.Forms.Label();
            this.minArchiveSize = new System.Windows.Forms.NumericUpDown();
            this.minArchiveSizeLabel = new System.Windows.Forms.Label();
            this.minArchiveSizeUnitLabel = new System.Windows.Forms.Label();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.extensionPriorityListView = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.versionLabel = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.extensionPriorityListViewLabel = new System.Windows.Forms.Label();
            this.editPriorityButton = new System.Windows.Forms.Button();
            this.deletePriorityButton = new System.Windows.Forms.Button();
            this.addPriorityButton = new System.Windows.Forms.Button();
            this.okButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.forumLink = new System.Windows.Forms.LinkLabel();
            this.sourceLink = new System.Windows.Forms.LinkLabel();
            this.pluginLink = new System.Windows.Forms.LinkLabel();
            ((System.ComponentModel.ISupportInitialize)(this.cacheSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.minArchiveSize)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cachePath
            // 
            this.cachePath.Location = new System.Drawing.Point(12, 25);
            this.cachePath.MaxLength = 260;
            this.cachePath.Name = "cachePath";
            this.cachePath.Size = new System.Drawing.Size(523, 20);
            this.cachePath.TabIndex = 2;
            this.toolTip.SetToolTip(this.cachePath, "Location of cache on disk. Path is relative to the root LaunchBox folder, or abso" +
        "lute.");
            this.cachePath.TextChanged += new System.EventHandler(this.cachePath_TextChanged);
            // 
            // cachePathLabel
            // 
            this.cachePathLabel.AutoSize = true;
            this.cachePathLabel.Location = new System.Drawing.Point(9, 9);
            this.cachePathLabel.Name = "cachePathLabel";
            this.cachePathLabel.Size = new System.Drawing.Size(66, 13);
            this.cachePathLabel.TabIndex = 1;
            this.cachePathLabel.Text = "Cache Path:";
            // 
            // cachePathBrowseButton
            // 
            this.cachePathBrowseButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cachePathBrowseButton.Image = global::ArchiveCacheManager.Resources.folder_horizontal_open;
            this.cachePathBrowseButton.Location = new System.Drawing.Point(548, 23);
            this.cachePathBrowseButton.Name = "cachePathBrowseButton";
            this.cachePathBrowseButton.Size = new System.Drawing.Size(97, 23);
            this.cachePathBrowseButton.TabIndex = 3;
            this.cachePathBrowseButton.Text = "Browse...";
            this.cachePathBrowseButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cachePathBrowseButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.cachePathBrowseButton.UseVisualStyleBackColor = true;
            this.cachePathBrowseButton.Click += new System.EventHandler(this.cachePathBrowseButton_Click);
            // 
            // cacheSize
            // 
            this.cacheSize.Location = new System.Drawing.Point(12, 73);
            this.cacheSize.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.cacheSize.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.cacheSize.Name = "cacheSize";
            this.cacheSize.Size = new System.Drawing.Size(120, 20);
            this.cacheSize.TabIndex = 4;
            this.cacheSize.ThousandsSeparator = true;
            this.toolTip.SetToolTip(this.cacheSize, "Maximum cache size on disk in megabytes.");
            this.cacheSize.Value = new decimal(new int[] {
            20000,
            0,
            0,
            0});
            // 
            // cacheSizeLabel
            // 
            this.cacheSizeLabel.AutoSize = true;
            this.cacheSizeLabel.Location = new System.Drawing.Point(9, 57);
            this.cacheSizeLabel.Name = "cacheSizeLabel";
            this.cacheSizeLabel.Size = new System.Drawing.Size(64, 13);
            this.cacheSizeLabel.TabIndex = 6;
            this.cacheSizeLabel.Text = "Cache Size:";
            // 
            // cacheSizeUnitLabel
            // 
            this.cacheSizeUnitLabel.AutoSize = true;
            this.cacheSizeUnitLabel.Location = new System.Drawing.Point(138, 75);
            this.cacheSizeUnitLabel.Name = "cacheSizeUnitLabel";
            this.cacheSizeUnitLabel.Size = new System.Drawing.Size(23, 13);
            this.cacheSizeUnitLabel.TabIndex = 7;
            this.cacheSizeUnitLabel.Text = "MB";
            // 
            // minArchiveSize
            // 
            this.minArchiveSize.Location = new System.Drawing.Point(12, 123);
            this.minArchiveSize.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.minArchiveSize.Name = "minArchiveSize";
            this.minArchiveSize.Size = new System.Drawing.Size(120, 20);
            this.minArchiveSize.TabIndex = 5;
            this.minArchiveSize.ThousandsSeparator = true;
            this.toolTip.SetToolTip(this.minArchiveSize, "Minimum archive size in megabytes. Only archives larger than this will be added t" +
        "o the cache.");
            this.minArchiveSize.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // minArchiveSizeLabel
            // 
            this.minArchiveSizeLabel.AutoSize = true;
            this.minArchiveSizeLabel.Location = new System.Drawing.Point(9, 107);
            this.minArchiveSizeLabel.Name = "minArchiveSizeLabel";
            this.minArchiveSizeLabel.Size = new System.Drawing.Size(113, 13);
            this.minArchiveSizeLabel.TabIndex = 6;
            this.minArchiveSizeLabel.Text = "Minimum Archive Size:";
            // 
            // minArchiveSizeUnitLabel
            // 
            this.minArchiveSizeUnitLabel.AutoSize = true;
            this.minArchiveSizeUnitLabel.Location = new System.Drawing.Point(138, 125);
            this.minArchiveSizeUnitLabel.Name = "minArchiveSizeUnitLabel";
            this.minArchiveSizeUnitLabel.Size = new System.Drawing.Size(23, 13);
            this.minArchiveSizeUnitLabel.TabIndex = 7;
            this.minArchiveSizeUnitLabel.Text = "MB";
            // 
            // extensionPriorityListView
            // 
            this.extensionPriorityListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
            this.extensionPriorityListView.FullRowSelect = true;
            this.extensionPriorityListView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.extensionPriorityListView.HideSelection = false;
            this.extensionPriorityListView.Location = new System.Drawing.Point(15, 25);
            this.extensionPriorityListView.MultiSelect = false;
            this.extensionPriorityListView.Name = "extensionPriorityListView";
            this.extensionPriorityListView.Size = new System.Drawing.Size(604, 189);
            this.extensionPriorityListView.TabIndex = 0;
            this.toolTip.SetToolTip(this.extensionPriorityListView, "The file extension priority applied to files within an archive. Primarily for bin" +
        " / cue / iso files.");
            this.extensionPriorityListView.UseCompatibleStateImageBehavior = false;
            this.extensionPriorityListView.View = System.Windows.Forms.View.Details;
            this.extensionPriorityListView.SelectedIndexChanged += new System.EventHandler(this.extensionPriorityListView_SelectedIndexChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Emulator";
            this.columnHeader1.Width = 160;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Platform";
            this.columnHeader2.Width = 160;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Priority";
            this.columnHeader3.Width = 250;
            // 
            // versionLabel
            // 
            this.versionLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.versionLabel.Location = new System.Drawing.Point(545, 443);
            this.versionLabel.Name = "versionLabel";
            this.versionLabel.Size = new System.Drawing.Size(100, 19);
            this.versionLabel.TabIndex = 8;
            this.versionLabel.Text = "v0.0.0";
            this.versionLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.Controls.Add(this.extensionPriorityListViewLabel);
            this.panel1.Controls.Add(this.extensionPriorityListView);
            this.panel1.Controls.Add(this.editPriorityButton);
            this.panel1.Controls.Add(this.deletePriorityButton);
            this.panel1.Controls.Add(this.addPriorityButton);
            this.panel1.Location = new System.Drawing.Point(12, 159);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(633, 266);
            this.panel1.TabIndex = 9;
            // 
            // extensionPriorityListViewLabel
            // 
            this.extensionPriorityListViewLabel.AutoSize = true;
            this.extensionPriorityListViewLabel.Location = new System.Drawing.Point(12, 9);
            this.extensionPriorityListViewLabel.Name = "extensionPriorityListViewLabel";
            this.extensionPriorityListViewLabel.Size = new System.Drawing.Size(109, 13);
            this.extensionPriorityListViewLabel.TabIndex = 1;
            this.extensionPriorityListViewLabel.Text = "File Extension Priority:";
            // 
            // editPriorityButton
            // 
            this.editPriorityButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.editPriorityButton.Image = global::ArchiveCacheManager.Resources.pencil;
            this.editPriorityButton.Location = new System.Drawing.Point(96, 231);
            this.editPriorityButton.Name = "editPriorityButton";
            this.editPriorityButton.Size = new System.Drawing.Size(75, 23);
            this.editPriorityButton.TabIndex = 0;
            this.editPriorityButton.Text = "Edit...";
            this.editPriorityButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.editPriorityButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.editPriorityButton.UseVisualStyleBackColor = true;
            this.editPriorityButton.Click += new System.EventHandler(this.editPriorityButton_Click);
            // 
            // deletePriorityButton
            // 
            this.deletePriorityButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.deletePriorityButton.BackColor = System.Drawing.SystemColors.ControlLight;
            this.deletePriorityButton.Image = global::ArchiveCacheManager.Resources.cross_script;
            this.deletePriorityButton.Location = new System.Drawing.Point(177, 231);
            this.deletePriorityButton.Name = "deletePriorityButton";
            this.deletePriorityButton.Size = new System.Drawing.Size(75, 23);
            this.deletePriorityButton.TabIndex = 0;
            this.deletePriorityButton.Text = "Delete";
            this.deletePriorityButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.deletePriorityButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.deletePriorityButton.UseVisualStyleBackColor = true;
            this.deletePriorityButton.Click += new System.EventHandler(this.deletePriorityButton_Click);
            // 
            // addPriorityButton
            // 
            this.addPriorityButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.addPriorityButton.Image = global::ArchiveCacheManager.Resources.plus;
            this.addPriorityButton.Location = new System.Drawing.Point(15, 231);
            this.addPriorityButton.Name = "addPriorityButton";
            this.addPriorityButton.Size = new System.Drawing.Size(75, 23);
            this.addPriorityButton.TabIndex = 0;
            this.addPriorityButton.Text = "Add...";
            this.addPriorityButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.addPriorityButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.addPriorityButton.UseVisualStyleBackColor = true;
            this.addPriorityButton.Click += new System.EventHandler(this.addPriorityButton_Click);
            // 
            // okButton
            // 
            this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.okButton.Image = global::ArchiveCacheManager.Resources.tick;
            this.okButton.Location = new System.Drawing.Point(12, 441);
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
            this.cancelButton.Location = new System.Drawing.Point(93, 441);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 1;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cancelButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // forumLink
            // 
            this.forumLink.AutoSize = true;
            this.forumLink.Location = new System.Drawing.Point(360, 446);
            this.forumLink.Name = "forumLink";
            this.forumLink.Size = new System.Drawing.Size(76, 13);
            this.forumLink.TabIndex = 10;
            this.forumLink.TabStop = true;
            this.forumLink.Text = "Forum Support";
            this.forumLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.forumLink_LinkClicked);
            // 
            // sourceLink
            // 
            this.sourceLink.AutoSize = true;
            this.sourceLink.Location = new System.Drawing.Point(442, 446);
            this.sourceLink.Name = "sourceLink";
            this.sourceLink.Size = new System.Drawing.Size(93, 13);
            this.sourceLink.TabIndex = 11;
            this.sourceLink.TabStop = true;
            this.sourceLink.Text = "GitHub Repository";
            this.sourceLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.sourceLink_LinkClicked);
            // 
            // pluginLink
            // 
            this.pluginLink.AutoSize = true;
            this.pluginLink.Location = new System.Drawing.Point(263, 446);
            this.pluginLink.Name = "pluginLink";
            this.pluginLink.Size = new System.Drawing.Size(91, 13);
            this.pluginLink.TabIndex = 10;
            this.pluginLink.TabStop = true;
            this.pluginLink.Text = "Plugin Homepage";
            this.pluginLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.pluginLink_LinkClicked);
            // 
            // ConfigWindow
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(657, 476);
            this.Controls.Add(this.sourceLink);
            this.Controls.Add(this.pluginLink);
            this.Controls.Add(this.forumLink);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.versionLabel);
            this.Controls.Add(this.minArchiveSizeUnitLabel);
            this.Controls.Add(this.cacheSizeUnitLabel);
            this.Controls.Add(this.minArchiveSizeLabel);
            this.Controls.Add(this.cacheSizeLabel);
            this.Controls.Add(this.minArchiveSize);
            this.Controls.Add(this.cacheSize);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.cachePathBrowseButton);
            this.Controls.Add(this.cachePathLabel);
            this.Controls.Add(this.cachePath);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ConfigWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Archive Cache Manager";
            ((System.ComponentModel.ISupportInitialize)(this.cacheSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.minArchiveSize)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox cachePath;
        private System.Windows.Forms.Label cachePathLabel;
        private System.Windows.Forms.Button cachePathBrowseButton;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.NumericUpDown cacheSize;
        private System.Windows.Forms.Label cacheSizeLabel;
        private System.Windows.Forms.Label cacheSizeUnitLabel;
        private System.Windows.Forms.NumericUpDown minArchiveSize;
        private System.Windows.Forms.Label minArchiveSizeLabel;
        private System.Windows.Forms.Label minArchiveSizeUnitLabel;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.Label versionLabel;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ListView extensionPriorityListView;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.Button editPriorityButton;
        private System.Windows.Forms.Button deletePriorityButton;
        private System.Windows.Forms.Button addPriorityButton;
        private System.Windows.Forms.LinkLabel forumLink;
        private System.Windows.Forms.LinkLabel sourceLink;
        private System.Windows.Forms.Label extensionPriorityListViewLabel;
        private System.Windows.Forms.LinkLabel pluginLink;
    }
}