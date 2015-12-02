using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Liath.BigSpace.UI.Web.Areas.OuterSpace.Models.Planet;
using Liath.BigSpace.Session;
using Liath.BigSpace.Domain.DataAccessDefinitions;

namespace Liath.BigSpace.UI.Web.Areas.OuterSpace.Controllers
{
	public class PlanetController : Controller
	{
		private IPlanets _planetRespository;
		private ISessionManager _sessionManager;
		private ISolarSystems _solarSystemRepository;

		public PlanetController(ISessionManager sessionManager, IPlanets planetRespository, ISolarSystems solarSystemRepository)
		{
			if (sessionManager == null) throw new ArgumentNullException("sessionManager");
			if (solarSystemRepository == null) throw new ArgumentNullException("solarSystemRepository");
			if (planetRespository == null) throw new ArgumentNullException("planetRespository");

			_sessionManager = sessionManager;
			_solarSystemRepository = solarSystemRepository;
			_planetRespository = planetRespository;
		}

		[HttpGet]
		public ActionResult Orbit(long id)
		{
			using (_sessionManager.CreateUnitOfWork())
			{
				var planet = _planetRespository.GetPlanet(id);
				if (planet != null)
				{
					var solarSystem = _solarSystemRepository.GetSolarSystemDetails(planet.SolarSystemID);
				}
				var model = new Orbit();
				return View();
			}
		}
	}
}