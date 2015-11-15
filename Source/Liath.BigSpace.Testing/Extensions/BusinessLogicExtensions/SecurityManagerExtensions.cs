using Liath.BigSpace.Definitions;
using Liath.BigSpace.Domain;
using Liath.BigSpace.Domain.UserAccountDomain;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Liath.BigSpace.Testing.Extensions.BusinessLogicExtensions
{
    public static class SecurityManagerExtensions
    {
        public static Mock<ISecurityManager> SetupGetCurrentUserAccountToReturnNoUserAccount(this Mock<ISecurityManager> securityManager)
        {
            securityManager.Setup(x => x.GetCurrentUserAccount()).Returns<UserAccount>(null);
            return securityManager;
        }

        public static Mock<ISecurityManager> SetupGetCurrentUserAccountToReturnUserAccount(this Mock<ISecurityManager> securityManager, UserAccount userAccountResult = null)
        {
            var userAccount = userAccountResult ?? Create.DomainClasses.UserAccount();
            securityManager.Setup(x => x.GetCurrentUserAccount()).Returns(userAccount);
            return securityManager;
        }
    }
}
