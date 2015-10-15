using Liath.BigSpace.Definitions;
using Liath.BigSpace.Session;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Liath.BigSpace.Implementations
{
    public class NavigationManager : INavigationManager
    {
        private ISecurityManager _securityManager;
        private static ILogger logger = LogManager.GetCurrentClassLogger();

        public NavigationManager(ISecurityManager securityManager)
        {
            if (securityManager == null) throw new ArgumentNullException("securityManager");
            _securityManager = securityManager;
        }

        public void FindLocalSystems()
        {
            var currentUser = _securityManager.GetCurrentUserAccount();
        }
    }
}
