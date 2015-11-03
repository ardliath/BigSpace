using Liath.BigSpace.DataAccess.Definitions;
using Liath.BigSpace.Definitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Liath.BigSpace.Implementations
{
    public class FleetManager : IFleetManager
    {
        private IShips _ships;

        public FleetManager(IShips ships)
        {
            if (ships == null) throw new ArgumentNullException("ships");
            _ships = ships;
        }

        public void SelectShip(int id)
        {
            throw new NotImplementedException();
        }

        public void DeSelectShip(int id)
        {
            throw new NotImplementedException();
        }
    }
}
