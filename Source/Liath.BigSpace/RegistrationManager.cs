using Liath.BigSpace.Definitions;
using Liath.BigSpace.Domain.DataAccessDefinitions;
using Liath.BigSpace.Domain.UserAccountDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Liath.BigSpace
{
    public class RegistrationManager : IRegistrationManager
    {
        private IShips _shipRepository;
        private ISecurityManager _securityManager;
        private ISolarSystems _solarSystemRepository;
        private IEmpireManager _empireManager;

        public RegistrationManager(ISecurityManager securityManager, IEmpireManager empireManager, IShips shipRepository, ISolarSystems solarSystemRepository)
        {
            if (securityManager == null) throw new ArgumentNullException("securityManager");
            if (shipRepository == null) throw new ArgumentNullException("shipRepository");
            if (solarSystemRepository == null) throw new ArgumentNullException("solarSystemRepository");
            if (empireManager == null) throw new ArgumentNullException("empireManager");

            _securityManager = securityManager;
            _shipRepository = shipRepository;
            _solarSystemRepository = solarSystemRepository;
            _empireManager = empireManager;

        }

        public bool RegisterUser(string username, string emailAddress, string password, string confirmPassword, out UserAccount user, out string error)
        {
            UserAccount createdUser;
            string createUserError;
            var empire = _empireManager.CreateEmpire(string.Concat(username, "'s Empire"));
            var randomSolarSystem = _solarSystemRepository.GetRandomUnoccupiedSolarSystem();
            if (_securityManager.CreateUserAccount(username, emailAddress, password, confirmPassword, empire.EmpireID, randomSolarSystem.Coordinates, out createdUser, out createUserError))
            {
                var ship = _shipRepository.CreateShip(randomSolarSystem, createdUser, "New Ship");
                user = createdUser;
                error = null;
                return true;
            }
            else
            {
                user = null;
                error = createUserError;
                return false;
            }
        }
    }
}
