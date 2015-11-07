using Liath.BigSpace.DataAccess.Definitions;
using Liath.BigSpace.Definitions;
using Liath.BigSpace.Domain.DataAccessDefinitions;
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
            this.LoadShipAndSetSelectedTo(true, id);
        }

        private void LoadShipAndSetSelectedTo(bool newSelectedValue, int shipID)
        {
            var ship = _ships.GetShip(shipID);
            if(ship != null)
            {
                ship.IsSelected = newSelectedValue;
                _ships.Save(ship);
            }
        }

        public void DeSelectShip(int id)
        {
            this.LoadShipAndSetSelectedTo(false, id);
        }
    }
}
