using Liath.BigSpace.Definitions;
using Liath.BigSpace.Implementations;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Liath.BigSpace.Testing.Extensions.BusinessLogicExtensions;
using Liath.BigSpace.Testing.Create;
using Liath.BigSpace.Exceptions;
using Liath.BigSpace.Domain;
using Liath.BigSpace.Domain.DataAccessDefinitions;
using Liath.BigSpace.Testing.Extensions.DataAccessExtensions;
using Liath.BigSpace.Domain.UserAccountDomain;

namespace Liath.BigSpace.Tests.UserManagerTests
{
    public class UpdateFocusedCoordinatesTests
    {
        [Test]
        public void User_is_saved_with_updated_coordinates()
        {
            bool called = false;
            var currentCoordinates = new Coordinates { X = 1, Y = 2, Z = 3 };
            var security = new Mock<ISecurityManager>();
            security.SetupGetCurrentUserAccountToReturnUserAccount(DomainClasses.UserAccount(currentCoordinates));
            var dataAccess = new Mock<IUsers>();            
            dataAccess.Setup(x => x.Update(It.IsAny<UserAccount>())).Callback<UserAccount>(user =>
                {
                    Assert.AreEqual(5, user.FocusCoordinates.X);
                    Assert.AreEqual(7, user.FocusCoordinates.Y);
                    Assert.AreEqual(-3, user.FocusCoordinates.Z);
                    called = true;
                });
            var manager = BusinessLogicClass.UserManager(security.Object, dataAccess.Object);

            manager.UpdateFocusedCoordinates(4, 5, -6);

            Assert.IsTrue(called);
        }

        [Test]
        public void Exception_is_thrown_if_user_is_not_logged_in()
        {
            var security = new Mock<ISecurityManager>();
            security.SetupGetCurrentUserAccountToReturnNoUserAccount();
            var manager = BusinessLogicClass.UserManager(security.Object);

            Assert.Throws<CurrentUserNotFoundException>(() => manager.UpdateFocusedCoordinates(1, 1, 1));
        }
    }
}
