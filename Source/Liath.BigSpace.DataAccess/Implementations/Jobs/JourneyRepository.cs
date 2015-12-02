using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Liath.BigSpace.DataAccess.Extensions;
using Liath.BigSpace.Domain.DataAccessDefinitions.Jobs;
using Liath.BigSpace.Domain.DataAccessDefinitions;
using Liath.BigSpace.Domain.Jobs;
using Liath.BigSpace.DataAccess.Implementations;
using Liath.BigSpace.Session;
using Liath.BigSpace.Domain;
using Liath.BigSpace.Domain.SolarSystems;

namespace Liath.BigSpace.DataAccess.Implementations.Jobs
{
    public class JourneyRepository : JobChildRepositoryBase, IJobChildRepository, IJourneyRepository
    {        
        private IShips _ships;
        public JourneyRepository(ISessionManager sessionManager, IShips ships)
            :base(sessionManager)
        {     
            if (ships == null) throw new ArgumentNullException("ships");

            _ships = ships;
        }

        public IEnumerable<string> ColumnNames
        {
            get { return new string[] { "StartSolarSystemID", "EndSolarSystemID" }; }
        }

        public string TableName
        {
            get { return "Journeys"; }
        }

        public string PrimaryKeyColumnName
        {
            get { return "JobID"; }
        }

        public Job Create(IDataReader dr, string alias)
        {
            var journey = new Journey(_ships);
            
            journey.FromSolarSystemID = dr.GetInt64(string.Concat(alias, "_StartSolarSystemID"));
            journey.ToSolarSystemID = dr.GetInt64(string.Concat(alias, "_EndSolarSystemID"));

            return journey;
        }

        public Int64 CreateJourney(SolarSystem from, SolarSystem to, string description, DateTime start, TimeSpan duration)
        {            
            var jobID = this.CreateJob(description, start, duration);
            using(var cmd = this.SessionManager.GetCurrentUnitOfWork().CreateCommand("INSERT INTO Journeys (JobID, StartSolarSystemID, EndSolarSystemID) VALUES (@JobID, @StartSolarSystemID, @EndSolarSystemID)"))
            {
                cmd.AddParameter("JobID", DbType.Int64, jobID);
                cmd.AddParameter("StartSolarSystemID", DbType.Int64, from.SolarSystemID);
                cmd.AddParameter("EndSolarSystemID", DbType.Int64, to.SolarSystemID);                

                cmd.ExecuteNonQuery();

                return jobID;
            }
        }        
    }
}
