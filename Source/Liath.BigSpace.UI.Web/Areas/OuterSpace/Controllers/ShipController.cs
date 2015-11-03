using Liath.BigSpace.Definitions;
using Liath.BigSpace.Session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Liath.BigSpace.UI.Web.Areas.OuterSpace.Controllers
{
    public class ShipController : Controller
    {
        private ISessionManager _sessionManager;
        private IFleetManager _fleetManager;

        public ShipController(ISessionManager sessionManager, IFleetManager fleetManager)
        {
            if (sessionManager == null) throw new ArgumentNullException("sessionManager");
            if (fleetManager == null) throw new ArgumentNullException("fleetManager");

            _sessionManager = sessionManager;
            _fleetManager = fleetManager;
        }

        [HttpPost]
        public ActionResult SelectShip(int id)
        {
            using (_sessionManager.CreateUnitOfWork())
            {
                _fleetManager.SelectShip(id);
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.OK);
            }
        }

        [HttpPost]
        public ActionResult DeSelectShip(int id)
        {
            using (_sessionManager.CreateUnitOfWork())
            {
                _fleetManager.DeSelectShip(id);
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.OK);
            }
        }
    }
}