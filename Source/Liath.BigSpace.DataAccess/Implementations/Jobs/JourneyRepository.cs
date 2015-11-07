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
        private ISolarSystems _solarSystems;
        private IShips _ships;
        public JourneyRepository(ISolarSystems solarSystems, IShips ships)
        {
            if (solarSystems == null) throw new ArgumentNullException("solarSystems");
            if (ships == null) throw new ArgumentNullException("ships");

            _solarSystems = solarSystems;
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
            
            var fromID = dr.GetInt64(string.Concat(alias, ".StartSolarSystemID"));
            var endID = dr.GetInt64(string.Concat(alias, ".EndSolarSystemID"));

            journey.From = _solarSystems.GetSolarSystem(fromID);
            journey.To = _solarSystems.GetSolarSystem(endID);

            return journey;
        }
    }
}
