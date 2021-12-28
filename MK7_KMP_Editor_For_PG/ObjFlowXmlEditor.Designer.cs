
namespace MK7_KMP_Editor_For_PG_
{
    partial class ObjFlowXmlEditor
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
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.propertyGrid1 = new System.Windows.Forms.PropertyGrid();
            this.AddObjFlowXmlData = new System.Windows.Forms.Button();
            this.DeleteObjFlowXmlData = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 12;
            this.listBox1.Location = new System.Drawing.Point(5, 27);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(172, 316);
            this.listBox1.TabIndex = 0;
            this.listBox1.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // propertyGrid1
            // 
            this.propertyGrid1.Location = new System.Drawing.Point(183, 3);
            this.propertyGrid1.Name = "propertyGrid1";
            this.propertyGrid1.Size = new System.Drawing.Size(373, 398);
            this.propertyGrid1.TabIndex = 1;
            this.propertyGrid1.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.propertyGrid1_PropertyValueChanged);
            // 
            // AddObjFlowXmlData
            // 
            this.AddObjFlowXmlData.Location = new System.Drawing.Point(5, 349);
            this.AddObjFlowXmlData.Name = "AddObjFlowXmlData";
            this.AddObjFlowXmlData.Size = new System.Drawing.Size(172, 23);
            this.AddObjFlowXmlData.TabIndex = 2;
            this.AddObjFlowXmlData.Text = "Add ObjFlowXmlData";
            this.AddObjFlowXmlData.UseVisualStyleBackColor = true;
            this.AddObjFlowXmlData.Click += new System.EventHandler(this.AddObjFlowXmlData_Click);
            // 
            // DeleteObjFlowXmlData
            // 
            this.DeleteObjFlowXmlData.Location = new System.Drawing.Point(5, 378);
            this.DeleteObjFlowXmlData.Name = "DeleteObjFlowXmlData";
            this.DeleteObjFlowXmlData.Size = new System.Drawing.Size(172, 23);
            this.DeleteObjFlowXmlData.TabIndex = 3;
            this.DeleteObjFlowXmlData.Text = "Delete ObjFlowXmlData";
            this.DeleteObjFlowXmlData.UseVisualStyleBackColor = true;
            this.DeleteObjFlowXmlData.Click += new System.EventHandler(this.DeleteObjFlowXmlData_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 12);
            this.label1.TabIndex = 4;
            this.label1.Text = "Object list";
            // 
            // ObjFlowXmlEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(562, 408);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.DeleteObjFlowXmlData);
            this.Controls.Add(this.AddObjFlowXmlData);
            this.Controls.Add(this.propertyGrid1);
            this.Controls.Add(this.listBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximumSize = new System.Drawing.Size(578, 447);
            this.MinimumSize = new System.Drawing.Size(578, 447);
            this.Name = "ObjFlowXmlEditor";
            this.Text = "ObjFlowXmlEditor";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ObjFlowXmlEditor_FormClosing);
            this.Load += new System.EventHandler(this.ObjFlowXmlEditor_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.PropertyGrid propertyGrid1;
        private System.Windows.Forms.Button AddObjFlowXmlData;
        private System.Windows.Forms.Button DeleteObjFlowXmlData;
        private System.Windows.Forms.Label label1;
    }
}