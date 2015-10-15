using Liath.BigSpace.Configuration;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Liath.BigSpace.Session
{
    public class SessionManager : ISessionManager
    {
        private static ILogger logger = LogManager.GetCurrentClassLogger();
        private IConfigurationManager _configurationManager;

        public SessionManager(IConfigurationManager configurationManager)
        {
            if (configurationManager == null) throw new ArgumentNullException("configurationManager");
            _configurationManager = configurationManager;
        }

        public IUnitOfWork GetUnitOfWork()
        {
            logger.Trace("Getting UnitOfWork");
            return new UnitOfWork(_configurationManager.ConnectionString);
        }
    }
}
