using Liath.BigSpace.Definitions;
using Liath.BigSpace.Session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Liath.BigSpace.Domain;
using Liath.BigSpace.UI.Web.Areas.OuterSpace.Models.LocalArea;
using NLog;
using System.Net;

namespace Liath.BigSpace.UI.Web.Areas.OuterSpace.Controllers
{
	public class LocalAreaController : Controller
	{
		private static ILogger logger = LogManager.GetCurrentClassLogger();
		private INavigationManager _navigationManager;
		private ISessionManager _sessionManager;
		private IUserManager _userManager;

		public LocalAreaController(ISessionManager sessionManager, INavigationManager navigationManager,
			IUserManager userManager)
		{
			if (navigationManager == null) throw new ArgumentNullException("navigationManager");
			if (sessionManager == null) throw new ArgumentNullException("sessionManager");
			if (userManager == null) throw new ArgumentNullException("userManager");

			_navigationManager = navigationManager;
			_sessionManager = sessionManager;
			_userManager = userManager;
		}

		[HttpGet]
		public ActionResult Display(Int64? id)
		{
			using (_sessionManager.CreateUnitOfWork())
			{
				if (id.HasValue && id > 0)
				{
					_navigationManager.FocusCurrentUserAccountOnSolarSystem(id.Value);
				}

				var localSystems = _navigationManager.FindLocalSystems(new Liath.BigSpace.Domain.ScreenSize(21, 21));
				return View(new Display
				{
					ScreenSize = new Models.LocalArea.ScreenSize
					{
						Height = 21,
						Width = 21
					},
					Systems = localSystems.SolarSystems.Select(s => new SystemSummary
					{
						ID = s.SolarSystemID,
						Name = s.Name,
						X = s.ScreenOffset.X,
						Y = s.ScreenOffset.Y
					})
				});
			}
		}

		[HttpPost]
		public ActionResult UpdateFocusedCoordinates(UpdateView updateView)
		{
			try
			{
				using (_sessionManager.CreateUnitOfWork())
				{
					_userManager.UpdateFocusedCoordinates(updateView.ChangeX, updateView.ChangeY, updateView.ChangeZ);
				}
				return new HttpStatusCodeResult(HttpStatusCode.OK);
			}
			catch (Exception ex)
			{
				logger.Error(ex, "There was an error updating the view");
				return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
			}
		}
	}
}