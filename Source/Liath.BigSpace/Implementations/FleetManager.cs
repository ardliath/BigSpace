﻿using Liath.BigSpace.DataAccess.Definitions;
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
        private ISecurityManager _securityManager;

        public FleetManager(IShips ships, ISecurityManager securityManager)
        {
            if (ships == null) throw new ArgumentNullException("ships");
            if (securityManager == null) throw new ArgumentNullException("securityManager");

            _ships = ships;
            _securityManager = securityManager;
        }

        public void SelectShip(int id)
        {
            this.LoadShipAndSetSelectedTo(true, id);
        }

        private void LoadShipAndSetSelectedTo(bool newSelectedValue, int shipID)
        {
            var ship = _ships.GetShip(shipID);
            var me = _securityManager.GetCurrentUserAccount();
            if(ship != null && me != null && me.UserAccountID == ship.UserAccountID)
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
