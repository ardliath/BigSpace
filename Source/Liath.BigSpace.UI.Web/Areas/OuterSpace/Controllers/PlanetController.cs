﻿using System;
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
		private ISessionManager _sessionManager;
		private ISolarSystems _solarSystemRepository;

		public PlanetController(ISessionManager sessionManager, ISolarSystems solarSystemRepository)
		{
			if (sessionManager == null) throw new ArgumentNullException("sessionManager");
			if (solarSystemRepository == null) throw new ArgumentNullException("solarSystemRepository");

			_sessionManager = sessionManager;
			_solarSystemRepository = solarSystemRepository;
		}

		[HttpGet]
		public ActionResult Orbit(long id)
		{
			using (_sessionManager.CreateUnitOfWork())
			{
				var solarSystem = _solarSystemRepository.GetSolarSystemDetails(id);
				var model = new Orbit();
				return View();
			}
		}
	}
}