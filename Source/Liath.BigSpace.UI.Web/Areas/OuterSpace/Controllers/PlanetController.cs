using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Liath.BigSpace.UI.Web.Areas.OuterSpace.Models.Planet;

namespace Liath.BigSpace.UI.Web.Areas.OuterSpace.Controllers
{
	public class PlanetController : Controller
	{
		[HttpGet]
		public ActionResult Orbit(int id)
		{
			var model = new Orbit();
			return View();
		}
	}
}