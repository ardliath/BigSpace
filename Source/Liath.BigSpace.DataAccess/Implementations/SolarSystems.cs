using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Liath.BigSpace.DataAccess.Definitions;
using Liath.BigSpace.Domain;
using Liath.BigSpace.Session;

namespace Liath.BigSpace.DataAccess.Implementations
{
	public class SolarSystems : DataAccessBase, ISolarSystems
	{
		public SolarSystems(ISessionManager sessionManager) : base(sessionManager)
		{
		}

		public LocalAreaViewResult FindSystemsInLocalArea(LocalAreaView localAreaView)
		{
			if (localAreaView == null) throw new ArgumentNullException("localAreaView");

			throw new NotImplementedException();
		}
	}
}
