using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using EGIS.Mapbox.Vector.Tile;
using EGIS.ShapeFileLib;

/*
 * 
 * DISCLAIMER OF WARRANTY: THIS SOFTWARE IS PROVIDED ON AN "AS IS" BASIS, WITHOUT WARRANTY OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING,
 * WITHOUT LIMITATION, WARRANTIES THAT THE SOFTWARE IS FREE OF DEFECTS, MERCHANTABLE, FIT FOR A PARTICULAR PURPOSE OR NON-INFRINGING.
 * THE ENTIRE RISK AS TO THE QUALITY AND PERFORMANCE OF THE SOFTWARE IS WITH YOU. SHOULD ANY COVERED CODE PROVE DEFECTIVE IN ANY RESPECT,
 * YOU (NOT CORPORATE EASY GIS .NET) ASSUME THE COST OF ANY NECESSARY SERVICING, REPAIR OR CORRECTION.  
 * 
 * LIABILITY: IN NO EVENT SHALL CORPORATE EASY GIS .NET BE LIABLE FOR ANY DAMAGES WHATSOEVER (INCLUDING, WITHOUT LIMITATION, 
 * DAMAGES FOR LOSS OF BUSINESS PROFITS, BUSINESS INTERRUPTION, LOSS OF INFORMATION OR ANY OTHER PECUNIARY LOSS)
 * ARISING OUT OF THE USE OF INABILITY TO USE THIS SOFTWARE, EVEN IF CORPORATE EASY GIS .NET HAS BEEN ADVISED OF THE POSSIBILITY
 * OF SUCH DAMAGES.
 * 
 * Copyright: Easy GIS .NET 2020
 *
 */

namespace ShpToMapboxVT
{
    /// <summary>
    /// Main Form of the Shape to MapBox Vector Tiles Generator 
    /// </summary>
    public partial class MainForm : Form
    {

        private List<string> inputShapeFiles = new List<string>();

        public MainForm()
        {
            InitializeComponent();

            ValidateCanProcess();                        
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            LoadSettings();

        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            SaveSettings();
        }

        private void LoadSettings()
        {
            if (Properties.Settings.Default.LastOutputDir != null)
            {
                this.txtOutputDirectory.Text = Properties.Settings.Default.LastOutputDir;
            }
        }

        private void SaveSettings()
        {
            if (!string.IsNullOrEmpty(this.txtOutputDirectory.Text) && System.IO.Directory.Exists(this.txtOutputDirectory.Text))
            {
            }
            Properties.Settings.Default.LastOutputDir = this.txtOutputDirectory.Text;
            Properties.Settings.Default.Save();


        }
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void btnBrowseShapeFile_Click(object sender, EventArgs e)
        {
            if (ofdShapeFile.ShowDialog(this) == DialogResult.OK)
            {
                OpenShapeFiles(ofdShapeFile.FileName);
            }
        }


        

        private void btnBrowseOutputDirectory_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.txtOutputDirectory.Text) && System.IO.Directory.Exists(this.txtOutputDirectory.Text))
            {
                this.fbdOutput.SelectedPath = this.txtOutputDirectory.Text;
            }
            if (this.fbdOutput.ShowDialog(this) == DialogResult.OK)
            {
                SetOutputDirectory(fbdOutput.SelectedPath);

            }
        }


        private void OpenShapeFiles(string filename)
        {
            this.txtInputShapeFile.Text = filename;
            using (ShapeFile sf = new ShapeFile(filename))
            {
                clbSelectedAttributes.Items.Clear();
                string[] attributeNames = sf.GetAttributeFieldNames();
                foreach (string name in attributeNames)
                {
                    clbSelectedAttributes.Items.Add(name, true);
                }                                
            }
            ValidateCanProcess();

        }

        private void SetOutputDirectory(string outputDirectory)
        {
            this.txtOutputDirectory.Text = outputDirectory;
            ValidateCanProcess();
        }

        private void ValidateCanProcess()
        {
            bool ok = (!string.IsNullOrEmpty(this.txtOutputDirectory.Text) && System.IO.Directory.Exists(this.txtOutputDirectory.Text)) &&
                (!string.IsNullOrEmpty(this.txtInputShapeFile.Text) && System.IO.File.Exists(this.txtInputShapeFile.Text));

            this.btnProcess.Enabled = ok;

        }

        private async void btnProcess_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.btnProcess.Text.Equals("Cancel"))
                {
                    if (this.cancellationTokenSource != null && ! cancellationTokenSource.IsCancellationRequested) this.cancellationTokenSource.Cancel();
                }
                else
                {
                    this.btnProcess.Text = "Cancel";
                    try
                    {
                        await GenerateTiles();
                    }
                    finally
                    {
                        this.btnProcess.Text = "Process";
                    }
                }
            }
            catch (Exception ex)
            {
                OutputMessage(ex.ToString() + "\n");

            }

        }

        private void OutputMessage(string text)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() => OutputMessage(text)));                
            }
            else
            {
                this.rtbOutput.AppendText(text);
                this.rtbOutput.Refresh();
            }
        }

        private System.Threading.CancellationTokenSource cancellationTokenSource = null;

        private async Task<bool> GenerateTiles()
        {
            if (cancellationTokenSource != null)
            {
                cancellationTokenSource.Cancel();
                cancellationTokenSource.Dispose();
            }
            cancellationTokenSource = new System.Threading.CancellationTokenSource();

            return await Task.Run(() =>
            {

                TileGenerator generator = new TileGenerator()
                {
                    BaseOutputDirectory = this.txtOutputDirectory.Text,
                    StartZoomLevel = (int)nudStartZoom.Value,
                    EndZoomLevel = (int)nudEndZoom.Value,
                    ExportAttributesToSeparateFile = this.chkExportAttributesToSeparateFile.Checked
                };
                generator.StatusMessage += Generator_StatusMessage;
                try
                {
                    List<string> attributes = new List<string>();
                    for (int i = 0; i < clbSelectedAttributes.CheckedItems.Count; ++i)
                    {
                        attributes.Add(clbSelectedAttributes.CheckedItems[i].ToString());
                    }
                    OutputMessage("Processing Vector tiles..\n");
                    DateTime tick = DateTime.Now;
                    generator.Process(this.txtInputShapeFile.Text, cancellationTokenSource.Token, attributes);
                    OutputMessage("Processing Vector tiles complete. Elapsed time:" + DateTime.Now.Subtract(tick) + "\n");
                }
                finally
                {
                    generator.StatusMessage -= Generator_StatusMessage;                    
                }
                return true;
            });
        }

        private void Generator_StatusMessage(object sender, StatusMessageEventArgs e)
        {
            OutputMessage(e.Status+"\n");
        }

        private void btnSelectAllAttributes_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < clbSelectedAttributes.Items.Count; i++)
                clbSelectedAttributes.SetItemChecked(i, true);
        }

        private void btnSelectNoAttributes_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < clbSelectedAttributes.Items.Count; i++)
                clbSelectedAttributes.SetItemChecked(i, false);
        }
    }
}
