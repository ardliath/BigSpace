using Liath.BigSpace.DataAccess.Definitions;
using Liath.BigSpace.Definitions;
using Liath.BigSpace.Implementations;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Liath.BigSpace.Testing.Create
{
    public class BusinessLogicClass
    {
        public static NavigationManager NavigationManager(ISecurityManager securityManager = null, ISolarSystems solarSystems = null)
        {
            return new NavigationManager(securityManager ?? Mock.Of<ISecurityManager>(),
                solarSystems ?? Mock.Of<ISolarSystems>(),
                Mock.Of<IShips>());
        }
    }
}
