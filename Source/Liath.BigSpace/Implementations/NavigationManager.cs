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
        private IShips _ships;

        public NavigationManager(ISecurityManager securityManager, ISolarSystems solarSystems, IShips ships)
        {
            if (securityManager == null) throw new ArgumentNullException("securityManager");
	        if (solarSystems == null) throw new ArgumentNullException("solarSystems");
            if (ships == null) throw new ArgumentNullException("ships");

	        _securityManager = securityManager;
	        _solarSystems = solarSystems;
            _ships = ships;
        }

	    public LocalAreaViewResult FindLocalSystems(ScreenSize screenSize)
	    {
		    if (screenSize == null) throw new ArgumentNullException("screenSize");

		    var currentUser = _securityManager.GetCurrentUserAccount();
		    if (currentUser == null) throw new CurrentUserNotFoundException();
		    var localAreaView = new LocalAreaView(currentUser.FocusCoordinates, screenSize);
		    var solarSystems = _solarSystems.FindSystemsInLocalArea(localAreaView);

            var screenCornerX = localAreaView.FocusCoordinates.X - ((screenSize.Width - 1) / 2);
            var screenCornerY = localAreaView.FocusCoordinates.Y - ((screenSize.Height - 1) / 2);

            var relative = solarSystems.Select(s => new RelativeSolarSystem
                {
                    Coordinates = s.Coordinates,
                    Name = s.Name,
                    SolarSystemID = s.SolarSystemID,
                    ScreenOffset = new ScreenOffSet
                    {
                        X = (int)(s.Coordinates.X - screenCornerX),
                        Y = (int)(s.Coordinates.Y - screenCornerY)
                    }
                });

		    return new LocalAreaViewResult(currentUser.FocusCoordinates, screenSize, relative);
	    }

        public SolarSystemDetails GetSolarSystemDetails(int id)
        {
            var solarSystem = _solarSystems.GetSolarSystem(id);
            if(solarSystem != null)
            {                
                return new SolarSystemDetails
                {
                    Coordinates = solarSystem.Coordinates,
                    Name = solarSystem.Name,
                    SolarSystemID = solarSystem.SolarSystemID,
                    Ships = _ships.ListShipsAtSolarSystem(id)
                };
            }

            return null;
        }
    }
}
