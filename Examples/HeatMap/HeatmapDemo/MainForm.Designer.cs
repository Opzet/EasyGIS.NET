
namespace HeatmapDemo
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
            this.sfMap1 = new EGIS.Controls.SFMap();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openShapeFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ofdShapeFile = new System.Windows.Forms.OpenFileDialog();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.nudSize = new System.Windows.Forms.NumericUpDown();
            this.nudSigma = new System.Windows.Forms.NumericUpDown();
            this.button1 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.cbTileSource = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblLon = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSigma)).BeginInit();
            this.SuspendLayout();
            // 
            // sfMap1
            // 
            this.sfMap1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.sfMap1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.sfMap1.Cursor = System.Windows.Forms.Cursors.Default;
            this.sfMap1.DefaultMapCursor = System.Windows.Forms.Cursors.Default;
            this.sfMap1.DefaultSelectionCursor = System.Windows.Forms.Cursors.Hand;
            this.sfMap1.Location = new System.Drawing.Point(24, 129);
            this.sfMap1.MapBackColor = System.Drawing.SystemColors.Control;
            this.sfMap1.Margin = new System.Windows.Forms.Padding(12, 12, 12, 12);
            this.sfMap1.MaxZoomLevel = 1.7976931348623157E+308D;
            this.sfMap1.MinZomLevel = 0D;
            this.sfMap1.MouseWheelZoomMode = EGIS.Controls.MouseWheelZoomMode.Default;
            this.sfMap1.Name = "sfMap1";
            this.sfMap1.PanSelectMode = EGIS.Controls.PanSelectMode.Pan;
            this.sfMap1.RenderQuality = EGIS.ShapeFileLib.RenderQuality.Auto;
            this.sfMap1.Size = new System.Drawing.Size(1070, 712);
            this.sfMap1.TabIndex = 0;
            this.sfMap1.UseMemoryStreams = false;
            this.sfMap1.UseMercatorProjection = false;
            this.sfMap1.ZoomToSelectedExtentWhenCtrlKeydown = false;
            this.sfMap1.Paint += new System.Windows.Forms.PaintEventHandler(this.sfMap1_Paint);
            this.sfMap1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.sfMap1_MouseMove);
            // 
            // menuStrip1
            // 
            this.menuStrip1.GripMargin = new System.Windows.Forms.Padding(2, 2, 0, 2);
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(2158, 48);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openShapeFileToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(71, 40);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // openShapeFileToolStripMenuItem
            // 
            this.openShapeFileToolStripMenuItem.Name = "openShapeFileToolStripMenuItem";
            this.openShapeFileToolStripMenuItem.Size = new System.Drawing.Size(316, 44);
            this.openShapeFileToolStripMenuItem.Text = "Open ShapeFile";
            this.openShapeFileToolStripMenuItem.Click += new System.EventHandler(this.openShapeFileToolStripMenuItem_Click);
            // 
            // ofdShapeFile
            // 
            this.ofdShapeFile.Filter = "ShapeFile(*.shp)|*.shp";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 81);
            this.label1.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(146, 25);
            this.label1.TabIndex = 2;
            this.label1.Text = "Heatmap Size";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(394, 81);
            this.label2.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(164, 25);
            this.label2.TabIndex = 3;
            this.label2.Text = "Heatmap Sigma";
            // 
            // nudSize
            // 
            this.nudSize.Location = new System.Drawing.Point(182, 77);
            this.nudSize.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.nudSize.Maximum = new decimal(new int[] {
            250,
            0,
            0,
            0});
            this.nudSize.Minimum = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.nudSize.Name = "nudSize";
            this.nudSize.Size = new System.Drawing.Size(134, 31);
            this.nudSize.TabIndex = 4;
            this.nudSize.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.nudSize.ValueChanged += new System.EventHandler(this.nudSize_ValueChanged);
            // 
            // nudSigma
            // 
            this.nudSigma.Location = new System.Drawing.Point(570, 77);
            this.nudSigma.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.nudSigma.Maximum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.nudSigma.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudSigma.Name = "nudSigma";
            this.nudSigma.Size = new System.Drawing.Size(134, 31);
            this.nudSigma.TabIndex = 5;
            this.nudSigma.Value = new decimal(new int[] {
            8,
            0,
            0,
            0});
            this.nudSigma.ValueChanged += new System.EventHandler(this.nudSigma_ValueChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(1582, 85);
            this.button1.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(180, 79);
            this.button1.TabIndex = 7;
            this.button1.Text = "Create 2d Point  Shape File";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(1150, 175);
            this.textBox1.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(940, 633);
            this.textBox1.TabIndex = 8;
            // 
            // cbTileSource
            // 
            this.cbTileSource.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTileSource.FormattingEnabled = true;
            this.cbTileSource.Location = new System.Drawing.Point(928, 75);
            this.cbTileSource.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.cbTileSource.Name = "cbTileSource";
            this.cbTileSource.Size = new System.Drawing.Size(382, 33);
            this.cbTileSource.TabIndex = 10;
            this.cbTileSource.SelectedIndexChanged += new System.EventHandler(this.cbTileSource_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(728, 85);
            this.label3.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(187, 25);
            this.label3.TabIndex = 9;
            this.label3.Text = "Select Tile Source";
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.White;
            this.label4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label4.Location = new System.Drawing.Point(1245, 820);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(220, 33);
            this.label4.TabIndex = 11;
            this.label4.Text = "LblLat";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblLon
            // 
            this.lblLon.BackColor = System.Drawing.Color.White;
            this.lblLon.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblLon.Location = new System.Drawing.Point(1582, 820);
            this.lblLon.Name = "lblLon";
            this.lblLon.Size = new System.Drawing.Size(220, 33);
            this.lblLon.TabIndex = 12;
            this.lblLon.Text = "LblLat";
            this.lblLon.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(1197, 824);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(42, 25);
            this.label6.TabIndex = 13;
            this.label6.Text = "Lat";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(1534, 824);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(48, 25);
            this.label7.TabIndex = 14;
            this.label7.Text = "Lon";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(2158, 865);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.lblLon);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cbTileSource);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.nudSigma);
            this.Controls.Add(this.nudSize);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.sfMap1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.Name = "MainForm";
            this.Text = "Heatmap Demo";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSigma)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private EGIS.Controls.SFMap sfMap1;
		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem openShapeFileToolStripMenuItem;
		private System.Windows.Forms.OpenFileDialog ofdShapeFile;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.NumericUpDown nudSize;
		private System.Windows.Forms.NumericUpDown nudSigma;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.ComboBox cbTileSource;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblLon;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
    }
}

