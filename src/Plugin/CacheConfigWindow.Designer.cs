
namespace ArchiveCacheManager
{
    partial class CacheConfigWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CacheConfigWindow));
            this.minArchiveSizeUnitLabel = new System.Windows.Forms.Label();
            this.cacheSizeUnitLabel = new System.Windows.Forms.Label();
            this.minArchiveSizeLabel = new System.Windows.Forms.Label();
            this.cacheSizeLabel = new System.Windows.Forms.Label();
            this.minArchiveSize = new System.Windows.Forms.NumericUpDown();
            this.cacheSize = new System.Windows.Forms.NumericUpDown();
            this.cachePath = new System.Windows.Forms.TextBox();
            this.cachePathLabel = new System.Windows.Forms.Label();
            this.okButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.cachePathBrowseButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.minArchiveSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cacheSize)).BeginInit();
            this.SuspendLayout();
            // 
            // minArchiveSizeUnitLabel
            // 
            this.minArchiveSizeUnitLabel.AutoSize = true;
            this.minArchiveSizeUnitLabel.Location = new System.Drawing.Point(315, 75);
            this.minArchiveSizeUnitLabel.Name = "minArchiveSizeUnitLabel";
            this.minArchiveSizeUnitLabel.Size = new System.Drawing.Size(23, 13);
            this.minArchiveSizeUnitLabel.TabIndex = 14;
            this.minArchiveSizeUnitLabel.Text = "MB";
            // 
            // cacheSizeUnitLabel
            // 
            this.cacheSizeUnitLabel.AutoSize = true;
            this.cacheSizeUnitLabel.Location = new System.Drawing.Point(138, 75);
            this.cacheSizeUnitLabel.Name = "cacheSizeUnitLabel";
            this.cacheSizeUnitLabel.Size = new System.Drawing.Size(23, 13);
            this.cacheSizeUnitLabel.TabIndex = 15;
            this.cacheSizeUnitLabel.Text = "MB";
            // 
            // minArchiveSizeLabel
            // 
            this.minArchiveSizeLabel.AutoSize = true;
            this.minArchiveSizeLabel.Location = new System.Drawing.Point(186, 57);
            this.minArchiveSizeLabel.Name = "minArchiveSizeLabel";
            this.minArchiveSizeLabel.Size = new System.Drawing.Size(113, 13);
            this.minArchiveSizeLabel.TabIndex = 12;
            this.minArchiveSizeLabel.Text = "Minimum Archive Size:";
            // 
            // cacheSizeLabel
            // 
            this.cacheSizeLabel.AutoSize = true;
            this.cacheSizeLabel.Location = new System.Drawing.Point(9, 57);
            this.cacheSizeLabel.Name = "cacheSizeLabel";
            this.cacheSizeLabel.Size = new System.Drawing.Size(64, 13);
            this.cacheSizeLabel.TabIndex = 13;
            this.cacheSizeLabel.Text = "Cache Size:";
            // 
            // minArchiveSize
            // 
            this.minArchiveSize.Location = new System.Drawing.Point(189, 73);
            this.minArchiveSize.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.minArchiveSize.Name = "minArchiveSize";
            this.minArchiveSize.Size = new System.Drawing.Size(120, 20);
            this.minArchiveSize.TabIndex = 11;
            this.minArchiveSize.ThousandsSeparator = true;
            this.minArchiveSize.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
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
            this.cacheSize.TabIndex = 10;
            this.cacheSize.ThousandsSeparator = true;
            this.cacheSize.Value = new decimal(new int[] {
            20000,
            0,
            0,
            0});
            // 
            // cachePath
            // 
            this.cachePath.Location = new System.Drawing.Point(12, 25);
            this.cachePath.MaxLength = 260;
            this.cachePath.Name = "cachePath";
            this.cachePath.Size = new System.Drawing.Size(497, 20);
            this.cachePath.TabIndex = 8;
            this.cachePath.TextChanged += new System.EventHandler(this.cachePath_TextChanged);
            // 
            // cachePathLabel
            // 
            this.cachePathLabel.AutoSize = true;
            this.cachePathLabel.Location = new System.Drawing.Point(9, 9);
            this.cachePathLabel.Name = "cachePathLabel";
            this.cachePathLabel.Size = new System.Drawing.Size(66, 13);
            this.cachePathLabel.TabIndex = 16;
            this.cachePathLabel.Text = "Cache Path:";
            // 
            // okButton
            // 
            this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.okButton.Image = global::ArchiveCacheManager.Resources.tick;
            this.okButton.Location = new System.Drawing.Point(12, 115);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 2;
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
            this.cancelButton.Location = new System.Drawing.Point(93, 115);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 3;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cancelButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // cachePathBrowseButton
            // 
            this.cachePathBrowseButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cachePathBrowseButton.Image = global::ArchiveCacheManager.Resources.folder_horizontal_open;
            this.cachePathBrowseButton.Location = new System.Drawing.Point(515, 23);
            this.cachePathBrowseButton.Name = "cachePathBrowseButton";
            this.cachePathBrowseButton.Size = new System.Drawing.Size(97, 23);
            this.cachePathBrowseButton.TabIndex = 9;
            this.cachePathBrowseButton.Text = "Browse...";
            this.cachePathBrowseButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cachePathBrowseButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.cachePathBrowseButton.UseVisualStyleBackColor = true;
            this.cachePathBrowseButton.Click += new System.EventHandler(this.cachePathBrowseButton_Click);
            // 
            // CacheConfigWindow
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(624, 150);
            this.Controls.Add(this.cachePathLabel);
            this.Controls.Add(this.minArchiveSizeUnitLabel);
            this.Controls.Add(this.cacheSizeUnitLabel);
            this.Controls.Add(this.minArchiveSizeLabel);
            this.Controls.Add(this.cacheSizeLabel);
            this.Controls.Add(this.minArchiveSize);
            this.Controls.Add(this.cacheSize);
            this.Controls.Add(this.cachePathBrowseButton);
            this.Controls.Add(this.cachePath);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CacheConfigWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Cache Configuration";
            ((System.ComponentModel.ISupportInitialize)(this.minArchiveSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cacheSize)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Label minArchiveSizeUnitLabel;
        private System.Windows.Forms.Label cacheSizeUnitLabel;
        private System.Windows.Forms.Label minArchiveSizeLabel;
        private System.Windows.Forms.Label cacheSizeLabel;
        private System.Windows.Forms.NumericUpDown minArchiveSize;
        private System.Windows.Forms.NumericUpDown cacheSize;
        private System.Windows.Forms.Button cachePathBrowseButton;
        private System.Windows.Forms.TextBox cachePath;
        private System.Windows.Forms.Label cachePathLabel;
    }
}