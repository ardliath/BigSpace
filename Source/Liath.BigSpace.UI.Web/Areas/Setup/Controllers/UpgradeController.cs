using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Liath.BigSpace.Definitions;
using Liath.BigSpace.Session;

namespace Liath.BigSpace.UI.Web.Areas.Setup.Controllers
{
	public class UpgradeController :  Controller
	{
		private readonly ISessionManager _sessionManager;
		private readonly IUpgradeManager _upgradeManager;

		public UpgradeController(ISessionManager sessionManager, IUpgradeManager upgradeManager)
		{
			if (sessionManager == null) throw new ArgumentNullException(nameof(sessionManager));
			if (upgradeManager == null) throw new ArgumentNullException(nameof(upgradeManager));

			_sessionManager = sessionManager;
			_upgradeManager = upgradeManager;
		}

		[HttpGet]
		public ActionResult EnsureEnumValuesAreSynced()
		{
			using (_sessionManager.CreateUnitOfWork())
			{
				_upgradeManager.EnsureEnumValuesAreSynced();
				return new HttpStatusCodeResult(HttpStatusCode.OK);
			}
		}
	}
}