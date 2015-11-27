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

        public string SolarSystem { get; set; }

        public string Job { get; set; }
    }
}
