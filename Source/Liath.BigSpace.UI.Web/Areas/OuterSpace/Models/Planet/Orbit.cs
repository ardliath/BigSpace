using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Liath.BigSpace.UI.Web.Areas.OuterSpace.Models.Planet
{
	public class Orbit
	{
		public string Image { get; set; }
		public long? Next { get; set; }
		public long? Previous { get; set; }
		public string Name { get; set; }
		public long Population { get; set; }
		public long SolarSystemID { get; set; }
	}
}