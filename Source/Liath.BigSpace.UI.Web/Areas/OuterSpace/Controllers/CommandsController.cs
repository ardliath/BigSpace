using Liath.BigSpace.Definitions;
using Liath.BigSpace.Session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Liath.BigSpace.UI.Web.Areas.OuterSpace.Controllers
{
    public class CommandsController : Controller
    {
        private INavigationManager _navigationManager;
        private ISessionManager _sessionManager;
        public CommandsController(ISessionManager sessionManager, INavigationManager navigationManager)
        {
            if (sessionManager == null) throw new ArgumentNullException("sessionManager");
            if (navigationManager == null) throw new ArgumentNullException("navigationManager");

            _sessionManager = sessionManager;
            _navigationManager = navigationManager;
        }

        [HttpPost]
        public ActionResult SendShips(int id)
        {
            using (_sessionManager.CreateUnitOfWork())
            {
                _navigationManager.CreateJourneyToSendSelectedShipsToSolarSystem(id);
                return new HttpStatusCodeResult(HttpStatusCode.OK);
            }
        }
    }
}