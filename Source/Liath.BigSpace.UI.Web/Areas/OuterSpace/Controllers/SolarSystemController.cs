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
        private INavigationManager _navigationManager;

        public SolarSystemController(ISessionManager sessionManager, INavigationManager navigationManager)
        {
            if (sessionManager == null) throw new ArgumentNullException("sessionManager");
            if (navigationManager == null) throw new ArgumentNullException("navigationManager");

            _sessionManager = sessionManager;
            _navigationManager = navigationManager;
        }

        [HttpGet]
        public ActionResult Summary(int id)
        {
            using (_sessionManager.CreateUnitOfWork())
            {
                var solarSystem = _navigationManager.GetSolarSystemDetails(id);

                return View(new SolarSystemSummary
                {
                    Name = solarSystem.Name,
                    Ships = solarSystem.Ships.Select(s => new ShipSummary
                    {
                        ID = s.ShipID,
                        Name = s.Name,
                        IsSelected = s.IsSelected
                    })
                });
            }
        }
    }
}