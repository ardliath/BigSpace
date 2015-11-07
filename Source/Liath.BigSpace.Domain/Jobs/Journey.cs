﻿using Liath.BigSpace.Domain;
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
            throw new NotImplementedException();
        }
    }
}
