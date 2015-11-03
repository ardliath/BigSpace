using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Liath.BigSpace.UI.Web.Areas.OuterSpace.Models.SolarSystem
{
    public class ShipSummary
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public bool IsSelected { get; set; }

        public string CellClass
        {
            get
            {
                return this.IsSelected ? "SelectedShip" : "NotSelectedShip";
            }
        }
    }
}