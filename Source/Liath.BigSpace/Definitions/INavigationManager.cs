using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Liath.BigSpace.Domain;
using Liath.BigSpace.Domain.SolarSystems;

namespace Liath.BigSpace.Definitions
{
    public interface INavigationManager
    {
		LocalAreaViewResult FindLocalSystems(ScreenSize screenSize);
		SolarSystemShipDetails GetSolarSystemDetails(int id);
        void CreateJourneyToSendSelectedShipsToSolarSystem(int id);

        void FocusCurrentUserAccountOnSolarSystem(long solarSystemID);
    }
}
