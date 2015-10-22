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
            securityManager.Setup(x => x.GetCurrentUserAccount()).Returns<UserAccount>(null);
            var manager = Create.BusinessLogicClass.NavigationManager(securityManager.Object);

            Assert.Throws<CurrentUserNotFoundException>(() => manager.FindLocalSystems(Create.DomainClasses.ScreenSize()));
        }
    }
}
