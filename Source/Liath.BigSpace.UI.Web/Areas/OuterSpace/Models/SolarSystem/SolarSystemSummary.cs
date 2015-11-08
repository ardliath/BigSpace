using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Liath.BigSpace.UI.Web.Areas.OuterSpace.Models.SolarSystem
{
    public class SolarSystemSummary
    {
        public Int64 SolarSystemID { get; set; }
        public string Name { get; set; }
        public IEnumerable<ShipSummary> Ships { get; set; }
    }
}