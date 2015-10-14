using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Liath.BigSpace.UI.Web.Areas.OuterSpace.Controllers
{
    public class LocalAreaController : Controller
    {
        [HttpGet]
        public ActionResult Display()
        {
            return View();
        }
    }
}