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
 * Copyright: Easy GIS .NET 2010
 *
 */
namespace Example8
{
    /// <summary>
    /// Main form of Example 8
    /// </summary>
    /// <remarks>
    /// <para>
    /// This example shows how to use an ICustomRenderSettings class to render roads in shapefile in different colors
    /// depending on the type of road.<br/>
    /// The example also show how to use an ICustomRenderSettings class to render a POI point shapefile with different
    /// icons depending on the POI category.
    /// </para>
    /// <para>
    /// The example also shows how the SFMap control can display Unicode characters
    /// </para>
    /// <para>
    /// The Shapefiles used in this example are sourced from OpenStreetMap under the Creative Commons Attribution-Share Alike 2.0 License
    /// </para>
    /// </remarks>
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

            var crs = EGIS.Projections.CoordinateReferenceSystemFactory.Default.GetCRSById(EGIS.Projections.CoordinateReferenceSystemFactory.Wgs84EpsgCode);
            this.sfMap1.MapCoordinateReferenceSystem = crs;

            //open the roads and POI shapefiles
            OpenRoadShapefile("taiwan_highway.shp");
            OpenPOIShapefile("taiwan_poi.shp");
            //set the zoom and center location of the map
            sfMap1.SetZoomAndCentre(25000, new EGIS.ShapeFileLib.PointD(120.63, 24.175));

			sfMap1.PreviewKeyDown += SfMap1_PreviewKeyDown;
			sfMap1.KeyUp += SfMap1_KeyUp;
			
        }

		private void SfMap1_KeyUp(object sender, KeyEventArgs e)
		{
			if (disablingControlSelect)
			{
				sfMap1.PanSelectMode = EGIS.Controls.PanSelectMode.Pan;
				disablingControlSelect = false;
			}
		}

		private bool disableControlDragSelect = true;

		private bool disablingControlSelect = false;

		private void SfMap1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
		{
			if (disableControlDragSelect && (e.KeyCode == Keys.ControlKey || e.KeyCode == Keys.ShiftKey))
			{				
				sfMap1.PanSelectMode = EGIS.Controls.PanSelectMode.None;
				disablingControlSelect = true;
			}
		}

		private void miOpen_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void OpenRoadShapefile(string path)
        {
            
            // open the shapefile passing in the path, display name of the shapefile and
            // the field name to be used when rendering the shapes (we use an empty string
            // as the field name (3rd parameter) can not be null)
            EGIS.ShapeFileLib.ShapeFile sf = this.sfMap1.AddShapeFile(path, "ShapeFile", "name");
            
            // Setup a dictionary collection of road types and colors
            // We will use this when creating a RoadTypeCustomRenderSettings class to setup which
            //colors should be used to render each type of road
            sf.RenderSettings.FieldName = "name";
            sf.RenderSettings.Font = new Font(this.Font.FontFamily, 12);
            Dictionary<string, Color> colors = new Dictionary<string,Color>();
            colors.Add("motorway", Color.Green);
            colors.Add("motorway_link", Color.Green);
            colors.Add("primary", Color.Blue);
            colors.Add("secondary", Color.Yellow);
            RoadTypeCustomRenderSettings rs = new RoadTypeCustomRenderSettings(sf.RenderSettings, "TYPE", colors);
            sf.RenderSettings.CustomRenderSettings = rs;
            sf.RenderSettings.UseToolTip = true;
            sf.RenderSettings.ToolTipFieldName = "name";
            sf.RenderSettings.MaxPixelPenWidth = 20;
			//  sf.RenderSettings.PenWidthScale = 0.0001f;

			sf.RenderSettings.IsSelectable = true;
        }

        private void OpenPOIShapefile(string path)
        {
            
            // open the shapefile passing in the path, display name of the shapefile and
            // the field name to be used when rendering the shapes (we use an empty string
            // as the field name (3rd parameter) can not be null)
            EGIS.ShapeFileLib.ShapeFile sf = this.sfMap1.AddShapeFile(path, "ShapeFile", "");

            sf.RenderSettings.FieldName = "name";
            sf.RenderSettings.Font = new Font(this.Font.FontFamily, 12);
            
            //BUG: currently RenderSettings need a default image for the shapefile to use the CustomRenderSetting images
            //just supply a default image to get custom images rendering
            //sf.RenderSettings.PointImageSymbol = "def.gif";

            // Setup a dictionary collection of POI categrories and images
            // We will use this when creating a POICustomRenderSettings class to setup which
            //images should be used to render each POI            
            Dictionary<string, System.Drawing.Image> images = new Dictionary<string, Image>();
            images.Add("Automotive", Image.FromStream(this.GetType().Assembly.GetManifestResourceStream("Example8.1.bmp")));
            images.Add("Eating&Drinking", Image.FromStream(this.GetType().Assembly.GetManifestResourceStream("Example8.Dining.BMP"))); //note resource name is case sensitive
            images.Add("Government and Public Services", Image.FromStream(this.GetType().Assembly.GetManifestResourceStream("Example8.gov.bmp")));
            images.Add("Health care", Image.FromStream(this.GetType().Assembly.GetManifestResourceStream("Example8.Hospitals.bmp")));
            images.Add("Lodging", Image.FromStream(this.GetType().Assembly.GetManifestResourceStream("Example8.Lodging.bmp")));
            images.Add("Sports", Image.FromStream(this.GetType().Assembly.GetManifestResourceStream("Example8.sport.bmp")));
            POICustomRenderSettings rs = new POICustomRenderSettings(sf.RenderSettings, "CATEGORY", images, null);//Image.FromStream(this.GetType().Assembly.GetManifestResourceStream("Example8.def.gif")));
            sf.RenderSettings.CustomRenderSettings = rs;
            sf.RenderSettings.UseToolTip = true;
            sf.RenderSettings.ToolTipFieldName = "NAME";                       
        }

        /// <summary>
        /// Simple function to determine whether the map is using lat long decimal degrees coordinates
        /// Note that this function is not guaranteed to be correct.
        /// Assumes that if UTM coords are used map extent will be outside -90 to +90
        /// </summary>
        /// <returns></returns>
        private bool IsMapUsingLatLong()
        {
            RectangleF ext = sfMap1.Extent;
            return (ext.Top <= 90 && ext.Bottom >= -90);
        }

        private void GetMapDimensionsInMeters(ref double w, ref double h)
        {
            RectangleF r = this.sfMap1.VisibleExtent;
            if (IsMapUsingLatLong())
            {
                //assume using latitude longitude
                w = EGIS.ShapeFileLib.ConversionFunctions.DistanceBetweenLatLongPoints(EGIS.ShapeFileLib.ConversionFunctions.RefEllipse,
                    r.Bottom, r.Left, r.Bottom, r.Right);
                h = EGIS.ShapeFileLib.ConversionFunctions.DistanceBetweenLatLongPoints(EGIS.ShapeFileLib.ConversionFunctions.RefEllipse,
                    r.Bottom, r.Left, r.Top, r.Left);
            }
            else
            {
                //assume coord in meters
                w = r.Width;
                h = r.Height;
            }            
        }

        private const int ScaleLineWidth = 150;
        private const int ScaleOffY = 10;
        private void sfMap1_Paint(object sender, PaintEventArgs e)
        {
            DisplayInstructions(e.Graphics);
            //draw a simple scale at the bottom of the map
            double w=0, h=0;
            //get the map width/height in meters
            GetMapDimensionsInMeters(ref w, ref h);

            //draw the scale line
            Point p1 = new Point(10, sfMap1.ClientSize.Height - ScaleOffY);
            
            e.Graphics.DrawLine(Pens.Black, p1.X, p1.Y, p1.X + ScaleLineWidth, p1.Y);
            e.Graphics.DrawLine(Pens.Black, p1.X, p1.Y, p1.X, p1.Y-8);
            e.Graphics.DrawLine(Pens.Black, p1.X+ScaleLineWidth, p1.Y, p1.X+ScaleLineWidth, p1.Y - 8);
            StringFormat sf = new StringFormat(StringFormatFlags.NoWrap);
            sf.Alignment = StringAlignment.Center;
            sf.LineAlignment = StringAlignment.Center;
            e.Graphics.DrawString(w.ToString("0000000.0m"), this.Font, Brushes.Black, new RectangleF(p1.X, p1.Y - 20, ScaleLineWidth, 20), sf);

        }

        private bool instructionsDisplayed = false;
        private void DisplayInstructions(Graphics g)
        {
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            if (instructionsDisplayed) return;
            instructionsDisplayed = true;
            StringFormat sf = new StringFormat();
            sf.Alignment = StringAlignment.Center;
            sf.LineAlignment = StringAlignment.Center;
            Font f = new Font(this.Font.FontFamily, 32, FontStyle.Bold);
            g.DrawString("Double click mouse or use mouse wheel to Zoom in/out\nClick and drag mouse to pan", f, Brushes.Black, new RectangleF(0,0,sfMap1.ClientSize.Width, sfMap1.ClientSize.Height), sf);            
        }

        private void sfMap1_MouseUp(object sender, MouseEventArgs e)
        {
            //display a message box of a shape's attributes if it is clicked (within 5 pixels)
            Point pt = new Point(e.X, e.Y);
            //loop backwards on the shapefiles as layers are drawn in the order
            for(int n=sfMap1.ShapeFileCount-1;n>=0;--n)
            {
                int recordNumber = sfMap1.GetShapeIndexAtPixelCoord(n, pt, 5);
                if (recordNumber >= 0)
                {
                    StringBuilder sb = new StringBuilder();
                    string[] attributeValues = sfMap1[n].GetAttributeFieldValues(recordNumber);
                    string[] fieldNames = sfMap1[n].GetAttributeFieldNames();
                    for (int i = 0; i < fieldNames.Length; ++i)
                    {
                        if (i > 0) sb.Append("\n");
                        sb.Append(string.Format("{0}:{1}", fieldNames[i], attributeValues[i].Trim()));
                    }
                    MessageBox.Show(this, sb.ToString());
                    return; 
                }
                
            }
        }

    }
}