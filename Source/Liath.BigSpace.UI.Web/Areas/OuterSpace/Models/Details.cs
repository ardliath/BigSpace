namespace Liath.BigSpace.UI.Web.Areas.OuterSpace.Models.Ship
{
	public class Details
	{
		public int ShipID { get; set; }

		public string Name { get; set; }

		public long? CurrentLocationID { get; set; }
		public string LocationName { get; set; }

		public string CurrentTask { get; set; }
	}
}