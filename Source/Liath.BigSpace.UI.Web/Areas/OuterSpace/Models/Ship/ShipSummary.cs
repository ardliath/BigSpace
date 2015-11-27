using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Liath.BigSpace.UI.Web.Areas.OuterSpace.Models.Ship
{
    public class ShipSummary
    {
        public Int64 ShipID { get; set; }
        public string ShipName { get; set; }

        public Int64? SolarSystem { get; set; }
        public string SolarSystemName { get; set; }

        public string Job { get; set; }
    }
}
