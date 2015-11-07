using Liath.BigSpace.DataAccess.Definitions;
using Liath.BigSpace.Definitions;
using Liath.BigSpace.Domain;
using Liath.BigSpace.Domain.DataAccessDefinitions;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Liath.BigSpace.Implementations
{
    public class SecurityManager : ISecurityManager
    {
        private IUsers _dataAccess;
        private static ILogger logger = LogManager.GetCurrentClassLogger();

        public SecurityManager(IUsers userDataAccess)
        {
            if (userDataAccess == null) throw new ArgumentNullException("userDataAccess");
            _dataAccess = userDataAccess;
        }

        public UserAccount GetCurrentUserAccount()
        {
            var currentUsername = this.CurrentUsername;
            return _dataAccess.GetUserByUsername(currentUsername);
        }

        public string CurrentUsername
        {
            get
            {
                return Environment.UserName;
            }
        }
    }
}
