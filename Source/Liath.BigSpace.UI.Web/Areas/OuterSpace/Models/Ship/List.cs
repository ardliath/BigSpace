using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Liath.BigSpace.UI.Web.Areas.OuterSpace.Models.Ship
{
    public class List
    {

        public string EmpireName { get; set; }
        public IEnumerable<ShipSummary> Ships { get; set; }
    }
}