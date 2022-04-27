
namespace ArchiveCacheManager
{
    partial class EmulatorPlatformSelectionWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EmulatorPlatformSelectionWindow));
            this.platformComboBox = new System.Windows.Forms.ComboBox();
            this.emulatorComboBox = new System.Windows.Forms.ComboBox();
            this.emulatorComboBoxLabel = new System.Windows.Forms.Label();
            this.platformComboBoxLabel = new System.Windows.Forms.Label();
            this.cancelButton = new System.Windows.Forms.Button();
            this.okButton = new System.Windows.Forms.Button();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.SuspendLayout();
            // 
            // platformComboBox
            // 
            this.platformComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.platformComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.platformComboBox.FormattingEnabled = true;
            this.platformComboBox.Location = new System.Drawing.Point(12, 77);
            this.platformComboBox.Name = "platformComboBox";
            this.platformComboBox.Size = new System.Drawing.Size(428, 21);
            this.platformComboBox.TabIndex = 3;
            this.toolTip.SetToolTip(this.platformComboBox, "Platform to apply extension priority to.");
            // 
            // emulatorComboBox
            // 
            this.emulatorComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.emulatorComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.emulatorComboBox.FormattingEnabled = true;
            this.emulatorComboBox.Location = new System.Drawing.Point(12, 28);
            this.emulatorComboBox.Name = "emulatorComboBox";
            this.emulatorComboBox.Size = new System.Drawing.Size(428, 21);
            this.emulatorComboBox.TabIndex = 2;
            this.toolTip.SetToolTip(this.emulatorComboBox, "Emulator to apply extension priority to.");
            this.emulatorComboBox.SelectionChangeCommitted += new System.EventHandler(this.emulatorComboBox_SelectionChangeCommitted);
            // 
            // emulatorComboBoxLabel
            // 
            this.emulatorComboBoxLabel.AutoSize = true;
            this.emulatorComboBoxLabel.Location = new System.Drawing.Point(9, 12);
            this.emulatorComboBoxLabel.Name = "emulatorComboBoxLabel";
            this.emulatorComboBoxLabel.Size = new System.Drawing.Size(51, 13);
            this.emulatorComboBoxLabel.TabIndex = 5;
            this.emulatorComboBoxLabel.Text = "Emulator:";
            // 
            // platformComboBoxLabel
            // 
            this.platformComboBoxLabel.AutoSize = true;
            this.platformComboBoxLabel.Location = new System.Drawing.Point(9, 61);
            this.platformComboBoxLabel.Name = "platformComboBoxLabel";
            this.platformComboBoxLabel.Size = new System.Drawing.Size(48, 13);
            this.platformComboBoxLabel.TabIndex = 6;
            this.platformComboBoxLabel.Text = "Platform:";
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Image = global::ArchiveCacheManager.Resources.cross_script;
            this.cancelButton.Location = new System.Drawing.Point(93, 117);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 1;
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
            this.okButton.Location = new System.Drawing.Point(12, 117);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 0;
            this.okButton.Text = "OK";
            this.okButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.okButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // EmulatorPlatformSelectionWindow
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(452, 152);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.platformComboBoxLabel);
            this.Controls.Add(this.emulatorComboBoxLabel);
            this.Controls.Add(this.emulatorComboBox);
            this.Controls.Add(this.platformComboBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "EmulatorPlatformSelectionWindow";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Emulator \\ Platform Selection";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox platformComboBox;
        private System.Windows.Forms.ComboBox emulatorComboBox;
        private System.Windows.Forms.Label emulatorComboBoxLabel;
        private System.Windows.Forms.Label platformComboBoxLabel;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.ToolTip toolTip;
    }
}