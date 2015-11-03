﻿using Liath.BigSpace.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Liath.BigSpace.DataAccess.Definitions
{
    public interface IShips
    {
        IEnumerable<Ship> ListShipsAtSolarSystem(Int64 solarSystemID);

        Ship GetShip(int shipID);

        void Save(Ship ship);
    }
}
