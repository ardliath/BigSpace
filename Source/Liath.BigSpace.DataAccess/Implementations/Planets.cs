using Liath.BigSpace.Domain.DataAccessDefinitions;
using Liath.BigSpace.Session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Liath.BigSpace.Domain.SolarSystems;

namespace Liath.BigSpace.DataAccess.Implementations
{
	public class Planets : DataAccessBase, IPlanets
	{
		public Planets(ISessionManager sessionManager) : base(sessionManager)
		{

		}

		public IEnumerable<Planet> ListPlanetsInSolarSystem(long id)
		{
			throw new NotImplementedException();
		}
	}
}
