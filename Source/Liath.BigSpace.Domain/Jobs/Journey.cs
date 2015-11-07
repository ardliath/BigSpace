using Liath.BigSpace.Domain;
using Liath.BigSpace.Domain.DataAccessDefinitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Liath.BigSpace.Domain.Jobs
{
    public class Journey : Job
    {
        private IShips _ships;

        public SolarSystem From { get; set; }
        public SolarSystem To { get; set; }

        public Journey(IShips ships)
        {
            if (ships == null) throw new ArgumentNullException("ships");
            _ships = ships;
        }

        public override void Complete()
        {
            var shipsOnJourney = _ships.ListShipsDoingJob(this.JobID);
            foreach(var ship in shipsOnJourney)
            {
                _ships.SetShipLocation(ship.ShipID, this.To.SolarSystemID);
                _ships.SetShipJob(ship.ShipID, null);
            }
        }
    }
}
