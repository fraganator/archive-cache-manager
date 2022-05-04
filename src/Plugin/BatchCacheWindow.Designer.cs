
namespace ArchiveCacheManager
{
    partial class BatchCacheWindow
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BatchCacheWindow));
            this.cacheStatusGridView = new System.Windows.Forms.DataGridView();
            this.Index = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GameId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AppId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ArchivePath = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Archive = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ArchivePlatform = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ArchiveSize = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ArchiveSizeMb = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CacheAction = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CacheStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.closeButton = new System.Windows.Forms.Button();
            this.progressBar = new ArchiveCacheManager.ProgressBarFlat();
            this.stopButton = new System.Windows.Forms.Button();
            this.cacheButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.cacheStatusGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // cacheStatusGridView
            // 
            this.cacheStatusGridView.AllowUserToAddRows = false;
            this.cacheStatusGridView.AllowUserToDeleteRows = false;
            this.cacheStatusGridView.AllowUserToResizeRows = false;
            this.cacheStatusGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cacheStatusGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.cacheStatusGridView.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            this.cacheStatusGridView.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.Padding = new System.Windows.Forms.Padding(0, 3, 0, 3);
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.cacheStatusGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.cacheStatusGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.cacheStatusGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Index,
            this.GameId,
            this.AppId,
            this.ArchivePath,
            this.Archive,
            this.ArchivePlatform,
            this.ArchiveSize,
            this.ArchiveSizeMb,
            this.CacheAction,
            this.CacheStatus});
            this.cacheStatusGridView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.cacheStatusGridView.Location = new System.Drawing.Point(12, 12);
            this.cacheStatusGridView.MultiSelect = false;
            this.cacheStatusGridView.Name = "cacheStatusGridView";
            this.cacheStatusGridView.RowHeadersVisible = false;
            this.cacheStatusGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.cacheStatusGridView.Size = new System.Drawing.Size(920, 374);
            this.cacheStatusGridView.StandardTab = true;
            this.cacheStatusGridView.TabIndex = 8;
            this.cacheStatusGridView.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.cacheStatusGridView_CellPainting);
            // 
            // Index
            // 
            this.Index.HeaderText = "Index";
            this.Index.Name = "Index";
            this.Index.Visible = false;
            this.Index.Width = 39;
            // 
            // GameId
            // 
            this.GameId.HeaderText = "GameId";
            this.GameId.Name = "GameId";
            this.GameId.Visible = false;
            this.GameId.Width = 50;
            // 
            // AppId
            // 
            this.AppId.HeaderText = "AppId";
            this.AppId.Name = "AppId";
            this.AppId.Visible = false;
            this.AppId.Width = 41;
            // 
            // ArchivePath
            // 
            this.ArchivePath.HeaderText = "Path";
            this.ArchivePath.Name = "ArchivePath";
            this.ArchivePath.Visible = false;
            this.ArchivePath.Width = 35;
            // 
            // Archive
            // 
            this.Archive.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle2.Padding = new System.Windows.Forms.Padding(24, 0, 3, 0);
            this.Archive.DefaultCellStyle = dataGridViewCellStyle2;
            this.Archive.HeaderText = "Archive";
            this.Archive.MinimumWidth = 300;
            this.Archive.Name = "Archive";
            this.Archive.ReadOnly = true;
            this.Archive.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // ArchivePlatform
            // 
            this.ArchivePlatform.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ArchivePlatform.FillWeight = 60F;
            this.ArchivePlatform.HeaderText = "Platform";
            this.ArchivePlatform.MinimumWidth = 150;
            this.ArchivePlatform.Name = "ArchivePlatform";
            this.ArchivePlatform.ReadOnly = true;
            this.ArchivePlatform.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // ArchiveSize
            // 
            this.ArchiveSize.HeaderText = "Size";
            this.ArchiveSize.Name = "ArchiveSize";
            this.ArchiveSize.Visible = false;
            this.ArchiveSize.Width = 52;
            // 
            // ArchiveSizeMb
            // 
            this.ArchiveSizeMb.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle3.Format = "N1";
            this.ArchiveSizeMb.DefaultCellStyle = dataGridViewCellStyle3;
            this.ArchiveSizeMb.HeaderText = "Size (MB)";
            this.ArchiveSizeMb.Name = "ArchiveSizeMb";
            this.ArchiveSizeMb.ReadOnly = true;
            this.ArchiveSizeMb.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ArchiveSizeMb.Width = 58;
            // 
            // CacheAction
            // 
            this.CacheAction.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.CacheAction.HeaderText = "Cache Action";
            this.CacheAction.Name = "CacheAction";
            this.CacheAction.ReadOnly = true;
            this.CacheAction.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.CacheAction.Width = 77;
            // 
            // CacheStatus
            // 
            this.CacheStatus.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle4.Padding = new System.Windows.Forms.Padding(24, 0, 3, 0);
            this.CacheStatus.DefaultCellStyle = dataGridViewCellStyle4;
            this.CacheStatus.FillWeight = 60F;
            this.CacheStatus.HeaderText = "Status";
            this.CacheStatus.MinimumWidth = 220;
            this.CacheStatus.Name = "CacheStatus";
            this.CacheStatus.ReadOnly = true;
            this.CacheStatus.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // closeButton
            // 
            this.closeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.closeButton.Location = new System.Drawing.Point(804, 401);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(128, 28);
            this.closeButton.TabIndex = 12;
            this.closeButton.Text = "Close";
            this.closeButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.closeButton.UseVisualStyleBackColor = true;
            this.closeButton.Click += new System.EventHandler(this.closeButton_Click);
            // 
            // progressBar
            // 
            this.progressBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar.Location = new System.Drawing.Point(12, 385);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(920, 10);
            this.progressBar.Step = 1;
            this.progressBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progressBar.TabIndex = 11;
            this.progressBar.Visible = false;
            // 
            // stopButton
            // 
            this.stopButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.stopButton.Image = global::ArchiveCacheManager.Resources.cross_octagon;
            this.stopButton.Location = new System.Drawing.Point(146, 401);
            this.stopButton.Name = "stopButton";
            this.stopButton.Size = new System.Drawing.Size(128, 28);
            this.stopButton.TabIndex = 10;
            this.stopButton.Text = "Stop";
            this.stopButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.stopButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.stopButton.UseVisualStyleBackColor = true;
            this.stopButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // cacheButton
            // 
            this.cacheButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cacheButton.Image = global::ArchiveCacheManager.Resources.box__plus;
            this.cacheButton.Location = new System.Drawing.Point(12, 401);
            this.cacheButton.Name = "cacheButton";
            this.cacheButton.Size = new System.Drawing.Size(128, 28);
            this.cacheButton.TabIndex = 9;
            this.cacheButton.Text = "Cache Games";
            this.cacheButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cacheButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.cacheButton.UseVisualStyleBackColor = true;
            this.cacheButton.Click += new System.EventHandler(this.cacheButton_Click);
            // 
            // BatchCacheWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(944, 441);
            this.Controls.Add(this.closeButton);
            this.Controls.Add(this.stopButton);
            this.Controls.Add(this.cacheButton);
            this.Controls.Add(this.cacheStatusGridView);
            this.Controls.Add(this.progressBar);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(480, 180);
            this.Name = "BatchCacheWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Batch Cache Games";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.BatchCacheWindow_FormClosing);
            this.Shown += new System.EventHandler(this.BatchCacheWindow_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.cacheStatusGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView cacheStatusGridView;
        private System.Windows.Forms.Button stopButton;
        private System.Windows.Forms.Button cacheButton;
        private ProgressBarFlat progressBar;
        private System.Windows.Forms.DataGridViewTextBoxColumn Index;
        private System.Windows.Forms.DataGridViewTextBoxColumn GameId;
        private System.Windows.Forms.DataGridViewTextBoxColumn AppId;
        private System.Windows.Forms.DataGridViewTextBoxColumn ArchivePath;
        private System.Windows.Forms.DataGridViewTextBoxColumn Archive;
        private System.Windows.Forms.DataGridViewTextBoxColumn ArchivePlatform;
        private System.Windows.Forms.DataGridViewTextBoxColumn ArchiveSize;
        private System.Windows.Forms.DataGridViewTextBoxColumn ArchiveSizeMb;
        private System.Windows.Forms.DataGridViewTextBoxColumn CacheAction;
        private System.Windows.Forms.DataGridViewTextBoxColumn CacheStatus;
        private System.Windows.Forms.Button closeButton;
    }
}