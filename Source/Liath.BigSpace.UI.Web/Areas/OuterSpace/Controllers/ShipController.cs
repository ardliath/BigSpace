using Liath.BigSpace.Definitions;
using Liath.BigSpace.Session;
using Liath.BigSpace.UI.Web.Areas.OuterSpace.Models.Ship;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Liath.BigSpace.Domain.DataAccessDefinitions;

namespace Liath.BigSpace.UI.Web.Areas.OuterSpace.Controllers
{
	public class ShipController : Controller
	{
		private ISessionManager _sessionManager;
		private IFleetManager _fleetManager;
		private IEmpireManager _empireManager;
		private readonly IShips _ships;

		public ShipController(ISessionManager sessionManager, IFleetManager fleetManager, IEmpireManager empireManager, IShips ships)
		{
			if (sessionManager == null) throw new ArgumentNullException(nameof(sessionManager));
			if (fleetManager == null) throw new ArgumentNullException(nameof(fleetManager));
			if (empireManager == null) throw new ArgumentNullException(nameof(empireManager));
			if (ships == null) throw new ArgumentNullException(nameof(ships));

			_sessionManager = sessionManager;
			_fleetManager = fleetManager;
			_empireManager = empireManager;
			_ships = ships;
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

		[HttpPost]
		public ActionResult UpdateOrder(UpdateOrder order)
		{
			using (_sessionManager.CreateUnitOfWork())
			{
				_fleetManager.GiveCommand(order.ShipID, order.OrderID, order.Applied);
				return new HttpStatusCodeResult(System.Net.HttpStatusCode.OK);
			}
		}

		[HttpGet]
		public ActionResult List()
		{
			using (_sessionManager.CreateUnitOfWork())
			{
				var myEmpire = _empireManager.GetMyEmpire();
				var allShipsInFleet = _fleetManager.ListAllShipsInMyEmpire();
				var model = new List
				{
					EmpireName = myEmpire.Name,
					Ships = allShipsInFleet.Select(s => new ShipSummary
					{
						ShipID = s.ShipID,
						ShipName = s.Name,
						SolarSystem = s.SolarSystemID,
						SolarSystemName = s.SolarSystemName,
						Job = s.JobDescription
					})
				};
				return View(model);
			}
		}

		[HttpGet]
		public ActionResult Details(int id)
		{
			using (_sessionManager.CreateUnitOfWork())
			{
				var ship = _fleetManager.GetShipFromMyFleet(id);
				var allCommands = _ships.ListAllCommands();
				var commandsForThisShip = _ships.ListCommandsForShip(ship.ShipID);
				var model = new Details
				{
					ShipID = ship.ShipID,
					Name =  ship.Name,
					CurrentLocationID = ship.SolarSystemID,
					LocationName = ship.SolarSystemName,
					CurrentTask = ship.JobDescription,
					ShipCommands = allCommands.Select(c => new ShipCommandSummary
					{
						Value = c.CommandID,
						Description = c.Description,
						IsApplied = commandsForThisShip.Any(tsc => tsc.CommandID == c.CommandID)
					}).ToArray()
				};

				return View(model);
			}
		}
	}
}