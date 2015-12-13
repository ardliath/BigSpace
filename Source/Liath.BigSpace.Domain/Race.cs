using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Liath.BigSpace.Domain
{
	public class Race
	{
		public int RaceID { get; set; }
		public string Name { get; set; }

		public Guid Guid { get; set; }
	}
}
