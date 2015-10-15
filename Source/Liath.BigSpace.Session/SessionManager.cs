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
        private IUnitOfWork _unitOfWork;

        public SessionManager(IConfigurationManager configurationManager)
        {
            if (configurationManager == null) throw new ArgumentNullException("configurationManager");
            _configurationManager = configurationManager;
        }

        public IUnitOfWork GetCurrentUnitOfWork()
        {
            logger.Trace("Getting UnitOfWork");
            if (_unitOfWork == null)
            {
                throw new NoUnitOfWorkException();                
            }
            return _unitOfWork;
        }

        public IUnitOfWork CreateUnitOfWork()
        {
            logger.Trace("Creating a new UnitOfWork");
            _unitOfWork = new UnitOfWork(_configurationManager.ConnectionString);
            return _unitOfWork;
        }
    }
}
