using Liath.BigSpace.Domain;
using Liath.BigSpace.Domain.SolarSystems;
using Liath.BigSpace.Domain.UserAccountDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Liath.BigSpace.Domain.DataAccessDefinitions
{
    public interface IShips
    {
        IEnumerable<Ship> ListShipsAtSolarSystem(Int64 solarSystemID);

        Ship GetShip(int shipID);

        void Save(Ship ship);

        IEnumerable<Ship> ListShipsDoingJob(long jobID);

        void SetShipLocation(int shipID, Int64? solarSystemID);

        void SetShipJob(int shipID, Int64? jobID);

        IEnumerable<Ship> ListSelectedShips(int userAccountID);

        Ship CreateShip(SolarSystem located, UserAccount owner, string name);

        IEnumerable<ShipWithCurrentStatus> ListAllShipsInEmpire(int empireID);
    }
}
