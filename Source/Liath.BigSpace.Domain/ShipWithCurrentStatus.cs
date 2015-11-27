using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Liath.BigSpace.Domain
{
    public class ShipWithCurrentStatus : Ship
    {
        public string Username { get; set; }

        public Int64? JobID { get; set; }
        public string JobDescription { get; set; }

        public Int64? SolarSystemID { get; set; }
        public string SolarSystemName { get; set; }
    }
}
