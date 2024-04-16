using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
 * Copyright: Winston Fletcher 2010 - 2021
 *
 */

namespace Example6
{

    /// <summary>
    /// Base Map Layer for displaying tiled image layers like OSM, ESRI satellite etc
    /// </summary>
	public class BaseMapLayer : IDisposable
	{

		private EGIS.Controls.SFMap mapReference;

		private TileCollection tileCollection = null;


		public BaseMapLayer(EGIS.Controls.SFMap map, TileSource tileSource)
		{
			this.mapReference = map;
			map.MapCoordinateReferenceSystem = EGIS.Projections.CoordinateReferenceSystemFactory.Default.GetCRSById(EGIS.Projections.CoordinateReferenceSystemFactory.Wgs84PseudoMercatorEpsgCode);
			map.PaintMapBackground += Map_PaintMapBackground;
			map.ZoomLevelChanged += Map_ZoomLevelChanged;
			this.TileSource = tileSource;
			this.Transparency = 1;
			map.Invalidate();
		}

		private void Map_ZoomLevelChanged(object sender, EventArgs e)
		{		
			//check zoom level is ok
			double currentZoom = mapReference.ZoomLevel;
			int zoomLevel = TileCollection.WebMercatorScaleToZoomLevel(currentZoom);
			if (zoomLevel < 0) zoomLevel = 0;
			double requiredZoom = TileCollection.ZoomLevelToWebMercatorScale(zoomLevel);
			if (Math.Abs(currentZoom - requiredZoom) > double.Epsilon)
			{
				mapReference.ZoomLevel = requiredZoom;
			}

		}

		private TileSource _tileSource;
		private bool disposedValue;

		public TileSource TileSource
		{
			get { return _tileSource; }
			set
			{
				_tileSource = value;
				mapReference.Invalidate();
			}

		}

		public float Transparency
		{
			get;
			set;
		}

		private void Map_PaintMapBackground(object sender, System.Windows.Forms.PaintEventArgs e)
		{
            try
            {
                DrawMap(e.Graphics, Math.Max(0, Math.Min(Transparency, 1.0f)), this.TileSource, this.mapReference);
            }
            catch
            {
            }
		}


		private void DrawMap(Graphics g, float transparency, TileSource tileSource, EGIS.Controls.SFMap map )
		{
			if (tileSource == null) return;
			TileCollection tiles = new TileCollection(map.ZoomLevel, map.CentrePoint2D, map.ClientSize.Width, map.ClientSize.Height, map, tileSource.Urls, tileSource.MaxZoomLevel);
			if (this.tileCollection != null)
			{
				//abort if our zoom level has changed
				if (this.tileCollection.ZoomLevel != tiles.ZoomLevel)
				{
					this.tileCollection.Abort();
				}
			}
			this.tileCollection = tiles;
			try
			{
				tiles.Render(g, transparency);
			}
			catch (Exception ex)
			{
#if DEBUG
				System.Diagnostics.Debug.WriteLine(ex);
				throw;
#endif
			}

		}

		protected virtual void Dispose(bool disposing)
		{
			if (!disposedValue)
			{
				if (disposing)
				{
					// TODO: dispose managed state (managed objects)
				}
				this.mapReference.PaintMapBackground -= Map_PaintMapBackground;
				this.mapReference.ZoomLevelChanged -= Map_ZoomLevelChanged;
				this.mapReference = null;
				this.tileCollection = null;
				// TODO: free unmanaged resources (unmanaged objects) and override finalizer
				// TODO: set large fields to null
				disposedValue = true;
			}
		}

		// // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
		// ~BaseMapLayer()
		// {
		//     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
		//     Dispose(disposing: false);
		// }

		public void Dispose()
		{
			// Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
			Dispose(disposing: true);
			GC.SuppressFinalize(this);
		}
	}



	
}
