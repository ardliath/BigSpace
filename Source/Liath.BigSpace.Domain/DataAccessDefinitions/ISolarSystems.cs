using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Liath.BigSpace.Domain.SolarSystems;

namespace Liath.BigSpace.Domain.DataAccessDefinitions
{
	public interface ISolarSystems
	{
		IEnumerable<SolarSystem> FindSystemsInLocalArea(LocalAreaView localAreaView);
        SolarSystem GetSolarSystem(Int64 id);
        SolarSystem CreateSolarSystem(SolarSystem solarSystem);

        SolarSystem GetRandomUnoccupiedSolarSystem();
		SolarSystemPlanetDetails GetSolarSystemDetails(long id);
	}
}
