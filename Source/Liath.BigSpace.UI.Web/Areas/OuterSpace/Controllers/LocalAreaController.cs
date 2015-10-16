using Liath.BigSpace.Definitions;
using Liath.BigSpace.Session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Liath.BigSpace.Domain;

namespace Liath.BigSpace.UI.Web.Areas.OuterSpace.Controllers
{
	public class LocalAreaController : Controller
	{
		private INavigationManager _navigationManager;
		private ISessionManager _sessionManager;

		public LocalAreaController(ISessionManager sessionManager, INavigationManager navigationManager)
		{
			if (navigationManager == null) throw new ArgumentNullException("navigationManager");
			if (sessionManager == null) throw new ArgumentNullException("sessionManager");

			_navigationManager = navigationManager;
			_sessionManager = sessionManager;
		}

		[HttpGet]
		public ActionResult Display()
		{
			using (_sessionManager.CreateUnitOfWork())
			{
				var localSystems = _navigationManager.FindLocalSystems(new ScreenSize(21, 21));
				return View();
			}
		}
	}
}