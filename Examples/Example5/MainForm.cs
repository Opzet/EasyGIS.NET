using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

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
 * Copyright: Easy GIS .NET 2009
 *
 */
namespace Example5
{
    /// <summary>
    /// Main Form of Example 5
    /// </summary>
    /// <remarks>
    /// <para>
    /// This example shows how to display a marker centered on a map loaded in the EGIS.Controls.SFMap Control
    /// The example loads a collection of logged GPS data packets and iterates over the GPS data, displaying a marker at the 
    /// current location.
    /// </para>
    /// <para>
    /// This example could be easily extended to read live GPS data instead of a data file of gps data packets. See the ProcessGPSDataFile method
    /// and GpsPacket class for further information.
    /// </para>
    /// </remarks>
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();            
        }

        private void miOpen_Click(object sender, EventArgs e)
        {
            if(ofdShapefile.ShowDialog(this) == DialogResult.OK)
            {
                try
                {
                    OpenShapefile(ofdShapefile.FileName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(this, "Error : " + ex.Message);
                }
            }
        }

        private void OpenShapefile(string path)
        {
            // clear any shapefiles the map is currently displaying
            this.sfMap1.ClearShapeFiles();
            
            // open the shapefile passing in the path, display name of the shapefile and
            // the field name to be used when rendering the shapes (we use an empty string
            // as the field name (3rd parameter) can not be null)
            this.sfMap1.AddShapeFile(path, "ShapeFile", "");
            
            // read the shapefile dbf field names and set the shapefiles's RenderSettings
            // to use the first field to label the shapes.
            EGIS.ShapeFileLib.ShapeFile sf = this.sfMap1[0];
            sf.RenderSettings.FieldName = sf.RenderSettings.DbfReader.GetFieldNames()[0];
        }

        private EGIS.ShapeFileLib.PointD currentMarkerPosition = new EGIS.ShapeFileLib.PointD(0, 0);

        private void sfMap1_Paint(object sender, PaintEventArgs e)
        {
            DrawMarker(e.Graphics, currentMarkerPosition.X, currentMarkerPosition.Y);

            DrawMarker(e.Graphics, marker2.X, marker2.Y);
        }

        

        private const int MarkerWidth = 10;
        
        //draws a marker at gis location locX,locY
        private void DrawMarker(Graphics g, double locX, double locY)
        {
            //convert the gis location to pixel coordinates
            Point pt = sfMap1.GisPointToPixelCoord(locX, locY);
            
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

            //draw a marker centered at the gis location
            //alternative is to draw an image/icon
            g.DrawLine(Pens.Red, pt.X, pt.Y - MarkerWidth, pt.X, pt.Y + MarkerWidth);
            g.DrawLine(Pens.Red, pt.X - MarkerWidth, pt.Y, pt.X + MarkerWidth, pt.Y);
            pt.Offset(-MarkerWidth / 2, -MarkerWidth/2);
            g.FillEllipse(Brushes.Yellow, pt.X, pt.Y, MarkerWidth, MarkerWidth);
            g.DrawEllipse(Pens.Red, pt.X, pt.Y, MarkerWidth, MarkerWidth);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            try
            {
                OpenShapefile(Application.StartupPath + "\\roads.shp");
                sfMap1[0].RenderSettings.FieldName = "ROAD_NAME";
                sfMap1[0].RenderSettings.Font = new Font(this.Font.FontFamily, 8);
                sfMap1.ZoomLevel *= 4;
                ProcessGPSData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

        /// <summary>
        /// Processes a GPS data text file and returns a list of GpsPacket objects
        /// Each line in the data file is expected to conain a valid $GPGGA GPS signature string
        /// </summary>
        /// <param name="pathIn"></param>
        /// <returns></returns>
        private System.Collections.Generic.List<GpsPacket> ProcessGPSDataFile(string pathIn)
        {
            System.Collections.Generic.List<GpsPacket> packetList = new List<GpsPacket>();
            using (System.IO.StreamReader sr = new System.IO.StreamReader(pathIn))
            {                
                string nextLine;
                while ((nextLine = sr.ReadLine()) != null)
                {
                    nextLine = nextLine.Trim();
                    if (nextLine.Length > 0 && nextLine.IndexOf("GPGGA") >=0)
                    {
                        string gpsString = nextLine.Substring(nextLine.IndexOf("$GPGGA")).Trim();
                        GpsPacket packet = new GpsPacket(gpsString);
                        if (packet.IsValid && packet.Fix)
                        {
                            packetList.Add(packet);
                        }
                    }
                }
                
            }
            return packetList;
        }

        /// <summary>
        /// Processes the gps data file and reset the marker to the first GPS point
        /// </summary>
        private void ProcessGPSData()
        {
            gpsDataList = this.ProcessGPSDataFile(Application.StartupPath + "\\gpsdata.txt");
            if (gpsDataList.Count > 0)
            {
                currentMarkerPosition = new EGIS.ShapeFileLib.PointD(gpsDataList[0].Longitude, gpsDataList[0].Latitude);
                sfMap1.CentrePoint2D = currentMarkerPosition;
            }
            currentPacketIndex = 0;
        }        

        private List<GpsPacket> gpsDataList = new List<GpsPacket>();
        private int currentPacketIndex = 0;

        
        private void packetTimer_Tick(object sender, EventArgs e)
        {
            if (currentPacketIndex < gpsDataList.Count)
            {
                currentMarkerPosition = new EGIS.ShapeFileLib.PointD(gpsDataList[currentPacketIndex].Longitude, gpsDataList[currentPacketIndex].Latitude);
                currentPacketIndex++;
                if (this.miCenterMarker.Checked)
                {
                    sfMap1.CentrePoint2D = currentMarkerPosition;
                }
                sfMap1.Refresh();
            }
        }
        

        private void restartGPSProcessingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ProcessGPSData();
        }

        private void sfMap1_Click(object sender, EventArgs e)
        {
            //MessageBox.Show(this, Cursor.Position.ToString());
        }

        private void sfMap1_MouseClick(object sender, MouseEventArgs e)
        {
           // MessageBox.Show(this, sfMap1.PixelCoordToGisPoint(e.Location).ToString());

        }


        private EGIS.ShapeFileLib.PointD marker2 = new EGIS.ShapeFileLib.PointD(0, 0);
        
       

        private void button1_Click(object sender, EventArgs e)
        {
            marker2.Y = double.Parse(txtLat.Text);
            marker2.X = double.Parse(txtLon.Text);

            sfMap1.Refresh();
        }

                
    }
}