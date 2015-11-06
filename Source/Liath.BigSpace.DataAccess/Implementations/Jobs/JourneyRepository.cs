using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Liath.BigSpace.DataAccess.Extensions;

namespace Liath.BigSpace.DataAccess.Definitions.Jobs
{
    public class JourneyRepository : IJobChildRepository
    {
        private ISolarSystems _solarSystems;
        public JourneyRepository(ISolarSystems solarSystems)
        {
            if (solarSystems == null) throw new ArgumentNullException("solarSystems");
            _solarSystems = solarSystems;
        }

        public IEnumerable<string> ColumnNames
        {
            get { return new string[] { "StartSolarSystemID", "EndSolarSystemID" }; }
        }

        public string TableName
        {
            get { return "Journies"; }
        }

        public string PrimaryKeyColumnName
        {
            get { return "JobID"; }
        }

        public Job Create(IDataReader dr, string alias)
        {
            var journey = new Journey();
            
            var fromID = dr.GetInt64(string.Concat(alias, ".StartSolarSystemID"));
            var endID = dr.GetInt64(string.Concat(alias, ".EndSolarSystemID"));

            journey.From = _solarSystems.GetSolarSystem(fromID);
            journey.To = _solarSystems.GetSolarSystem(endID);

            return journey;
        }
    }
}
