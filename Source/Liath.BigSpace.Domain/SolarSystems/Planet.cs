using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Liath.BigSpace.Domain.SolarSystems
{
	public class Planet
	{
		public string Image { get; set; }
		public long MaxPopulation { get; set; }
		public string Name { get; set; }
		public long PlanetID { get; set; }
		public long Population { get; set; }
		public short PositionIndex { get; set; }
		public long SolarSystemID { get; set; }

		public int? RaceID { get; set; }

		public override string ToString()
		{
			return this.Name;
		}
	}
}
