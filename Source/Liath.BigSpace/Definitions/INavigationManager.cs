using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Liath.BigSpace.Domain;

namespace Liath.BigSpace.Definitions
{
    public interface INavigationManager
    {
		LocalAreaViewResult FindLocalSystems(ScreenSize screenSize);
        SolarSystemDetails GetSolarSystemDetails(int id);
        void CreateJourneyToSendSelectedShipsToSolarSystem(int id);
    }
}
