using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EGISCodeSnippets
{
	public class ExampleCustomRenderSettings : EGIS.ShapeFileLib.BaseCustomRenderSettings
	{
		public ExampleCustomRenderSettings(EGIS.ShapeFileLib.RenderSettings settings)
			:base(settings)
		{
		}

		public override bool RenderShape(int recordNumber)
		{
			//render every second record
			return recordNumber % 2 == 0;

			//add logic here to control whether a record shouold be rendered
			//examples:
			//- reference to a map zoom level?
			//- based on an attribute
			//	renderSettings.DbfReader.GetFields(recordNumber)[fieldIndex] == "blah"
		}
	}
}
