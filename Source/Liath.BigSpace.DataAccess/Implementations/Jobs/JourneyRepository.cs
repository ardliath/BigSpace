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

namespace Liath.BigSpace.DataAccess.Definitions.Jobs
{
    public class JourneyRepository : IJobChildRepository
    {        
        private IShips _ships;
        public JourneyRepository(IShips ships)
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
    }
}
