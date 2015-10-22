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

        private static NavigationManager CreateNavigationManagerToReturnSolarSystem(SolarSystem solarSystem)
        {
            var securityManager = new Mock<ISecurityManager>();
            securityManager.SetupGetCurrentUserAccountToReturnUserAccount();
            var solarSystems = new Mock<ISolarSystems>();
            solarSystems.SetupFindSystemsInLocalArea(solarSystem);
            var manager = Create.BusinessLogicClass.NavigationManager(securityManager.Object, solarSystems.Object);
            return manager;
        }
    }
}
