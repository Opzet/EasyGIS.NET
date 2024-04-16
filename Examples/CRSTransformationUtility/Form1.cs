using System;
using System.Collections.Generic;
using System.Windows.Forms;

using EGIS.Projections;
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
namespace CRSTransformationUtility
{
    /// <summary>
    /// Main Form of CRS Transformation Utility.    
    /// <remarks>
    /// The CRSTransformationUtility is a utility to change a shapefile's Coordinate Reference System.<br/>
    /// The utility can be used to change a shapefile coordinates from geodetic lat/lon to projected coordinates or vice-versa.
    /// </remarks>
    /// </summary>
    public partial class Form1 : Form
    {

        private EGIS.Projections.ICRS sourceCrs = null;
        private EGIS.Projections.ICRS destinationCrs = EGIS.Projections.CoordinateReferenceSystemFactory.Default.GetCRSById(CoordinateReferenceSystemFactory.Wgs84EpsgCode);

        public Form1()
        {
            InitializeComponent();

            txtDestinationCRS.Text = destinationCrs.ToString();

            formToolTip.SetToolTip(chkRestrictToAreaOfUse, "Select this option if transforming to WGS 84 / Pseudo-Mercator [EPSG:3857]\nand source contains data outside of +/-85 latitude");
            formToolTip.SetToolTip(btnSelectCRS, "Click to select the target Coordinate Reference System");
        }

        /// <summary>
        /// Select CRS button click event handler.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSelectCRS_Click(object sender, EventArgs e)
        {
            //open and display a CRSSelectionForm to allow user to select the target CRS
            using (EGIS.Controls.CRSSelectionForm form = new EGIS.Controls.CRSSelectionForm())
            {
                form.SelectedCRS = destinationCrs;
                if (form.ShowDialog(this) == DialogResult.OK)
                {
                    destinationCrs = form.SelectedCRS;
                    //Console.Out.WriteLine("AreaOfUse:" + destinationCrs.AreaOfUse);
                    txtDestinationCRS.Text = destinationCrs.ToString();
                }
            }
        }

        private void btnBrowseSource_Click(object sender, EventArgs e)
        {            
            if (ofdSource.ShowDialog(this) == DialogResult.OK)
            {
                OpenSourceShapeFile(ofdSource.FileName);
            }

        }

        private void OpenSourceShapeFile(string fileName)
        {
            this.txtSource.Text = fileName;
            using (EGIS.ShapeFileLib.ShapeFile shapeFile = new EGIS.ShapeFileLib.ShapeFile(fileName))
            {
                this.sourceCrs = shapeFile.CoordinateReferenceSystem;
                this.txtSourceCRS.Text = sourceCrs.ToString();
            }
        }

        private void btnBrowseTarget_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.txtTarget.Text) && System.IO.File.Exists(this.txtTarget.Text))
            {
                this.sfdTarget.FileName = this.txtTarget.Text;
            }
            if (this.sfdTarget.ShowDialog(this) == DialogResult.OK)
            {
                this.txtTarget.Text = sfdTarget.FileName;
            }
        }

        private void btnTransform_Click(object sender, EventArgs e)
        {
            string sourceFileName = this.txtSource.Text;
            string targetFileName = this.txtTarget.Text;
            if (string.IsNullOrEmpty(sourceFileName) || !System.IO.File.Exists(sourceFileName) ||
                string.IsNullOrEmpty(targetFileName))
            {
                MessageBox.Show(this, "Please select a valid source and destination shape file", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                return;
            }
            if (sourceFileName == targetFileName)
            {
                MessageBox.Show(this, "Source and target cannot be the same shape file", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                return;
            }
            try
            {
                this.Cursor = Cursors.WaitCursor;
                DateTime tick = DateTime.Now;
                TransformShapeFileCRS(sourceFileName, targetFileName, this.destinationCrs, this.chkRestrictToAreaOfUse.Checked);
                DateTime tock = DateTime.Now;
                Console.Out.WriteLine("Total time to transform shapefile: " + tock.Subtract(tick).TotalSeconds + "s");
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// Transforms the coordinates a shapefile and writes to a new shapefile
        /// </summary>
        /// <param name="sourceShapeFileName">full path to the source shapefile</param>
        /// <param name="targetShapeFileName">full path to the target shapefile that will be created</param>
        /// <param name="targetCRS">target Coordinate Reference System</param>
        /// <param name="restrictToAreaOfUse">flag to indicate whether the source coordinates should be restricted to the targetCRS area of use before transforming coordinates</param>
        private void TransformShapeFileCRS(string sourceShapeFileName, string targetShapeFileName, ICRS targetCRS, bool restrictToAreaOfUse)
        {
            transformationProgress.Value = 0;
            int currentPercent = 0;
            using (ShapeFile sourceShapeFile = new ShapeFile(sourceShapeFileName))
            {
                ICoordinateTransformation wgs84Transformation = null;
                if (restrictToAreaOfUse)
                {
                    //area of use is defined in geodetic coordinates. Create a transformation to convert the source coordinates into WGS84
                    wgs84Transformation = CoordinateReferenceSystemFactory.Default.CreateCoordinateTrasformation(sourceShapeFile.CoordinateReferenceSystem,
                        CoordinateReferenceSystemFactory.Default.GetCRSById(CoordinateReferenceSystemFactory.Wgs84EpsgCode));
                }
                try
                {
                    //create a ICoordinateTransformation to transform coordinates from source shapefiles CRS to the target CRS
                    using (ICoordinateTransformation coordinateTransformation = CoordinateReferenceSystemFactory.Default.CreateCoordinateTrasformation(sourceShapeFile.CoordinateReferenceSystem, targetCRS))
                    using (ShapeFileWriter writer = ShapeFileWriter.CreateWriter(System.IO.Path.GetDirectoryName(targetShapeFileName),
                            System.IO.Path.GetFileNameWithoutExtension(targetShapeFileName),
                            sourceShapeFile.ShapeType,
                            sourceShapeFile.RenderSettings.DbfReader.DbfRecordHeader.GetFieldDescriptions()))
                    {
						byte[] zDataBuffer = new byte[1024];
                        for (int n = 0; n < sourceShapeFile.RecordCount; ++n)
                        {
							int pointCount = 0;
                            var shapeData = sourceShapeFile.GetShapeDataD(n);
                            if (restrictToAreaOfUse)
                            {
                                shapeData = RestrictToAreaOfUse(shapeData, wgs84Transformation, targetCRS.AreaOfUse);
                            }
                            foreach (var part in shapeData)
                            {
								pointCount += part.Length;
								coordinateTransformation.Transform(part);
                            }
                            string[] attributes = sourceShapeFile.GetAttributeFieldValues(n);
							if (sourceShapeFile.ShapeType == ShapeType.PolyLineZ)
							{
								var measureData = sourceShapeFile.GetShapeMDataD(n);
								if (zDataBuffer.Length < (pointCount * 32 + 128)) zDataBuffer = new byte[pointCount * 32 + 128];
								var zData = sourceShapeFile.GetShapeZDataD(n, zDataBuffer);
								writer.AddRecord(shapeData, measureData,zData, attributes);
							}
							else if (sourceShapeFile.ShapeType == ShapeType.PolyLineM)
                            {
                                var measureData = sourceShapeFile.GetShapeMDataD(n);
                                writer.AddRecord(shapeData, measureData, attributes);
                            }
                            else
                            {
                                writer.AddRecord(shapeData, attributes);
                            }
                            //update the progress
                            int percent = (int)Math.Round(100 * (double)(n + 1) / (double)sourceShapeFile.RecordCount);
                            if (percent != currentPercent)
                            {
                                currentPercent = percent;
                                transformationProgress.Value = percent;
                                transformationProgress.Invalidate();
                            }
                        }
                    }
                }
                finally
                {
                    if (wgs84Transformation != null) wgs84Transformation.Dispose();
                }

                //write the .prj file
                using (System.IO.StreamWriter writer = new System.IO.StreamWriter(System.IO.Path.ChangeExtension(targetShapeFileName, ".prj")))
                {
                    writer.WriteLine(targetCRS.WKT);
                }
            }
        }

        private System.Collections.ObjectModel.ReadOnlyCollection<PointD[]> RestrictToAreaOfUse(System.Collections.ObjectModel.ReadOnlyCollection<PointD[]> pointData, ICoordinateTransformation wgs84Transformation, CRSBoundingBox areaOfUse)
        {
            if (!areaOfUse.IsDefined) return pointData;

            List<PointD[]> result = new List<PointD[]>();
            foreach (PointD[] part in pointData)
            {
                PointD[] points = (PointD[])part.Clone();
                wgs84Transformation.Transform(points);
                bool cropped = false;
                for (int n = 0; n < points.Length; ++n)
                {
                    if (points[n].X < areaOfUse.WestLongitudeDegrees)
                    {
                        cropped = true;
                        points[n].X = areaOfUse.WestLongitudeDegrees;
                    }
                    else if (points[n].X > areaOfUse.EastLongitudeDegrees)
                    {
                        cropped = true;
                        points[n].X = areaOfUse.EastLongitudeDegrees;
                    }
                    if (points[n].Y > areaOfUse.NorthLatitudeDegrees)
                    {
                        cropped = true;
                        points[n].Y = areaOfUse.NorthLatitudeDegrees;
                    }
                    else if (points[n].Y < areaOfUse.SouthLatitudeDegrees)
                    {
                        cropped = true;
                        points[n].Y = areaOfUse.SouthLatitudeDegrees;
                    }
                }
                if (cropped)
                {
                    wgs84Transformation.Transform(points, TransformDirection.Inverse);
                    result.Add(points);
                }
                else
                {
                    //just use the original points to avoid transforming back to source CRS (cpu use + minor error transforming fwd and back)
                    result.Add(part);
                }
            }
            return new System.Collections.ObjectModel.ReadOnlyCollection<PointD[]>(result);
        }
    }
}
