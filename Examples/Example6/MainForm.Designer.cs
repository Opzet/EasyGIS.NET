namespace Example6
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
            this.components = new System.ComponentModel.Container();
            this.sfMap1 = new EGIS.Controls.SFMap();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.miOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.restartGPSProcessingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.miCenterMarker = new System.Windows.Forms.ToolStripMenuItem();
            this.ofdShapefile = new System.Windows.Forms.OpenFileDialog();
            this.packetTimer = new System.Windows.Forms.Timer(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.cbTileSource = new System.Windows.Forms.ComboBox();
            this.menuStrip1.SuspendLayout();
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
            this.sfMap1.Location = new System.Drawing.Point(12, 68);
            this.sfMap1.MapBackColor = System.Drawing.SystemColors.Control;
            this.sfMap1.Margin = new System.Windows.Forms.Padding(4);
            this.sfMap1.MaxZoomLevel = 1.7976931348623157E+308D;
            this.sfMap1.MinZomLevel = 0D;
            this.sfMap1.MouseWheelZoomMode = EGIS.Controls.MouseWheelZoomMode.Default;
            this.sfMap1.Name = "sfMap1";
            this.sfMap1.PanSelectMode = EGIS.Controls.PanSelectMode.Pan;
            this.sfMap1.RenderQuality = EGIS.ShapeFileLib.RenderQuality.Auto;
            this.sfMap1.Size = new System.Drawing.Size(704, 396);
            this.sfMap1.TabIndex = 0;
            this.sfMap1.UseMemoryStreams = false;
            this.sfMap1.UseMercatorProjection = false;
            this.sfMap1.ZoomLevel = 1D;
            this.sfMap1.ZoomToSelectedExtentWhenCtrlKeydown = false;
            this.sfMap1.Paint += new System.Windows.Forms.PaintEventHandler(this.sfMap1_Paint);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Size = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.optionsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(4, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(728, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miOpen});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // miOpen
            // 
            this.miOpen.Name = "miOpen";
            this.miOpen.Size = new System.Drawing.Size(103, 22);
            this.miOpen.Text = "Open";
            this.miOpen.Click += new System.EventHandler(this.miOpen_Click);
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.restartGPSProcessingToolStripMenuItem,
            this.miCenterMarker});
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.optionsToolStripMenuItem.Text = "Options";
            // 
            // restartGPSProcessingToolStripMenuItem
            // 
            this.restartGPSProcessingToolStripMenuItem.Name = "restartGPSProcessingToolStripMenuItem";
            this.restartGPSProcessingToolStripMenuItem.Size = new System.Drawing.Size(194, 22);
            this.restartGPSProcessingToolStripMenuItem.Text = "Restart GPS Processing";
            this.restartGPSProcessingToolStripMenuItem.Click += new System.EventHandler(this.restartGPSProcessingToolStripMenuItem_Click);
            // 
            // miCenterMarker
            // 
            this.miCenterMarker.CheckOnClick = true;
            this.miCenterMarker.Name = "miCenterMarker";
            this.miCenterMarker.Size = new System.Drawing.Size(194, 22);
            this.miCenterMarker.Text = "Center Marker on Map";
            // 
            // ofdShapefile
            // 
            this.ofdShapefile.Filter = "ESRI ShapeFile|*.shp";
            this.ofdShapefile.InitialDirectory = ".";
            // 
            // packetTimer
            // 
            this.packetTimer.Enabled = true;
            this.packetTimer.Interval = 500;
            this.packetTimer.Tick += new System.EventHandler(this.packetTimer_Tick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 46);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(94, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Select Tile Source";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // cbTileSource
            // 
            this.cbTileSource.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTileSource.FormattingEnabled = true;
            this.cbTileSource.Location = new System.Drawing.Point(113, 41);
            this.cbTileSource.Name = "cbTileSource";
            this.cbTileSource.Size = new System.Drawing.Size(193, 21);
            this.cbTileSource.TabIndex = 3;
            this.cbTileSource.SelectedIndexChanged += new System.EventHandler(this.cbTileSource_SelectedIndexChanged);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(728, 476);
            this.Controls.Add(this.cbTileSource);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.sfMap1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "Example 6";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private EGIS.Controls.SFMap sfMap1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem miOpen;
        private System.Windows.Forms.OpenFileDialog ofdShapefile;
        private System.Windows.Forms.Timer packetTimer;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem restartGPSProcessingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem miCenterMarker;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ComboBox cbTileSource;
	}
}

