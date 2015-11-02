using Liath.BigSpace.UI.Web.Areas.OuterSpace.Models.SolarSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Liath.BigSpace.UI.Web.Areas.OuterSpace.Controllers
{
    public class SolarSystemController : Controller
    {
        [HttpGet]
        public ActionResult Summary(int id)
        {
            return View(new SolarSystemSummary
            {
                Name = id == 1 ? "Sol" : "Proxima Centuri"
            });
        }
    }
}