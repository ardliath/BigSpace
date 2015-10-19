using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Liath.BigSpace.Domain
{
	public class LocalAreaViewResult : LocalAreaView
	{
		public IEnumerable<RelativeSolarSystem> SolarSystems { get; set; }

        public LocalAreaViewResult(Coordinates focusCoordinates, ScreenSize screenSize, IEnumerable<RelativeSolarSystem> solarSystems)
            : base(focusCoordinates, screenSize)
		{
			if (solarSystems == null) throw new ArgumentNullException("solarSystems");
			SolarSystems = solarSystems;
		}
	}
}
