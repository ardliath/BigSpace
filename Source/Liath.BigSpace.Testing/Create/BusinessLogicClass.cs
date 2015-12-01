using Liath.BigSpace.DataAccess.Definitions;
using Liath.BigSpace.Definitions;
using Liath.BigSpace.Domain.DataAccessDefinitions;
using Liath.BigSpace.Domain.DataAccessDefinitions.Jobs;
using Liath.BigSpace.Implementations;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Liath.BigSpace.Configuration;

namespace Liath.BigSpace.Testing.Create
{
    public class BusinessLogicClass
    {
        public static NavigationManager NavigationManager(ISecurityManager securityManager = null, ISolarSystems solarSystems = null)
        {
            return new NavigationManager(securityManager ?? Mock.Of<ISecurityManager>(),
                Mock.Of<IConfigurationManager>(),
                solarSystems ?? Mock.Of<ISolarSystems>(),
                Mock.Of<IShips>(),
                Mock.Of<IJourneyRepository>(),
								Mock.Of<IUsers>());
        }

        public static UserManager UserManager(ISecurityManager securityManager = null, IUsers userDataAccess = null)
        {
            return new UserManager(securityManager ?? Mock.Of<ISecurityManager>(),
                userDataAccess ?? Mock.Of<IUsers>());
        }
    }
}
