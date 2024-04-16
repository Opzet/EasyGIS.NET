using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using EGIS.ShapeFileLib;

namespace EGISCodeSnippets
{
	public partial class MainForm : Form
	{
		public MainForm()
		{
			InitializeComponent();

			//set maps CRS to WGS84
			sfMap1.MapCoordinateReferenceSystem = EGIS.Projections.CoordinateReferenceSystemFactory.Default.GetCRSById(EGIS.Projections.CoordinateReferenceSystemFactory.Wgs84EpsgCode);			
		}


		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			CreateShapeFile();
			
			LoadBackgroundForegroundExampleData();
			LoadCustomLabelsExample();

			TestDrawShapeFilesToBitmap(System.IO.Path.Combine(Application.StartupPath, "data", "basemap_route_road.shp"));

			TestDrawShapeFilesToBitmap(System.IO.Path.Combine(Application.StartupPath, "data/world_adm0.shp"));

			WriteShapeFile();

			TestPolyline();

			
		}

		/// <summary>
		/// Creates a new shapefile using MemoryStreams and a ShapeFileWriter. The ShapeFile is then loaded from the memory streams
		/// and added to a SFMap
		/// </summary>
		private void CreateShapeFile()
		{
			//setup the attribute fields (stored in dbf file)
			//Note that FieldLength is length in bytes. If you are writing text using Unicode characters they are 
			//encoded in UTF-8, which means they may b 1,2,3 or 4 byts long. Make sure the FieldLength is long enough!

			DbfFieldDesc[] dbfFields = new DbfFieldDesc[2];

			dbfFields[0].FieldName = "RecordNo";
			dbfFields[0].FieldType = DbfFieldType.General;
			dbfFields[0].FieldLength = 10;

			dbfFields[1].FieldName = "Desc";
			dbfFields[1].FieldType = DbfFieldType.General;
			dbfFields[1].FieldLength = 100;

			//Create indiviidual memory streams and pass then to the ShapeFileWriter
			
			System.IO.MemoryStream shxStream = new System.IO.MemoryStream();
			System.IO.MemoryStream shpStream = new System.IO.MemoryStream();
			System.IO.MemoryStream dbfStream = new System.IO.MemoryStream();
			System.IO.MemoryStream prjStream = new System.IO.MemoryStream();
			//wrap in a using clause so the writer is disposed when you finish writing the records
			//when the writer is disposed it will properly update the shapefile file headers and footers
			//This example is creating a polyline shapefile. ShapeFiles don't allow mixing points, polylines and polygons in the 
			//same shapefile, so you need to create separate shapefiles for each geometry type
			using (ShapeFileWriter writer = ShapeFileWriter.CreateWriter(shxStream, shpStream, dbfStream, prjStream, ShapeType.PolyLine, dbfFields, sfMap1.MapCoordinateReferenceSystem.WKT))
			{
				//add some records
				PointD[] points = new PointD[10];
				string[] attributes = new string[2];

				attributes[0] = "0";
				attributes[1] = "Record number 0 description";
				points[0] = new PointD(145, -37.5);
				points[1] = new PointD(146, -37.5);
				points[2] = new PointD(146, -38);
				writer.AddRecord(points, 3, attributes);

				//add a record with an attribute with some unicode text
				attributes[0] = "1";
				attributes[1] = "Record number 1\nGreek characters\nΣὲ γνωρίζω ἀπὸ τὴν κόψη";
				points[0] = new PointD(140, -36.5);
				points[1] = new PointD(143, -36.5);				
				writer.AddRecord(points, 2, attributes);

				attributes[0] = "2";
				attributes[1] = "Record number 2 XXXX";
				points[0] = new PointD(143, -36.5);
				points[1] = new PointD(143, -37.5);				
				writer.AddRecord(points, 2, attributes);

				attributes[0] = "3";
				attributes[1] = "Record 3 --- XXXX OOO XXXX ---";
				points[0] = new PointD(143, -37.5);
				points[1] = new PointD(140, -37.5);
				points[2] = new PointD(135.5, -34.5);

				writer.AddRecord(points, 3, attributes);
			}

			//create a ShapeFile object from the memory streams and add it to the map
			ShapeFile shapeFile = new ShapeFile(shxStream, shpStream, dbfStream, prjStream);
			//set the RenderSettings FieldName property the the desc field. This is used to label the records
			shapeFile.RenderSettings.FieldName = "desc";
			sfMap1.AddShapeFile(shapeFile);
		}


		ShapeFile foregroundLayer = null;



		void Create2dPoints()
		{

            //--sample for show smothness
            var rnd = new Random();
            var rangeX = Convert.ToInt32((sfMap1.Extent.Right - sfMap1.Extent.Left) * 100000);
            var rangeY = Convert.ToInt32((sfMap1.Extent.Top - sfMap1.Extent.Bottom) * 100000);


            //PointD Midpt = new PointD((sfMap1.Extent.Left + sfMap1.Extent.Right) / 2, (sfMap1.Extent.Top + sfMap1.Extent.Bottom) / 2);

            DbfFieldDesc[] dbfFields = new DbfFieldDesc[2];

            dbfFields[0].FieldName = "RecordNo";
            dbfFields[0].FieldType = DbfFieldType.Number;
            dbfFields[0].FieldLength = 10;

            dbfFields[1].FieldName = "RSSI";
            dbfFields[1].FieldType = DbfFieldType.General;
            dbfFields[1].FieldLength = 100;



            //Create indiviidual memory streams and pass then to the ShapeFileWriter

            System.IO.MemoryStream shxStream = new System.IO.MemoryStream();
            System.IO.MemoryStream shpStream = new System.IO.MemoryStream();
            System.IO.MemoryStream dbfStream = new System.IO.MemoryStream();
            System.IO.MemoryStream prjStream = new System.IO.MemoryStream();
            //wrap in a using clause so the writer is disposed when you finish writing the records
            //when the writer is disposed it will properly update the shapefile file headers and footers
            //you need to create separate shapefiles for each geometry type

            using (ShapeFileWriter writer = ShapeFileWriter.CreateWriter(shxStream, shpStream, dbfStream, prjStream, ShapeType.Point, dbfFields, sfMap1.MapCoordinateReferenceSystem.WKT))
			{
			
				for  (int i = 0; i<10; i++)
				{
                    PointD[] pt = new PointD[1];
                    string[] fielddata = new string[2];

                    double longitude1 = Convert.ToDouble(sfMap1.Extent.Left + (double)rnd.Next(0, rangeX) / 100000);
                    double latitude1 = Convert.ToDouble(sfMap1.Extent.Bottom + (double)rnd.Next(0, rangeY) / 100000);


					fielddata[0] = rnd.Next(-89, -20).ToString();  //Weight - RSSI
					fielddata[1] = "RSSI";

					pt[0] = new PointD(latitude1, longitude1);
					
					writer.AddRecord(pt, 1, fielddata);
				}

            }
            //create a ShapeFile object from the memory streams and add it to the map
            ShapeFile shapeFile = new ShapeFile(shxStream, shpStream, dbfStream, prjStream);
            //set the RenderSettings FieldName property the the desc field. This is used to label the records
            shapeFile.RenderSettings.FieldName = "desc";
            sfMap1.AddShapeFile(shapeFile);
        }

        private void LoadBackgroundForegroundExampleData()
		{
			//set SFMap CRS to WGS84 (optional)
			var crs = EGIS.Projections.CoordinateReferenceSystemFactory.Default.GetCRSById(EGIS.Projections.CoordinateReferenceSystemFactory.Wgs84EpsgCode);
			this.sfMap2.MapCoordinateReferenceSystem = crs;


			//add a basemap shapefile to the background
			string shapeFilePath = System.IO.Path.Combine(Application.StartupPath, "data", "basemap_route_road.shp");
			sfMap2.AddShapeFile(shapeFilePath, "basemap_route", "",false,true,true,EGIS.Controls.LayerPositionEnum.Background);
			
			//add a second layer to the foreground so we can toggle it on/off without having to redraw the background layer 
			shapeFilePath = System.IO.Path.Combine(Application.StartupPath, "data", "test.shp");
			foregroundLayer = sfMap2.AddShapeFile(shapeFilePath, "basemap_route", "", false, false, true, EGIS.Controls.LayerPositionEnum.Foreground);
			foregroundLayer.RenderSettings.OutlineColor = Color.Red;			
		}

		private void chkToggleForegroundLayer_CheckedChanged(object sender, EventArgs e)
		{
			//chkToggleForegroundLayer.Checked = !chkToggleForegroundLayer.Checked;

			if (!chkToggleForegroundLayer.Checked)
			{
				sfMap2.RemoveShapeFile(foregroundLayer);
			}
			else
			{
				sfMap2.AddShapeFile(foregroundLayer, false, true, EGIS.Controls.LayerPositionEnum.Foreground);
			}
		}


		#region custom labels

		private void LoadCustomLabelsExample()
		{
			string worldShapefilePath = System.IO.Path.Combine(Application.StartupPath, "data/world_adm0.shp");
			var shapefile = sfMap3.AddShapeFile(worldShapefilePath, "world", "", false);
			shapefile.RenderSettings.Font = new Font(this.Font.FontFamily, 12);
			shapefile.RenderSettings.CustomRenderSettings = new CustomLabelRenderSettings(shapefile.RenderSettings);
		}

		/// <summary>
		/// simple ICustomRenderSetgings class that creates record labels using all attributes
		/// </summary>
		private class CustomLabelRenderSettings : EGIS.ShapeFileLib.BaseCustomRenderSettings
		{
			public CustomLabelRenderSettings(EGIS.ShapeFileLib.RenderSettings renderSettings) :
				base(renderSettings)
			{

			}

			public override bool UseCustomRecordLabels
			{
				get { return true; }
			}

			public override string GetRecordLabel(int recordNumber)
			{
				//return all of the record attributes separated by newline
				string[] recordAttributes = base.renderSettings.DbfReader.GetFields(recordNumber);

				string label = string.Join("\n", recordAttributes);
				return label;
			}

		}

		#endregion


		#region Draw Shapefiles to a bitmap

		private void TestDrawShapeFilesToBitmap(string shapeFilePath)
		{
			List<ShapeFile> layers = new List<ShapeFile>();
			
			ShapeFile sf = new ShapeFile(shapeFilePath);
			layers.Add(sf);
			using (Bitmap bm = new Bitmap(512, 512))
			{
				PointD pt = new PointD((sf.Extent.Left + sf.Extent.Right) / 2, (sf.Extent.Top + sf.Extent.Bottom) / 2);
				double scale = bm.Width/ sf.Extent.Width;
				DrawShapeFilesToBitmap(bm, layers, Color.White, pt, scale, sf.CoordinateReferenceSystem);
				bm.Save(System.IO.Path.ChangeExtension(shapeFilePath, ".png"), System.Drawing.Imaging.ImageFormat.Png);
			}
		}

		public static void DrawShapeFilesToBitmap(Bitmap bitmap, List<ShapeFile> shapeFiles, Color backgroundColor, 
			PointD centrePoint, double scale, EGIS.Projections.ICRS crs=null)
		{
			if (crs == null) crs = shapeFiles[0].CoordinateReferenceSystem;
			//expand the render size by 10 pixels to avoid drawing cropped shapes on borders
			Size renderSize = bitmap.Size;
			renderSize.Width += 10;
			renderSize.Height += 10;
			using (Graphics g = System.Drawing.Graphics.FromImage(bitmap))
			{
				g.Clear(backgroundColor);				
				foreach (EGIS.ShapeFileLib.ShapeFile sf in shapeFiles)
				{
					sf.Render(g, renderSize, centrePoint, scale, ProjectionType.None, crs);
				}								
			}
		}

		#endregion



		private void WriteShapeFile()
		{
			string fileName = "test_25832";
			DbfFieldDesc[] fieldDescriptions = new DbfFieldDesc[1];
			fieldDescriptions[0].FieldName = "Record";
			fieldDescriptions[0].FieldLength = 10;
			fieldDescriptions[0].FieldType = DbfFieldType.Number;
			PointD[] pts = new PointD[5];

			string wkt = EGIS.Projections.CoordinateReferenceSystemFactory.Default.GetCRSById(EGIS.Projections.CoordinateReferenceSystemFactory.Wgs84EpsgCode).WKT;// GetWKT(EGIS.Projections.PJ_WKT_TYPE.PJ_WKT1_GDAL, false);

			wkt = EGIS.Projections.CoordinateReferenceSystemFactory.Default.GetCRSById(EGIS.Projections.CoordinateReferenceSystemFactory.Wgs84EpsgCode).GetWKT(EGIS.Projections.PJ_WKT_TYPE.PJ_WKT1_GDAL, true);
			fileName = "test_4326";
			using (ShapeFileWriter shapeWriter = ShapeFileWriter.CreateWriter(Application.StartupPath, fileName, ShapeType.Polygon, fieldDescriptions,
																		wkt))
			{
				//500000
				pts[0] = new PointD(7, 45);
				pts[1].X = pts[0].X;
				pts[1].Y = pts[0].Y + 5;
				pts[2].X = pts[1].X + 3.5;
				pts[2].Y = pts[1].Y;
				pts[3].X = pts[2].X;
				pts[3].Y = pts[0].Y;
				pts[4] = pts[0];

				bool isHole = EGIS.ShapeFileLib.GeometryAlgorithms.IsPolygonHole(pts, 5);
				Console.Out.WriteLine("is hole: " + isHole);

				string[] attributes = { "1" };
				shapeWriter.AddRecord(pts, pts.Length, attributes);
			}

			fileName = "test_25832";
			wkt = EGIS.Projections.CoordinateReferenceSystemFactory.Default.GetCRSById(25832).GetWKT(EGIS.Projections.PJ_WKT_TYPE.PJ_WKT1_GDAL, false);
			

			using (ShapeFileWriter shapeWriter = ShapeFileWriter.CreateWriter(Application.StartupPath, fileName, ShapeType.Polygon, fieldDescriptions, wkt))
			{
				//500000

				using (var transform = EGIS.Projections.CoordinateReferenceSystemFactory.Default.CreateCoordinateTrasformation(	EGIS.Projections.CoordinateReferenceSystemFactory.Default.GetCRSById(4326),
					EGIS.Projections.CoordinateReferenceSystemFactory.Default.GetCRSById(25832)))
				{

					transform.Transform(pts);
				}

				
				bool isHole = EGIS.ShapeFileLib.GeometryAlgorithms.IsPolygonHole(pts, 5);
				Console.Out.WriteLine("is hole: " + isHole);

				string[] attributes = { "1" };
				shapeWriter.AddRecord(pts, pts.Length, attributes);
			}


			//BBOX[38.76,6,83.92,12]]

			

		}


		private void TestPolyline()
		{
			PointD[] polyline = new PointD[] { new PointD(-10, 0), new PointD(-5, 0), new PointD(0, 0), new PointD(10, 0) };

			PolylineDistanceInfo pdi = new PolylineDistanceInfo();

			PointD testPoint = new PointD(5, 5);

			double distance = GeometryAlgorithms.ClosestPointOnPolyline(polyline, 0, polyline.Length, testPoint, out pdi);

			Console.Out.WriteLine("distance:{0}", distance);
			Console.Out.WriteLine("pdi.PointIndex:{0}", pdi.PointIndex);
			Console.Out.WriteLine("pdi.TVal:{0}", pdi.TVal);
			Console.Out.WriteLine("pdi.PolylinePoint:{0}", pdi.PolylinePoint);

			testPoint = new PointD(-6, -5);

			distance = GeometryAlgorithms.ClosestPointOnPolyline(polyline, 0, polyline.Length, testPoint, out pdi);

			Console.Out.WriteLine("\ndistance:{0}", distance);
			Console.Out.WriteLine("pdi.PointIndex:{0}", pdi.PointIndex);
			Console.Out.WriteLine("pdi.TVal:{0}", pdi.TVal);
			Console.Out.WriteLine("pdi.PolylinePoint:{0}", pdi.PolylinePoint);

			testPoint = new PointD(-4, -5);

			distance = GeometryAlgorithms.ClosestPointOnPolyline(polyline, 0, polyline.Length, testPoint, out pdi);

			Console.Out.WriteLine("\ndistance:{0}", distance);
			Console.Out.WriteLine("pdi.PointIndex:{0}", pdi.PointIndex);
			Console.Out.WriteLine("pdi.TVal:{0}", pdi.TVal);
			Console.Out.WriteLine("pdi.PolylinePoint:{0}", pdi.PolylinePoint);



		}

		int rectangleWidth = 1;
		int increment = 1;

		int paintCount = 0;
		DateTime paintTime = DateTime.Now;
		int fps = 0;

		private void timer1_Tick(object sender, EventArgs e)
		{
			rectangleWidth += increment;
			sfMap2.Invalidate();

			if (rectangleWidth > 300) increment = -1;
			else if (rectangleWidth == 1) increment = 1;
			
		}

		private void sfMap2_Paint(object sender, PaintEventArgs e)
		{
			e.Graphics.DrawRectangle(Pens.Red, 5, 5, rectangleWidth, rectangleWidth);

			++paintCount;
			double elapsedtime = DateTime.Now.Subtract(paintTime).TotalSeconds;
			if (elapsedtime > 3)
			{
				fps = (int)Math.Round(paintCount / elapsedtime);
				paintTime = DateTime.Now;
				paintCount = 0;
			}
			e.Graphics.DrawString(string.Format("FPS: {0}", fps), this.Font, Brushes.Red, 20, 20);
		}

		private void chkHideEverySecondRecord_CheckedChanged(object sender, EventArgs e)
		{
			if (chkHideEverySecondRecord.Checked)
			{
				sfMap3[0].RenderSettings.CustomRenderSettings = new ExampleCustomRenderSettings(sfMap3[0].RenderSettings);
			}
			else
			{
				sfMap3[0].RenderSettings.CustomRenderSettings = null;
			}
			sfMap3.Refresh(true);
		}
	}
}
