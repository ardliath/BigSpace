using Liath.BigSpace.Definitions;
using Liath.BigSpace.Session;
using Liath.BigSpace.UI.Web.Areas.Setup.Models.Galaxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Liath.BigSpace.UI.Web.Areas.Setup.Controllers
{
    public class GalaxyController : Controller
    {
        private ISessionManager _sessionManager;
        private IGalaxyManager _galaxyManager;
        public GalaxyController(ISessionManager sessionManager, IGalaxyManager galaxyManager)
        {
            if (sessionManager == null) throw new ArgumentNullException("sessionManager");
            if (galaxyManager == null) throw new ArgumentNullException("galaxyManager");

            _sessionManager = sessionManager;
            _galaxyManager = galaxyManager;
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Create create)
        {
            using (_sessionManager.CreateUnitOfWork())
            {
                _galaxyManager.CreateGalaxy();
                return View();
            }
        }
    }
}