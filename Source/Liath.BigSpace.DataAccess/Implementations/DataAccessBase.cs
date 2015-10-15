using Liath.BigSpace.Session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Liath.BigSpace.DataAccess.Implementations
{
    public abstract class DataAccessBase
    {
        protected readonly ISessionManager SessionManager { get; protected set; }

        public DataAccessBase(ISessionManager sessionManager)
        {
            if (sessionManager == null) throw new ArgumentNullException("sessionManager");
            this.SessionManager = sessionManager;
        }
    }
}
