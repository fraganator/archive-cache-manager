
namespace ArchiveCacheManager
{
    partial class ArchiveListWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ArchiveListWindow));
            this.cancelButton = new System.Windows.Forms.Button();
            this.okButton = new System.Windows.Forms.Button();
            this.archiveNameLabel = new System.Windows.Forms.Label();
            this.emulatorComboBox = new System.Windows.Forms.ComboBox();
            this.emulatorComboBoxLabel = new System.Windows.Forms.Label();
            this.fileListGridView = new System.Windows.Forms.DataGridView();
            this.File = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.fileListGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Image = global::ArchiveCacheManager.Resources.cross_script;
            this.cancelButton.Location = new System.Drawing.Point(93, 413);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 28);
            this.cancelButton.TabIndex = 2;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cancelButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // okButton
            // 
            this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okButton.Image = global::ArchiveCacheManager.Resources.tick;
            this.okButton.Location = new System.Drawing.Point(12, 413);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 28);
            this.okButton.TabIndex = 1;
            this.okButton.Text = "Play!";
            this.okButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.okButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // archiveNameLabel
            // 
            this.archiveNameLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.archiveNameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.archiveNameLabel.Location = new System.Drawing.Point(9, 9);
            this.archiveNameLabel.Name = "archiveNameLabel";
            this.archiveNameLabel.Size = new System.Drawing.Size(522, 21);
            this.archiveNameLabel.TabIndex = 4;
            this.archiveNameLabel.Text = "Game.zip";
            this.archiveNameLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // emulatorComboBox
            // 
            this.emulatorComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.emulatorComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.emulatorComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.emulatorComboBox.FormattingEnabled = true;
            this.emulatorComboBox.Location = new System.Drawing.Point(304, 418);
            this.emulatorComboBox.Name = "emulatorComboBox";
            this.emulatorComboBox.Size = new System.Drawing.Size(228, 21);
            this.emulatorComboBox.TabIndex = 5;
            // 
            // emulatorComboBoxLabel
            // 
            this.emulatorComboBoxLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.emulatorComboBoxLabel.AutoSize = true;
            this.emulatorComboBoxLabel.Location = new System.Drawing.Point(243, 421);
            this.emulatorComboBoxLabel.Name = "emulatorComboBoxLabel";
            this.emulatorComboBoxLabel.Size = new System.Drawing.Size(51, 13);
            this.emulatorComboBoxLabel.TabIndex = 6;
            this.emulatorComboBoxLabel.Text = "Emulator:";
            // 
            // fileListGridView
            // 
            this.fileListGridView.AllowUserToAddRows = false;
            this.fileListGridView.AllowUserToDeleteRows = false;
            this.fileListGridView.AllowUserToResizeRows = false;
            this.fileListGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.fileListGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.fileListGridView.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            this.fileListGridView.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.Padding = new System.Windows.Forms.Padding(0, 3, 0, 3);
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.fileListGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.fileListGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.fileListGridView.ColumnHeadersVisible = false;
            this.fileListGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.File});
            this.fileListGridView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.fileListGridView.Location = new System.Drawing.Point(12, 33);
            this.fileListGridView.MultiSelect = false;
            this.fileListGridView.Name = "fileListGridView";
            this.fileListGridView.RowHeadersVisible = false;
            this.fileListGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.fileListGridView.Size = new System.Drawing.Size(520, 374);
            this.fileListGridView.StandardTab = true;
            this.fileListGridView.TabIndex = 9;
            // 
            // File
            // 
            dataGridViewCellStyle2.Padding = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.File.DefaultCellStyle = dataGridViewCellStyle2;
            this.File.HeaderText = "File";
            this.File.Name = "File";
            this.File.ReadOnly = true;
            // 
            // ArchiveListWindow
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(544, 453);
            this.Controls.Add(this.fileListGridView);
            this.Controls.Add(this.emulatorComboBox);
            this.Controls.Add(this.emulatorComboBoxLabel);
            this.Controls.Add(this.archiveNameLabel);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okButton);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(500, 200);
            this.Name = "ArchiveListWindow";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Select File";
            ((System.ComponentModel.ISupportInitialize)(this.fileListGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Label archiveNameLabel;
        private System.Windows.Forms.ComboBox emulatorComboBox;
        private System.Windows.Forms.Label emulatorComboBoxLabel;
        private System.Windows.Forms.DataGridView fileListGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn File;
    }
}