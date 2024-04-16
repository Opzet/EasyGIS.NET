using System;
using System.Collections.Generic;
using System.Text;
using EGIS.ShapeFileLib;

/*
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
    /// ICustomRenderSettings class used to render a road shapefile record in different colors
    /// according to the road type of each record.
    /// </summary>
    class RoadTypeCustomRenderSettings : BaseCustomRenderSettings//ICustomRenderSettings
    {
        private List<System.Drawing.Color> colorList;
//        RenderSettings defaultSettings;
        public RoadTypeCustomRenderSettings(RenderSettings defaultSettings, string typeField, Dictionary<string,System.Drawing.Color> roadtypeColors)
            : base(defaultSettings)
        {
            
           // this.defaultSettings = defaultSettings;
            BuildColorList(defaultSettings, typeField, roadtypeColors);           
        }

        private void BuildColorList(RenderSettings defaultSettings, string typeField,  Dictionary<string, System.Drawing.Color> roadtypeColors)
        {
            int fieldIndex = defaultSettings.DbfReader.IndexOfFieldName(typeField);
            if(fieldIndex >=0)
            {
                colorList = new List<System.Drawing.Color>();
                int numRecords = defaultSettings.DbfReader.DbfRecordHeader.RecordCount;
                for (int n = 0; n < numRecords; ++n)
                {
                    string nextField = defaultSettings.DbfReader.GetField(n, fieldIndex).Trim();
                    if (roadtypeColors.ContainsKey(nextField))
                    {
                        colorList.Add(roadtypeColors[nextField]);
                    }
                    else
                    {
                        colorList.Add(defaultSettings.FillColor);
                    }
                }                
            }
        }

        #region BaseCustomRenderSettings overrides

        public override System.Drawing.Color GetRecordFillColor(int recordNumber)
        {
            if (colorList != null)
            {
                return colorList[recordNumber];
            }
            return renderSettings.FillColor;
        }

       
		#endregion
	}

    /// <summary>
    /// ICustomRenderSettings class used to display POI using different icons according to the 
    /// category of the POI
    /// </summary>
    class POICustomRenderSettings : ICustomRenderSettings
    {
        private List<System.Drawing.Image> imageList;
        RenderSettings defaultSettings;
        public POICustomRenderSettings(RenderSettings defaultSettings, string typeField, Dictionary<string, System.Drawing.Image> poiImages, System.Drawing.Image defaultImage)
        {
            this.defaultSettings = defaultSettings;
            BuildPOIList(defaultSettings, typeField, poiImages, defaultImage);
        }

        private void BuildPOIList(RenderSettings defaultSettings, string typeField, Dictionary<string, System.Drawing.Image> poiImages, System.Drawing.Image defaultImage)
        {
            int fieldIndex = defaultSettings.DbfReader.IndexOfFieldName(typeField);
            if (fieldIndex >= 0)
            {
                imageList = new List<System.Drawing.Image>();
                int numRecords = defaultSettings.DbfReader.DbfRecordHeader.RecordCount;
                for (int n = 0; n < numRecords; ++n)
                {
                    string nextField = defaultSettings.DbfReader.GetField(n, fieldIndex).Trim();
                    if (poiImages.ContainsKey(nextField))
                    {
                        imageList.Add(poiImages[nextField]);
                    }
                    else
                    {
                        imageList.Add(defaultImage);
                    }
                }
            }
        }

        #region ICustomRenderSettings Members

        public System.Drawing.Color GetRecordFillColor(int recordNumber)
        {
            return defaultSettings.FillColor;
        }

        public System.Drawing.Color GetRecordFontColor(int recordNumber)
        {
            return defaultSettings.FontColor;
        }

        public System.Drawing.Image GetRecordImageSymbol(int recordNumber)
        {
            return imageList[recordNumber];
        }

        public System.Drawing.Color GetRecordOutlineColor(int recordNumber)
        {
            return defaultSettings.OutlineColor;
        }
        
        public string GetRecordToolTip(int recordNumber)
        {
            return "";
        }

        public bool RenderShape(int recordNumber)
        {
            return true;
        }

		public string GetRecordLabel(int recordNumber)
		{
            return "";
		}

		public int GetDirection(int recordNumber)
		{
            return defaultSettings.DrawDirectionArrows ? 1 : 0;
        }

		public bool UseCustomImageSymbols
        {
            get { return true; }
        }

        public bool UseCustomTooltips
        {
            get { return false; }
        }

		public bool UseCustomRecordLabels
        {
            get { return false; }
        }

		#endregion
	}
}
