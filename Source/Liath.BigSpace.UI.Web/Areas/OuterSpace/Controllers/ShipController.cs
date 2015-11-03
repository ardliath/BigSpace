using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Liath.BigSpace.UI.Web.Areas.OuterSpace.Controllers
{
    public class ShipController : Controller
    {
        [HttpPost]
        public ActionResult SelectShip(int id)
        {
            return new HttpStatusCodeResult(System.Net.HttpStatusCode.OK);
        }

        [HttpPost]
        public ActionResult DeSelectShip(int id)
        {
            return new HttpStatusCodeResult(System.Net.HttpStatusCode.OK);
        }
    }
}