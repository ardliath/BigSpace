using Liath.BigSpace.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Liath.BigSpace.Definitions
{
    public interface IFleetManager
    {
        void SelectShip(int id);
        void DeSelectShip(int id);
        IEnumerable<ShipWithCurrentStatus> ListAllShipsInMyEmpire();
	    ShipWithCurrentStatus GetShipFromMyFleet(int shipID);
    }
}
