
namespace ArchiveCacheManager
{
    partial class PriorityEditWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PriorityEditWindow));
            this.platformComboBox = new System.Windows.Forms.ComboBox();
            this.emulatorComboBox = new System.Windows.Forms.ComboBox();
            this.extensionPriorityTextBox = new System.Windows.Forms.TextBox();
            this.emulatorComboBoxLabel = new System.Windows.Forms.Label();
            this.platformComboBoxLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cancelButton = new System.Windows.Forms.Button();
            this.okButton = new System.Windows.Forms.Button();
            this.infoLabel = new System.Windows.Forms.Label();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.SuspendLayout();
            // 
            // platformComboBox
            // 
            this.platformComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.platformComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.platformComboBox.FormattingEnabled = true;
            this.platformComboBox.Location = new System.Drawing.Point(12, 384);
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
            this.emulatorComboBox.Location = new System.Drawing.Point(12, 335);
            this.emulatorComboBox.Name = "emulatorComboBox";
            this.emulatorComboBox.Size = new System.Drawing.Size(428, 21);
            this.emulatorComboBox.TabIndex = 2;
            this.toolTip.SetToolTip(this.emulatorComboBox, "Emulator to apply extension priority to.");
            this.emulatorComboBox.SelectionChangeCommitted += new System.EventHandler(this.emulatorComboBox_SelectionChangeCommitted);
            // 
            // extensionPriorityTextBox
            // 
            this.extensionPriorityTextBox.Location = new System.Drawing.Point(12, 433);
            this.extensionPriorityTextBox.Name = "extensionPriorityTextBox";
            this.extensionPriorityTextBox.Size = new System.Drawing.Size(428, 20);
            this.extensionPriorityTextBox.TabIndex = 4;
            this.toolTip.SetToolTip(this.extensionPriorityTextBox, "Comma delimited list of file extensions. The first extension is highest priority," +
        " the second is next priority, and so on.");
            this.extensionPriorityTextBox.TextChanged += new System.EventHandler(this.extensionPriorityTextBox_TextChanged);
            // 
            // emulatorComboBoxLabel
            // 
            this.emulatorComboBoxLabel.AutoSize = true;
            this.emulatorComboBoxLabel.Location = new System.Drawing.Point(9, 319);
            this.emulatorComboBoxLabel.Name = "emulatorComboBoxLabel";
            this.emulatorComboBoxLabel.Size = new System.Drawing.Size(51, 13);
            this.emulatorComboBoxLabel.TabIndex = 5;
            this.emulatorComboBoxLabel.Text = "Emulator:";
            // 
            // platformComboBoxLabel
            // 
            this.platformComboBoxLabel.AutoSize = true;
            this.platformComboBoxLabel.Location = new System.Drawing.Point(9, 368);
            this.platformComboBoxLabel.Name = "platformComboBoxLabel";
            this.platformComboBoxLabel.Size = new System.Drawing.Size(48, 13);
            this.platformComboBoxLabel.TabIndex = 6;
            this.platformComboBoxLabel.Text = "Platform:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 417);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(149, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Filename Priority (e.g. bin, iso):";
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Image = global::ArchiveCacheManager.Resources.cross_script;
            this.cancelButton.Location = new System.Drawing.Point(93, 484);
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
            this.okButton.Location = new System.Drawing.Point(12, 484);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 0;
            this.okButton.Text = "OK";
            this.okButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.okButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // infoLabel
            // 
            this.infoLabel.Location = new System.Drawing.Point(9, 9);
            this.infoLabel.Name = "infoLabel";
            this.infoLabel.Size = new System.Drawing.Size(431, 310);
            this.infoLabel.TabIndex = 10;
            this.infoLabel.Text = resources.GetString("infoLabel.Text");
            // 
            // PriorityEditWindow
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(452, 519);
            this.Controls.Add(this.infoLabel);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.platformComboBoxLabel);
            this.Controls.Add(this.emulatorComboBoxLabel);
            this.Controls.Add(this.extensionPriorityTextBox);
            this.Controls.Add(this.emulatorComboBox);
            this.Controls.Add(this.platformComboBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PriorityEditWindow";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Priority Editor";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox platformComboBox;
        private System.Windows.Forms.ComboBox emulatorComboBox;
        private System.Windows.Forms.TextBox extensionPriorityTextBox;
        private System.Windows.Forms.Label emulatorComboBoxLabel;
        private System.Windows.Forms.Label platformComboBoxLabel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Label infoLabel;
        private System.Windows.Forms.ToolTip toolTip;
    }
}