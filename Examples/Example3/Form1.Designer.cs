namespace Example3
{
    partial class MainForm
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
            this.btnBrowseInputFile = new System.Windows.Forms.Button();
            this.txtFilterInputFile = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.pnlFilter = new System.Windows.Forms.Panel();
            this.chkFilterRecordLimit = new System.Windows.Forms.CheckBox();
            this.label7 = new System.Windows.Forms.Label();
            this.clbFilterRecords = new System.Windows.Forms.CheckedListBox();
            this.label6 = new System.Windows.Forms.Label();
            this.btnGenerateFilteredShapefile = new System.Windows.Forms.Button();
            this.btnBrowseFilterOutputShapefile = new System.Windows.Forms.Button();
            this.txtFilterOutputShapefile = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cboxFilterField = new System.Windows.Forms.ComboBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripProgressBar1 = new System.Windows.Forms.ToolStripProgressBar();
            this.ofdInputFiles = new System.Windows.Forms.OpenFileDialog();
            this.sfdOutputFile = new System.Windows.Forms.SaveFileDialog();
            this.chkAppend = new System.Windows.Forms.CheckBox();
            this.pnlFilter.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnBrowseInputFile
            // 
            this.btnBrowseInputFile.Location = new System.Drawing.Point(380, 4);
            this.btnBrowseInputFile.Name = "btnBrowseInputFile";
            this.btnBrowseInputFile.Size = new System.Drawing.Size(75, 23);
            this.btnBrowseInputFile.TabIndex = 16;
            this.btnBrowseInputFile.Text = "Browse";
            this.btnBrowseInputFile.UseVisualStyleBackColor = true;
            this.btnBrowseInputFile.Click += new System.EventHandler(this.btnBrowseInputFile_Click);
            // 
            // txtFilterInputFile
            // 
            this.txtFilterInputFile.Location = new System.Drawing.Point(89, 4);
            this.txtFilterInputFile.Name = "txtFilterInputFile";
            this.txtFilterInputFile.Size = new System.Drawing.Size(285, 20);
            this.txtFilterInputFile.TabIndex = 15;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 9);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(78, 13);
            this.label5.TabIndex = 14;
            this.label5.Text = "Input Shapefile";
            // 
            // pnlFilter
            // 
            this.pnlFilter.Controls.Add(this.chkAppend);
            this.pnlFilter.Controls.Add(this.chkFilterRecordLimit);
            this.pnlFilter.Controls.Add(this.label7);
            this.pnlFilter.Controls.Add(this.clbFilterRecords);
            this.pnlFilter.Controls.Add(this.label6);
            this.pnlFilter.Controls.Add(this.btnGenerateFilteredShapefile);
            this.pnlFilter.Controls.Add(this.btnBrowseFilterOutputShapefile);
            this.pnlFilter.Controls.Add(this.txtFilterOutputShapefile);
            this.pnlFilter.Controls.Add(this.label4);
            this.pnlFilter.Controls.Add(this.cboxFilterField);
            this.pnlFilter.Enabled = false;
            this.pnlFilter.Location = new System.Drawing.Point(12, 33);
            this.pnlFilter.Name = "pnlFilter";
            this.pnlFilter.Size = new System.Drawing.Size(452, 227);
            this.pnlFilter.TabIndex = 13;
            // 
            // chkFilterRecordLimit
            // 
            this.chkFilterRecordLimit.AutoSize = true;
            this.chkFilterRecordLimit.Checked = true;
            this.chkFilterRecordLimit.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkFilterRecordLimit.Location = new System.Drawing.Point(233, 73);
            this.chkFilterRecordLimit.Name = "chkFilterRecordLimit";
            this.chkFilterRecordLimit.Size = new System.Drawing.Size(129, 17);
            this.chkFilterRecordLimit.TabIndex = 15;
            this.chkFilterRecordLimit.Text = "Limit to 1000 Records";
            this.chkFilterRecordLimit.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(3, 77);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(80, 13);
            this.label7.TabIndex = 14;
            this.label7.Text = "Include records";
            // 
            // clbFilterRecords
            // 
            this.clbFilterRecords.CheckOnClick = true;
            this.clbFilterRecords.FormattingEnabled = true;
            this.clbFilterRecords.Location = new System.Drawing.Point(5, 92);
            this.clbFilterRecords.Name = "clbFilterRecords";
            this.clbFilterRecords.ScrollAlwaysVisible = true;
            this.clbFilterRecords.Size = new System.Drawing.Size(357, 94);
            this.clbFilterRecords.TabIndex = 13;
            this.clbFilterRecords.UseTabStops = false;
            this.clbFilterRecords.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.clbFilterRecords_ItemCheck);
            this.clbFilterRecords.Click += new System.EventHandler(this.clbFilterRecords_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(3, 39);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(114, 13);
            this.label6.TabIndex = 12;
            this.label6.Text = "Select Field to Filter on";
            // 
            // btnGenerateFilteredShapefile
            // 
            this.btnGenerateFilteredShapefile.Enabled = false;
            this.btnGenerateFilteredShapefile.Location = new System.Drawing.Point(121, 192);
            this.btnGenerateFilteredShapefile.Name = "btnGenerateFilteredShapefile";
            this.btnGenerateFilteredShapefile.Size = new System.Drawing.Size(202, 23);
            this.btnGenerateFilteredShapefile.TabIndex = 11;
            this.btnGenerateFilteredShapefile.Text = "Generate Filtered Shapefile";
            this.btnGenerateFilteredShapefile.UseVisualStyleBackColor = true;
            this.btnGenerateFilteredShapefile.Click += new System.EventHandler(this.btnGenerateFilteredShapefile_Click);
            // 
            // btnBrowseFilterOutputShapefile
            // 
            this.btnBrowseFilterOutputShapefile.Location = new System.Drawing.Point(368, 6);
            this.btnBrowseFilterOutputShapefile.Name = "btnBrowseFilterOutputShapefile";
            this.btnBrowseFilterOutputShapefile.Size = new System.Drawing.Size(75, 23);
            this.btnBrowseFilterOutputShapefile.TabIndex = 10;
            this.btnBrowseFilterOutputShapefile.Text = "Browse..";
            this.btnBrowseFilterOutputShapefile.UseVisualStyleBackColor = true;
            this.btnBrowseFilterOutputShapefile.Click += new System.EventHandler(this.btnBrowseFilterOutputShapefile_Click);
            // 
            // txtFilterOutputShapefile
            // 
            this.txtFilterOutputShapefile.Location = new System.Drawing.Point(68, 8);
            this.txtFilterOutputShapefile.Name = "txtFilterOutputShapefile";
            this.txtFilterOutputShapefile.Size = new System.Drawing.Size(294, 20);
            this.txtFilterOutputShapefile.TabIndex = 9;
            this.txtFilterOutputShapefile.Text = "example3.shp";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 11);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(58, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Output File";
            // 
            // cboxFilterField
            // 
            this.cboxFilterField.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboxFilterField.FormattingEnabled = true;
            this.cboxFilterField.Location = new System.Drawing.Point(121, 36);
            this.cboxFilterField.Name = "cboxFilterField";
            this.cboxFilterField.Size = new System.Drawing.Size(241, 21);
            this.cboxFilterField.TabIndex = 7;
            this.cboxFilterField.SelectedIndexChanged += new System.EventHandler(this.cboxFilterField_SelectedIndexChanged);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripProgressBar1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 270);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(474, 22);
            this.statusStrip1.TabIndex = 17;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripProgressBar1
            // 
            this.toolStripProgressBar1.Name = "toolStripProgressBar1";
            this.toolStripProgressBar1.Size = new System.Drawing.Size(150, 16);
            // 
            // ofdInputFiles
            // 
            this.ofdInputFiles.FileName = "CD_VIC_06_imr.shp";
            this.ofdInputFiles.Filter = "shapefiles (*.shp)|*.shp";
            this.ofdInputFiles.InitialDirectory = ".";
            // 
            // sfdOutputFile
            // 
            this.sfdOutputFile.Filter = "shapefile (*.shp) | *.shp";
            // 
            // chkAppend
            // 
            this.chkAppend.AutoSize = true;
            this.chkAppend.Location = new System.Drawing.Point(363, 196);
            this.chkAppend.Name = "chkAppend";
            this.chkAppend.Size = new System.Drawing.Size(63, 17);
            this.chkAppend.TabIndex = 16;
            this.chkAppend.Text = "Append";
            this.chkAppend.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(474, 292);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.btnBrowseInputFile);
            this.Controls.Add(this.txtFilterInputFile);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.pnlFilter);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "MainForm";
            this.Text = "Example 3";
            this.pnlFilter.ResumeLayout(false);
            this.pnlFilter.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnBrowseInputFile;
        private System.Windows.Forms.TextBox txtFilterInputFile;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel pnlFilter;
        private System.Windows.Forms.CheckBox chkFilterRecordLimit;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.CheckedListBox clbFilterRecords;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnGenerateFilteredShapefile;
        private System.Windows.Forms.Button btnBrowseFilterOutputShapefile;
        private System.Windows.Forms.TextBox txtFilterOutputShapefile;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cboxFilterField;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar1;
        private System.Windows.Forms.OpenFileDialog ofdInputFiles;
        private System.Windows.Forms.SaveFileDialog sfdOutputFile;
        private System.Windows.Forms.CheckBox chkAppend;
    }
}

