using Liath.BigSpace.Definitions;
using Liath.BigSpace.Session;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Liath.BigSpace.DataAccess.Definitions;
using Liath.BigSpace.Domain;
using Liath.BigSpace.Exceptions;

namespace Liath.BigSpace.Implementations
{
    public class NavigationManager : INavigationManager
    {
        private ISecurityManager _securityManager;
	    private readonly ISolarSystems _solarSystems;
	    private static ILogger logger = LogManager.GetCurrentClassLogger();

        public NavigationManager(ISecurityManager securityManager, ISolarSystems solarSystems)
        {
            if (securityManager == null) throw new ArgumentNullException("securityManager");
	        if (solarSystems == null) throw new ArgumentNullException("solarSystems");
	        _securityManager = securityManager;
	        _solarSystems = solarSystems;
        }

	    public LocalAreaViewResult FindLocalSystems(ScreenSize screenSize)
	    {
		    if (screenSize == null) throw new ArgumentNullException(nameof(screenSize));

		    var currentUser = _securityManager.GetCurrentUserAccount();
		    if (currentUser == null) throw new CurrentUserNotFoundException();
		    var localAreaView = new LocalAreaView(currentUser.FocusCoordinates, screenSize);
		    var solarSystems = _solarSystems.FindSystemsInLocalArea(localAreaView);

		    return new LocalAreaViewResult(currentUser.FocusCoordinates, screenSize, solarSystems);
	    }
    }
}
