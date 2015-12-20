using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Liath.BigSpace.UI.Web.Areas.OuterSpace.Models.Ship
{
	public class UpdateOrder
	{
		public int OrderID { get; set; }
		public int ShipID { get; set; }
		public bool Applied { get; set; }
	}
}