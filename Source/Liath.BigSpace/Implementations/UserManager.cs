using Liath.BigSpace.Definitions;
using Liath.BigSpace.Domain.DataAccessDefinitions;
using Liath.BigSpace.Exceptions;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Liath.BigSpace.Implementations
{
    public class UserManager : IUserManager
    {
        private static ILogger logger = LogManager.GetCurrentClassLogger();
        private ISecurityManager _securityManager;
        private IUsers _userDataAccess;

        public UserManager(ISecurityManager securityManager, IUsers userDataAccess)
        {
            if (securityManager == null) throw new ArgumentNullException("securityManager");
            if (userDataAccess == null) throw new ArgumentNullException("userDataAccess");

            _securityManager = securityManager;
            _userDataAccess = userDataAccess;
        }
        public void UpdateFocusedCoordinates(int diffX, int diffY, int diffZ)
        {
            var me = _securityManager.GetCurrentUserAccount();
            if (me == null) throw new CurrentUserNotFoundException();

            me.FocusCoordinates.X += diffX;
            me.FocusCoordinates.Y += diffY;
            me.FocusCoordinates.Z += diffZ;

            _userDataAccess.Update(me);
        }
    }
}
