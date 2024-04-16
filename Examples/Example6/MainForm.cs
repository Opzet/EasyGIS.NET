using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text;
using System.Windows.Forms;
using System.Net.Cache;
using EGIS.ShapeFileLib;
using System.Net.Http;
using EGIS.Controls;

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
 * Copyright: Easy GIS .NET 2009 - 2020
 *
 */
namespace Example6
{
    /// <summary>
    /// Main Form of Example 6
    /// </summary>
    /// <remarks>
    /// <para>
    /// This example extends Example 5 and shows a map displayed in a Windows Form, overlayed with tiles from Open Street Map
    /// </para>
    /// </remarks>
    public partial class MainForm : Form
    {
        /// <summary>
        /// EGIS.Controls.BaseMapLayer - renders tiles from WMS serves such as OSM
        /// </summary>
        private BaseMapLayer baseMapLayer = null;

        public MainForm()
        {
            InitializeComponent();

            // TO display BaseMapLayer the map must use Wgs84PseudoMercator CRS
            sfMap1.MapCoordinateReferenceSystem = EGIS.Projections.CoordinateReferenceSystemFactory.Default.GetCRSById(EGIS.Projections.CoordinateReferenceSystemFactory.Wgs84PseudoMercatorEpsgCode);

            //create a transformation object to transform gps lat/lon to PseudoMercator (3857)
            //this wil lbe used to transform wgs84 lat/lon coordinates to/from PseudoMercator
            gpsTransformation = EGIS.Projections.CoordinateReferenceSystemFactory.Default.CreateCoordinateTrasformation(
                EGIS.Projections.CoordinateReferenceSystemFactory.Default.GetCRSById(EGIS.Projections.CoordinateReferenceSystemFactory.Wgs84EpsgCode),
                EGIS.Projections.CoordinateReferenceSystemFactory.Default.GetCRSById(EGIS.Projections.CoordinateReferenceSystemFactory.Wgs84PseudoMercatorEpsgCode));

            baseMapLayer = new BaseMapLayer(this.sfMap1, null);
         
            LoadTileSources();
		}

		#region Open Shapefile

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
		
		#endregion

		private EGIS.ShapeFileLib.PointD currentMarkerPosition = new EGIS.ShapeFileLib.PointD(0, 0);

      
        private void sfMap1_Paint(object sender, PaintEventArgs e)
        {
            DrawMarker(e.Graphics, currentMarkerPosition.X, currentMarkerPosition.Y);           
        }


      

        #region Tile Sources
			

        /// <summary>
        /// load availalbe tile sources in a combo box for user selection
        /// </summary>
		private void LoadTileSources()
		{
            TileSource[] tileSources = TileSource.DefaultTileSources();

			this.cbTileSource.Items.Clear();
			this.cbTileSource.Items.AddRange(tileSources);
			this.cbTileSource.SelectedIndex = 0;
            if (tileSources.Length > 0) this.baseMapLayer.TileSource = tileSources[0];
		}

		private void cbTileSource_SelectedIndexChanged(object sender, EventArgs e)
		{
            this.baseMapLayer.TileSource = cbTileSource.SelectedItem as TileSource;
			//sfMap1.Refresh(true);
		}



        #endregion

     
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
                sfMap1[0].RenderSettings.OutlineColor = Color.RoyalBlue;
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
            currentPacketIndex = 0;
            currentMarkerPosition = GetNextGpsPosition();            
        }        

        private List<GpsPacket> gpsDataList = new List<GpsPacket>();
        private int currentPacketIndex = 0;
        private EGIS.Projections.ICoordinateTransformation gpsTransformation = null;

        private PointD GetNextGpsPosition()
        {
            if (currentPacketIndex < gpsDataList.Count)
            {
                var latLonCoord = new EGIS.ShapeFileLib.PointD(gpsDataList[currentPacketIndex].Longitude, gpsDataList[currentPacketIndex].Latitude);
                currentMarkerPosition = gpsTransformation.Transform(latLonCoord);
                ++currentPacketIndex;
            }
            return currentMarkerPosition;            
        }
        
        private void packetTimer_Tick(object sender, EventArgs e)
        {
            if (currentPacketIndex < gpsDataList.Count)
            {
                currentMarkerPosition = GetNextGpsPosition();
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

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }

    
}