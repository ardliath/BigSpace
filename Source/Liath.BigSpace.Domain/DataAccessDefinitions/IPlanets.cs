using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Liath.BigSpace.Domain.SolarSystems;

namespace Liath.BigSpace.Domain.DataAccessDefinitions
{
	public interface IPlanets
	{
		IEnumerable<Planet> ListPlanetsInSolarSystem(long id);
	}
}
