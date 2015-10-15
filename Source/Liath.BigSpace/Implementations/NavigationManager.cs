﻿using Liath.BigSpace.Definitions;
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
        private ISessionManager _sessionManager;
        private static ILogger logger = LogManager.GetCurrentClassLogger();

        public NavigationManager(ISessionManager sessionManager)
        {
            if (sessionManager == null) throw new ArgumentNullException("sessionManager");
            _sessionManager = sessionManager;
        }

        public void FindLocalSystems()
        {
            var uow = _sessionManager.GetCurrentUnitOfWork();
        }
    }
}
