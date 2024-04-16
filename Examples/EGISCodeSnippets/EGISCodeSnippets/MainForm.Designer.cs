
namespace EGISCodeSnippets
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.sfMap1 = new EGIS.Controls.SFMap();
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tabPage2 = new System.Windows.Forms.TabPage();
			this.chkToggleForegroundLayer = new System.Windows.Forms.CheckBox();
			this.sfMap2 = new EGIS.Controls.SFMap();
			this.tabPage3 = new System.Windows.Forms.TabPage();
			this.sfMap3 = new EGIS.Controls.SFMap();
			this.timer1 = new System.Windows.Forms.Timer(this.components);
			this.chkHideEverySecondRecord = new System.Windows.Forms.CheckBox();
			this.tabPage1.SuspendLayout();
			this.tabControl1.SuspendLayout();
			this.tabPage2.SuspendLayout();
			this.tabPage3.SuspendLayout();
			this.SuspendLayout();
			// 
			// tabPage1
			// 
			this.tabPage1.Controls.Add(this.sfMap1);
			this.tabPage1.Location = new System.Drawing.Point(4, 22);
			this.tabPage1.Margin = new System.Windows.Forms.Padding(2);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Padding = new System.Windows.Forms.Padding(2);
			this.tabPage1.Size = new System.Drawing.Size(708, 440);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "Create ShapeFile using Memory Streams";
			this.tabPage1.UseVisualStyleBackColor = true;
			// 
			// sfMap1
			// 
			this.sfMap1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.sfMap1.CentrePoint2D = ((EGIS.ShapeFileLib.PointD)(resources.GetObject("sfMap1.CentrePoint2D")));
			this.sfMap1.Cursor = System.Windows.Forms.Cursors.Default;
			this.sfMap1.DefaultMapCursor = System.Windows.Forms.Cursors.Default;
			this.sfMap1.DefaultSelectionCursor = System.Windows.Forms.Cursors.Hand;
			this.sfMap1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.sfMap1.Location = new System.Drawing.Point(2, 2);
			this.sfMap1.MapBackColor = System.Drawing.SystemColors.Control;
			this.sfMap1.MaxZoomLevel = 1.7976931348623157E+308D;
			this.sfMap1.MinZomLevel = 0D;
			this.sfMap1.MouseWheelZoomMode = EGIS.Controls.MouseWheelZoomMode.Default;
			this.sfMap1.Name = "sfMap1";
			this.sfMap1.PanSelectMode = EGIS.Controls.PanSelectMode.Pan;
			this.sfMap1.RenderQuality = EGIS.ShapeFileLib.RenderQuality.Auto;
			this.sfMap1.Size = new System.Drawing.Size(704, 436);
			this.sfMap1.TabIndex = 0;
			this.sfMap1.UseMemoryStreams = false;
			this.sfMap1.UseMercatorProjection = false;
			this.sfMap1.ZoomLevel = 1.0016611295681064D;
			this.sfMap1.ZoomToSelectedExtentWhenCtrlKeydown = false;
			// 
			// tabControl1
			// 
			this.tabControl1.Controls.Add(this.tabPage1);
			this.tabControl1.Controls.Add(this.tabPage2);
			this.tabControl1.Controls.Add(this.tabPage3);
			this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabControl1.Location = new System.Drawing.Point(0, 0);
			this.tabControl1.Margin = new System.Windows.Forms.Padding(2);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(716, 466);
			this.tabControl1.TabIndex = 0;
			// 
			// tabPage2
			// 
			this.tabPage2.Controls.Add(this.chkToggleForegroundLayer);
			this.tabPage2.Controls.Add(this.sfMap2);
			this.tabPage2.Location = new System.Drawing.Point(4, 22);
			this.tabPage2.Margin = new System.Windows.Forms.Padding(2);
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.Padding = new System.Windows.Forms.Padding(2);
			this.tabPage2.Size = new System.Drawing.Size(708, 440);
			this.tabPage2.TabIndex = 1;
			this.tabPage2.Text = "SFMap Background Foreground Layers ";
			this.tabPage2.UseVisualStyleBackColor = true;
			// 
			// chkToggleForegroundLayer
			// 
			this.chkToggleForegroundLayer.AutoSize = true;
			this.chkToggleForegroundLayer.Checked = true;
			this.chkToggleForegroundLayer.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkToggleForegroundLayer.Location = new System.Drawing.Point(6, 14);
			this.chkToggleForegroundLayer.Margin = new System.Windows.Forms.Padding(2);
			this.chkToggleForegroundLayer.Name = "chkToggleForegroundLayer";
			this.chkToggleForegroundLayer.Size = new System.Drawing.Size(146, 17);
			this.chkToggleForegroundLayer.TabIndex = 2;
			this.chkToggleForegroundLayer.Text = "Display Foreground Layer";
			this.chkToggleForegroundLayer.UseVisualStyleBackColor = true;
			this.chkToggleForegroundLayer.CheckedChanged += new System.EventHandler(this.chkToggleForegroundLayer_CheckedChanged);
			// 
			// sfMap2
			// 
			this.sfMap2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.sfMap2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.sfMap2.CentrePoint2D = ((EGIS.ShapeFileLib.PointD)(resources.GetObject("sfMap2.CentrePoint2D")));
			this.sfMap2.Cursor = System.Windows.Forms.Cursors.Default;
			this.sfMap2.DefaultMapCursor = System.Windows.Forms.Cursors.Default;
			this.sfMap2.DefaultSelectionCursor = System.Windows.Forms.Cursors.Hand;
			this.sfMap2.Location = new System.Drawing.Point(2, 37);
			this.sfMap2.MapBackColor = System.Drawing.SystemColors.Control;
			this.sfMap2.MaxZoomLevel = 1.7976931348623157E+308D;
			this.sfMap2.MinZomLevel = 0D;
			this.sfMap2.MouseWheelZoomMode = EGIS.Controls.MouseWheelZoomMode.Default;
			this.sfMap2.Name = "sfMap2";
			this.sfMap2.PanSelectMode = EGIS.Controls.PanSelectMode.Pan;
			this.sfMap2.RenderQuality = EGIS.ShapeFileLib.RenderQuality.Auto;
			this.sfMap2.Size = new System.Drawing.Size(708, 403);
			this.sfMap2.TabIndex = 1;
			this.sfMap2.UseMemoryStreams = false;
			this.sfMap2.UseMercatorProjection = false;
			this.sfMap2.ZoomLevel = 1.0016611295681064D;
			this.sfMap2.ZoomToSelectedExtentWhenCtrlKeydown = false;
			this.sfMap2.Paint += new System.Windows.Forms.PaintEventHandler(this.sfMap2_Paint);
			// 
			// tabPage3
			// 
			this.tabPage3.Controls.Add(this.chkHideEverySecondRecord);
			this.tabPage3.Controls.Add(this.sfMap3);
			this.tabPage3.Location = new System.Drawing.Point(4, 22);
			this.tabPage3.Margin = new System.Windows.Forms.Padding(2);
			this.tabPage3.Name = "tabPage3";
			this.tabPage3.Padding = new System.Windows.Forms.Padding(2);
			this.tabPage3.Size = new System.Drawing.Size(708, 440);
			this.tabPage3.TabIndex = 2;
			this.tabPage3.Text = "Custom Labels";
			this.tabPage3.UseVisualStyleBackColor = true;
			// 
			// sfMap3
			// 
			this.sfMap3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.sfMap3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.sfMap3.CentrePoint2D = ((EGIS.ShapeFileLib.PointD)(resources.GetObject("sfMap3.CentrePoint2D")));
			this.sfMap3.Cursor = System.Windows.Forms.Cursors.Default;
			this.sfMap3.DefaultMapCursor = System.Windows.Forms.Cursors.Default;
			this.sfMap3.DefaultSelectionCursor = System.Windows.Forms.Cursors.Hand;
			this.sfMap3.Location = new System.Drawing.Point(5, 44);
			this.sfMap3.MapBackColor = System.Drawing.SystemColors.Control;
			this.sfMap3.MaxZoomLevel = 1.7976931348623157E+308D;
			this.sfMap3.MinZomLevel = 0D;
			this.sfMap3.MouseWheelZoomMode = EGIS.Controls.MouseWheelZoomMode.Default;
			this.sfMap3.Name = "sfMap3";
			this.sfMap3.PanSelectMode = EGIS.Controls.PanSelectMode.Pan;
			this.sfMap3.RenderQuality = EGIS.ShapeFileLib.RenderQuality.Auto;
			this.sfMap3.Size = new System.Drawing.Size(700, 391);
			this.sfMap3.TabIndex = 0;
			this.sfMap3.UseMemoryStreams = false;
			this.sfMap3.UseMercatorProjection = false;
			this.sfMap3.ZoomLevel = 1.0022172949002217D;
			this.sfMap3.ZoomToSelectedExtentWhenCtrlKeydown = false;
			// 
			// timer1
			// 
			this.timer1.Enabled = true;
			this.timer1.Interval = 10;
			this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
			// 
			// chkHideEverySecondRecord
			// 
			this.chkHideEverySecondRecord.AutoSize = true;
			this.chkHideEverySecondRecord.Location = new System.Drawing.Point(9, 21);
			this.chkHideEverySecondRecord.Name = "chkHideEverySecondRecord";
			this.chkHideEverySecondRecord.Size = new System.Drawing.Size(131, 17);
			this.chkHideEverySecondRecord.TabIndex = 1;
			this.chkHideEverySecondRecord.Text = "Hide every 2nd record";
			this.chkHideEverySecondRecord.UseVisualStyleBackColor = true;
			this.chkHideEverySecondRecord.CheckedChanged += new System.EventHandler(this.chkHideEverySecondRecord_CheckedChanged);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(716, 466);
			this.Controls.Add(this.tabControl1);
			this.Margin = new System.Windows.Forms.Padding(2);
			this.Name = "MainForm";
			this.Text = "EGIS Code Snippets";
			this.tabPage1.ResumeLayout(false);
			this.tabControl1.ResumeLayout(false);
			this.tabPage2.ResumeLayout(false);
			this.tabPage2.PerformLayout();
			this.tabPage3.ResumeLayout(false);
			this.tabPage3.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TabPage tabPage1;
		private EGIS.Controls.SFMap sfMap1;
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tabPage2;
		private EGIS.Controls.SFMap sfMap2;
		private System.Windows.Forms.CheckBox chkToggleForegroundLayer;
		private System.Windows.Forms.TabPage tabPage3;
		private EGIS.Controls.SFMap sfMap3;
		private System.Windows.Forms.Timer timer1;
		private System.Windows.Forms.CheckBox chkHideEverySecondRecord;
	}
}

