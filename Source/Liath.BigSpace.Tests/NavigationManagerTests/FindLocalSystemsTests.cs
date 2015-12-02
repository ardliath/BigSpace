using Create = Liath.BigSpace.Testing.Create;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using Liath.BigSpace.Definitions;
using Liath.BigSpace.Domain;
using Liath.BigSpace.Exceptions;
using Liath.BigSpace.Testing.Extensions.BusinessLogicExtensions;
using Liath.BigSpace.Testing.Extensions.DataAccessExtensions;
using Liath.BigSpace.DataAccess.Definitions;
using Liath.BigSpace.Implementations;
using Liath.BigSpace.Domain.DataAccessDefinitions;
using Liath.BigSpace.Domain.UserAccountDomain;
using Liath.BigSpace.Domain.SolarSystems;

namespace Liath.BigSpace.Tests.NavigationManagerTests
{
    public class FindLocalSystemsTests
    {
        [Test]
        public void ArgumentNullException_is_thrown_if_screenSize_is_null()
        {
            var manager = Create.BusinessLogicClass.NavigationManager();

            var ex = Assert.Throws<ArgumentNullException>(() => manager.FindLocalSystems(null));
            Assert.AreEqual("screenSize", ex.ParamName);
        }

        [Test]
        public void CurrentUserNotFoundException_is_thrown_if_current_user_cannot_be_found()
        {
            var securityManager = new Mock<ISecurityManager>();
            securityManager.SetupGetCurrentUserAccountToReturnNoUserAccount();
            var manager = Create.BusinessLogicClass.NavigationManager(securityManager.Object);

            Assert.Throws<CurrentUserNotFoundException>(() => manager.FindLocalSystems(Create.DomainClasses.ScreenSize()));
        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(5)]
        [TestCase(20)]
        [TestCase(100)]
        public void All_located_solarSystems_are_returned(int number)
        {
            var securityManager = new Mock<ISecurityManager>();
            securityManager.SetupGetCurrentUserAccountToReturnUserAccount();
            var solarSystems = new Mock<ISolarSystems>();
            solarSystems.SetupFindSystemsInLocalArea(number);
            var manager = Create.BusinessLogicClass.NavigationManager(securityManager.Object, solarSystems.Object);

            var result = manager.FindLocalSystems(Create.DomainClasses.ScreenSize());

            Assert.AreEqual(number, result.SolarSystems.Count());
        }

        [Test]
        public void SolarSystem_ID_is_returned_correctly()
        {
            var solarSystem = Create.DomainClasses.SolarSystem(7);
            var manager = CreateNavigationManagerToReturnSolarSystem(solarSystem);

            var result = manager.FindLocalSystems(Create.DomainClasses.ScreenSize());

            Assert.AreEqual(solarSystem.SolarSystemID, result.SolarSystems.Single().SolarSystemID);
        }

        [Test]
        public void SolarSystem_name_is_returned_correctly()
        {            
            var solarSystem = Create.DomainClasses.SolarSystem(name: Guid.NewGuid().ToString());
            var manager = CreateNavigationManagerToReturnSolarSystem(solarSystem);

            var result = manager.FindLocalSystems(Create.DomainClasses.ScreenSize());

            Assert.AreEqual(solarSystem.Name, result.SolarSystems.Single().Name);
        }

        [Test]
        public void SolarSystem_coordinates_are_returned_correctly()
        {
            var solarSystem = Create.DomainClasses.SolarSystem(coordinates: Create.DomainClasses.Coordinates(7, 4, 8));
            var manager = CreateNavigationManagerToReturnSolarSystem(solarSystem);

            var result = manager.FindLocalSystems(Create.DomainClasses.ScreenSize());

            AssertCoordinatesAreCorrect(solarSystem, result);
        }

        [Test]
        public void ScreenOffset_coordinates_should_be_calculated_correctly_tiny_screen_at_origin()
        {
            var origin = Create.DomainClasses.Coordinates(0, 0, 0);
            var userAccount = Create.DomainClasses.UserAccount(focusCoordinates: origin);
            var solarSystem = Create.DomainClasses.SolarSystem(coordinates: origin);
            var manager = CreateNavigationManagerToReturnSolarSystem(solarSystem, userAccount);

            var result = manager.FindLocalSystems(Create.DomainClasses.ScreenSize(3, 3));

            AssertOffsetCoordinatesAreCorrect(1, 1, result);
        }

        [Test]
        public void ScreenOffset_coordinates_should_be_calculated_correctly_normal_screen_at_origin()
        {
            var origin = Create.DomainClasses.Coordinates(5, 2, 0);
            var userAccount = Create.DomainClasses.UserAccount(focusCoordinates: origin);
            var solarSystem = Create.DomainClasses.SolarSystem(coordinates: origin);
            var manager = CreateNavigationManagerToReturnSolarSystem(solarSystem, userAccount);

            var result = manager.FindLocalSystems(Create.DomainClasses.ScreenSize(21, 21));

            AssertOffsetCoordinatesAreCorrect(10, 10, result);
        }

        [Test]
        public void ScreenOffset_coordinates_should_be_calculated_correctly()
        {
            var userAccount = Create.DomainClasses.UserAccount(focusCoordinates: Create.DomainClasses.Coordinates(11, 11, 0));
            var solarSystem = Create.DomainClasses.SolarSystem(coordinates: Create.DomainClasses.Coordinates(12, 17, -3));
            var manager = CreateNavigationManagerToReturnSolarSystem(solarSystem, userAccount);

            var result = manager.FindLocalSystems(Create.DomainClasses.ScreenSize(21, 21));

            AssertOffsetCoordinatesAreCorrect(11, 16, result);
        }

        [Test]
        public void ScreenOffset_coordinates_should_be_calculated_correctly2()
        {
            var userAccount = Create.DomainClasses.UserAccount(focusCoordinates: Create.DomainClasses.Coordinates(100, 150, 0));
            var solarSystem = Create.DomainClasses.SolarSystem(coordinates: Create.DomainClasses.Coordinates(98, 155, 0));
            var manager = CreateNavigationManagerToReturnSolarSystem(solarSystem, userAccount);

            var result = manager.FindLocalSystems(Create.DomainClasses.ScreenSize(21, 21));

            AssertOffsetCoordinatesAreCorrect(8, 15, result);
        }

        private static void AssertOffsetCoordinatesAreCorrect(int x, int y, LocalAreaViewResult result)
        {
            var screenOffset = result.SolarSystems.Single().ScreenOffset;
            Assert.AreEqual(x, screenOffset.X, "ScreenOffset X was incorrect");
            Assert.AreEqual(y, screenOffset.Y, "ScreenOffset Y was incorrect");
        }

        private static void AssertCoordinatesAreCorrect(SolarSystem solarSystem, LocalAreaViewResult result)
        {
            Assert.AreEqual(solarSystem.Coordinates.X, result.SolarSystems.Single().Coordinates.X);
            Assert.AreEqual(solarSystem.Coordinates.Y, result.SolarSystems.Single().Coordinates.Y);
            Assert.AreEqual(solarSystem.Coordinates.Z, result.SolarSystems.Single().Coordinates.Z);
        }

        private static NavigationManager CreateNavigationManagerToReturnSolarSystem(SolarSystem solarSystem, UserAccount userAccount = null)
        {
            var securityManager = new Mock<ISecurityManager>();
            securityManager.SetupGetCurrentUserAccountToReturnUserAccount(userAccount);
            var solarSystems = new Mock<ISolarSystems>();
            solarSystems.SetupFindSystemsInLocalArea(solarSystem);
            var manager = Create.BusinessLogicClass.NavigationManager(securityManager.Object, solarSystems.Object);
            return manager;
        }
    }
}
