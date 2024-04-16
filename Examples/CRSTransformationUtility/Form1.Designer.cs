namespace CRSTransformationUtility
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.label1 = new System.Windows.Forms.Label();
            this.txtSource = new System.Windows.Forms.TextBox();
            this.btnBrowseSource = new System.Windows.Forms.Button();
            this.btnBrowseTarget = new System.Windows.Forms.Button();
            this.txtTarget = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.ofdSource = new System.Windows.Forms.OpenFileDialog();
            this.sfdTarget = new System.Windows.Forms.SaveFileDialog();
            this.lblSourceCRS = new System.Windows.Forms.Label();
            this.lblTargetCRS = new System.Windows.Forms.Label();
            this.txtSourceCRS = new System.Windows.Forms.TextBox();
            this.txtDestinationCRS = new System.Windows.Forms.TextBox();
            this.gbSource = new System.Windows.Forms.GroupBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnSelectCRS = new System.Windows.Forms.Button();
            this.btnTransform = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.transformationProgress = new System.Windows.Forms.ToolStripProgressBar();
            this.chkRestrictToAreaOfUse = new System.Windows.Forms.CheckBox();
            this.formToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.gbSource.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Select Source";
            // 
            // txtSource
            // 
            this.txtSource.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSource.Location = new System.Drawing.Point(86, 22);
            this.txtSource.Name = "txtSource";
            this.txtSource.ReadOnly = true;
            this.txtSource.Size = new System.Drawing.Size(496, 20);
            this.txtSource.TabIndex = 1;
            // 
            // btnBrowseSource
            // 
            this.btnBrowseSource.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBrowseSource.Location = new System.Drawing.Point(588, 20);
            this.btnBrowseSource.Name = "btnBrowseSource";
            this.btnBrowseSource.Size = new System.Drawing.Size(75, 23);
            this.btnBrowseSource.TabIndex = 2;
            this.btnBrowseSource.Text = "Browse..";
            this.btnBrowseSource.UseVisualStyleBackColor = true;
            this.btnBrowseSource.Click += new System.EventHandler(this.btnBrowseSource_Click);
            // 
            // btnBrowseTarget
            // 
            this.btnBrowseTarget.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBrowseTarget.Location = new System.Drawing.Point(588, 29);
            this.btnBrowseTarget.Name = "btnBrowseTarget";
            this.btnBrowseTarget.Size = new System.Drawing.Size(75, 23);
            this.btnBrowseTarget.TabIndex = 5;
            this.btnBrowseTarget.Text = "Browse..";
            this.btnBrowseTarget.UseVisualStyleBackColor = true;
            this.btnBrowseTarget.Click += new System.EventHandler(this.btnBrowseTarget_Click);
            // 
            // txtTarget
            // 
            this.txtTarget.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTarget.Location = new System.Drawing.Point(86, 31);
            this.txtTarget.Name = "txtTarget";
            this.txtTarget.Size = new System.Drawing.Size(496, 20);
            this.txtTarget.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 34);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Select Target";
            // 
            // ofdSource
            // 
            this.ofdSource.Filter = "Shape File(*.shp)|*.shp";
            this.ofdSource.Title = "Source Shape File";
            // 
            // sfdTarget
            // 
            this.sfdTarget.Filter = "Shape File(*.shp)|*.shp";
            this.sfdTarget.Title = "Target Shape File";
            // 
            // lblSourceCRS
            // 
            this.lblSourceCRS.AutoSize = true;
            this.lblSourceCRS.Location = new System.Drawing.Point(7, 52);
            this.lblSourceCRS.Name = "lblSourceCRS";
            this.lblSourceCRS.Size = new System.Drawing.Size(29, 13);
            this.lblSourceCRS.TabIndex = 6;
            this.lblSourceCRS.Text = "CRS";
            // 
            // lblTargetCRS
            // 
            this.lblTargetCRS.AutoSize = true;
            this.lblTargetCRS.Location = new System.Drawing.Point(7, 63);
            this.lblTargetCRS.Name = "lblTargetCRS";
            this.lblTargetCRS.Size = new System.Drawing.Size(29, 13);
            this.lblTargetCRS.TabIndex = 7;
            this.lblTargetCRS.Text = "CRS";
            // 
            // txtSourceCRS
            // 
            this.txtSourceCRS.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSourceCRS.Location = new System.Drawing.Point(86, 49);
            this.txtSourceCRS.Name = "txtSourceCRS";
            this.txtSourceCRS.ReadOnly = true;
            this.txtSourceCRS.Size = new System.Drawing.Size(496, 20);
            this.txtSourceCRS.TabIndex = 8;
            // 
            // txtDestinationCRS
            // 
            this.txtDestinationCRS.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDestinationCRS.Location = new System.Drawing.Point(86, 63);
            this.txtDestinationCRS.Name = "txtDestinationCRS";
            this.txtDestinationCRS.ReadOnly = true;
            this.txtDestinationCRS.Size = new System.Drawing.Size(496, 20);
            this.txtDestinationCRS.TabIndex = 9;
            // 
            // gbSource
            // 
            this.gbSource.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbSource.Controls.Add(this.label1);
            this.gbSource.Controls.Add(this.txtSource);
            this.gbSource.Controls.Add(this.txtSourceCRS);
            this.gbSource.Controls.Add(this.btnBrowseSource);
            this.gbSource.Controls.Add(this.lblSourceCRS);
            this.gbSource.Location = new System.Drawing.Point(6, 12);
            this.gbSource.Name = "gbSource";
            this.gbSource.Size = new System.Drawing.Size(676, 83);
            this.gbSource.TabIndex = 10;
            this.gbSource.TabStop = false;
            this.gbSource.Text = "Source";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.chkRestrictToAreaOfUse);
            this.groupBox1.Controls.Add(this.btnSelectCRS);
            this.groupBox1.Controls.Add(this.btnBrowseTarget);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtDestinationCRS);
            this.groupBox1.Controls.Add(this.txtTarget);
            this.groupBox1.Controls.Add(this.lblTargetCRS);
            this.groupBox1.Location = new System.Drawing.Point(6, 101);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(676, 113);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Target";
            // 
            // btnSelectCRS
            // 
            this.btnSelectCRS.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSelectCRS.Location = new System.Drawing.Point(588, 61);
            this.btnSelectCRS.Name = "btnSelectCRS";
            this.btnSelectCRS.Size = new System.Drawing.Size(75, 23);
            this.btnSelectCRS.TabIndex = 10;
            this.btnSelectCRS.Text = "Select";
            this.btnSelectCRS.UseVisualStyleBackColor = true;
            this.btnSelectCRS.Click += new System.EventHandler(this.btnSelectCRS_Click);
            // 
            // btnTransform
            // 
            this.btnTransform.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnTransform.Location = new System.Drawing.Point(284, 230);
            this.btnTransform.Name = "btnTransform";
            this.btnTransform.Size = new System.Drawing.Size(117, 23);
            this.btnTransform.TabIndex = 12;
            this.btnTransform.Text = "Transform";
            this.btnTransform.UseVisualStyleBackColor = true;
            this.btnTransform.Click += new System.EventHandler(this.btnTransform_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.transformationProgress});
            this.statusStrip1.Location = new System.Drawing.Point(0, 265);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(685, 22);
            this.statusStrip1.TabIndex = 13;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // transformationProgress
            // 
            this.transformationProgress.Name = "transformationProgress";
            this.transformationProgress.Size = new System.Drawing.Size(100, 16);
            // 
            // chkRestrictToAreaOfUse
            // 
            this.chkRestrictToAreaOfUse.AutoSize = true;
            this.chkRestrictToAreaOfUse.Checked = true;
            this.chkRestrictToAreaOfUse.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkRestrictToAreaOfUse.Location = new System.Drawing.Point(9, 89);
            this.chkRestrictToAreaOfUse.Name = "chkRestrictToAreaOfUse";
            this.chkRestrictToAreaOfUse.Size = new System.Drawing.Size(135, 17);
            this.chkRestrictToAreaOfUse.TabIndex = 11;
            this.chkRestrictToAreaOfUse.Text = "Restrict to Area Of Use";
            this.chkRestrictToAreaOfUse.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(685, 287);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.btnTransform);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.gbSource);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(1200, 325);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(500, 325);
            this.Name = "Form1";
            this.Text = "Coordinate Reference System Transformation Utility";
            this.gbSource.ResumeLayout(false);
            this.gbSource.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtSource;
        private System.Windows.Forms.Button btnBrowseSource;
        private System.Windows.Forms.Button btnBrowseTarget;
        private System.Windows.Forms.TextBox txtTarget;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.OpenFileDialog ofdSource;
        private System.Windows.Forms.SaveFileDialog sfdTarget;
        private System.Windows.Forms.Label lblSourceCRS;
        private System.Windows.Forms.Label lblTargetCRS;
        private System.Windows.Forms.TextBox txtSourceCRS;
        private System.Windows.Forms.TextBox txtDestinationCRS;
        private System.Windows.Forms.GroupBox gbSource;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnSelectCRS;
        private System.Windows.Forms.Button btnTransform;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripProgressBar transformationProgress;
        private System.Windows.Forms.CheckBox chkRestrictToAreaOfUse;
        private System.Windows.Forms.ToolTip formToolTip;
    }
}

