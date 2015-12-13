using Liath.BigSpace.DataAccess.Definitions;
using Liath.BigSpace.Definitions;
using Liath.BigSpace.Session;
using Liath.BigSpace.UI.Web.Areas.OuterSpace.Models.SolarSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Liath.BigSpace.Domain.DataAccessDefinitions;

namespace Liath.BigSpace.UI.Web.Areas.OuterSpace.Controllers
{
    public class SolarSystemController : Controller
    {
        private ISessionManager _sessionManager;
        private INavigationManager _navigationManager;
        private ISecurityManager _securityManager;
	    private readonly IPlanets _planets;

	    public SolarSystemController(ISessionManager sessionManager, INavigationManager navigationManager, ISecurityManager securityManager, IPlanets planets)
        {
            if (sessionManager == null) throw new ArgumentNullException(nameof(sessionManager));
            if (navigationManager == null) throw new ArgumentNullException(nameof(navigationManager));
            if (securityManager == null) throw new ArgumentNullException(nameof(securityManager));
		    if (planets == null) throw new ArgumentNullException(nameof(planets));

		    _sessionManager = sessionManager;
            _navigationManager = navigationManager;
            _securityManager = securityManager;
		    _planets = planets;
        }

	    [HttpGet]
	    public ActionResult Summary(int id)
	    {
		    using (_sessionManager.CreateUnitOfWork())
		    {
			    var solarSystem = _navigationManager.GetSolarSystemDetails(id);
			    var me = _securityManager.GetCurrentUserAccount();
			    var mostPopulatedPlanet = _planets.GetMostPopulatedPlanetInSolarSystem(solarSystem.SolarSystemID);

			    return View(new SolarSystemSummary
			    {
				    SolarSystemID = solarSystem.SolarSystemID,
				    Name = solarSystem.Name,
				    MostPopulatedPlanet = mostPopulatedPlanet?.PlanetID,
				    Ships = solarSystem.Ships.Select(s => new ShipSummary
				    {
					    ID = s.ShipID,
					    Name = s.Name,
					    IsSelected = s.IsSelected,
					    IsMine = s.UserAccountID == me.UserAccountID
				    })
			    });
		    }
	    }
    }
}