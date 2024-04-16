using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
	/// represents a tile image map service
	/// </summary>
	public class TileSource
	{
		/// <summary>
		/// Name of the Tile Source
		/// </summary>
		public string Name
		{
			get;
			set;
		}

		public string[] Urls
		{
			get;
			set;
		}

		public int MaxZoomLevel
		{
			get;
			set;
		}

		public override string ToString()
		{
			return this.Name;
		}


		public static TileSource[] DefaultTileSources()
		{
			
			var tileSourceList = new List<TileSource>();
			tileSourceList.Add(new TileSource()
			{

				Name = "Open Street Map",
				Urls = new string[]
				{
				"https://a.tile.openstreetmap.org/{0}/{1}/{2}.png",
				"https://b.tile.openstreetmap.org/{0}/{1}/{2}.png",
				"https://c.tile.openstreetmap.org/{0}/{1}/{2}.png"
				},
				MaxZoomLevel = 19
			});

			tileSourceList.Add(new TileSource()
			{

				Name = "Australian National Base Map",
				Urls = new string[]
				{
				"http://services.ga.gov.au/gis/rest/services/NationalBaseMap/MapServer/WMTS/tile/1.0.0/NationalBaseMap/default/GoogleMapsCompatible/{0}/{2}/{1}.png"
				},
				MaxZoomLevel = 16
			});

			tileSourceList.Add(new TileSource()
			{
				Name = "NASA Satellite imagery via ESRI",
				Urls = new string[]
				{
				"https://server.arcgisonline.com/ArcGIS/rest/services/World_Imagery/MapServer/tile/{0}/{2}/{1}.jpg"

				},
				MaxZoomLevel = 19
			});

			tileSourceList.Add(new TileSource()
			{
				Name = "World Topo map via ESRI",
				Urls = new string[]
				{
				"https://services.arcgisonline.com/ArcGIS/rest/services/World_Topo_Map/MapServer/tile/{0}/{2}/{1}.jpg"
				},
				MaxZoomLevel = 19
			});

			// https://services.arcgisonline.com/ArcGIS/rest/services/World_Topo_Map/MapServer/tile/0/0/0.jpg




			// get a Free API KEY from https://www.maptiler.com/ and uncomment the following lines

			string key = null;


			if (!string.IsNullOrEmpty(key))
			{
				tileSourceList.Add(new TileSource()
				{
					Name = "MapTiler Satellite",
					Urls = new string[]
					{
				"https://api.maptiler.com/tiles/satellite/{0}/{1}/{2}.jpg?key=" + key//YOUR_API_KEY"
					}
				});
				tileSourceList.Add(new TileSource()
				{

					Name = "MapTiler Voyager",
					Urls = new string[]
					{
				"https://api.maptiler.com/maps/voyager/256/{0}/{1}/{2}.png?key=" + key//YOUR_API_KEY"
					}
				});
			}

			return tileSourceList.ToArray();
		}
	}


}
