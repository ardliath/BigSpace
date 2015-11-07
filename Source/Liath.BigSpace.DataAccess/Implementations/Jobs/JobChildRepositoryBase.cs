using Liath.BigSpace.Session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Liath.BigSpace.DataAccess.Extensions;

namespace Liath.BigSpace.DataAccess.Implementations.Jobs
{
    public abstract class JobChildRepositoryBase : DataAccessBase
    {
        protected JobChildRepositoryBase(ISessionManager sessionManager)
            :base(sessionManager)
        {

        }

        protected Int64 CreateJob(DateTime start, TimeSpan duration)
        {
            using(var cmd = SessionManager.GetCurrentUnitOfWork().CreateCommand("INSERT INTO Jobs(StartTime, Duration) VALUES (@StartTime, @Duration) SELECT SCOPE_IDENTITY()"))
            {
                cmd.AddParameter("StartTime", System.Data.DbType.DateTime, start);
                cmd.AddParameter("Duration", System.Data.DbType.Int64, duration.Ticks);

                return (Int64)(decimal)cmd.ExecuteScalar();
            }
        }
    }
}
