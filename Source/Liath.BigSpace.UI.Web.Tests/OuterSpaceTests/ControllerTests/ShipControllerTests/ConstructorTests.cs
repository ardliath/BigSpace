using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Liath.BigSpace.Definitions;
using Liath.BigSpace.Domain.DataAccessDefinitions;
using Liath.BigSpace.Session;
using Liath.BigSpace.UI.Web.Areas.OuterSpace.Controllers;
using Moq;
using NUnit.Framework;

namespace Liath.BigSpace.UI.Web.Tests.OuterSpaceTests.ControllerTests.ShipControllerTests
{
	public class ConstructorTests
	{
		[Test]
		public void Exception_is_thrown_if_sessionManager_is_null()
		{
			
			Action act = () => new ShipController(null, Mock.Of<IFleetManager>(), Mock.Of<IEmpireManager>(), Mock.Of<IShips>());

			act.ShouldThrow<ArgumentNullException>()
				.And.ParamName.Equals("sessionManager");
		}


		[Test]
		public void Exception_is_thrown_if_fleetManager_is_null()
		{

			Action act = () => new ShipController(Mock.Of<ISessionManager>(), null, Mock.Of<IEmpireManager>(), Mock.Of<IShips>());

			act.ShouldThrow<ArgumentNullException>()
				.And.ParamName.Equals("fleetManager");
		}


		[Test]
		public void Exception_is_thrown_if_empireManager_is_null()
		{

			Action act = () => new ShipController(Mock.Of<ISessionManager>(), Mock.Of<IFleetManager>(), null, Mock.Of<IShips>());

			act.ShouldThrow<ArgumentNullException>()
				.And.ParamName.Equals("empireManager");
		}


		[Test]
		public void Exception_is_thrown_if_ships_is_null()
		{

			Action act = () => new ShipController(Mock.Of<ISessionManager>(), Mock.Of<IFleetManager>(), Mock.Of<IEmpireManager>(), null);

			act.ShouldThrow<ArgumentNullException>()
				.And.ParamName.Equals("ships");
		}


		[Test]
		public void No_exception_is_thrown_if_all_arguments_are_non_null()
		{

			Action act = () => new ShipController(Mock.Of<ISessionManager>(), Mock.Of<IFleetManager>(), Mock.Of<IEmpireManager>(), Mock.Of<IShips>());

			act.ShouldNotThrow<Exception>();
		}
	}
}
