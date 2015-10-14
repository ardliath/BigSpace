using Liath.BigSpace.Definitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Liath.BigSpace.UI.Web.Areas.OuterSpace.Controllers
{
    public class LocalAreaController : Controller
    {
        private INavigationManager _navigationManager;

        public LocalAreaController(INavigationManager navigationManager)
        {
            if (navigationManager == null) throw new ArgumentNullException("navigationManager");
            _navigationManager = navigationManager;
        }

        [HttpGet]
        public ActionResult Display()
        {
            return View();
        }
    }
}