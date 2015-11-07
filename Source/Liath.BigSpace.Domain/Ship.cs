using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Liath.BigSpace.Domain
{
    public class Ship
    {
        public int ShipID { get; set; }
        public string Name { get; set; }
        public bool IsSelected { get; set; }
        public Int64? SolarSystemID { get; set; }
        public Int64? JobID { get; set; }

        public override string ToString()
        {
            return this.Name;
        }
    }
}
