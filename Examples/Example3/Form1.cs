using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
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
 * Copyright: Easy GIS .NET 2010
 *
 */

namespace Example3
{
    /// <summary>
    /// Main class of Example 3
    /// <remarks>
    /// Example 3 demonstrates opening a shapefile and creating a new shapefile, by filtering the record 
    /// data on a DBF field selected by the user.
    /// <para>To run the demo, open the included example shapefile by clicking on the first Browse button. After the
    /// shapefile opens select "LGA_NAME" from the drop-down combo box. Select 1 or 2 of records to include and then click Generate Filetered Shapefile.
    /// A new shapefile will be created, which will only contain records from the selected records.</para>
    /// <para>The example shows how easy it is to create new shapefiles from an existing shapefile. The same technique can be used to 
    /// extract "highways" or "main roads" from a large shapefile that contains all road types in an area</para>
    /// 
    /// </remarks>
    /// </summary>
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

            this.txtFilterOutputShapefile.Text = Application.StartupPath + "\\example3.shp";
        }

        #region event handlers

        private void btnBrowseInputFile_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.ofdInputFiles.ShowDialog(this) == DialogResult.OK)
                {
                    LoadInputFilterShapefile(ofdInputFiles.FileName);
                }
            }            
            catch(System.IO.IOException ioe)
            {
                MessageBox.Show(ioe.Message);
            }
            finally
            {
                pnlFilter.Enabled = !string.IsNullOrEmpty(txtFilterInputFile.Text);
            }
        }

        private void LoadInputFilterShapefile(string path)
        {
            this.txtFilterInputFile.Text = path;
            this.cboxFilterField.Items.Clear();
            this.clbFilterRecords.Items.Clear();
            DbfReader r = null;
            try
            {
                r = new DbfReader(System.IO.Path.ChangeExtension(path, ".dbf"));
                this.cboxFilterField.Items.AddRange(r.GetFieldNames());
            }
            finally
            {
                if(r!=null) r.Close();
            }
            this.cboxFilterField.SelectedIndex = 0;
        }

        private void btnBrowseFilterOutputShapefile_Click(object sender, EventArgs e)
        {
            if (sfdOutputFile.ShowDialog(this) == DialogResult.OK)
            {
                this.txtFilterOutputShapefile.Text = sfdOutputFile.FileName;
            }
        }

        private void clbFilterRecords_Click(object sender, EventArgs e)
        {
            this.btnGenerateFilteredShapefile.Enabled = clbFilterRecords.CheckedIndices.Count > 0;
        }

        private void clbFilterRecords_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            this.btnGenerateFilteredShapefile.Enabled = (clbFilterRecords.CheckedIndices.Count > 1) || (e.NewValue == CheckState.Checked);
        }

        private void btnGenerateFilteredShapefile_Click(object sender, EventArgs e)
        {
            GenerateFilteredShapefile();
        }

        private void cboxFilterField_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboxFilterField.SelectedIndex >= 0)
            {
                this.Cursor = Cursors.WaitCursor;
                this.clbFilterRecords.Items.Clear();
                clbFilterRecords.Enabled = false;
                this.clbFilterRecords.SuspendLayout();
                clbFilterRecords.BeginUpdate();

                DbfReader r = null;
                try
                {
                    r = new DbfReader(System.IO.Path.ChangeExtension(txtFilterInputFile.Text, ".dbf"));
                    string[] records = r.GetDistinctRecords(cboxFilterField.SelectedIndex);
                    if (records.Length > 1000 && this.chkFilterRecordLimit.Checked)
                    {
                        Array.Resize(ref records, 1000);
                    }
                    this.clbFilterRecords.Items.AddRange(records);
                }
                finally
                {
                    clbFilterRecords.EndUpdate();
                    clbFilterRecords.Enabled = true;
                    this.clbFilterRecords.ResumeLayout();
                    this.Cursor = Cursors.Default;
                    r.Close();
                    this.btnGenerateFilteredShapefile.Enabled = clbFilterRecords.CheckedIndices.Count > 0;
                }
            }
        }


        #endregion


        /// <summary>
        /// This is the main method of the example.
        /// The method opens the input shapefile and then uses a 
        /// ShapeFileWriter to create a new shapefile, which will only
        /// contain records selected by the user
        /// </summary>
        private void GenerateFilteredShapefile()
        {
            if (string.IsNullOrEmpty(this.txtFilterOutputShapefile.Text) || this.clbFilterRecords.CheckedIndices.Count == 0) return;
            // find the index of the field the user has selected
            int fieldIndex = this.cboxFilterField.SelectedIndex;
            if (fieldIndex == -1)
            {
                MessageBox.Show(this, "No field selected", "Error");
                return;
            }
            
            //Open the input shapefile;
            ShapeFile sf = new ShapeFile(txtFilterInputFile.Text);
            DbfReader dbfReader = new DbfReader(System.IO.Path.ChangeExtension(txtFilterInputFile.Text, ".dbf"));
                       
            // add the selected records from the CheckListBox to a StringCollection
            System.Collections.Specialized.StringCollection includedRecords = new System.Collections.Specialized.StringCollection();
            for (int n = 0; n < clbFilterRecords.CheckedIndices.Count; n++)
            {
                includedRecords.Add(clbFilterRecords.Items[clbFilterRecords.CheckedIndices[n]] as string);
            }

            
            string rootDir = System.IO.Path.GetDirectoryName(this.txtFilterOutputShapefile.Text);
            string shapefileName = System.IO.Path.GetFileNameWithoutExtension(txtFilterOutputShapefile.Text);

            //create a new ShapeFileWriter
            ShapeFileWriter sfw;
            ShapeType shapeType = sf.ShapeType;
            if (sf.ShapeType == ShapeType.PolygonZ) shapeType = ShapeType.Polygon;//currently dont support polgonz writer 
            DbfFieldDesc[] fieldDescs = dbfReader.DbfRecordHeader.GetFieldDescriptions();
            for(int f=0;f<fieldDescs.Length;++f)
            {
                fieldDescs[f].FieldType = DbfFieldType.Character;
            }
            if (chkAppend.Checked)
            {
                sfw = ShapeFileWriter.OpenWriter(rootDir, shapefileName);
            }
            else
            {
                sfw = ShapeFileWriter.CreateWriter(rootDir, shapefileName, shapeType, fieldDescs, sf.CoordinateReferenceSystem.WKT);
            }
            try
            {
                this.Cursor = Cursors.WaitCursor;                
                this.toolStripProgressBar1.Maximum = sf.RecordCount;
                this.toolStripProgressBar1.Value = 0;

                // Get a ShapeFileEnumerator from the shapefile and read each record
                ShapeFileEnumerator sfEnum = sf.GetShapeFileEnumerator();//r,ShapeFileEnumerator.IntersectionType.Contains);
                
                for(int r=0;r<sf.RecordCount;++r)//while (sfEnum.MoveNext())
                {
                    // get the raw point data
                    //PointD[] points = sfEnum.Current[0];5
                    //get the DBF record
                    string[] fields = dbfReader.GetFields(r);//sfEnum.CurrentShapeIndex);

                    //check whether to add the record to the new shapefile
                    bool include = false;
                    for (int n = 0; !include && n < includedRecords.Count; n++)
                    {
                        include = (string.Compare(fields[fieldIndex].Trim(), includedRecords[n], true) == 0);
                    }
                    if (include)
                    {
                        //sfw.AddRecord(points, points.Length, fields);
                        if (shapeType == ShapeType.Point)
                        {
                            //sfw.AddRecord(sfEnum.Current[0], 1, fields);
                            sfw.AddRecord(sf.GetShapeDataD(r)[0], 1, fields);
                        }
                        else
                        {
                            //sfw.AddRecord(sfEnum.Current, fields);
                            sfw.AddRecord(sf.GetShapeDataD(r),fields);
                        }
                    }
                    toolStripProgressBar1.Increment(1);
                }

                
                MessageBox.Show("New ShapeFile successfully generated");
                
            }
            finally
            {
                //close the shapefile, shapefilewriter and dbfreader
                this.Cursor = Cursors.Default;
                // This is very important. You must call the ShapeFileWriter Close method so that the 
                // headers in the shapefile will be updated correctly
                sfw.Close(); 
                sf.Close();
                dbfReader.Close();
                //write the .prj file
                //no longer required as shapefile writer now performs this
                //using (System.IO.StreamWriter writer = new System.IO.StreamWriter(System.IO.Path.ChangeExtension(this.txtFilterOutputShapefile.Text, ".prj")))
                //{
                //    writer.WriteLine(sf.CoordinateReferenceSystem.WKT);
                //}

            }
        }

        

    }
}