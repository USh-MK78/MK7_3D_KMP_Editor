namespace MK7_3D_KMP_Editor.EditorSettings
{
    partial class EditorSettingForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditorSettingForm));
            this.EditorSettingTabControl = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.DefaultNameKMP_TXT = new System.Windows.Forms.TextBox();
            this.DefaultNameXML_TXT = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.DefaultDirectoryTXT = new System.Windows.Forms.TextBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.DefaultObjectID_TXT = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.EditorSettingTabControl.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.SuspendLayout();
            // 
            // EditorSettingTabControl
            // 
            this.EditorSettingTabControl.Controls.Add(this.tabPage3);
            this.EditorSettingTabControl.Controls.Add(this.tabPage1);
            this.EditorSettingTabControl.Controls.Add(this.tabPage2);
            this.EditorSettingTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.EditorSettingTabControl.Location = new System.Drawing.Point(0, 0);
            this.EditorSettingTabControl.Name = "EditorSettingTabControl";
            this.EditorSettingTabControl.SelectedIndex = 0;
            this.EditorSettingTabControl.Size = new System.Drawing.Size(395, 372);
            this.EditorSettingTabControl.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.DefaultDirectoryTXT);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(387, 346);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "FilePath";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.DefaultNameKMP_TXT);
            this.groupBox1.Controls.Add(this.DefaultNameXML_TXT);
            this.groupBox1.Location = new System.Drawing.Point(6, 42);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(371, 76);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Default file name";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(126, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "Default KMP file name :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 46);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(125, 12);
            this.label3.TabIndex = 6;
            this.label3.Text = "Default XML file name :";
            // 
            // DefaultNameKMP_TXT
            // 
            this.DefaultNameKMP_TXT.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DefaultNameKMP_TXT.Location = new System.Drawing.Point(138, 18);
            this.DefaultNameKMP_TXT.Name = "DefaultNameKMP_TXT";
            this.DefaultNameKMP_TXT.Size = new System.Drawing.Size(225, 19);
            this.DefaultNameKMP_TXT.TabIndex = 3;
            // 
            // DefaultNameXML_TXT
            // 
            this.DefaultNameXML_TXT.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DefaultNameXML_TXT.Location = new System.Drawing.Point(138, 43);
            this.DefaultNameXML_TXT.Name = "DefaultNameXML_TXT";
            this.DefaultNameXML_TXT.Size = new System.Drawing.Size(225, 19);
            this.DefaultNameXML_TXT.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(97, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "Default directory :";
            // 
            // DefaultDirectoryTXT
            // 
            this.DefaultDirectoryTXT.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DefaultDirectoryTXT.Location = new System.Drawing.Point(116, 15);
            this.DefaultDirectoryTXT.Name = "DefaultDirectoryTXT";
            this.DefaultDirectoryTXT.Size = new System.Drawing.Size(253, 19);
            this.DefaultDirectoryTXT.TabIndex = 0;
            this.DefaultDirectoryTXT.KeyUp += new System.Windows.Forms.KeyEventHandler(this.DefaultDirectoryTXT_KeyUp);
            this.DefaultDirectoryTXT.Leave += new System.EventHandler(this.DefaultDirectoryTXT_Leave);
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(387, 346);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Theme";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.label5);
            this.tabPage3.Controls.Add(this.DefaultObjectID_TXT);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(387, 346);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "General";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // DefaultObjectID_TXT
            // 
            this.DefaultObjectID_TXT.Location = new System.Drawing.Point(110, 9);
            this.DefaultObjectID_TXT.Name = "DefaultObjectID_TXT";
            this.DefaultObjectID_TXT.Size = new System.Drawing.Size(76, 19);
            this.DefaultObjectID_TXT.TabIndex = 2;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(8, 12);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(96, 12);
            this.label5.TabIndex = 3;
            this.label5.Text = "Default ObjectID :";
            // 
            // EditorSettingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(395, 372);
            this.Controls.Add(this.EditorSettingTabControl);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "EditorSettingForm";
            this.Text = "EditorSettingForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.EditorSettingForm_FormClosing);
            this.Load += new System.EventHandler(this.EditorSettingForm_Load);
            this.EditorSettingTabControl.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl EditorSettingTabControl;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox DefaultDirectoryTXT;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox DefaultNameKMP_TXT;
        private System.Windows.Forms.TextBox DefaultNameXML_TXT;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox DefaultObjectID_TXT;
    }
}