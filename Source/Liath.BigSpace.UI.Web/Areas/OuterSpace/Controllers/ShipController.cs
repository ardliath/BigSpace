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
        private IEmpireManager _empireManager;

        public ShipController(ISessionManager sessionManager, IFleetManager fleetManager, IEmpireManager empireManager)
        {
            if (sessionManager == null) throw new ArgumentNullException("sessionManager");
            if (fleetManager == null) throw new ArgumentNullException("fleetManager");
            if (empireManager == null) throw new ArgumentNullException("empireManager");            

            _sessionManager = sessionManager;
            _fleetManager = fleetManager;
            _empireManager = empireManager;
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

        [HttpGet]
        public ActionResult List()
        {
            using(_sessionManager.CreateUnitOfWork())
            {
                var myEmpire = _empireManager.GetMyEmpire();
                var model = new Models.Ship.List
                {
                    EmpireName = myEmpire.Name
                };
                return View(model);
            }
        }
    }
}