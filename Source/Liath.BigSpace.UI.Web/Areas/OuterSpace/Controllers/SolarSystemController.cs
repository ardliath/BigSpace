using Liath.BigSpace.DataAccess.Definitions;
using Liath.BigSpace.Definitions;
using Liath.BigSpace.Session;
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
        private ISessionManager _sessionManager;
        private ISolarSystems _solarSystemDataAccess;

        public SolarSystemController(ISessionManager sessionManager, ISolarSystems solarSystemDataAccess)
        {
            if (sessionManager == null) throw new ArgumentNullException("sessionManager");
            if (solarSystemDataAccess == null) throw new ArgumentNullException("solarSystemDataAccess");

            _sessionManager = sessionManager;
            _solarSystemDataAccess = solarSystemDataAccess;
        }

        [HttpGet]
        public ActionResult Summary(int id)
        {
            using (_sessionManager.CreateUnitOfWork())
            {
                var solarSystem = _solarSystemDataAccess.GetSolarSystem(id);

                return View(new SolarSystemSummary
                {
                    Name = solarSystem.Name
                });
            }
        }
    }
}