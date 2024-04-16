using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using EGIS.Controls;
using EGIS.ShapeFileLib;

namespace HeatmapDemo
{
	public partial class MainForm : Form
	{
        private BaseMapLayer baseMapLayer = null;

        public MainForm()
		{
			InitializeComponent();
		}

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

        private void openShapeFileToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (ofdShapeFile.ShowDialog(this) == DialogResult.OK)
			{
				OpenShapeFile(ofdShapeFile.FileName);

			}
		}

		private void OpenShapeFile(string fileName)
		{
			this.sfMap1.ClearShapeFiles();
			var shapeFile = this.sfMap1.AddShapeFile(fileName, "layer", "");

			shapeFile.RenderSettings.PointSize = 2;
			sfMap1.Refresh(true);
		}

		private void sfMap1_Paint(object sender, PaintEventArgs e)
		{
			if (sfMap1.ShapeFileCount > 0)
			{
				var heatMapImage = CreateHeatMap();

				using (var bitmap = heatMapImage.GetHeatMap())
				{
					e.Graphics.DrawImage(bitmap, new Point(0, 0));
				}
			}
		}

        private HeatMap.HeatMapImage CreateHeatMap()
        {
            var shapeFile = sfMap1[0];
            if (shapeFile.ShapeType != EGIS.ShapeFileLib.ShapeType.Point) return null;

            int w = sfMap1.ClientSize.Width;
            int h = sfMap1.ClientSize.Height;

            int gaussianSize = (int)nudSize.Value;
            int gSigma = (int)nudSigma.Value;

            HeatMap.HeatMapImage heatMapImage = new HeatMap.HeatMapImage(w, h, gaussianSize, gSigma);

            int count = shapeFile.RecordCount;

            List<HeatMap.DataType> data = new List<HeatMap.DataType>(count + 10);
            Random rand = new Random();

            // Introduce a function to calculate weight based on density or other criteria.
            Func<int, double> CalculateWeight = (index) =>
            {
                // As a simple example, we add random variability to the weight
                return 10 * rand.NextDouble();  // this will generate a weight between 0 and 10
            };

            for (int n = 0; n < count; ++n)
            {
                EGIS.ShapeFileLib.PointD pt = shapeFile.GetShapeDataD(n)[0][0];
                var pixelPt = sfMap1.GisPointToPixelCoord(pt);

                data.Add(new HeatMap.DataType()
                {
                    X = pixelPt.X,
                    Y = pixelPt.Y,
                    Weight = CalculateWeight(n)
                });
            }

            heatMapImage.SetDatas(data);

            return heatMapImage;
        }


        private void nudSize_ValueChanged(object sender, EventArgs e)
		{
			sfMap1.Refresh();
		}

		private void nudSigma_ValueChanged(object sender, EventArgs e)
		{
			sfMap1.Refresh();

		}

        private void MainForm_Load(object sender, EventArgs e)
        {
            // TO display BaseMapLayer the map must use Wgs84PseudoMercator CRS
            sfMap1.MapCoordinateReferenceSystem = EGIS.Projections.CoordinateReferenceSystemFactory.Default.GetCRSById(EGIS.Projections.CoordinateReferenceSystemFactory.Wgs84PseudoMercatorEpsgCode);

            //create a transformation object to transform gps lat/lon to PseudoMercator (3857)
            //this wil lbe used to transform wgs84 lat/lon coordinates to/from PseudoMercator
            //gpsTransformation = EGIS.Projections.CoordinateReferenceSystemFactory.Default.CreateCoordinateTrasformation(
            //    EGIS.Projections.CoordinateReferenceSystemFactory.Default.GetCRSById(EGIS.Projections.CoordinateReferenceSystemFactory.Wgs84EpsgCode),
            //    EGIS.Projections.CoordinateReferenceSystemFactory.Default.GetCRSById(EGIS.Projections.CoordinateReferenceSystemFactory.Wgs84PseudoMercatorEpsgCode));

            baseMapLayer = new BaseMapLayer(this.sfMap1, null);

            LoadTileSources();
        }

        private void button1_Click(object sender, EventArgs e)
        {
			Create2dPoints();
        }

		int fileId = 0;

		
        void Create2dPoints()
        {

           

            //PointD Midpt = new PointD((sfMap1.VisibleExtent.Left + sfMap1.VisibleExtent.Right) / 2, (sfMap1.VisibleExtent.Top + sfMap1.VisibleExtent.Bottom) / 2);

            DbfFieldDesc[] dbfFields = new DbfFieldDesc[3];

            dbfFields[0].FieldName = "RecordNo";
            dbfFields[0].FieldType = DbfFieldType.Number;
            dbfFields[0].FieldLength = 10;

            dbfFields[1].FieldName = "SSID";
            dbfFields[1].FieldType = DbfFieldType.General;
            dbfFields[1].FieldLength = 100;


            dbfFields[2].FieldName = "RSSI";
            dbfFields[2].FieldType = DbfFieldType.Number;
            dbfFields[2].FieldLength = 10;



            //Create indiviidual memory streams and pass then to the ShapeFileWriter

            System.IO.MemoryStream shxStream = new System.IO.MemoryStream();
            System.IO.MemoryStream shpStream = new System.IO.MemoryStream();
            System.IO.MemoryStream dbfStream = new System.IO.MemoryStream();
            System.IO.MemoryStream prjStream = new System.IO.MemoryStream();
			//wrap in a using clause so the writer is disposed when you finish writing the records
			//when the writer is disposed it will properly update the shapefile file headers and footers
			//you need to create separate shapefiles for each geometry type
			textBox1.Text = "";

            // string fileName = $"RssiRandom{fileId}";
            //--sample for show smothness
            var rnd = new Random();

            double rangeX = sfMap1.VisibleExtent.Right - sfMap1.VisibleExtent.Left;
            double rangeY = sfMap1.VisibleExtent.Top - sfMap1.VisibleExtent.Bottom;

            //            using (ShapeFileWriter shapeWriter = ShapeFileWriter.CreateWriter(Application.StartupPath, fileName, ShapeType.Polygon, dbfFields, sfMap1.MapCoordinateReferenceSystem.WKT))
            using (ShapeFileWriter writer = ShapeFileWriter.CreateWriter(shxStream, shpStream, dbfStream, prjStream, ShapeType.Point, dbfFields,  sfMap1.MapCoordinateReferenceSystem.WKT))
            {
                PointD[] pt = new PointD[10];
                string[] fielddata = new string[3];

                for (int i = 0; i < 10; i++)
                {
                    
                    double longitudeX = Convert.ToDouble(sfMap1.VisibleExtent.Left + (double)rnd.Next(0, 300 )/ 1000000);
                    double latitudeY = Convert.ToDouble(sfMap1.VisibleExtent.Bottom  + (double)rnd.Next(0, 300)/1000000);

                    LatLongCoordinate coord = new LatLongCoordinate(); 
                    coord.Longitude = longitudeX;
                    coord.Latitude = latitudeY;

                    

					//   dataFields:
					//     array of DbfFieldDesc objects describing the fields to be created in the shape
					//     file's DBF file
					fielddata[0] = i.ToString();  //  //Weight - RSSI
                    fielddata[1] = $" A ";
                    fielddata[2] = rnd.Next(-89, -20).ToString();

                    pt[i] = new PointD( longitudeX, latitudeY);

                    writer.AddRecord(pt, 1, fielddata);

					textBox1.Text += $"{fielddata[0]} : SSID{fielddata[1]} : RSSI : {fielddata[2]} : Lat (x) - {longitudeX}  | Lon(y) - {latitudeY} \r\n";
                }

                writer.Close();

            }
            //create a ShapeFile object from the memory streams and add it to the map
            ShapeFile shapeFile = new ShapeFile(shxStream, shpStream, dbfStream, prjStream);
            //set the RenderSettings FieldName property the the desc field. This is used to label the records
            shapeFile.RenderSettings.FieldName = "SSID";
            sfMap1.AddShapeFile(shapeFile);
        }

        private void sfMap1_MouseMove(object sender, MouseEventArgs e)
        {
            //lblLon
        }
    }
}
