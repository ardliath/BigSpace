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
		private IPlanets _planetRespository;
		private ISessionManager _sessionManager;
		private ISolarSystems _solarSystemRepository;
		private readonly IRaces _races;

		public PlanetController(ISessionManager sessionManager, IPlanets planetRespository, ISolarSystems solarSystemRepository, IRaces races)
		{
			if (sessionManager == null) throw new ArgumentNullException(nameof(sessionManager));
			if (solarSystemRepository == null) throw new ArgumentNullException(nameof(solarSystemRepository));
			if (races == null) throw new ArgumentNullException(nameof(races));
			if (planetRespository == null) throw new ArgumentNullException(nameof(planetRespository));

			_sessionManager = sessionManager;
			_solarSystemRepository = solarSystemRepository;
			_races = races;
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
					var race = planet.RaceID.HasValue ? _races.GetRace(planet.RaceID.Value) : null;
					var model = new Orbit
					{
						Name = planet.Name,
						Population = planet.Population,
						Race = race?.Name,
						SolarSystemID = planet.SolarSystemID,
						Image = this.Url.Content($"~/Content/Themes/BigSpace/Images/{planet.Image}"),
						Next = solarSystem.Planets.SingleOrDefault(p => p.PositionIndex == planet.PositionIndex + 1)?.PlanetID,
						Previous = solarSystem.Planets.SingleOrDefault(p => p.PositionIndex == planet.PositionIndex - 1)?.PlanetID
					};
					return View(model);
				}				
			}

			return this.HttpNotFound();
		}
	}
}